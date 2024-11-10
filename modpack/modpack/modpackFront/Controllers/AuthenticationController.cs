using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using modpackFront.DTO;
using modpackFront.Models;
using modpackFront.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages; // 引入 ViewModel

namespace modpackFront.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ModPackContext _context;

        public AuthenticationController(ModPackContext context)
        {
            _context = context;
        }

        // 登錄相關方法
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            else { return View("LoginRegister"); }

        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 增加防止CSRF攻擊
        public async Task<IActionResult> Login(LoginRegisterViewModel model)
        {

            // 使用async方法和FirstOrDefaultAsync進行資料庫查詢
            var user = await _context.Members.FirstOrDefaultAsync(u => u.Account == model.Login.Account && u.Password == model.Login.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.MemberId.ToString()), // 儲存會員ID
                    new Claim(ClaimTypes.Name, user.Name) // 儲存會員名稱
                    // 根據需要添加更多聲明
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // 配置屬性，如需要
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "登入失敗");
            }

            return View("LoginRegister", model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }

        // 註冊相關方法
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 確保註冊方法也使用ValidateAntiForgeryToken屬性
        public async Task<IActionResult> Register(LoginRegisterViewModel viewModel)
        {
            // 檢查帳號是否已存在
            if (_context.Members.Any(m => m.Account == viewModel.Account))
            {
                ModelState.AddModelError("", "此會員帳號已存在");
                return View("LoginRegister", viewModel);
            }

            var member = new Member
            {
                LevelId = viewModel.LevelId,
                Account = viewModel.Account,
                Password = viewModel.Password,
                Name = viewModel.Name,
                Email = viewModel.Email,
                CreationTime = DateTime.Now,
                ModificationTime = DateTime.Now
            };

            _context.Add(member);
            await _context.SaveChangesAsync();

            // 生成驗證Url
            var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(member.MemberId.ToString()));
            var verificationUrl = Url.Action("Confirm", "Authentication", new { token }, protocol: HttpContext.Request.Scheme);

            //將驗證Url傳遞給視圖
            ViewBag.VerificationUrl = verificationUrl;

            return RedirectToAction("RegisterSuccess", "Authentication", new { verificationUrl });
        }

        public IActionResult RegisterSuccess(string verificationUrl)
        {
            ViewBag.VerificationUrl = verificationUrl;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Confirm(string token)
        {
            // 檢查token是否為null或空字符串
            if (string.IsNullOrEmpty(token))
            {
                // 如果token為null或空，處理錯誤或返回合適的錯誤頁面
                // 例如：返回到一個錯誤頁面或顯示一個友好的錯誤信息
                return View("404");
            }

            try
            {
                // 從token解碼得到會員ID
                var memberId = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token)));
                // 查找對應的會員實體
                var member = await _context.Members.FindAsync(memberId);
                if (member != null)
                {
                    // 更新會員狀態為已確認
                    member.IsConfirmed = true;
                    _context.Update(member);
                    await _context.SaveChangesAsync();

                    // 自動登入這個用戶
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid, member.MemberId.ToString()), // 儲存會員ID
                        new Claim(ClaimTypes.Name, member.Name) // 儲存會員名稱
                        // 可以根據需要添加更多聲明
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // 如果有特定的配置屬性需求，此處添加
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // 會員找不到，返回NotFound
                    return NotFound();
                }
            }
            catch (FormatException)
            {
                // 處理從Base64字符串轉換時可能出現的格式異常
                // 返回適當的錯誤頁面或錯誤信息
                return View("404");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(LoginRegisterViewModel model)
        {

            // 檢查帳號和信箱是否匹配
            var user = await _context.Members.FirstOrDefaultAsync(u => u.Account == model.Account && u.Email == model.Email);
            if (user != null)
            {
                // 生成令牌和儲存邏輯
                var token = "2386";/*new Random().Next(1000, 9999).ToString();*/

                var passwordReset = new PasswordReset
                {
                    UserId = user.MemberId,
                    Token = token,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(5)
                };

                _context.PasswordResets.Add(passwordReset);
                await _context.SaveChangesAsync();
                // 發送郵件邏輯

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // 可以選擇是否顯示錯誤信息，或是直接導航到確認頁面避免洩露用戶信息
            ModelState.AddModelError(string.Empty, "提供的信息不匹配任何帳戶，或錯誤操作。");


            return View("LoginRegister", model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPasswordConfirmation(string token)
        {
            var passwordReset = await _context.PasswordResets
                .FirstOrDefaultAsync(pr => pr.Token == token && pr.ExpiryDate >= DateTime.UtcNow);

            if (passwordReset != null)
            {
                // 令牌有效，導航到重設密碼頁面
                return RedirectToAction("ResetPassword", new { token = token });
            }
            else
            {
                // 令牌無效或過期
                ModelState.AddModelError(string.Empty, "無效或過期的驗證碼。");
                return View("ForgotPasswordConfirmation");
            }
        }

        [HttpGet]

        public IActionResult ResetPassword(string token)
        {

            var model = new ResetPasswordViewModel
            {
                Token = token
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            var passwordReset = await _context.PasswordResets
            .FirstOrDefaultAsync(pr => pr.Token == model.Token && pr.ExpiryDate >= DateTime.UtcNow);

            if (passwordReset != null)
            {
                var user = await _context.Members.FindAsync(passwordReset.UserId);
                if (user != null)
                {
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                        // 更新用戶密碼
                        user.Password = model.NewPassword;
                        _context.Update(user);
                        await _context.SaveChangesAsync();

                        // 刪除令牌或使其失效
                        _context.PasswordResets.Remove(passwordReset);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Login");
                    }

                }
            }
            // 處理錯誤情況
            return View(model);
        }

    }
}
