using System.ComponentModel;

namespace modpackFront.DTO
{
    public class ProductsPageDTO
    {
        [DisplayName("編號")]
        public int ProductId { get; set; }
        [DisplayName("優惠")]
        public int PromotionId { get; set; }
        [DisplayName("優惠")]
        public string PromotionName { get; set; }
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
        public List<int> Customizedid { get; set; }
        public List<string> CustomizedName { get; set; }
        public List<string> CustomizedImageFileName { get; set; }
        public List<float> CustomizedSalePrice { get; set; }
        [DisplayName("相關產品")]
        public List<int> relatedproductsId { get; set; }
        [DisplayName("相關產品")]
        public List<string> relatedproductsName { get; set; }
        [DisplayName("相關產品")]
        public List<string> relatedproductsImageFileName { get; set; }
        [DisplayName("相關產品")]
        public List<float> relatedproductsSalePrice { get; set; }
        [DisplayName("曾購買產品")]
        public List<int> purchasedproductsId { get; set; }
        [DisplayName("曾購買產品")]
        public List<string> purchasedproductsName { get; set; }
        [DisplayName("曾購買產品")]
        public List<string> purchasedproductsImageFileName { get; set; }
        [DisplayName("曾購買產品")]
        public List<float> purchasedproductsSalePrice { get; set; }
    }
}
