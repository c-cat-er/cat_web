using System.ComponentModel.DataAnnotations;

namespace Niseko.Server.DTOs
{
    public class MemberBothLoginDTO
    {
        ///TMemberWebsiteAccount
        [Required(ErrorMessage = "帳號未填")]
        public string? FAccount { get; set; } ///byte[]
        [Required(ErrorMessage = "密碼未填")]
        public string? FPassword { get; set; } ///byte[]

        ///TMemberLoginRecord
        ///public required byte FLoginTypeID { get; set; }
        public string? FLoginTimezone { get; set; } //開發後改不可為空
        ///public string? FLoginIPAddress { get; set; } //開發後改不可為空
        ///public required DateTime FLoginDatetime { get; set; }

        ///TMemberThirdPartyAccount
        //public string? FThirdPartyUniqueID { get; set; } //開發後改不可為空
    }
}
