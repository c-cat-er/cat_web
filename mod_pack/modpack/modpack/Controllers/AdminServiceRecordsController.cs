using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modpack.Models;
using modpack.ViewModels;
using X.PagedList;
using Microsoft.AspNetCore.SignalR;
using modpack.Service;

namespace modpack.Controllers
{
    public class AdminServiceRecordsController : Controller
    {
        private readonly ModPackContext _context;

        public AdminServiceRecordsController(ModPackContext context)
        {
            _context = context;
        }

        // GET: ServiceRecords
        public async Task<IActionResult> List()
        {
            List<CServiceRecordsViewModel> sVM = await _context.ServiceRecords
               .Select(record => new CServiceRecordsViewModel
               {
                   aRecordID = record.RecordId,
                   aMemberID = record.MemberId,
                   aMemberName = record.Member.Name,
                   aQuestionContent = record.Question,
                   aQuestionTime = record.QuestionTime,
                   aAdminCode = record.AdministratorId,
                   aAdminName = record.Administrator.Name,
                   aAnswerContent = record.Answer,
                   aAnswerTime = record.AnswerTime,
                   aIsResolved = record.IsResolved,
               })
               .ToListAsync();
            return View(sVM);
        }

        public async Task<IActionResult> ReplyTwo(int id)
        {
            string currentAdmin = HttpContext.Session.GetString(CDictionary.SK_LOGINED_ADMIN);
            Administrator admin = JsonSerializer.Deserialize<Administrator>(currentAdmin);
            int currentAdminID = admin.AdministratorId;
            string currentAdminName = admin.Name;
            var serviceRecords = await _context.ServiceRecords
                .Include(sr => sr.Member)
                .Where(sr => sr.RecordId == id)
                .ToListAsync();
            if (serviceRecords == null || serviceRecords.Count == 0) return NotFound();
            ViewData["recordId"] = id;
            ViewData["CurrentAdminId"] = currentAdminID;
            ViewData["CurrentAdminName"] = currentAdminName;
            ViewData["CurrentMemberId"] = serviceRecords.FirstOrDefault()?.MemberId;
            ViewData["CurrentMemberName"] = serviceRecords.FirstOrDefault()?.Member?.Name;
            return View(serviceRecords);
        }

        // GET: ServiceRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var serviceRecord = await _context.ServiceRecords
                .Include(s => s.Administrator)
                .Include(s => s.Member)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (serviceRecord == null) return NotFound();
            return View(serviceRecord);
        }

        // GET: ServiceRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var serviceRecord = await _context.ServiceRecords.FindAsync(id);
            if (serviceRecord == null) return NotFound();
            ViewData["AdministratorId"] = new SelectList(_context.Administrators, "AdministratorId", "Account", serviceRecord.AdministratorId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", serviceRecord.MemberId);
            return View(serviceRecord);
        }

        // POST: ServiceRecords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,MemberId,Question,QuestionTime,AdministratorId,Answer,AnswerTime,IsResolved")] ServiceRecord serviceRecord)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceRecordExists(serviceRecord.RecordId)) return NotFound(); else throw;
                }
                return RedirectToAction(nameof(List));
            }
            ViewData["AdministratorId"] = new SelectList(_context.Administrators, "AdministratorId", "Account", serviceRecord.AdministratorId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", serviceRecord.MemberId);
            return View(serviceRecord);
        }

        // GET: ServiceRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var serviceRecord = await _context.ServiceRecords
                .Include(s => s.Administrator)
                .Include(s => s.Member)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (serviceRecord == null) return NotFound();
            return View(serviceRecord);
        }

        // POST: ServiceRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceRecord = await _context.ServiceRecords.FindAsync(id);
            if (serviceRecord != null) _context.ServiceRecords.Remove(serviceRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool ServiceRecordExists(int id)
        {
            return _context.ServiceRecords.Any(e => e.RecordId == id);
        }
    }
}
