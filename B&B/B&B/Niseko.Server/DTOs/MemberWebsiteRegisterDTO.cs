using System.ComponentModel.DataAnnotations;

namespace Niseko.Server.DTOs
{
    public class MemberWebsiteRegisterDTO
    {
        ///TMember
        ///FMemberID
        //public required string FMemberCode { get; set; }

        [Required(ErrorMessage = "會員名稱未填")]
        public required string FMemberName { get; set; }

        [Range(1, 3, ErrorMessage = "滑雪級別無效，必須為 1(初級), 2(中級), 3(高級)")]
        public required byte FSkiLevelID { get; set; }
        [Required(ErrorMessage = "性別未填")]
        public string? FGender { get; set; }
        public string? FBirthdate { get; set; } ///client string turn to server DateOnly
        public string? FCountryCode { get; set; }
        public string? FPhone { get; set; }
        public string? FEmail { get; set; }
        //public required byte FPermissions { get; set; }
        ///FPermissions

        ///TMemberWebsiteAccount
        [Required(ErrorMessage = "帳號未填")]
        public required string FAccount { get; set; } ///byte[]
        [Required(ErrorMessage = "密碼未填")]
        public required string FPassword { get; set; } ///byte[]

        ///TMemberLoginRecord
        //public required byte FLoginTypeID { get; set; }
        public string? FLoginTimezone { get; set; } //開發後改不可為空
        //public string? FLoginIPAddress { get; set; } //開發後改不可為空
        //public required DateTime FLoginDatetime { get; set; }
    }
}
