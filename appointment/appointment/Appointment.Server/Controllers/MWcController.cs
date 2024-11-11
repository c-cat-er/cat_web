using Appointment.Server.DTOs;
using Appointment.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MWcController(AppointmentContext context) : ControllerBase
    {
        private readonly AppointmentContext _context = context;

        //GET: api/MWc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WCUSTOM>>> GetWCUSTOMs()
        {
            return await _context.WCUSTOMs.ToListAsync();
        }

        //GET: api/MWc/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WCUSTOM>> GetWCUSTOM(string id)
        {
            var wCUSTOM = await _context.WCUSTOMs.FindAsync(id);

            if (wCUSTOM == null)
            {
                return NotFound();
            }

            return wCUSTOM;
        }

        //GET: api/MWc/QueryCustomerCure?cnum={cnum}
        [HttpGet("QueryCustomerCure")]
        public async Task<ActionResult<IEnumerable<CureTableDTO>>> QueryCustomerCure([FromQuery] string cnum)
        {
            try
            {
                var re = await _context.WSUBSAMs
                   .Where(w => w.CNUM == cnum)
                   .Select(r => new CureTableDTO
                   {
                       SID = r.SID,
                       SERNO = r.SERNO,
                       SDATE = r.SDATE,
                       CURENO = r.CURENO,
                       CURENAME = r.CURENAME,
                       SUBCNT = r.SUBCNT,
                       SUBQTY = r.SUBQTY
                   }).ToListAsync();

                if (re == null || re.Count == 0) return NotFound();
                return Ok(re);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"QueryCustomerCure server error: {ex.Message}");
            }
        }

        //PUT: api/MWc/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWCUSTOM(string id, WCUSTOM wCUSTOM)
        {
            if (id != wCUSTOM.CNUM)
            {
                return BadRequest();
            }

            _context.Entry(wCUSTOM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WCUSTOMExists(id))
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

        //POST: api/MWc
        [HttpPost]
        public async Task<ActionResult<WCUSTOM>> PostWCUSTOM(WCUSTOM wCUSTOM)
        {
            _context.WCUSTOMs.Add(wCUSTOM);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WCUSTOMExists(wCUSTOM.CNUM))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWCUSTOM", new { id = wCUSTOM.CNUM }, wCUSTOM);
        }

        //POST: api/MWc/QueryCustomers
        [HttpPost("QueryCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerTableDTO>>> QueryCustomers([FromBody] CustomerTableDTO dto)
        {
            try
            {
                var re = await _context.WCUSTOMs
                    .Where(w =>
                        (string.IsNullOrEmpty(dto.CNUM) || w.CNUM.Contains(dto.CNUM)) &&
                        (string.IsNullOrEmpty(dto.CNAME) || w.CNAME.Contains(dto.CNAME)) &&
                        (string.IsNullOrEmpty(dto.CBIRTH) || w.CBIRTH.Contains(dto.CBIRTH)) &&
                        (string.IsNullOrEmpty(dto.CMOBILE) || w.CMOBILE.Contains(dto.CMOBILE)))
                    .Select(r => new CustomerTableDTO
                    {
                        CNUM = r.CNUM,
                        CNAME = r.CNAME,
                        CBIRTH = r.CBIRTH,
                        CMOBILE = r.CMOBILE
                    })
                    .ToListAsync();

                if (re == null || re.Count == 0) return NotFound();
                return Ok(re);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"QueryCustomers server error: {ex.Message}");
            }
        }

        //DELETE: api/MWc/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWCUSTOM(string id)
        //{
        //    var wCUSTOM = await _context.WCUSTOMs.FindAsync(id);
        //    if (wCUSTOM == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.WCUSTOMs.Remove(wCUSTOM);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool WCUSTOMExists(string id)
        {
            return _context.WCUSTOMs.Any(e => e.CNUM == id);
        }
    }
}
