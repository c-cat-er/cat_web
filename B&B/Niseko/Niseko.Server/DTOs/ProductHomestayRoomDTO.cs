namespace Niseko.Server.DTOs
{
    public class ProductHomestayRoomDTO
    {
        public int FHomestayRoomID { get; set; }
        public required string FRoomCode { get; set; }
        public byte FBathroomCount { get; set; }
        public byte FToiletCount { get; set; }
        public byte FQueenBedCount { get; set; }
        public byte FSingleBedCount { get; set; }
        public required string FMaxCapacity { get; set; }

        public int FHomestayPriceID { get; set; }
        public bool FIsPeakSeason { get; set; }
        public byte FStayDays { get; set; }
        public decimal FStayPrice { get; set; }

        public List<string> FRoomImages { get; set; } //用於比對 FHomestayRoomID
    }
}
