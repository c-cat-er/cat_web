using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace modpackFront.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Account { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
