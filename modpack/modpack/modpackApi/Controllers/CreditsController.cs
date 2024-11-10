using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modpackApi.DTO;
using modpackApi.Models;

namespace modpackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditsController : ControllerBase
    {
        private readonly ModPackContext _context;

        public CreditsController(ModPackContext context)
        {
            _context = context;
        }

        // GET: api/Credits
        [HttpGet]
        public async Task<IEnumerable<CreditDTO>> GetCredits()
        {
            return await _context.Credits.Include(c => c.Member).Select(c => new CreditDTO
            {
                CreditId = c.CreditId,
                MemberId = c.Member.MemberId,
                HistoryName = c.HistoryName,
                IncomingAmount = c.IncomingAmount,
                UsageAmount = c.UsageAmount,
                ModificationTime = c.ModificationTime
            }).ToListAsync();
        }

        // GET: api/Credits/5
        [HttpGet("{id}")]
        public async Task<CreditDTO> GetCredit(int id)
        {
            var credit = await _context.Credits.Include(c => c.Member).FirstOrDefaultAsync(c => c.CreditId == id);
            if (credit == null)
            {
                return null;
            }
            CreditDTO crDTO = new CreditDTO()
            {
                CreditId = credit.CreditId,
                MemberId = credit.Member.MemberId,
                HistoryName = credit.HistoryName,
                IncomingAmount = credit.IncomingAmount,
                UsageAmount = credit.UsageAmount,
                ModificationTime = credit.ModificationTime
            };
            return crDTO;
        }

        // PUT: api/Credits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutCredit(int id, CreditDTO crDTO)
        {
            if (id != crDTO.CreditId)
            {
                return "更新購物金成功";
            }
            Credit cr = await _context.Credits.FindAsync(id);

            cr.HistoryName = crDTO.HistoryName;
            cr.IncomingAmount = crDTO.IncomingAmount;
            cr.UsageAmount= crDTO.UsageAmount;
            cr.ModificationTime = crDTO.ModificationTime;
               
            _context.Entry(crDTO).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditExists(id))
                {
                    return "更新購物金失敗";
                }
                else
                {
                    throw;
                }
            }
            return "更新購物金成功";
        }

        // POST: api/Credits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Credit>> PostCredit(Credit credit)
        {
            _context.Credits.Add(credit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredit", new { id = credit.CreditId }, credit);
        }

        // DELETE: api/Credits/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteCredit(int id)
        {
            var credit = await _context.Credits.FindAsync(id);
            if (credit == null)
            {
                return "刪除失敗";
            }

            _context.Credits.Remove(credit);
            await _context.SaveChangesAsync();

            return "刪除成功";
        }

        private bool CreditExists(int id)
        {
            return _context.Credits.Any(e => e.CreditId == id);
        }
    }
}
