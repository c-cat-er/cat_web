namespace modpackApi.DTO
{
    public class CartsDTO
    {
        public int cartId { get; set; }

        public int? memberId { get; set; }

        public int? productId { get; set; }

        public int? inspirationId { get; set; }

        public int? customizedId { get; set; }

        public int quantity { get; set; }

        public string? name { get; set; }

        public decimal? price { get; set; }

        public string? ImageFileName { get; set; }
        public string? ImageFile { get;  set; }
    }
}
