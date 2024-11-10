using Microsoft.AspNetCore.Mvc.Rendering;
using modpack.Models;
using System.ComponentModel;
using System.Drawing;

namespace modpack.ViewModels
{
    public class COrderViewModel
    {
        public Order order { get; set; }

        public Member member { get; set; }

        public OrderStatus orderStatus { get; set; }

        public PaymentStatus paymentStatus { get; set; }

        public ShippingStatus shippingStatus { get; set; }

        public Shipping shipping { get; set; }

        public Payment payment { get; set; }

        public IEnumerable<SelectListItem> orderstatus { get; set; }

        public IEnumerable<SelectListItem> paymentstatus { get; set; }

        public IEnumerable<SelectListItem> paymentmethod { get; set; }

        public IEnumerable<SelectListItem> shippingstatus { get; set; }

        public IEnumerable<SelectListItem> shippingmethod { get; set; }
    }
}