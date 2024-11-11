using FormSubmitApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormSubmitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public IActionResult FormSubmit4([FromForm] TUserDTO userDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); ///404.

            try
            {
                ///DTO to Model.
                var user = new TUserDTO
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                };
                //_context save to db.
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
