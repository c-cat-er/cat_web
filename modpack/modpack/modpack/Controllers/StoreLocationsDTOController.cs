using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using modpack.ViewModels;
using modpackApi.DTO;
using modpack.Models;
using Newtonsoft.Json;
using AutoMapper.QueryableExtensions;

namespace modpack.Controllers
{
    public class StoreLocationsDTOController : Controller
    {
        private readonly ModPackContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly IMapper _mapper;

        public StoreLocationsDTOController(ModPackContext context, IHttpClientFactory httpClientFactory,
            IConfiguration configuration, IMapper mapper)
        {
            _context = context;

            _httpClient = new HttpClient();
            ///使用 httpClientFactory，可避免在應用程式中建立大量的 HttpClient 實例.
            ///清除默认请求头中的所有 Accept 头部.
            ///从 API 接收 JSON 格式的响应数据.
            _httpClientFactory = httpClientFactory;
            ///_httpClient = httpClientFactory.CreateClient();
            ///_httpClient.DefaultRequestHeaders.Accept.Clear();
            ///_httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            ///若 Program.cs 中没有為 ApiBaseUrl 指定值，或該值為 null，就抛出異常，而不是使 _apiUrl 字段為 null 造成空引用異常.
            _apiUrl = configuration.GetValue<string>("ApiUrl") ?? throw new InvalidOperationException("ApiUrl must be configured in appsettings.json");

            ///自動映射.
            _mapper = mapper;
        }

        #region GET
        // GET: StoreLocationsDTO
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> List(CKeywordViewModel vm)
        {
            var httpClient = _httpClientFactory.CreateClient();
            //string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
            HttpResponseMessage response;
            if (!string.IsNullOrEmpty(vm.txtKeyWord))
                response = await httpClient.GetAsync($"{_apiUrl}/api/StoreLocationsDTO_REST/Keywrod/" + vm.txtKeyWord);
            else
                response = await httpClient.GetAsync($"{_apiUrl}/api/StoreLocationsDTO_REST/AJ");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var storeLocationsDTO = System.Text.Json.JsonSerializer.Deserialize<List<StoreLocationDTO>>(responseData);

                var storeLocationVMs = _mapper.Map<List<StoreLocationVM>>(storeLocationsDTO);
                return View(storeLocationVMs);
            }
            else
            {
                return View("Error");
            }
        }
        #endregion

