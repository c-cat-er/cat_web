using Appointment.clientN.DTOs;
using Appointment.clientN.Models;
using Appointment.clientN.Services;
using Appointment.clientN.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Appointment.clientN.Controllers
{
    public class HomeController(IOptions<UrlSettings> urlSettings,
        HttpClient httpClient, ILogger<HomeController> logger) : Controller
    {
        private readonly UrlSettings _urlSettings = urlSettings.Value;
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            ViewBag.SwaggerApiUrl = _urlSettings.SwaggerApiUrl;
            return View();
        }

        public async Task<IActionResult> Login()
        {
            ViewBag.SwaggerApiUrl = _urlSettings.SwaggerApiUrl;
            var storeData = await _httpClient.GetFromJsonAsync<IEnumerable<Brstor>>($"{_urlSettings.SwaggerApiUrl}/api/MWs");
            TempData["StoreList"] = JsonSerializer.Serialize(storeData);
            var vm = new LoginVM
            {
                Brstor = new Brstor { CNUM = "", CNAME = "" },
                SHOPNO = "",
                USERID = "",
                PASSWD = "",
                StoreList = storeData ?? []
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string StoreListJson)
        {
            var dto = new LoginDTO
            {
                SHOPNO = vm.SHOPNO,
                USERID = vm.USERID,
                PASSWD = vm.PASSWD
            };

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_urlSettings.SwaggerApiUrl}/api/MWu/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(result);
                var jsonResult = JsonSerializer.Deserialize<JsonElement>(result);
                var token = JsonSerializer.Deserialize<JsonElement>(result).GetProperty("JWTToken").GetString();

                var userId = dto.USERID;

                HttpContext.Session.SetString("JWTToken", token!);
                HttpContext.Session.SetString("USERID", userId);

                TempData["AlertMessage"] = "登入成功";
                TempData["RedirectUrl"] = Url.Action("Query", "Home");
                return View(vm);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Login failed: {errorContent}");

                vm.StoreList = JsonSerializer.Deserialize<IEnumerable<Brstor>>(StoreListJson);

                TempData["AlertMessage"] = "登入失敗: " + errorContent;
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWTToken");
            HttpContext.Session.Remove("USERID");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public JsonResult TransferData(List<TransferDataVM> data)
        {
            TempData["CureData"] = data;
            var redirectUrl = Url.Action("Registration");
            return Json(new { redirectUrl });
        }

        public IActionResult Query()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var userId = HttpContext.Session.GetString("USERID");
            ViewBag.UserId = userId;
            ViewBag.SwaggerApiUrl = _urlSettings.SwaggerApiUrl;
            return View();
        }

        public IActionResult Registration()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var userId = HttpContext.Session.GetString("USERID");
            ViewBag.UserId = userId;
            ViewBag.SwaggerApiUrl = _urlSettings.SwaggerApiUrl;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RegistrationEdit(int id)
        {
            ViewBag.SwaggerApiUrl = _urlSettings.SwaggerApiUrl;
            var response = await _httpClient.GetAsync($"{_urlSettings.SwaggerApiUrl}/api/MWbe/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to get data，請稍後再試。");
                return View(new RegistrationCD());
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var model = JsonSerializer.Deserialize<RegistrationCD>(responseData, options);
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to deserialize data，請稍後再試。");
                return View(new RegistrationCD());
            }

            var operUserResponse = await _httpClient.GetAsync($"{_urlSettings.SwaggerApiUrl}/api/MWu/GetNamesByIds?operuser={model.OPERUSER}&cdoc={model.CDOC}&cdocs={model.CDOCS}&cdocp={model.CDOCP}");
            if (!operUserResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to get data operuser.");
                return View(model);
            }

            var operUserData = await operUserResponse.Content.ReadAsStringAsync();
            var operUser = JsonSerializer.Deserialize<UserNameQueryDTO>(operUserData, options);

            ViewBag.ACUTEId = model.ACUTE;
            ViewBag.ACUTEValue = ConvertAcuteToString(model.ACUTE!);
            ViewBag.OPERUSERId = model.OPERUSER;
            ViewBag.OPERUSERName = operUser!.OPERUSERName;
            ViewBag.CDOCName = operUser.CDOCName;
            ViewBag.CDOCSName = operUser.CDOCSName;
            ViewBag.CDOCPName = operUser.CDOCPName;

            var allUsersResponse = await _httpClient.GetAsync($"{_urlSettings.SwaggerApiUrl}/api/MWu/GetUsersByAcute?acute={model.ACUTE}");
            if (!allUsersResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Unable to get data acute");
                return View(model);
            }

            var allUsersData = await allUsersResponse.Content.ReadAsStringAsync();
            var allUsers = JsonSerializer.Deserialize<List<UserQueryDTO>>(allUsersData, options);

            ViewBag.RDATE = model.RDATE;
            ViewBag.RTIME = model.RTIME!.Replace(" ", "").Insert(2, ":");
            ViewBag.Users = allUsers;
            ViewBag.CDOCId = model.CDOC;
            ViewBag.CDOCSId = model.CDOCS;
            ViewBag.CDOCPId = model.CDOCP;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationEdit(RegistrationCD m)
        {
            var dto = new RegistrationCD()
            {
                WBID = m.WBID,
                ACUTE = m.ACUTE,
                CNUM = m.CNUM,
                SERNO = m.SERNO,
                SDATE = m.SDATE,
                CURENO = m.CURENO,
                RDATE = m.RDATE,
                RTIME = m.RTIME!.Replace(":", ""),
                OPERUSER = HttpContext.Session.GetString("USERID"),
                CDOC = m.CDOC,
                CDOCS = m.CDOCS,
                CDOCP = m.CDOCP,
                CURECNT = m.CURECNT,
                CURECNTS = m.CURECNTS,
                CSTAT = m.CSTAT,
                CMESS = m.CMESS,
                CUPDATE = DateTime.Now.ToString("yyymmddHH:mm:ss")
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_urlSettings.SwaggerApiUrl}/api/MWbe/{m.WBID}", requestContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Registration");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "更新失敗，請稍後再試。");
                return View(m);
            }
        }

        public IActionResult Calendar()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            ViewBag.SwaggerApiUrl = _urlSettings.SwaggerApiUrl;
            return View();
        }

        private static string ConvertAcuteToString(string acute)
        {
            return acute switch
            {
                "1" => "看診",
                "2" => "療程",
                "3" => "諮詢",
                "4" => "手術",
                _ => "無",
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
