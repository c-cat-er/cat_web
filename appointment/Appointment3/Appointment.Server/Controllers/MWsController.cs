using Appointment.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MWsController(AppointmentContext context) : ControllerBase
    {
        private readonly AppointmentContext _context = context;

        //GET api/MWs/GetStore
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WBRSTOR>>> GetStore()
        {
            try
            {
                var r = await _context.WBRSTORs.ToListAsync();
                return Ok(r);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
