namespace Niseko.Server.DTOs
{
    public class MemberThirdPartyRegisterDTO
    {
        ///TMemberLoginRecord
        public required byte FLoginTypeID { get; set; }
        public string? FLoginTimezone { get; set; } //開發後改不可為空
        public string? FLoginIPAddress { get; set; } //開發後改不可為空
        public required DateTime FLoginDatetime { get; set; }

        ///TMemberThirdPartyAccount
        public string? FThirdPartyUniqueID { get; set; } //開發後改不可為空
    }
}
