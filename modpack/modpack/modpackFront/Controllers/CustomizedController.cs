using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using modpackFront.DTO;
using modpackFront.ViewModels;
using System.Text;
using System.Text.Json;

namespace modpackFront.Controllers
{
    public class CustomizedController : Controller
    {
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Edit()
        { 
            return View();
        }
   
    }
}
