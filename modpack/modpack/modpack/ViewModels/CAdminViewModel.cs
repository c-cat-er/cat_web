using modpack.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace modpack.ViewModels
{
    public class CAdminViewModel
    {
        public CAdminViewModel() { }

        [DisplayName("序")]
        public int AdminID { get; set; }

        [DisplayName("職級")]
        public int AdminTitleID { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名不可為空")]
        public string AdminName { get; set; }

        [DisplayName("員工編號")]
        public string aAdminCode { get; set; }

        [DisplayName("照片")]
        public string AdminImage { get; set; }

        public IFormFile photo { get; set; }

        [DisplayName("帳號")]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "帳號錯誤(只能包含英文字母和數字)")]
        public string AdminAccount { get; set; }

        [DisplayName("密碼")]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "密碼錯誤(只能包含英文字母和數字)")]
        public string AdminPassword { get; set; }

        [DisplayName("職級")]
        public int TitleID { get; set; }

        public string TitleName { get; set; }

        public int aPermissions { get; set; }

        public string txtKeyword { get; set; }
    }
}
