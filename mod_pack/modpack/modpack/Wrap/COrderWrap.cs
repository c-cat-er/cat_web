using System.ComponentModel;
using modpack.Models;

namespace modpack.Wrap
{

    public class COrderWrap
    {
        private Order _order;

        public Order order { get { return _order; } set { _order = value; } }

        public COrderWrap()
        {
            _order = new Order();
        }

        [DisplayName("訂單編號")]
        public int OrderId { get { return _order.OrderId; } set { _order.OrderId = value; } }
        [DisplayName("會員編號")]
        public int MemberId { get { return _order.MemberId; } set { _order.MemberId = value; } }

        [DisplayName("優惠碼編號")]
        public int? PromoCodeId { get { return _order.PromoCodeId; } set { _order.PromoCodeId = value; } }
    }

}
