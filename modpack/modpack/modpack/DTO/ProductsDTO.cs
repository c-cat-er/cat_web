using System.ComponentModel;

namespace modpack.DTO
{
    public class ProductsDTO
    {
        [DisplayName("編號")]
        public int ProductId { get; set; }
        [DisplayName("優惠")]
        public int PromotionId { get; set; }
        [DisplayName("優惠")]
        public string PromotionName { get; set; }
        [DisplayName("狀態")]
        public int StatusId { get; set; }
        [DisplayName("狀態")]
        public string StatusName { get; set; }
        [DisplayName("名稱")]
        public string Name { get; set; }
        [DisplayName("類別")]
        public string Category { get; set; }
        [DisplayName("產品圖片")]
        public string ImageFileName { get; set; }
        [DisplayName("原價")]
        public float OriginalPrice { get; set; }
        [DisplayName("特價")]
        public float SalePrice { get; set; }
    }
}
