using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modpack.Models;
using modpack.ViewModels;
using System.Text.Json;
using X.PagedList;
using AutoMapper;

namespace modpack.Controllers
{
    public class AdminEmployeeInfoController : Controller
    {
        private readonly ModPackContext _context;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _environment;

        public AdminEmployeeInfoController(ModPackContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        // GET: AdminEmployeeInfo
        public async Task<IActionResult> List(CKeywordViewModel vm, string searchStringTitle,
            string searchStringName, string sortOrder, int page = 1, int pageSize = 7)
        {
            ///檢查登入
            string adminUser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_ADMIN);
            if (string.IsNullOrEmpty(adminUser)) return RedirectToAction("Login", "AdminUser");

            Administrator admin = System.Text.Json.JsonSerializer.Deserialize<Administrator>(json: adminUser);
            int adminTitleId = admin.TitleId;
            var adminPermissions = await _context.AdministratorTitles.FirstOrDefaultAsync(p => p.TitleId == adminTitleId);

            /*
            if (adminPermissions != null)
            {
                // 查詢權限小於當前登入用戶的所有員工信息
                var query = _context.Administrators
                    .Where(e => e.Title.Permissions < adminPermissions.Permissions); // 排除當前登入用戶的職位

                // 根據搜索條件過濾員工信息
                if (!string.IsNullOrEmpty(searchStringTitle))
                {
                    ///k-p, 從 admin 的關聯表 title 表搜尋
                    query = query.Where(e => e.Title.Name.Contains(searchStringTitle));
                }
                if (!string.IsNullOrEmpty(searchStringName))
                {
                    ///從 admin 表搜尋
                    query = query.Where(e => e.Name.Contains(searchStringName));
                }

                ///order.
                switch (sortOrder)
                {
                    ///從 admin 關聯的 title 表
                    case "aPermission_desc":
                        query = query.OrderByDescending(a => a.Title.Permissions);
                        break;
                    case "aPermission_asc":
                        query = query.OrderBy(a => a.Title.Permissions);
                        break;
                    ///從 admin 表
                    case "aAdminCode_desc":
                        query = query.OrderByDescending(a => a.AdminCode);
                        break;
                    case "aAdminCode_asc":
                        query = query.OrderBy(a => a.AdminCode);
                        break;
                    default:
                        query = query.OrderBy(a => a.AdministratorId);
                        break;
                }

                ///分頁
                var employeesWithLowerPermissions = await query
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                var adminViewModels = _mapper.Map<List<CAdminViewModel>>(employeesWithLowerPermissions);
                return View(adminViewModels);
                */

            IQueryable<CAdminViewModel> list = _context.Administrators
           .Where(a => a.TitleId == adminTitleId || a.Title.Permissions >= _context.AdministratorTitles
               .Where(at => at.TitleId == adminTitleId)
               .Select(at => at.Permissions)
               .FirstOrDefault())
           .Select(a => new CAdminViewModel
           {
               AdminID = a.AdministratorId,
               AdminTitleID = a.TitleId,
               TitleName = a.Title.Name,
               aPermissions = a.Title.Permissions,
               AdminName = a.Name,
               aAdminCode = a.AdminCode,
               AdminImage = a.Image,
               AdminAccount = a.Account,
               AdminPassword = a.Password,
           });

            if (!string.IsNullOrEmpty(searchStringTitle))
                list = list.Where(a => a.TitleName.Contains(searchStringTitle));
            if (!string.IsNullOrEmpty(searchStringName))
                list = list.Where(a => a.AdminName.Contains(searchStringName));
            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "AdminID";
            list = sortOrder switch
            {
                //k-p, 也別傳統 switch 寫法.
                "aPermission_desc" => list.OrderByDescending(a => a.aPermissions),
                "aPermission_asc" => list.OrderBy(a => a.aPermissions),
                "aAdminCode_desc" => list.OrderByDescending(a => a.aAdminCode),
                "aAdminCode_asc" => list.OrderBy(a => a.aAdminCode),
                _ => list.OrderBy(a => a.AdminID),
            };
            HttpContext.Session.SetString(CDictionary.SK_LOGINED_ADMIN, JsonSerializer.Serialize(admin));
            var result = await list.ToPagedListAsync(page, pageSize);
            ViewBag.NoSearchResults = result.Count == 0;
            return View(result);
        
            //return RedirectToAction("Login", "AdminUser");
        }

