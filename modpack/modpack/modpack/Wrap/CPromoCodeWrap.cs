using System.ComponentModel;
using modpack.Models;

namespace modpack.Wrap
{
    public class CPromoCodeWrap
    {
        private PromoCode _promoCode;

        public PromoCode promoCode { get { return _promoCode; } set { _promoCode = value; } }

        public CPromoCodeWrap()
        {
            _promoCode = new PromoCode();
        }

        public int PromoCodeId { get { return _promoCode.PromoCodeId; } set { _promoCode.PromoCodeId = value; } }
        [DisplayName("優惠碼編號")]
        public string Code { get { return _promoCode.Code; } set { _promoCode.Code = value; } }
        [DisplayName("折扣方式")]
        public string Method { get { return _promoCode.Method; } set { _promoCode.Method = value; } }
        [DisplayName("折扣描述")]
        public string Description { get { return _promoCode.Description; } set { _promoCode.Description = value; } }
        [DisplayName("使用限制")]
        public string Limitation { get { return _promoCode.Limitation; } set { _promoCode.Limitation = value; } }
        [DisplayName("開始日期")]
        public DateTime? StartDate { get { return _promoCode.StartDate; } set { _promoCode.StartDate = value; } }
        [DisplayName("結束日期")]
        public DateTime? EndDate { get { return _promoCode.EndDate; } set { _promoCode.EndDate = value; } }
        [DisplayName("啟用狀態")]
        public bool Status { get { return _promoCode.Status; } set { _promoCode.Status = value; } }


    }
}
