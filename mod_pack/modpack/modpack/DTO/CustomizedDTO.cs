using System.ComponentModel;

namespace modpack.DTO
{
    public class CustomizedDTO
    {
        [DisplayName("編號")]
        public int CustomizedId { get; set; }
        [DisplayName("會員")]
        public int MemberId { get; set; }
        [DisplayName("會員")]
        public string MemberName { get; set; }
        [DisplayName("優惠")]
        public int PromotionId { get; set; }
        [DisplayName("優惠")]
        public string PromotionName { get; set; }
        [DisplayName("名稱")]
        public string Name { get; set; }
        [DisplayName("圖片")]
        public string ImageFileName { get; set; }
        [DisplayName("價格")]
        public float SalePrice { get; set; }
    }
}
