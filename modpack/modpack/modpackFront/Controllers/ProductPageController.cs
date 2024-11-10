using Microsoft.AspNetCore.Mvc;
using modpackFront.ViewModels;
using modpackFront.DTO;
using System.Text.Json;
using System.Security.Principal;

namespace modpackFront.Controllers
{
    public class ProductPageController : Controller
    {
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Productsdetail(int id)
        {
            using (var httpClient = new HttpClient())
            {
                string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
                HttpResponseMessage response;
                if (User.Identity.IsAuthenticated == true)
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/ProductsPage/product/member/" + id+"/"+ User.Claims.ToList()[0].Value);
                }
                else
                {
                    response = await httpClient.GetAsync($"{apiBaseUrl}/api/ProductsPage/product/" + id);
                }
                var productsPageDTO = JsonSerializer.Deserialize<ProductsPageDTO>(response.Content.ReadAsStringAsync().Result);
                return View(productsPageDTO);
            }
        }
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Customizeddetail(int id)
        {
            if (User.Identity.IsAuthenticated==true)
            {
                string userId = User.Claims.ToList()[0].Value;
                using (var httpClient = new HttpClient())
                {
                    string apiBaseUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("ApiUrl").Value;
                    HttpResponseMessage response = await httpClient.GetAsync($"{apiBaseUrl}/api/ProductsPage/memberproduct/" + id+"/"+ userId);
                    var productsPageDTO = JsonSerializer.Deserialize<ProductsPageDTO>(response.Content.ReadAsStringAsync().Result);
                    return View(productsPageDTO);
                }
            }
            else
            {
                return Redirect(new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build().GetSection("Kestrel").GetSection("Endpoints")
                    .GetSection("Http").GetSection("Url").Value+ "/Authentication/Login");
            }
        }
    }
}
