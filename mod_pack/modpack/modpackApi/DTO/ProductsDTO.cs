namespace modpackApi.DTO
{
    public class ProductsDTO
    {
        public int ProductId { get; set; }
        public int PromotionId { get; set; }
        public int StatusId { get; set; }
        public string PromotionName { get; set; }
        public string StatusName { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageFileName { get; set; }
        public float OriginalPrice { get; set; }
        public float SalePrice { get; set; }
    }
}
