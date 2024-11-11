using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using modpackFront.DTO;
using modpackFront.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace modpackFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModPackContext _context;
        private readonly ILogger<HomeController> _logger;

		public HomeController(ModPackContext modPackContext, ILogger<HomeController> logger)
        {
            _context = modPackContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var imageCarousels = await _context.ImageCarousels.ToListAsync();
            var products = await _context.Products
                .Include(p => p.Status)
                .Include(p => p.Promotion)
                .ToListAsync();

            var homeDTO = new HomeDTO
            {
                ImageCarousel = imageCarousels,
                Product = products,
            };
            ViewData["CustomerId"] = User.Identity.IsAuthenticated ? User.Identity.Name : "Guest";
            return View(homeDTO);
        }

        public IActionResult ExampleHome()
        {
            var imageCarousels = _context.ImageCarousels.ToList();
            return View(imageCarousels);
        }

        public async Task<IActionResult> TestService()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity userIdentity || !userIdentity.IsAuthenticated)
                return RedirectToAction("Login", "Authentication");

            var memberIdClaim = userIdentity.FindFirst(ClaimTypes.Sid);
            if (memberIdClaim == null || !int.TryParse(memberIdClaim.Value, out int memberId))
                return RedirectToAction("Login", "Authentication");

            var member = await _context.Members.FindAsync(memberId);
            if (member == null) return NotFound();
            var serviceRecord = await _context.ServiceRecords.FirstOrDefaultAsync(sr => sr.MemberId == memberId);
            ViewBag.MemberID = memberId;
            ViewBag.MemberName = member.Name;
            return View(serviceRecord);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ReachOut()
        {
            return View();
        }

        //接收聯絡我們資料的action ??
        //public IActionResult aReachOut(接收資料)---------------------------------------------------------------------



        public IActionResult Sendout()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------
        [HttpPost]

        public IActionResult Sendout(ServiceRecord record)
        {
           
            try
            {
                _context.ServiceRecords.Add(record);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //string errorMessage = "顯示錯誤";
                //ViewBag["Error"] = errorMessage;

                ViewData["Error"] = "非會員ID請重新輸入";
            }
            return View("ReachOut");

        }

        //-----------------------------------------------------------------------------------------------------------


        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult GoBlog()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult StoreLocations() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
