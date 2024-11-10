using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using modpackFront.DTO; // 確保 LoginDTO 在這個命名空間下

namespace modpackFront.ViewModels
{
    public class LoginRegisterViewModel
    {
        // 登入部分
        public LoginDTO Login { get; set; }

        // 註冊部分
        [DisplayName("會員等級")]
        public int LevelId { get; set; } = 1; // 對應為新註冊會員

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "帳號長度必須在 4 到 16 個字符之間")]
        [DisplayName("帳號")]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "密碼長度必須在 4 到 16 個字符之間")]
        [DisplayName("密碼")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相同。")]
        [DisplayName("密碼確認")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "名稱必須在 8 個字符以內")]
        [DisplayName("名稱")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="請輸入符合格式的電子信箱")]
        [DisplayName("電子郵件")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "電話必須為數字，通常為9碼或10碼數字")]
        [DisplayName("電話")]
        public string Phone { get; set; }

        [StringLength(100)]
        [DisplayName("地址")]
        public string Address { get; set; }
    }

}
