using Appointment.Server.DTOs;
using Appointment.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MWbeController(AppointmentContext context) : ControllerBase
    {
        private readonly AppointmentContext _context = context;

        //GET: api/MWbe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WBEAURE>>> GetWBEAUREs()
        {
            return await _context.WBEAUREs.ToListAsync();
        }

        //GET: api/MWbe/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WBEAURE>> GetWBEAURE(int id)
        {
            var wBEAURE = await _context.WBEAUREs.FindAsync(id);

            if (wBEAURE == null)
            {
                return NotFound();
            }

            return wBEAURE;
        }

        //GET: api/MWbe/QueryCustomerRegistration?cnum={cnum}
        [HttpGet("QueryCustomerRegistration")]
        public async Task<ActionResult<IEnumerable<RegistrationTableDTO>>> QueryCustomerRegistration([FromQuery] string cnum)
        {
            try
            {
                var re = await _context.WBEAUREs
                   .Where(w => w.CNUM == cnum)
                   .Select(r => new RegistrationTableDTO
                   {
                       WBID = r.WBID,
                       ACUTE = r.ACUTE,
                       CNUM = r.CNUM,
                       SERNO = r.SERNO,
                       SDATE = r.SDATE,
                       CURENO = r.CURENO,
                       RDATE = r.RDATE,
                       RTIME = r.RTIME,
                       OPERUSER = r.OPERUSER,
                       CDOC = r.CDOC,
                       CDOCS = r.CDOCS,
                       CDOCP = r.CDOCP,
                       CURECNT = r.CURECNT,
                       CURECNTS = r.CURECNTS,
                       CUPDATE = r.CUPDATE,
                       CSTAT = r.CSTAT
                   }).ToListAsync();

                if (re == null || re.Count == 0) return NotFound();
                return Ok(re);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"QueryCustomerRegistration server error: {ex.Message}");
            }
        }

        //GET: api/MWbe/InitCalendar
        [HttpGet("InitCalendar")]
        public async Task<ActionResult<IEnumerable<CalendarDTO>>> InitCalendar()
        {
            try
            {
                var today = DateTime.Today;
                var taiwanYearToday = today.Year - 1911;
                var todayString = $"{taiwanYearToday:D3}{today:MMdd}";

                var initcal = await _context.WBEAUREs
                    .Where(w => w.RDATE.CompareTo(todayString) >= 0)
                    .Where(w => w.CSTAT == "A" || w.CSTAT == "Y")
                    .OrderBy(w => w.RDATE)
                    .Select(i => new CalendarDTO
                    {
                        WBID = i.WBID,
                        ACUTE = i.ACUTE,
                        CNUM = i.CNUM,
                        CURENO = i.CURENO,
                        CURENAME = i.CURENAME,
                        RDATE = i.RDATE,
                        RTIME = i.RTIME,
                        OPERUSER = i.OPERUSER,
                        CDOC = i.CDOC,
                        CDOCS = i.CDOCS,
                        CDOCP = i.CDOCP,
                        CSTAT = i.CSTAT,
                        CURECNT = i.CURECNT,
                        CURECNTS = i.CURECNTS
                    })
                    .ToListAsync();
                return Ok(initcal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"InitCalendar server error: {ex.Message}");
            }
        }

        //GET: api/MWbe/InitCalendarPageData
        [HttpGet("InitCalendarPageData")]
        public async Task<ActionResult<IEnumerable<CalendarDTO>>> InitCalendarPageData(int page = 1, int pageSize = 10)
        {
            try
            {
                var today = DateTime.Today;
                var taiwanYearToday = today.Year - 1911;
                var todayString = $"{taiwanYearToday:D3}{today:MMdd}";

                var query = _context.WBEAUREs
                    .Where(w => w.RDATE.CompareTo(todayString) >= 0)
                    .Where(w => w.CSTAT == "A" || w.CSTAT == "Y");

                var totalRecords = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                var pagedData = await query
                    .OrderBy(i => i.RDATE)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(i => new CalendarDTO
                    {
                        WBID = i.WBID,
                        ACUTE = i.ACUTE,
                        CNUM = i.CNUM,
                        CURENO = i.CURENO,
                        CURENAME = i.CURENAME,
                        RDATE = i.RDATE,
                        RTIME = i.RTIME,
                        OPERUSER = i.OPERUSER,
                        CDOC = i.CDOC,
                        CDOCS = i.CDOCS,
                        CDOCP = i.CDOCP,
                        CSTAT = i.CSTAT,
                        CURECNT = i.CURECNT,
                        CURECNTS = i.CURECNTS
                    })
                    .ToListAsync();

                var response = new
                {
                    draw = page,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    Data = pagedData
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"InitCalendar server error: {ex.Message}");
            }
        }

        //api/MWbe/FilterCalendar?acute={acute}&cstat={cstat}&userId={userId}
        [HttpGet("FilterCalendar")]
        public async Task<ActionResult<IEnumerable<CalendarDTO>>> FilterCalendar([FromQuery] string? cnum, string? rdate, string? acute, string? cstat, string? userId)
        {
            try
            {
                var today = DateTime.Today;
                var cal = await _context.WBEAUREs
                    .Where(w => DateTime.ParseExact(w.RDATE, "yyyMMdd", null) >= today)
                    .Where(w => w.CSTAT == "A" || w.CSTAT == "Y")
                    .Where(w =>
                        (string.IsNullOrEmpty(cnum) || w.CNUM == cnum) &&
                        (string.IsNullOrEmpty(acute) || w.ACUTE == acute) &&
                        (string.IsNullOrEmpty(cstat) || w.CSTAT == cstat) &&
                        (string.IsNullOrEmpty(userId) || w.CDOC == userId || w.CDOCS == userId || w.CDOCP == userId))
                    .Select(c => new CalendarDTO
                    {
                        WBID = c.WBID,
                        ACUTE = c.ACUTE,
                        CNUM = c.CNUM,
                        CURENO = c.CURENO,
                        CURENAME = c.CURENAME,
                        RDATE = c.RDATE,
                        RTIME = c.RTIME,
                        OPERUSER = c.OPERUSER,
                        CDOC = c.CDOC,
                        CDOCS = c.CDOCS,
                        CDOCP = c.CDOCP,
                        CSTAT = c.CSTAT,
                        CURECNT = c.CURECNT,
                        CURECNTS = c.CURECNTS,
                    }).ToListAsync();

                if (cal == null || cal.Count == 0) return NotFound();
                return Ok(cal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"FilterCalendar server error: {ex.Message}");
            }
        }

        //PUT: api/MWbe/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWBEAURE(int id, WBEAURE wBEAURE)
        {
            if (id != wBEAURE.WBID)
            {
                return BadRequest();
            }

            _context.Entry(wBEAURE).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WBEAUREExists(id))
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

        [HttpPut("PutWBEAURECstat/{id}")]
        public async Task<IActionResult> PutWBEAURECstat(int id, [FromBody] WBEAURE wBEAURE)
        {
            if (id != wBEAURE.WBID)
            {
                return BadRequest();
            }

            var existingWBEAURE = await _context.WBEAUREs.FindAsync(id);
            if (existingWBEAURE == null)
            {
                return NotFound();
            }

            existingWBEAURE.CSTAT = wBEAURE.CSTAT;
            existingWBEAURE.CUPDATE = wBEAURE.CUPDATE;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WBEAUREExists(id))
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

        //POST: api/MWbe
        [HttpPost]
        public async Task<ActionResult<WBEAURE>> PostWBEAURE(WBEAURE wBEAURE)
        {
            _context.WBEAUREs.Add(wBEAURE);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWBEAURE", new { id = wBEAURE.WBID }, wBEAURE);
        }


        [HttpPost("UpdateCalendar")]
        public async Task<ActionResult<IEnumerable<CalendarDTO>>> UpdateCalendar([FromBody] CalendarSelectDTO d)
        {
            try
            {
                var today = DateTime.Today;
                var taiwanYearToday = today.Year - 1911;
                var todayString = $"{taiwanYearToday:D3}{today:MMdd}";

                var cal = await _context.WBEAUREs
                    .Where(w => w.RDATE.CompareTo(todayString) >= 0)
                    .Where(w => w.CSTAT == "A" || w.CSTAT == "Y")
                    .Where(w =>
                        (string.IsNullOrEmpty(d.cnum) || w.CNUM == d.cnum) &&
                        (string.IsNullOrEmpty(d.rdate) || w.RDATE == d.rdate) &&
                        (d.acute == "" || w.ACUTE == d.acute) &&
                        (string.IsNullOrEmpty(d.cstat) || w.CSTAT == d.cstat) &&
                        (string.IsNullOrEmpty(d.userId) || w.OPERUSER == d.userId || w.CDOC == d.userId || w.CDOCS == d.userId || w.CDOCP == d.userId))
                    .Select(c => new CalendarDTO
                    {
                        WBID = c.WBID,
                        ACUTE = c.ACUTE,
                        CNUM = c.CNUM,
                        CURENO = c.CURENO,
                        CURENAME = c.CURENAME,
                        RDATE = c.RDATE,
                        RTIME = c.RTIME,
                        OPERUSER = c.OPERUSER,
                        CDOC = c.CDOC,
                        CDOCS = c.CDOCS,
                        CDOCP = c.CDOCP,
                        CSTAT = c.CSTAT,
                        CURECNT = c.CURECNT,
                        CURECNTS = c.CURECNTS,
                        CUPDATE = c.CUPDATE,
                    }).ToListAsync();

                if (cal == null || cal.Count == 0) return NotFound();
                return Ok(cal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"UpdateCalendar server error: {ex.Message}");
            }
        }

        //POST: api/MWbe/ConfirmRegistrationBtn
        [HttpPost("ConfirmRegistrationBtn")]
        public async Task<IActionResult> ConfirmRegistrationBtn([FromBody] WBEAURE r)
        {
            if (!ModelState.IsValid) return BadRequest("資料錯誤");

            try
            {
                var conflictingRecords = await _context.WBEAUREs
                    .Where(w => w.RDATE == r.RDATE && w.RTIME == r.RTIME)
                    .Where(w => w.CSTAT == "A" || w.CSTAT == "Y")
                    .ToListAsync();

                var hasConflict = conflictingRecords.Any(w =>
                    w.CDOC == r.CDOC || w.CDOC == r.CDOCS || w.CDOC == r.CDOCP ||
                    (!string.IsNullOrEmpty(r.CDOCS) && (w.CDOCS == r.CDOC || w.CDOCS == r.CDOCS || w.CDOCS == r.CDOCP)) ||
                    (!string.IsNullOrEmpty(r.CDOCP) && (w.CDOCP == r.CDOC || w.CDOCP == r.CDOCS || w.CDOCP == r.CDOCP)));

                if (hasConflict)
                {
                    return BadRequest("在相同日期和時間下預約了重複的執行人員。");
                }

                var registration = new WBEAURE
                {
                    WBID = r.WBID,
                    CNUM = r.CNUM,
                    ACUTE = r.ACUTE,
                    SERNO = r.SERNO,
                    SDATE = r.SDATE,
                    CURENO = r.CURENO,
                    CURENAME = r.CURENAME,
                    RDATE = r.RDATE,
                    RTIME = r.RTIME,
                    OPERUSER = r.OPERUSER,
                    CDOC = r.CDOC,
                    CDOCS = r.CDOCS,
                    CDOCP = r.CDOCP,
                    CURECNT = r.CURECNT,
                    CURECNTS = r.CURECNTS,
                    CSTAT = r.CSTAT,
                    CMESS = r.CMESS,
                    CUPDATE = r.CUPDATE
                };

                _context.WBEAUREs.Add(registration);
                await _context.SaveChangesAsync();
                return Ok(new { message = "提交成功" });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database update error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ConfirmRegistrationBtn server error: {ex.Message}");
            }
        }

        //POST: api/MWbe/ListConfirmRegistrationBtn
        [HttpPost("ListConfirmRegistrationBtn")]
        public async Task<IActionResult> ListConfirmRegistrationBtn([FromBody] List<WBEAURE> registrations)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                foreach (var r in registrations)
                {
                    var conflictingRecords = await _context.WBEAUREs
                        .Where(w => w.RDATE == r.RDATE && w.RTIME == r.RTIME)
                        .Where(w => w.CSTAT == "A" || w.CSTAT == "Y")
                        .ToListAsync();

                    var hasConflict = conflictingRecords.Any(w =>
                        w.CDOC == r.CDOC || w.CDOC == r.CDOCS || w.CDOC == r.CDOCP ||
                        (!string.IsNullOrEmpty(r.CDOCS) && (w.CDOCS == r.CDOC || w.CDOCS == r.CDOCS || w.CDOCS == r.CDOCP)) ||
                        (!string.IsNullOrEmpty(r.CDOCP) && (w.CDOCP == r.CDOC || w.CDOCP == r.CDOCS || w.CDOCP == r.CDOCP)));

                    if (hasConflict)
                    {
                        return BadRequest("在相同日期和時間下預約了重複的執行人員。");
                    }

                    var registration = new WBEAURE
                    {
                        CNUM = r.CNUM,
                        ACUTE = r.ACUTE,
                        SERNO = r.SERNO,
                        SDATE = r.SDATE,
                        CURENO = r.CURENO,
                        CURENAME = r.CURENAME,
                        RDATE = r.RDATE,
                        RTIME = r.RTIME,
                        OPERUSER = r.OPERUSER,
                        CDOC = r.CDOC,
                        CDOCS = r.CDOCS,
                        CDOCP = r.CDOCP,
                        CURECNT = r.CURECNT,
                        CURECNTS = r.CURECNTS,
                        CSTAT = r.CSTAT,
                        CMESS = r.CMESS,
                        CUPDATE = r.CUPDATE
                    };

                    _context.WBEAUREs.Add(registration);
                }
                await _context.SaveChangesAsync();
                return Ok(new { message = "提交成功" });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database update error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ListConfirmRegistrationBtn server error: {ex.Message}");
            }
        }

        //DELETE: api/MWbe/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWBEAURE(int id)
        //{
        //    var wBEAURE = await _context.WBEAUREs.FindAsync(id);
        //    if (wBEAURE == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.WBEAUREs.Remove(wBEAURE);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool WBEAUREExists(int id)
        {
            return _context.WBEAUREs.Any(e => e.WBID == id);
        }
    }
}
