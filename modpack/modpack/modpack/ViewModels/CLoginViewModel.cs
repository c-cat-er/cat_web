using modpack.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace modpack.ViewModels
{
    public class CLoginViewModel
    {
        public CLoginViewModel() { }

        [DisplayName("帳號")]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "帳號錯誤(只能包含英文字母和數字)")]
        public string txtAccount { get; set; }

        [DisplayName("密碼")]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "密碼錯誤(只能包含英文字母和數字)")]
        public string txtPassword { get; set; }
    }
}
