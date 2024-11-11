using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Niseko.Server.DTOs;
using Niseko.Server.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberAuthThirdPartyController(NisekoContext context, IConfiguration configuration) : ControllerBase
    {
        private readonly NisekoContext _context = context;
        private readonly IConfiguration _configuration = configuration; ///用於讀取應用程序的配置文件

        ///GET /api/OAuth/third/login/google
        [HttpGet("third/login/google")]
        public IActionResult GoogleLogin()
        {//k-p, 用於重定向用戶到 Google 授權頁面。

            //k-p, 從配置文件中獲取以下：
            ///Google OAuth 的授權端點 URL。
            var authorizationEndpoint = _configuration["OAuth:Google:AuthorizationEndpoint"];
            ///Google OAuth 的客戶端 ID。
            var clientId = _configuration["OAuth:Google:ClientId"];
            ///授權完成後的重定向 URI。
            var redirectUri = _configuration["OAuth:Google:RedirectUri"];
            ///定義 OAuth 授權請求的範圍。在此請求 profile 和 email 的權限。
            var scope = "profile email";

            ///生成授權 URL
            ///response_type=code：表示我們正在請求一個授權碼（authorization code），這是 OAuth 授權流程中的一部分。
            ///client_id ={ clientId}：我們的應用程序的客戶端 ID。
            ///redirect_uri ={ redirectUri}：當用戶授權完成後，Google 會重定向用戶到這個 URI，並附帶授權碼。
            ///scope ={ scope}：我們請求的授權範圍。
            var authorizationUrl = $"{authorizationEndpoint}?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope={scope}";
            ///重定向用戶到授權 URL
            return Redirect(authorizationUrl);
        }

        ///GET /api/OAuth/third/callback/google
        [HttpGet("third/callback/google")]
        public async Task<IActionResult> GoogleCallback(string code)
        {//k-p, 用於處理 Google 的授權回調，完成用戶信息獲取和註冊/登錄邏輯。
         //用戶授權後，Google 將回調 GET /api/OAuth/Callback/Google?code=AUTHORIZATION_CODE URL 並附帶授權碼。

            ///從配置中獲取 Google OAuth 的 tokenEndpoint、clientId、clientSecret 和 redirectUri
            var tokenEndpoint = _configuration["OAuth:Google:TokenEndpoint"];
            var clientId = _configuration["OAuth:Google:ClientId"];
            var clientSecret = _configuration["OAuth:Google:ClientSecret"];
            var redirectUri = _configuration["OAuth:Google:RedirectUri"];

            ///使用授權碼組裝請求體，向 tokenEndpoint 發送 POST 請求以獲取訪問令牌
            var tokenRequestBody = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            var httpClient = new HttpClient();
            var tokenResponse = await httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(tokenRequestBody));
            var tokenResponseString = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResponseJson = JsonSerializer.Deserialize<JsonElement>(tokenResponseString);
            string accessToken = tokenResponseJson.GetProperty("access_token").GetString();

            ///將訪問令牌添加到 HTTP 請求的授權標頭中，向 userInfoEndpoint 發送 GET 請求以獲取用戶信息
            var userInfoEndpoint = _configuration["OAuth:Google:UserInfoEndpoint"];
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var userInfoResponse = await httpClient.GetAsync(userInfoEndpoint);
            var userInfoResponseString = await userInfoResponse.Content.ReadAsStringAsync();
            var userInfo = JsonSerializer.Deserialize<JsonElement>(userInfoResponseString);

            ///從用戶信息中提取電子郵件和用戶 ID
            var email = userInfo.GetProperty("email").GetString();
            var userId = userInfo.GetProperty("id").GetString();

            ///檢查數據庫中是否已存在此第三方帳號
            var existingUser = await _context.TMemberThirdPartyAccounts
                .Include(m => m.FMember)
                .FirstOrDefaultAsync(u => u.FThirdPartyUniqueID == userId);

            TMember member;
            if (existingUser != null)
            {
                ///若用戶已存在，直接使用已存在的用戶信息
                member = existingUser.FMember;
            }
            else
            {
                ///若用戶不存在，創建新用戶和新第三方帳號並保存到數據庫
                member = new TMember
                {
                    FEmail = email,
                    FMemberName = userInfo.GetProperty("name").GetString()
                };

                var nmtr = new TMemberThirdPartyAccount
                {
                    FThirdPartyUniqueID = userId,
                    FLoginTypeID = 2,
                    FMember = member
                };

                _context.TMembers.Add(member);
                _context.TMemberThirdPartyAccounts.Add(nmtr);
                await _context.SaveChangesAsync();
            }

            //k-p, 取得登入 IP 和 時區 ??
            var nml = new TMemberLoginRecord
            {
                FMemberID = member.FMemberID,
                FLoginTypeID = 2, // Google LoginTypeID
                FLoginDatetime = DateTime.Now,
                FLoginTimezone = Request.Headers["Timezone"], //
                FLoginIPAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString() //
            };

            _context.TMemberLoginRecords.Add(nml);
            await _context.SaveChangesAsync();

            ///創建token
            var token = GenerateJwtToken(member);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddHours(1)
            };

            Response.Cookies.Append("token", token, cookieOptions);
            return Ok(new { token });
        }

        private string GenerateJwtToken(TMember member)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtToken"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, member.FMemberID.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("FMemberID", member.FMemberID.ToString()),
                new("FMemberCode", member.FMemberCode ?? string.Empty),
                new("FSkiLevelID", member.FSkiLevelID.ToString()),
                new("FMemberName", member.FMemberName ?? string.Empty),
                new("FBirthdate", member.FBirthdate?.ToString("yyyy-MM-dd") ?? string.Empty),
                new("FPhone", member.FPhone ?? string.Empty),
                new("FEmail", member.FEmail ?? string.Empty)
            };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
