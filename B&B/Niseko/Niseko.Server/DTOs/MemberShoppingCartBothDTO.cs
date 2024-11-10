namespace Niseko.Server.DTOs
{
    public class MemberShoppingCartBothDTO
    {
        public int? FShoppingCartID { get; set; }
        public int FMemberID { get; set; }
        public required string FProductType { get; set; } ///C, A, S, E, T
        public int FProductID { get; set; } ///storage string(CourseCode) or int(AccommodationRoomID, ShuttleID, EquipmentID, StorageID)
        public decimal FPrice { get; set; } //Both
        public string? FRemark { get; set; }

        public byte? FPickupLocationID { get; set; }
        public byte? FDropoffLocationID { get; set; }
        public string? FStartDatetime { get; set; }
        public string? FEndDatetime { get; set; }

        public byte? FLocationID { get; set; }
        public byte? FDays { get; set; }
        public byte? FPeopleCount { get; set; }

        public bool FIsPeakSeason { get; set; }
        public decimal FStayPrice { get; set; }

        public string? FHomestayName { get; set; } // 新增：民宿名稱
        public string? FRoomCode { get; set; } // 新增：房間代碼
        public List<string>? FRoomImages { get; set; } // 新增：房間圖片
    }
}
