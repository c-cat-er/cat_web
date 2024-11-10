using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Niseko.Server.DTOs;
using Niseko.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SHA3.Net;
using Microsoft.VisualBasic;
using Humanizer;
using System.ComponentModel;
using System.Configuration;
using System.Security;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberAuthWebsiteController(NisekoContext context, IConfiguration configuration) : ControllerBase
    {
        private readonly NisekoContext _context = context;
        private readonly IConfiguration _configuration = configuration; ///用於讀取應用程序的配置文件

        ///POST /api/MemberAuthWebsite/website/register
        [HttpPost("website/register")]
        public async Task<IActionResult> WebsiteRegister([FromBody] MemberWebsiteRegisterDTO dmwr)
        {
            //DTO驗證
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    k => k.Key,
                    v => v.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { message = "資料未填", errors });
            }

            ///使用 SHA-3 轉為二進制
            ///var accountHash = Sha3Helper.ComputeHash(dmwr.FAccount);
            if (await _context.TMemberWebsiteAccounts.AnyAsync(u => u.FAccount == dmwr.FAccount))
            {
                //return BadRequest("帳號已存在");
                return BadRequest(new { message = "帳號已存在" });
            }

            //k-p,使用 SHA-3 加密，計算密碼雜湊值
            var salt = GenerateSalt();
            var passwordHash = Sha3Helper.ComputeHash(dmwr.FPassword, salt);

            var nm = new TMember
            {
                FMemberCode = GenerateMemberCode(), //
                FMemberName = dmwr.FMemberName,
                FSkiLevelID = dmwr.FSkiLevelID,
                FGender = dmwr.FGender,
                FBirthdate = string.IsNullOrEmpty(dmwr.FBirthdate) ? null : DateOnly.ParseExact(dmwr.FBirthdate, "yyyy-MM-dd"), //
                FCountryCode = dmwr.FCountryCode,
                FPhone = dmwr.FPhone,
                FEmail = dmwr.FEmail,
                FPermissions = 1,
            };

            var nmw = new TMemberWebsiteAccount
            {
                FAccount = dmwr.FAccount, //Convert.FromBase64String(dmwr.FAccount), //
                FPasswordHash = passwordHash, //
                FPasswordSalt = salt, //
                FMember = nm
            };

            _context.TMembers.Add(nm);
            _context.TMemberWebsiteAccounts.Add(nmw);
            await _context.SaveChangesAsync();

            //k-p, 取得登入 IP 和 時區 ??
            var nml = new TMemberLoginRecord
            {
                FMemberID = nm.FMemberID,
                FLoginTypeID = 1,
                FLoginTimezone = Request.Headers["Timezone"].ToString() ?? null, ///獲取客戶端提供的時區
                FLoginIPAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? null, ///獲取 server 端取得的 IP
                FLoginDatetime = DateTime.Now
            };
            _context.TMemberLoginRecords.Add(nml);
            await _context.SaveChangesAsync();

            ///創建token
            var token = GenerateJwtToken(nm);

            //在後端儲存 cookie 目前前端取不到 token，改在前端儲存
            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true, ///set cookie 無法通過 JavaScript 訪問
            //    Secure = true, ///只在 HTTPS 上傳輸
            //    SameSite = SameSiteMode.Strict, ///避免 CSRF 攻擊
            //    Expires = DateTimeOffset.Now.AddHours(1) ///cookie 過期時間
            //};

            ////k-p,將生成的 JWT token 存儲在客戶端的 cookie 中
            //Response.Cookies.Append("token", token, cookieOptions);
            return Ok(new { token, loginRecordId = nml.FLoginRecordID });
        }

        ///POST /api/MemberAuthWebsite/website/login
        [HttpPost("website/login")]
        public async Task<IActionResult> WebsiteLogin([FromBody] MemberBothLoginDTO dmbl)
        {
            //DTO驗證
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    k => k.Key,
                    v => v.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return BadRequest(new { message = "資料未填", errors });
            }

            var userAcc = await _context.TMemberWebsiteAccounts
                .Include(m => m.FMember)
                .FirstOrDefaultAsync(u => u.FAccount == dmbl.FAccount);
            if (userAcc == null) return BadRequest("帳號錯誤");

            //若是已停權會員
            //if (!string.IsNullOrWhiteSpace(userAcc.UOUTDATE)) return BadRequest("此帳號已停權，請聯繫客服");
            //if (!string.IsNullOrEmpty(userAcc.UOUTDATE)) return BadRequest("此帳號已停權，請聯繫客服");

            //k-p
            using var hmac = new HMACSHA512(userAcc.FPasswordSalt);
            var computedHash = Sha3Helper.ComputeHash(dmbl.FPassword!, userAcc.FPasswordSalt);
            if (!computedHash.SequenceEqual(userAcc.FPasswordHash))
                return BadRequest("密碼錯誤");

            var nml = new TMemberLoginRecord //am
            {
                FMemberID = userAcc.FMemberID,
                FLoginTypeID = 1,
                FLoginTimezone = Request.Headers["Timezone"].ToString() ?? null, ///獲取 client 端取得的時區
                FLoginIPAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? null, ///獲取 server 端取得的 IP
                FLoginDatetime = DateTime.Now
            };
            _context.TMemberLoginRecords.Add(nml);
            await _context.SaveChangesAsync();

            ///創建token
            var token = GenerateJwtToken(userAcc.FMember);

            //目前在後端儲存 cookie 目前前端取不到 token，改在前端儲存
            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = DateTimeOffset.Now.AddHours(1)
            //};

            //Response.Cookies.Append("token", token, cookieOptions);
            return Ok(new { token, loginRecordId = nml.FLoginRecordID });
        }

        ///POST /api/MemberAuthWebsite/website/logout
        [HttpPost("website/logout")]
        public async Task<IActionResult> WebsiteLogout([FromBody] MemberWebsiteLogoutDTO dmo)  //改用dto
        {
            var loginRecord = await _context.TMemberLoginRecords.FindAsync(dmo.FLoginRecordID);
            if (loginRecord == null) return NotFound("登入紀錄未找到");

            loginRecord.FLogoutDatetime = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("generateMemberCode")]
        public string GenerateMemberCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = "M" + new string(Enumerable.Repeat(chars, 9).Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }

        private string GenerateJwtToken(TMember mm)
        {//JWT Token

            //var jwtSettings = _configuration.GetSection("Jwt");
            /////use SymmetricSecurityKey密鑰
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["JwtToken"])
            //    ?? throw new InvalidOperationException("Jwt Token not configured"));
            /////use HmacSha256 算法創建簽名憑據
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            ///k-p, 從 appsettings.json 取得 JWT token.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtToken"]!));
            ///_configuration.GetSection("JWT:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                ///JWT claim
                new(JwtRegisteredClaimNames.Sub, mm.FMemberID.ToString()), ///用戶的唯一標識符或用戶名
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), ///用於確保每個 JWT 令牌都是唯一的。
                ///.NET dafault claim
                ///new Claim(ClaimTypes.Name, me.FirstName + me.LastName),
                ///new Claim(ClaimTypes.NameIdentifier, me.EmployeeID.ToString())
                
                new("FMemberID", mm.FMemberID.ToString()),
                new("FMemberCode", mm.FMemberCode),
                new("FMemberName", mm.FMemberName),
                new("FSkiLevelID", mm.FSkiLevelID.ToString()),
                new("FGender", mm.FGender),
                new("FBirthdate", mm.FBirthdate?.ToString("yyyy-MM-dd") ?? ""), //將 null 轉為 "" 避免引發 null 問題
                new("FCountryCode", mm.FCountryCode ?? ""),
                new("FPhone", mm.FPhone ?? ""),
                new("FEmail", mm.FEmail ?? ""),
                new("FPermissions", mm.FPermissions.ToString())
                //下方寫法須配合改寫 model 才行
                //new(CustomClaimTypes.MemberName, mm.MemberName),
                //new(CustomClaimTypes.MemberCode, mm.MemberCode),
                //new(CustomClaimTypes.Phone, mm.Phone)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            ///處理 JWT 的物件，我們使用它的 WriteToken 方法將 JWT 物件轉換為字符串形式，以便於傳輸。
            ///返回 JWT 字符串，向客戶端返回身份驗證令牌。
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static byte[] GenerateSalt()
        {//k-p,
            var salt = new byte[128];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }
    }

    public static class Sha3Helper
    {//k-p, 計算使用 SHA-3 算法和鹽值的哈希值。這個幫助類將用戶的密碼和鹽值結合起來，然後使用 SHA-3 算法計算哈希值。
     //這有助於提高密碼存儲的安全性。

        public static byte[] ComputeHash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return Sha3.Sha3256().ComputeHash(inputBytes);
        }

        public static byte[] ComputeHash(string input, byte[] salt)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var saltedInput = new byte[inputBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, saltedInput, 0, salt.Length);
            Buffer.BlockCopy(inputBytes, 0, saltedInput, salt.Length, inputBytes.Length);

            return Sha3.Sha3256().ComputeHash(saltedInput);
        }
    }
}
//yet
//實現令牌刷新機制，確保用戶的會話可以延續而不需要重新登錄。client 也要??
//實施刷新 token 的機制，以自動更新過期的
