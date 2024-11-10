using System.ComponentModel;

namespace modpackApi.DTO
{
    public class ProductsPageDTO
    {
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageFileName { get; set; }
        public float SalePrice { get; set; }
        public List<int> Customizedid { get; set; }
        public List<string> CustomizedName { get; set; }
        public List<float> CustomizedSalePrice { get; set; }
        public List<string> CustomizedImageFileName { get; set; }
        public List<int> relatedproductsId { get; set; }
        public List<string> relatedproductsName { get; set; }
        public List<string> relatedproductsImageFileName { get; set; }
        public List<float> relatedproductsSalePrice { get; set; }
        public List<int> purchasedproductsId { get; set; }
        public List<string> purchasedproductsName { get; set; }
        public List<string> purchasedproductsImageFileName { get; set; }
        public List<float> purchasedproductsSalePrice { get; set; }
    }
}
