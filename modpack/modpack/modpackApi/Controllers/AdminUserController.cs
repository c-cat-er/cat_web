using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using modpackApi.DTO;
using modpackApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace modpackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly ModPackContext _context;
        private readonly IConfiguration _configuration;

        public AdminUserController(ModPackContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<Administrator> Register(AdminUserDTO request)
        {
            //使用了 **BCrypt.Net** 函式庫來將 **request.Password** 中的密碼進行哈希處理，處理後的密碼存儲在 **Administrator passwordHash** 變數中。
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 创建新的Administrator实例并设置属性值
            var administrator = new Administrator
            {
                TitleId = 7,
                AdminCode = request.AdminCode,
                Name = request.UserName,
                Account = request.Account,
                Password = passwordHash
            };
            
            _context.Administrators.Add(administrator);
            _context.SaveChanges();

            return Ok(administrator);
        }

        [HttpPost("login")]
        public ActionResult<Administrator> Login(AdminUserDTO request)
        {
            ///找用戶
            //var user = _context.Administrators.SingleOrDefault(u => u.AdminCode == request.AdminCode);
            var user = _context.Administrators.FirstOrDefault(u => u.AdminCode == request.AdminCode);
            ///若找不到用戶
            if (user == null) return BadRequest("沒有此號");
            ///驗證密碼是否匹配
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) return BadRequest("身分驗證失敗");

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(Administrator user)
        {
            List<Claim> claims = new List<Claim>
            {
				///創建 Claims List，其中包含了一個名為 "ClaimTypes.Name" 的聲明，該聲明的值是使用者的名稱（user.Username）
				new Claim(ClaimTypes.Name,user.Name)
            };
            ///k-p, 從 appsettings.json 取得 JWT token.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWT:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            ///處理 JWT 的物件，我們使用它的 WriteToken 方法將 JWT 物件轉換為字符串形式，以便於傳輸。
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            ///返回 JWT 字符串，向客戶端返回身份驗證令牌。
            return jwt;
        }
    }
}
