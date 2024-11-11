namespace Niseko.Server.DTOs
{
    public class ProductHomestayDTO
    {
        public int FHomestayID { get; set; }
        public required string FHomestayCode { get; set; }
        public required string FHomestayName { get; set; }
        public string? FDescription { get; set; }
        public byte FAddressID { get; set; } //用於前端篩選
        public required string FAddressName { get; set; } //用於前端UI
        public required List<ProductHomestayRoomDTO> ProductHomestayRoomDTO { get; set; }

        public List<string> FHomestayImages { get; set; }  //k-p, List 用來支持多個 FImage，用於比對 FHomestayID
    }
}
