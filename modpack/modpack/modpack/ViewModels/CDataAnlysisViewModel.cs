using modpack.Models;
using System.ComponentModel;

namespace modpack.ViewModels
{
    public class CDataAnlysisViewModel
    {
        public CDataAnlysisViewModel() { }

        [DisplayName("訂單編號")]
        public int aOrderID { get; set; }

        [DisplayName("訂單狀態編號")]
        public int aOrderStatusID { get; set; }

        [DisplayName("訂單狀態")]
        public string aOrderStatus { get; set; }

        [DisplayName("會員編號")]
        public int aMemberID { get; set; }

        [DisplayName("會員名")]
        public string aMember { get; set; }

        [DisplayName("商品編號")]
        public int? aProductID { get; set; }

        [DisplayName("商品名")]
        public string aProduct { get; set; }

        [DisplayName("商品單價")]
        public decimal aUnitPrice { get; set; }

        [DisplayName("商品數")]
        public int aQuantity { get; set; }

        [DisplayName("單件商品總價")]
        public decimal aUQPrice { get; set; }

        [DisplayName("優惠碼編號")]
        public int? aPromoCodeID { get; set; }

        [DisplayName("優惠碼")]
        public string? aPromoCode { get; set; }

        [DisplayName("購物金額(NT$)")]
        public int? aShoppingCredit { get; set; }

        [DisplayName("運費編號")]
        public int? aShippingID { get; set; }

        [DisplayName("運費(NT$)")]
        public decimal? aShippingFee { get; set; }

        [DisplayName("完成付款日")]
        public DateTime? aCompletionTime { get; set; }

        [DisplayName("總支付價(NT$)")]
        public decimal? aTotalPrice { get; set; }

        public string aYearMonth { get; set; }

        // chartjs - bar
        public string aProductCategory { get; set; }
        public int? aInspirationID { get; set; }
        public int? aCustomizedID { get; set; }

        public List<string> aLabels { get; set; }

        public List<decimal> aTotalPrices { get; set; }

        // chartjs - line
        public int aYear { get; set; }
        public int aMonth { get; set; }
    }
}
