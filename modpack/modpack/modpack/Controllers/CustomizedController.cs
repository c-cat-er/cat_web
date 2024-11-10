using modpack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using modpack.DTO;
using System.Text;

namespace modpack.Controllers
{
    public class CustomizedController : Controller
    {
        private string imagepath = "wwwroot/images/Customized/";
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> List(CKeywordViewModel vm)
        {
            using (var httpClient = new HttpClient())
            {
                string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
                HttpResponseMessage response;
                if (!string.IsNullOrEmpty(vm.txtKeyWord))
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/Customizeds/key/" + vm.txtKeyWord);
                }
                else
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/Customizeds");
                }
                var customizedDTO = JsonSerializer.Deserialize<List<CustomizedDTO>>(response.Content.ReadAsStringAsync().Result);
                return View(customizedDTO);
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
        public async Task<IActionResult> ViewImage(int id)
        {
            return View(new CKeywordViewModel { txtId = id });
        }
    }
}
