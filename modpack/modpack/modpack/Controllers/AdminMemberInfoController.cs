using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modpack.Models;

namespace modpack.Controllers
{
    public class AdminMemberInfoController : Controller
    {
        private readonly ModPackContext _context;

        public AdminMemberInfoController(ModPackContext context)
        {
            _context = context;
        }

        // GET: AdminMemberInfo
        public async Task<IActionResult> List()
        {
            var modPackContext = _context.Members.Include(m => m.Level);
            return View(await modPackContext.ToListAsync());
        }

        // GET: AdminMemberInfo/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var member = await _context.Members
                .Include(m => m.Level)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null) return NotFound();
            return View(member);
        }

        // GET: AdminMemberInfo/Create
        public IActionResult Create()
        {
            ViewData["LevelId"] = new SelectList(_context.MemberLevels, "LevelId", "Name");
            return View();
        }

        // POST: AdminMemberInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,LevelId,Account,Password,Name,Email,Phone,Address,CreationTime,ModificationTime")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            ViewData["LevelId"] = new SelectList(_context.MemberLevels, "LevelId", "Name", member.LevelId);
            return View(member);
        }

        // GET: AdminMemberInfo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return NotFound();
            ViewData["LevelId"] = new SelectList(_context.MemberLevels, "LevelId", "Name", member.LevelId);
            return View(member);
        }

        // POST: AdminMemberInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,LevelId,Account,Password,Name,Email,Phone,Address")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMember = await _context.Members.AsNoTracking().FirstOrDefaultAsync(m => m.MemberId == member.MemberId);
                    if (existingMember != null)
                    {
                        member.CreationTime = existingMember.CreationTime; // 使用現有的創建時間
                        member.ModificationTime = DateTime.Now; // 設置當前時間為修改時間
                    }
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            ViewData["LevelId"] = new SelectList(_context.MemberLevels, "LevelId", "Name", member.LevelId);
            return View(member);
        }

        // GET: AdminMemberInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Level)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: AdminMemberInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
