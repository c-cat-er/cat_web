namespace Niseko.Server.DTOs
{
    public class MemberShoppingCartHomestayDTO
    {
        public int? FShoppingCartID { get; set; }
        public int FMemberID { get; set; }
        public required string FProductType { get; set; } ///C, A, S, E, T
        public int FProductID { get; set; } ///storage string(CourseCode) or int(AccommodationRoomID, ShuttleID, EquipmentID, StorageID)
        public decimal FPrice { get; set; }
        public string? FRemark { get; set; }

        public byte? FPickupLocationID { get; set; }
        public byte? FDropoffLocationID { get; set; }
        public required string FStartDatetime { get; set; }
        public required string FEndDatetime { get; set; }
    }
}
