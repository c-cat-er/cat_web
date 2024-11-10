using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace modpackFront.DTO
{
    public class MemberDTO
    {
        public int id { get; set; }
        [StringLength(8, ErrorMessage = "名稱必須在 8 個字符以內")]
        [DisplayName("名稱")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "請輸入符合格式的電子信箱")]
        [DisplayName("電子郵件")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "電話必須為數字，通常為9碼或10碼數字")]
        [DisplayName("電話")]
        public string Phone { get; set; }

        [StringLength(100)]
        [DisplayName("地址")]
        public string Address { get; set; }

        public DateTime ModificationTime { get; set; }

        public string LevelName { get; set; }
    }
}
