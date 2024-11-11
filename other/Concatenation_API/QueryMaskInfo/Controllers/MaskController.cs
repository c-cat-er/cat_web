using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueryMaskInfo.Services;

namespace QueryMaskInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaskController : ControllerBase
    {
        private readonly MaskService _maskService;

        public MaskController(MaskService maskService)
        {
            _maskService = maskService;
        }

        public async Task<IActionResult> Get()
        {///呼叫 Services

            try
            {
                var maskCount = await _maskService.GetMaskInfo();
                return Ok(maskCount);
            }
            catch (HttpRequestException ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
