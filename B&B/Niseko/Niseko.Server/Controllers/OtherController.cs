using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        //private readonly Home _myService;

        //public UrlController(MyService myService)
        //{
        //    _myService = myService;
        //}

        //[HttpGet("currentUrl")]
        //public IActionResult GetCurrentUrl()
        //{///
        //    var url = _myService.GetApiUrl();
        //    return Ok(new { apiUrl = url });
        //}
    }
}
