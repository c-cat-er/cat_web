using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Niseko.Server.DTOs;
using Niseko.Server.Models;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TLocationController : ControllerBase
    {
        private readonly NisekoContext _context;

        public TLocationController(NisekoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TLocation>>> GetTLocation()
        {
            var locations = await _context.TLocations
                            .Select(location => new
                            {
                                location.FLocationID,
                                location.FLocationName
                            })
                            .Skip(1)  //跳过第一条记录
                            .Take(3)
                            .ToListAsync();
            return Ok(locations);
        }
    }
}
