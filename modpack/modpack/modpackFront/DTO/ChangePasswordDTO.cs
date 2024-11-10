using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace modpackFront.DTO
{
    public class ChangePasswordDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LevelName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("當前密碼")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("新密碼")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("確認新密碼")]
        [Compare("NewPassword", ErrorMessage = "新密碼和確認新密碼不相同")]
        public string ConfirmPassword { get; set; }
    }

}
