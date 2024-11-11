using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Niseko.Mana.Models;
using Niseko.Mana.Services;
using System.Diagnostics;

namespace Niseko.Mana.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlSettings _urlSettings;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IOptions<UrlSettings> urlSettings, IWebHostEnvironment env,
            ILogger<HomeController> logger)
        {
            _urlSettings = urlSettings.Value;
            _env = env;
            _logger = logger;
        }

        public string GetApiUrl()
        {
            if (_env.IsDevelopment())
            {
                // 根据需要选择 http 或 https
                return _urlSettings.HttpUrl;
            }
            else if (_env.IsStaging())
            {
                return _urlSettings.HttpsUrl;
            }
            else // Production or other environments
            {
                return _urlSettings.IISExpressUrl;
            }
        }



        public IActionResult Index()
        {
            return View();
        }











        public IActionResult Privacy()
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
