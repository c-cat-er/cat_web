using Appointment.Server.DTOs;
using Appointment.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Appointment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MWuController(AppointmentContext context, IConfiguration configuration) : ControllerBase
    {
        private readonly AppointmentContext _context = context;
        private readonly IConfiguration _configuration = configuration;

        //GET /api/MWu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WUSER>>> GetWUSERs()
        {
            try
            {
                var users = await _context.WUSERs
                    .Where(w => w.UEMPDEG != "Z" && (w.UOUTDATE == null || w.UOUTDATE == ""))
                    .Select(u => new UserQueryDTO
                    {
                        USERID = u.USERID,
                        USERNAME = u.USERNAME,
                    })
                    .ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "GetUsers server error.");
            }
        }

        //GET /api/MWu/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WUSER>> GetWUSER(int id)
        {
            var wUSER = await _context.WUSERs.FindAsync(id);

            if (wUSER == null)
            {
                return NotFound();
            }

            return wUSER;
        }

        //GET /api/MWu/GetByUserId/{userId}
        [HttpGet("GetByUserId/{userId}")]
        public async Task<ActionResult<UserQueryDTO>> GetByUserId(string userId)
        {
            var wUSER = await _context.WUSERs
                .Where(w => w.USERID == userId)
                .FirstAsync();

            if (wUSER == null)
            {
                return NotFound();
            }

            return Ok(wUSER);
        }

        //GET /api/MWu/GetNamesByIds?operuser={operuser}&cdoc={cdoc}&cdocs={cdocs}&cdocp={cdocp}
        [HttpGet("GetNamesByIds")]
        public async Task<ActionResult<UserNameQueryDTO>> GetNamesByIds([FromQuery] string? operuser, [FromQuery] string cdoc, [FromQuery] string? cdocs, [FromQuery] string? cdocp)
        {
            try
            {
                var operuserName = string.IsNullOrEmpty(operuser) ? null : await GetUserNameById(operuser);
                var cdocName = await GetUserNameById(cdoc);
                var cdocsName = string.IsNullOrEmpty(cdocs) ? null : await GetUserNameById(cdocs);
                var cdocpName = string.IsNullOrEmpty(cdocp) ? null : await GetUserNameById(cdocp);

                return Ok(new UserNameQueryDTO
                {
                    OPERUSERName = operuserName ?? "",
                    CDOCName = cdocName!,
                    CDOCSName = cdocsName,
                    CDOCPName = cdocpName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<string?> GetUserNameById(string userId)
        {
            return await _context.WUSERs
                        .Where(n => n.USERID == userId)
                        .Select(n => n.USERNAME)
                        .FirstOrDefaultAsync();
        }

        //GET /api/MWu/GetCurecnt?CNUM={CNUM}&SERNO={SERNO}&CURENO={CURENO}
        [HttpGet("GetCurecnt")]
        public async Task<ActionResult<IEnumerable<CurecntQueryDTO>>> GetCurecnt([FromQuery] string CNUM, string SERNO, string CURENO)
        {
            try
            {
                var re = await _context.WSUBSAMs
                   .Where(w => w.CNUM == CNUM && w.SERNO == SERNO && w.CURENO == CURENO)
                   .Select(r => new CurecntQueryDTO
                   {
                       CURECNT = r.CURECNT,
                       CURECNTS = r.CURECNTS
                   }).ToListAsync();

                if (re == null || re.Count == 0) return NotFound();
                return Ok(re);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"GetCurecnt server error: {ex.Message}");
            }
        }

        //GET /api/MWu/GetUsersByAcute?acute={acute}
        [HttpGet("GetUsersByAcute")]
        public async Task<ActionResult<IEnumerable<UserQueryDTO>>> GetUsersByAcute([FromQuery] int acute)
        {
            try
            {
                var allUsers = await _context.WUSERs
                    .Where(w => string.IsNullOrEmpty(w.UOUTDATE))
                    .ToListAsync();
                if (allUsers == null || allUsers.Count == 0) { return NotFound(); }

                var users = allUsers
                    .Where(w => int.TryParse(w.UEMPDEG, out _))
                    .Select(u => new
                    {
                        User = u,
                        Degree = int.Parse(u.UEMPDEG)
                    })
                    .Where(x => acute switch
                    {
                        1 => (x.Degree >= 31 && x.Degree <= 33) || (x.Degree >= 11 && x.Degree <= 16),
                        2 => x.Degree >= 71 && x.Degree <= 74,
                        3 => x.Degree == 5,
                        4 => x.Degree >= 11 && x.Degree <= 16,
                        _ => false
                    })
                    .Select(x => new UserQueryDTO
                    {
                        USERID = x.User.USERID,
                        USERNAME = x.User.USERNAME,
                    }).ToList();

                if (users == null || users.Count == 0) return NotFound();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"GetUsersByAcute server error: {ex.Message}");
            }
        }

        //PUT /api/MWu/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWUSER(string id, WUSER wUSER)
        {
            if (id != wUSER.USERID)
            {
                return BadRequest();
            }

            _context.Entry(wUSER).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WUSERExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST /api/MWu/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            if (await _context.WUSERs.AnyAsync(u => u.USERID == request.USERID)) return Conflict("此帳號已存在");
            var user = new WUSER
            {
                USERNAME = request.USERNAME,
                USERID = request.USERID,
                PASSWD = request.PASSWD,
            };

            _context.WUSERs.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "註冊成功" });
        }

        //POST: api/MWu/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO re)
        {
            var user = await _context.WUSERs.FirstOrDefaultAsync(u => u.USERID == re.USERID);
            if (user == null) return BadRequest("ID 輸入錯誤");
            if (!string.IsNullOrWhiteSpace(user.UOUTDATE)) return BadRequest("此帳號已離職不可使用");
            if (!string.IsNullOrEmpty(user.UOUTDATE)) return BadRequest("此帳號已離職不可使用");
            if (re.PASSWD != user.PASSWD) return BadRequest("密碼輸入錯誤");

            string token = CreateToken(user);
            return Ok(new { JWTToken = token });
        }

        //POST: api/MWu
        [HttpPost]
        public async Task<ActionResult<WUSER>> PostWUSER(WUSER wUSER)
        {
            _context.WUSERs.Add(wUSER);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWUSER", new { id = wUSER.USERID }, wUSER);
        }

        //DELETE: api/MWu/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWUSER(int id)
        //{
        //    var wUSER = await _context.WUSERs.FindAsync(id);
        //    if (wUSER == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.WUSERs.Remove(wUSER);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private string CreateToken(WUSER user)
        {
            List<Claim> claims =
            [
				new Claim(ClaimTypes.Name,user.USERNAME),
                new Claim(ClaimTypes.NameIdentifier, user.USERID.ToString())
            ];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:JWTToken"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool WUSERExists(string id)
        {
            return _context.WUSERs.Any(e => e.USERID == id);
        }
    }
}
