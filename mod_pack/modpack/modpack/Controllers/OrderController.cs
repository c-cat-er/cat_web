using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modpack.Models;
using modpack.ViewModels;
using System.Security.Cryptography;

namespace modpack.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult List()
        {
            ModPackContext db = new ModPackContext();
            List<COrderViewModel> COrderViewModelList = new List<COrderViewModel>();
            var COrderList = (from ord in db.Orders
                              join mem in db.Members on ord.MemberId equals mem.MemberId
                              join ordSta in db.OrderStatuses on ord.OrderStatusId equals ordSta.OrderStatusId
                              join paySta in db.PaymentStatuses on ord.PaymentStatusId equals paySta.PaymentStatusId
                              join shipSta in db.ShippingStatuses on ord.ShippingStatusId equals shipSta.ShippingStatusId
                              select new
                              {
                                  order = ord,
                                  member = mem,
                                  orderStatus = ordSta,
                                  paymentStatus = paySta,
                                  shippingStatus = shipSta,
                              });

            foreach (var items in COrderList)
            {
                COrderViewModel objovm = new COrderViewModel()
                {
                    order = items.order,
                    member = items.member,
                    orderStatus = items.orderStatus,
                    paymentStatus = items.paymentStatus,
                    shippingStatus = items.shippingStatus,
                };
                COrderViewModelList.Add(objovm);
            }
            return View(COrderViewModelList);
        }

        //public IActionResult Edit(int? id)
        //{
        //    ModPackContext db = new ModPackContext();
        //    //Order order = db.Orders.Include(o => o.PaymentStatus).Include(o => o.ShippingStatus).FirstOrDefault(p => p.OrderId == id);
        //    Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
        //    if (order == null)
        //        return RedirectToAction("List");
        //    COrderViewModel vm = new COrderViewModel();
        //    vm.order = order;

        //    // 加入 PaymentStatus 資料表的下拉選單
        //    List<SelectListItem> paymentStatuses = db.PaymentStatuses.OrderBy(p => p.Name).Select(p => new SelectListItem()
        //    {
        //        Text = p.Name,
        //        Value = p.PaymentStatusId.ToString()
        //    }).ToList();
        //    vm.paymentstatus = paymentStatuses;

        //    // 加入 ShippingStatus 資料表的下拉選單
        //    List<SelectListItem> shippingStatuses = db.ShippingStatuses.OrderBy(p => p.Name).Select(p => new SelectListItem()
        //    {
        //        Text = p.Name,
        //        Value = p.ShippingStatusId.ToString()
        //    }).ToList();
        //    vm.shippingstatus = shippingStatuses;

        //    // 加入 OrderStatus 資料表的下拉選單
        //    List<SelectListItem> orderStatuses = db.OrderStatuses.OrderBy(p => p.Name).Select(p => new SelectListItem()
        //    {
        //        Text = p.Name,
        //        Value = p.OrderStatusId.ToString()
        //    }).ToList();
        //    vm.orderstatus = orderStatuses;
        //    return View(vm);
        //}

        //[HttpPost]
        //public IActionResult Edit(COrderViewModel vm)
        //{
        //    ModPackContext db = new ModPackContext();
        //    Order order = db.Orders.FirstOrDefault(p => p.OrderId == vm.order.OrderId);
        //    if (order == null)
        //        return RedirectToAction("List");
        //    order.OrderStatusId = vm.order.OrderStatusId;
        //    order.PaymentStatusId = vm.order.PaymentStatusId;
        //    order.ShippingStatusId = vm.order.ShippingStatusId;
        //    db.SaveChanges();
        //    return RedirectToAction("List");
        //}

        public IActionResult OrderDetail(int? id)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
            var OrderDetails = from ord in db.Orders
                               join mem in db.Members on ord.MemberId equals mem.MemberId
                               join ordSta in db.OrderStatuses on ord.OrderStatusId equals ordSta.OrderStatusId
                               join paySta in db.PaymentStatuses on ord.PaymentStatusId equals paySta.PaymentStatusId
                               join shipSta in db.ShippingStatuses on ord.ShippingStatusId equals shipSta.ShippingStatusId
                               select new COrderViewModel
                               {
                                   order = ord,
                                   member = mem,
                                   orderStatus = ordSta,
                                   paymentStatus = paySta,
                                   shippingStatus = shipSta,
                               };
            return View(OrderDetails.FirstOrDefault());
        }

        public IActionResult OrderEdit(int? id)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
            if (order == null)
                return RedirectToAction("List");
            COrderViewModel vm = new COrderViewModel();
            vm.order = order;

            List<SelectListItem> orderStatuses = db.OrderStatuses.OrderBy(p => p.Name).Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.OrderStatusId.ToString()
            }).ToList();

            vm.orderstatus = orderStatuses;
            return View(vm);
        }
        [HttpPost]
        public IActionResult OrderEdit(COrderViewModel vm)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == vm.order.OrderId);
            if (order == null)
                return RedirectToAction("List");
            order.OrderStatusId = vm.order.OrderStatusId;
            db.SaveChanges();
            return RedirectToAction("OrderDetail");
        }

        public IActionResult OrderPaymentDetail(int? id)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
            var OrderDetails = from ord in db.Orders
                               join mem in db.Members on ord.MemberId equals mem.MemberId
                               join ordSta in db.OrderStatuses on ord.OrderStatusId equals ordSta.OrderStatusId
                               join paySta in db.PaymentStatuses on ord.PaymentStatusId equals paySta.PaymentStatusId
                               join shipSta in db.ShippingStatuses on ord.ShippingStatusId equals shipSta.ShippingStatusId
                               join ship in db.Shippings on ord.ShippingId equals ship.ShippingId
                               join pay in db.Payments on ord.PaymentId equals pay.PaymentId
                               select new COrderViewModel
                               {
                                   order = ord,
                                   member = mem,
                                   orderStatus = ordSta,
                                   paymentStatus = paySta,
                                   shippingStatus = shipSta,
                                   shipping = ship,
                                   payment = pay,
                               };
            return View(OrderDetails.FirstOrDefault());
        }

        public IActionResult OrderPaymentEdit(int? id)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
            if (order == null)
                return RedirectToAction("List");
            COrderViewModel vm = new COrderViewModel();
            vm.order = order;

            List<SelectListItem> paymentStatus = db.PaymentStatuses.OrderBy(p => p.Name).Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.PaymentStatusId.ToString()
            }).ToList();
            List<SelectListItem> paymentMethod = db.Payments.OrderBy(p => p.Description).Select(p => new SelectListItem()
            {
                Text = p.Description,
                Value = p.PaymentId.ToString()
            }).ToList();

            vm.paymentstatus = paymentStatus;
            vm.paymentmethod = paymentMethod;
            return View(vm);
        }
        [HttpPost]
        public IActionResult OrderPaymentEdit(COrderViewModel vm)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == vm.order.OrderId);
            if (order == null)
                return RedirectToAction("List");
            order.PaymentId = vm.order.PaymentId;
            order.PaymentStatusId = vm.order.PaymentStatusId;
            db.SaveChanges();
            return RedirectToAction("OrderPaymentDetail");
        }

        public IActionResult OrderShippingDetail(int? id)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
            var OrderDetails = from ord in db.Orders
                               join mem in db.Members on ord.MemberId equals mem.MemberId
                               join ordSta in db.OrderStatuses on ord.OrderStatusId equals ordSta.OrderStatusId
                               join paySta in db.PaymentStatuses on ord.PaymentStatusId equals paySta.PaymentStatusId
                               join shipSta in db.ShippingStatuses on ord.ShippingStatusId equals shipSta.ShippingStatusId
                               join ship in db.Shippings on ord.ShippingId equals ship.ShippingId
                               select new COrderViewModel
                               {
                                   order = ord,
                                   member = mem,
                                   orderStatus = ordSta,
                                   paymentStatus = paySta,
                                   shippingStatus = shipSta,
                                   shipping = ship,
                               };
            return View(OrderDetails.FirstOrDefault());
        }

        public IActionResult OrderShippingEdit(int? id)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
            if (order == null)
                return RedirectToAction("List");
            COrderViewModel vm = new COrderViewModel();
            vm.order = order;

            List<SelectListItem> shippingStatuses = db.ShippingStatuses.OrderBy(p => p.Name).Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.ShippingStatusId.ToString()
            }).ToList();

            List<SelectListItem> shippingMethod = db.Shippings.OrderBy(p => p.Description).Select(p => new SelectListItem()
            {
                Text = p.Description,
                Value = p.ShippingId.ToString()
            }).ToList();

            vm.shippingstatus = shippingStatuses;
            vm.shippingmethod = shippingMethod;

            return View(vm);
        }

        [HttpPost]
        public IActionResult OrderShippingEdit(COrderViewModel vm)
        {
            ModPackContext db = new ModPackContext();
            Order order = db.Orders.FirstOrDefault(p => p.OrderId == vm.order.OrderId);
            if (order == null)
                return RedirectToAction("List");
            order.RecipientName = vm.order.RecipientName;
            order.RecipientAddress = vm.order.RecipientAddress;
            order.BillRecipientName = vm.order.BillRecipientName;
            order.BillRecipientAddress = vm.order.BillRecipientAddress;
            order.ShippingStatusId = vm.order.ShippingStatusId;
            order.ShippingId = vm.order.ShippingId;
            db.SaveChanges();
            return RedirectToAction("OrderShippingDetail");
        }
    }
}