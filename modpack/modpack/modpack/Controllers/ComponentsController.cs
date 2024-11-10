using Microsoft.AspNetCore.Mvc;
using modpack.DTO;
using modpack.Models;
using modpack.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace modpack.Controllers
{
    public class ComponentsController : Controller
    {
        private string imagepath = "wwwroot/images/component/";
        public static string Linkstring = "0";
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> List(CKeywordViewModel vm)
        {
            using (var httpClient = new HttpClient())
            {
                string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
                HttpResponseMessage response;
                if (!string.IsNullOrEmpty(vm.txtKeyWord))
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/Components/key/" + vm.txtKeyWord);
                }
                else
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/Components");
                }
                var componentsDTO = JsonSerializer.Deserialize<List<ComponentsDTO>>(response.Content.ReadAsStringAsync().Result);
                return View(componentsDTO);
            }
        }
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Create(ComponentsDTO componentsDTO)
        {
            if (componentsDTO.productCategoriesjson != null)
            {
                componentsDTO.productCategories = JsonSerializer.Deserialize<List<ComponentsDTO.ProductCategory>>(componentsDTO.productCategoriesjson);
            }
            string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync($"{apiBaseUrl}/api/Components", new StringContent(JsonSerializer.Serialize(componentsDTO), Encoding.UTF8, "application/json"));
            return RedirectToAction("List");
        }
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Edit(int? id)
        {
            ModPackContext db = new ModPackContext();
            Component Comp = db.Components.FirstOrDefault(p => p.ComponentId == id);
            if (Comp == null)
                return RedirectToAction("List");
            ComponentsDTO cp = new ComponentsDTO();
            cp.ComponentID = Comp.ComponentId;
            cp.MaterialID = Comp.MaterialId;
            cp.ColorID = Comp.ColorId;
            cp.MaterialID = Comp.MaterialId;
            cp.StatusID = Comp.StatusId;
            cp.Name = Comp.Name;
            cp.Category = Comp.Category;
            cp.OriginalPrice = (float)Comp.OriginalPrice;
            cp.IsCustomized = Comp.IsCustomized;
            cp.FBXFileName = Comp.FbxfileName;
            cp.productCategories = new List<ComponentsDTO.ProductCategory>();
            foreach (var item in db.Categories.Where(p => p.ComponentId == id))
            {
                cp.productCategories.Add(new ComponentsDTO.ProductCategory
                {
                    CategoryID = item.CategoryId,
                    Name = item.Name,
                });
            }
            return View(cp);
        }
        [HttpPost]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Edit(ComponentsDTO componentsDTO)
        {
            if (componentsDTO.productCategoriesjson!=null)
            {
                componentsDTO.productCategories = JsonSerializer.Deserialize<List<ComponentsDTO.ProductCategory>>(componentsDTO.productCategoriesjson);
            }
            string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PutAsync($"{apiBaseUrl}/api/Components/" + componentsDTO.ComponentID, new StringContent(JsonSerializer.Serialize(componentsDTO), Encoding.UTF8, "application/json"));
            return RedirectToAction("List");
        }
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
                var response = await httpClient.DeleteAsync($"{apiBaseUrl}/api/Components/" + id);
                return RedirectToAction("List");
            }
        }

        [ResponseCache(NoStore = true)]
        public async Task<string> imagecreate()
        {
            Stream body = Request.Body;
            byte[] buffer = new byte[(int)Request.ContentLength + 1];
            int bytesRead;
            using (MemoryStream ms = new MemoryStream())
            {
                while ((bytesRead = await body.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await ms.WriteAsync(buffer, 0, bytesRead);
                }
                byte[] data = ms.ToArray();
                string filename = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10) + ".png";
                string filepath = imagepath + filename;
                System.IO.File.WriteAllBytes(filepath, data);
                HttpClient httpClient = new HttpClient();
                string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
                var request = await httpClient.PutAsync($"{apiBaseUrl}/api/Components/image/" + Linkstring, new StringContent(filename, Encoding.UTF8, "application/json"));
                if (request.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "OK";
                }
                else
                {
                    System.IO.File.Delete(filepath);
                    return "";
                }
            }
        }
        [ResponseCache(NoStore = true)]
        public async Task<string> imageDelete()
        {
            StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
            string requestBody = await reader.ReadToEndAsync();
            System.IO.File.Delete(imagepath + requestBody);
            return "";
        }
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> ViewImage(int id)
        {
            Linkstring= id.ToString();
            return View(new CKeywordViewModel { txtId = id });
        }
    }
}
