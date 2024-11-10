using System.ComponentModel.DataAnnotations;

namespace modpackApi.DTO
{
    public class AdminUserDTO
    {
        [Required]
        public string AdminCode { get; set; } = "D0001";

        [Required]
        public required string UserName { get; set; } = "22";

        [Required]
        public required string Account { get; set; } = string.Empty;

        [Required]
        public required string Password { get; set; } = string.Empty;
    }
}
