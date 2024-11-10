namespace Niseko.Server.DTOs
{
    public class MemberShoppingCartCourseDTO
    {
        public int? FShoppingCartID { get; set; }
        public required int FMemberID { get; set; }
        public required string FProductType { get; set; } ///C, A, S, E, T
        public required int FProductID { get; set; } ///storage string(CourseCode) or int(AccommodationRoomID, ShuttleID, EquipmentID, StorageID)
        public decimal FPrice { get; set; }
        public string? FRemark { get; set; }

        public required byte FLocationID { get; set; }
        public required byte FDays { get; set; }
        public required byte FPeopleCount { get; set; }
    }
}
