using modpack.Models;

namespace modpack.Wrap
{
    public class COrderDetailWrap
    {
        private OrderDetail _orderDetail;

        public OrderDetail orderDetail { get { return _orderDetail; } set { _orderDetail = value; } }

        public COrderDetailWrap()
        {
            _orderDetail = new OrderDetail();
        }
        public int DetailsId
        {
            get { return _orderDetail.DetailsId; }
            set { _orderDetail.DetailsId = value; }
        }

        public int OrderId
        {
            get { return _orderDetail.OrderId; }
            set { _orderDetail.OrderId = value; }
        }
        public int? ProductId
        {
            get { return _orderDetail.ProductId; }
            set { _orderDetail.ProductId = value; }
        }

        public int? InspirationId
        {
            get { return _orderDetail.InspirationId; }
            set { _orderDetail.InspirationId = value; }
        }

        public int? CustomizedId
        {
            get { return _orderDetail.CustomizedId; }
            set { _orderDetail.CustomizedId = value; }
        }

        public int? Quantity
        {
            get { return _orderDetail.Quantity; }
            set { _orderDetail.Quantity = (int)value; }
        }
    }
}
