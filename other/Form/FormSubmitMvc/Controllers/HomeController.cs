using FormSubmit.Models;
using FormSubmit.VM;
using FormSubmit.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Humanizer;

namespace FormSubmit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult FormSubmit()
        {
            return View();
        }

        ///POST: /Home/FormSubmit
        [HttpPost]
        public ActionResult FormSubmit(string name, string email)
        {///輸出到除錯輸出窗口.

            System.Diagnostics.Debug.WriteLine("姓名：" + name);
            System.Diagnostics.Debug.WriteLine("郵箱：" + email);

            return RedirectToAction("SubmitSuccess");
        }

        public IActionResult FormSubmit2()
        {
            return View();
        }

        ///POST: /Home/FormSubmit2
        [HttpPost]
        public IActionResult FormSubmit2(tUser tUser)
        {///儲存到 Model.

            return RedirectToAction("SubmitSuccess");
        }

        public IActionResult FormSubmit3()
        {
            return View();
        }

        ///POST: /Home/FormSubmit3
        [HttpPost]
        public IActionResult FormSubmit3(TUserVM userVM)
        {///use VM

            tUser tUser = new()
            {
                Name = userVM.UserName,
                Email = userVM.Email,
            };
            //save to db.
            return RedirectToAction("SubmitSuccess");
        }

        public IActionResult FormSubmit4()
        {
            return View();
        }

        ///POST: /Home/FormSubmit4
        [HttpPost]
        public async Task<IActionResult> FormSubmit4(TUserDTO userDTO)
        {///use API + DTO

            if (!ModelState.IsValid) return View(userDTO);

            //保存提交的數據到會話
            HttpContext.Session.SetString("SubmittedUserName", userDTO.Name);
            Console.WriteLine(userDTO.Name);
            HttpContext.Session.SetString("SubmittedEmail", userDTO.Email);
            Console.WriteLine(userDTO.Email);

            ///HttpClient to Api
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7282/api/Home", userDTO);
            if (response.IsSuccessStatusCode) return RedirectToAction("SubmitSuccess2");
            else
            {
                ///若請求失敗，讀取錯誤信息並顯示到視圖中.
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorMessage);
                return View(userDTO);   ///重新顯示表單頁面.
            }
        }


        /*
         資料驗證： 使用資料注解（Data Annotations）或Fluent驗證（Fluent Validation）等技術來實現資料驗證，以確保用戶輸入的資料符合要求。
服務層： 將業務邏輯抽象到服務層中，以提高代碼的可測試性、可讀性和可維護性。在控制器中調用服務層的方法來執行業務邏輯，而不是在控制器中直接執行業務邏輯。
非同步操作： 對於需要等待外部資源（如資料庫、Web服務器等）響應的操作，使用異步操作來提高應用程式的性能和吞吐量。在控制器動作方法中使用異步修飾符（async）和非同步方法來執行這些操作。
依賴注入： 使用依賴注入容器（Dependency Injection Container）來管理應用程式中的依賴關係，以實現鬆散耦合和易於測試的代碼。在控制器中通過構造函式注入服務依賴，而不是在控制器中直接創建服務實例。
日誌記錄： 使用日誌記錄框架（如Serilog、NLog等）來記錄應用程式中的操作和錯誤，以便追蹤和調試問題。
統一的錯誤處理： 使用統一的錯誤處理機制來處理應用程式中的錯誤，以提供一致的用戶體驗和方便的錯誤追蹤。
單元測試： 編寫單元測試來測試控制器、服務層和其他組件的行為，以確保它們正確地執行預期的功能。
         
         
         */


        public ActionResult SubmitSuccess()
        {
            return View();
        }

        public ActionResult SubmitSuccess2()
        {
            //從會話中檢索數據
            var submittedUserName = HttpContext.Session.GetString("SubmittedUserName");
            var submittedEmail = HttpContext.Session.GetString("SubmittedEmail");

            var vm = new TUserVM { UserName = submittedUserName, Email = submittedEmail };
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