        #region GET id
        // GET: StoreLocationsDTO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocationsDTO_REST/{id}";

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var storeLocationDTO = System.Text.Json.JsonSerializer.Deserialize<StoreLocationDTO>(responseData);
                var storeLocationVM = _mapper.Map<StoreLocationVM>(storeLocationDTO);
                return View(storeLocationVM);
            }
            else
            {
                return View("Error");
            }
        }
        #endregion

        #region POST
        // GET: StoreLocationsDTO/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreLocationsDTO/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreLocationVM storeLocationVM)
        {
            if (ModelState.IsValid)
            {
            }
            //return View(storeLocationVM);

            // 將 ViewModel 映射為 DTO
            var storeLocationDTO = _mapper.Map<StoreLocationDTO>(storeLocationVM);

            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocationsDTO_REST";

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(storeLocationDTO), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Failed to create data.");
                return View(storeLocationVM);
            }
        }
        
        // PATCH: StoreLocationsDTO/Edit/{id}
        [HttpPatch]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JsonPatchDocument<StoreLocationVM> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid data.");
            }

            // Retrieve the original data
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_apiUrl}/api/StoreLocationsDTO_REST/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Item not found.");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var storeLocationDTO = System.Text.Json.JsonSerializer.Deserialize<StoreLocationDTO>(responseData);

            // Map DTO to ViewModel
            var storeLocationVM = _mapper.Map<StoreLocationVM>(storeLocationDTO);

            // Apply the patch
            patchDoc.ApplyTo(storeLocationVM, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map ViewModel back to DTO
            storeLocationDTO = _mapper.Map<StoreLocationDTO>(storeLocationVM);

            // Send updated data to API
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(storeLocationDTO), Encoding.UTF8, "application/json");
            response = await httpClient.PatchAsync($"{_apiUrl}/api/StoreLocationsDTO_REST/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update data.");
                return View(storeLocationVM);
            }
        }

        
        // PUT: StoreLocationsDTO/Update/{id}
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, StoreLocationVM storeLocationVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map ViewModel to DTO
            var storeLocationDTO = _mapper.Map<StoreLocationDTO>(storeLocationVM);

            // Send updated data to API
            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocationsDTO_REST/{id}";

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(storeLocationDTO), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update data.");
                return View(storeLocationVM);
            }
        }
        #endregion


        /*
        #region
        // GET: StoreLocationsDTO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var storeLocation = await _context.StoreLocations.FindAsync(id);
            if (storeLocation == null) return NotFound();
            var storeLocationVM = _mapper.Map<StoreLocationVM>(storeLocation);
            return View(storeLocationVM);
        }

        // POST: StoreLocationsDTO/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreLocationVM storeLocationVM)
        {//主要

            //if (ModelState.IsValid)
            //{
                
            //}

            var storeLocationDTO = _mapper.Map<StoreLocationDTO>(storeLocationVM);

            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocationsDTO_REST/{storeLocationVM.StoreLocationId}";

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(storeLocationDTO), Encoding.UTF8, "application/json");

            //var response = await httpClient.PutAsync(apiUrl, jsonContent);
            var response = await httpClient.PostAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update data.");
                return View(storeLocationVM);
            }
           // return View(storeLocationVM);
        }*/


        // GET: StoreLocationsDTO/EditPut/5
        public async Task<IActionResult> EditPut(int? id)
        {
            if (id == null) return NotFound();
            var storeLocation = await _context.StoreLocations.FindAsync(id);
            if (storeLocation == null) return NotFound();
            var storeLocationVM = _mapper.Map<StoreLocationVM>(storeLocation);
            return View(storeLocationVM);
        }

        //[HttpPut]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPut(int id, StoreLocationVM storeLocationVM)
        {
            //if (ModelState.IsValid)
            //{

            //}

            var storeLocationDTO = _mapper.Map<StoreLocationDTO>(storeLocationVM);

            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocationsDTO_REST/{storeLocationVM.StoreLocationId}";

            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(storeLocationDTO), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(apiUrl, jsonContent);
            //var response = await httpClient.PostAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode) return RedirectToAction("List");
            else
                ModelState.AddModelError("", "Failed to update data.");
                return View(storeLocationVM);
            // return View(storeLocationVM);
        }

        /*
        // GET: StoreLocationsDTO/EditPatch/5
        public async Task<IActionResult> EditPatch(int id, StoreLocationVM storeLocationVM)
        {
            if (!ModelState.IsValid)
            {
                return View(storeLocationVM);
            }

            // 根据视图模型创建 patchDoc
            var patchDoc = new JsonPatchDocument<StoreLocationVM>();
            patchDoc.Replace(e => e.Name, storeLocationVM.Name);
            patchDoc.Replace(e => e.OfficeTelephone, storeLocationVM.OfficeTelephone);
            patchDoc.Replace(e => e.Address, storeLocationVM.Address);

            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocationsDTO_REST/{id}";

            var patchContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(patchDoc), Encoding.UTF8, "application/json-patch+json");

            var response = await httpClient.PatchAsync(apiUrl, patchContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update data.");
                return View(storeLocationVM);
            }
        }

        //[HttpPatch("{id}")]
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPatch(int id, [FromBody] JsonPatchDocument<StoreLocationVM> patchDoc)
        {
            if (patchDoc == null) return BadRequest(); // 如果請求主體為空，則返回 BadRequest
            
            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocations/{id}";

            // 將 JSON Patch 應用到請求主體
            var patchContent = new StringContent(JsonConvert.SerializeObject(patchDoc), Encoding.UTF8, "application/json-patch+json");

            // 發送 PATCH 請求
            var response = await httpClient.PatchAsync(apiUrl, patchContent);

            if (response.IsSuccessStatusCode) return RedirectToAction("List");
            else
                ModelState.AddModelError("", "Failed to update data.");
                return View();
        }*/


        // GET: StoreLocationsDTO/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"{_apiUrl}/api/StoreLocationsDTO_REST/{id}";

            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode) return RedirectToAction("List");
            else
            {
                ModelState.AddModelError("", "Failed to delete data.");
                return View("Error");
            }
        }

        // POST: StoreLocationsDTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeLocation = await _context.StoreLocations.FindAsync(id);
            if (storeLocation != null) _context.StoreLocations.Remove(storeLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool StoreLocationExists(int id)
        {
            return _context.StoreLocations.Any(e => e.StoreLocationId == id);
        }
    }
}