        // GET: AdminEmployeeInfo/Create
        public async Task<IActionResult> Create()
        {
            var titles = await _context.AdministratorTitles.ToListAsync();
            var defaultIndex = 8;
            if (titles.Count > defaultIndex)
            {
                var defaultTitleId = titles[defaultIndex].TitleId;
                ViewData["TitleId"] = new SelectList(titles, "TitleId", "Name", defaultTitleId);
            }
            else
            {
                ViewData["TitleId"] = new SelectList(titles, "TitleId", "Name");
            }
            return View();
        }

        // POST: AdminEmployeeInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CAdminViewModel aVM)
        {
            if (aVM.photo != null)
            {
                ///string photoName = "adm" + Guid.NewGuid().ToString().Substring(0, 5) + ".jpeg";
                string photoName = string.Concat("adm", Guid.NewGuid().ToString().AsSpan(0, 5), ".jpeg");
                using var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, "images/admin", photoName), FileMode.Create);
                await aVM.photo.CopyToAsync(fileStream);

                Administrator ad = new()
                {
                    Name = aVM.AdminName,
                    TitleId = aVM.TitleID,
                    AdminCode = aVM.aAdminCode,
                    Image = photoName,
                    Account = aVM.AdminAccount,
                    Password = aVM.AdminPassword
                };
                _context.Add(ad);
                await _context.SaveChangesAsync();
                ///return RedirectToAction(nameof(List));
                return RedirectToAction("List");
            }
            return View();
        }

        // GET: AdminEmployeeInfo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ad = await _context.Administrators
              .Include(a => a.Title)
              .FirstOrDefaultAsync(a => a.AdministratorId == id);
            if (ad == null) return NotFound();
            var vm = new CAdminViewModel
            {
                AdminID = ad.AdministratorId,
                AdminTitleID = ad.TitleId,
                AdminName = ad.Name,
                aAdminCode = ad.AdminCode,
                AdminImage = ad.Image,
                AdminAccount = ad.Account,
                AdminPassword = ad.Password
            };
            ///var viewModel = new CAdminViewModel(ad, ad.Title);  // abbr.
            ViewData["TitleId"] = new SelectList(_context.AdministratorTitles, "TitleId", "Name", ad.TitleId);
            return View(vm);
        }

        // POST: AdminEmployeeInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CAdminViewModel aVM)
        {
            Administrator aEdit = await _context.Administrators.FindAsync(aVM.AdminID);
            if (aEdit != null)
            {
                if (aVM.photo != null)
                {
                    string photoName = string.Concat("adm", Guid.NewGuid().ToString().AsSpan(0, 5), ".jpeg");
                    aEdit.Image = photoName;
                    ///using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, "images/admin", photoName), FileMode.Create))
                    ///{
                    ///    await aVM.photo.CopyToAsync(fileStream);
                    ///}
                    using var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, "images/admin", photoName), FileMode.Create);
                    await aVM.photo.CopyToAsync(fileStream);
                }
                aEdit.Name = aVM.AdminName;
                aEdit.AdminCode = aVM.aAdminCode;
                aEdit.TitleId = aVM.AdminTitleID;
                aEdit.Account = aVM.AdminAccount;
                aEdit.Password = aVM.AdminPassword;
                await _context.SaveChangesAsync();
            }
            HttpContext.Session.SetString(CDictionary.SK_LOGINED_ADMIN, JsonSerializer.Serialize(aEdit));
            ViewData["TitleId"] = new SelectList(_context.AdministratorTitles, "TitleId", "Name", aVM.AdminTitleID);
            return RedirectToAction("List");
        }

        // GET: AdminEmployeeInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var administrator = await _context.Administrators
                .Include(a => a.Title)
                .FirstOrDefaultAsync(m => m.AdministratorId == id);
            if (administrator == null) return NotFound();
            return View(administrator);
        }

        // POST: AdminEmployeeInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrator = await _context.Administrators.FindAsync(id);
            if (administrator != null) _context.Administrators.Remove(administrator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool AdministratorExists(int id)
        {
            return _context.Administrators.Any(e => e.AdministratorId == id);
        }

        // GET: AdminEmployeeInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ad = await _context.Administrators
                .Include(a => a.Title)
                .FirstOrDefaultAsync(m => m.AdministratorId == id);
            if (ad == null) return NotFound();
            return View(ad);
        }
    }
}
