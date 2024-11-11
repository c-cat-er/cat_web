using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace modpackFront.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "密碼長度必須在 4 到 16 個字符之間")]
        [DisplayName("新密碼")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "新密碼和確認密碼不匹配。")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "密碼長度必須在 4 到 16 個字符之間")]
        [DisplayName("確認新密碼")]
        public string ConfirmPassword { get; set; }
    }
}
