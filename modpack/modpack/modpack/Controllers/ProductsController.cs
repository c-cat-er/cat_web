using Azure;
using Microsoft.AspNetCore.Mvc;
using modpack.DTO;
using modpack.Models;
using modpack.ViewModels;
using System.Text;
using System.Text.Json;

namespace modpack.Controllers
{
    public class ProductsController : Controller
    {
        private string imagepath= "wwwroot/images/product/";
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> List(CKeywordViewModel vm)
        {
            using (var httpClient = new HttpClient())
            {
                string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
                HttpResponseMessage response;
                if (!string.IsNullOrEmpty(vm.txtKeyWord))
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/Products/key/" + vm.txtKeyWord);
                }
                else
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/Products");
                }
                var productsDTO = JsonSerializer.Deserialize<List<ProductsDTO>>(response.Content.ReadAsStringAsync().Result);
                return View(productsDTO);
            }
        }

        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Create()
        {
            return View(new CKeywordViewModel { txtId = 0 });
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
             
                return filename;
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
        public async Task<IActionResult> Edit(int id)
        {
            return View(new CKeywordViewModel { txtId = id });
        }

        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                string apiBaseUrl = "http://localhost:7250";
                var response = await httpClient.DeleteAsync($"{apiBaseUrl}/api/Products/" + id);
                return RedirectToAction("List");
            }
        }

        public static string Linkstring = "0";

    }
}

