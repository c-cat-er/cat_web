using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using modpack.Models;
using modpack.ViewModels;

namespace modpack.Controllers
{
    public class AdminDataAnlysisController : Controller
    {
        private readonly ModPackContext _context;

        public AdminDataAnlysisController(ModPackContext context)
        {
            _context = context;
        }

        // GET: AdminDataAnlysis
        public async Task<IActionResult> DataList()
        {
            var orderList = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .Include(o => o.PromoCode)
                .Include(o => o.ShippingStatus)
                .Include(o => o.Shipping)
                .ToListAsync();
            var viewModelList = new List<CDataAnlysisViewModel>();
            foreach (var order in orderList)
            {
                var uqPrice = order.OrderDetails
                    .Select(od => CalculateUnitPrice(od))
                    .ToList();
                decimal totalPrice = uqPrice.Sum();
                var viewModel = new CDataAnlysisViewModel
                {
                    aOrderID = order.OrderId,
                    //aOrderStatusID = order.OrderStatusId,
                    aOrderStatus = order.OrderStatus.Name,
                    //aMemberID = order.Member.MemberId,
                    aMember = order.Member.Name,
                    //aPromoCodeID = order.PromoCode?.PromoCodeId,
                    aPromoCode = order.PromoCode?.Code,
                    //aShoppingCredit
                    //aShippingID
                    aShippingFee = order.Shipping.DeliveryCost,
                    aCompletionTime = order.CompletionTime,
                    aTotalPrice = totalPrice
                };
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        public async Task<IActionResult> DataDetails(int id)
        {
            var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound();
            var uqPrice = order.OrderDetails
                .Select(od => CalculateUnitPrice(od))
                .ToList();
            decimal totalPrice = uqPrice.Sum();

            var viewModelList = new List<CDataAnlysisViewModel>();
            foreach (var orderDetail in order.OrderDetails)
            {
                var viewModel = new CDataAnlysisViewModel
                {
                    aOrderID = order.OrderId,
                    aProductID = order.OrderDetails.FirstOrDefault()?.ProductId,
                    aProduct = order.OrderDetails.FirstOrDefault(x => x.Product != null)?.Product.Name,
                    aUnitPrice = order.OrderDetails.FirstOrDefault()?.UnitPrice ?? 0,
                    aQuantity = order.OrderDetails.FirstOrDefault()?.Quantity ?? 0,
                    aUQPrice = (orderDetail?.UnitPrice ?? 0) * (orderDetail?.Quantity ?? 0)
                };
                viewModelList.Add(viewModel);
            }
            return View(viewModelList);
        }

        private static decimal CalculateUnitPrice(OrderDetail orderDetail)
        {//k-p, 不會存取執行個體資料，可以改為靜態.

            if (orderDetail.ProductId != null)
                return orderDetail.Quantity * orderDetail.UnitPrice;
            else if (orderDetail.InspirationId != null)
                return orderDetail.Quantity * orderDetail.UnitPrice;
            else if (orderDetail.CustomizedId != null)
                return orderDetail.Quantity * orderDetail.UnitPrice;
            return 0;
        }

        public async Task<IActionResult> ChartjsBarTwo()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();

            var monthlyChartDataTwo = orders
                .SelectMany(o => o.OrderDetails)
                .Where(od => od.Order != null && od.Product != null && od.Order.CompletionTime != null && od.ProductId != null)
                .GroupBy(od => new {
                    ///可用推斷的成員名稱.
                    ///Year = od.Order.CompletionTime.GetValueOrDefault().Year,
                    ///Month = od.Order.CompletionTime.GetValueOrDefault().Month,
                    ///ProductId = od.Product.ProductId
                    od.Order.CompletionTime.GetValueOrDefault().Year,
                    od.Order.CompletionTime.GetValueOrDefault().Month,
                    od.Product.ProductId })
                .Select(g => new CDataAnlysisViewModel
                {
                    aYearMonth = $"{g.Key.Year}-{g.Key.Month}",
                    //aProductID = g.Key.ProductId,
                    //aTotalPrice = g.Sum(od => od.Quantity * od.UnitPrice),
                    //aQuantity = g.Sum(od => od.Quantity),
                    //aUnitPrice = g.Average(od => od.UnitPrice),
                    //aUQPrice = g.Sum(od => od.Quantity * od.UnitPrice)
                })
                .ToList();
            return View("ChartjsBarTwo", monthlyChartDataTwo);
        }

        public async Task<IActionResult> ChartjsBarJsonTwo([FromQuery] string selectedYearMonth)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Inspiration)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Customized)
                .ToListAsync();

            var yearMonthParts = selectedYearMonth.Split('-');
            if (yearMonthParts.Length != 2 || !int.TryParse(yearMonthParts[0], out int year) || !int.TryParse(yearMonthParts[1], out int month))
                return BadRequest("Invalid year-month format. Please provide the year and month separated by a hyphen (e.g., '2024-02').");
            var filteredOrders = orders.Where(o => o.CompletionTime.GetValueOrDefault().Year == year && o.CompletionTime.GetValueOrDefault().Month == month);
            var allOrderDetails = filteredOrders
                .SelectMany(o => o.OrderDetails)
                .Where(od => (od.ProductId.HasValue && od.ProductId != null && od.ProductId != 0) ||
                             (od.InspirationId.HasValue && od.InspirationId != null && od.InspirationId != 0) ||
                             (od.CustomizedId.HasValue && od.CustomizedId != null && od.CustomizedId != 0))
                .ToList();

            var monthlyChartDataTwo = allOrderDetails
                .GroupBy(od => new {
                    od.Order.CompletionTime.GetValueOrDefault().Year,
                    od.Order.CompletionTime.GetValueOrDefault().Month,
                    ProductCategory = od.ProductId != null ? "Product" : od.InspirationId != null ? "Inspiration" : "Customized"
                })
                .Select(g => new CDataAnlysisViewModel
                {
                    aYearMonth = $"{g.Key.Year}-{g.Key.Month}",
                    aProductCategory = g.Key.ProductCategory,
                    aTotalPrice = g.Sum(od => od.Quantity * od.UnitPrice),
                    aProductID = g.Key.ProductCategory == "Product" ? g.First().ProductId : null,
                    aInspirationID = g.Key.ProductCategory == "Inspiration" ? g.First().InspirationId : null,
                    aCustomizedID = g.Key.ProductCategory == "Customized" ? g.First().CustomizedId : null
                })
                .ToList();
            return Json(monthlyChartDataTwo);
        }

        public async Task<IActionResult> ChartjsLine()
        {
            var years = await _context.Orders
                .Where(o => o.CompletionTime != null)
                .Select(o => o.CompletionTime.Value.Year)
                .Distinct()
                .OrderBy(year => year)
                .ToListAsync();
            ViewBag.Years = years;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ChartjsLineJson(int year)
        {
            var monthlySales = await _context.Orders
                .Where(o => o.CompletionTime != null && o.CompletionTime.Value.Year == year)
                .SelectMany(o => o.OrderDetails)
                .GroupBy(od => new {
                    od.Order.CompletionTime.Value.Year,
                    od.Order.CompletionTime.Value.Month
                })
                .Select(g => new CDataAnlysisViewModel
                {
                    aYear = g.Key.Year,
                    aMonth = g.Key.Month,
                    aTotalPrice = g.Sum(od => od.UnitPrice * od.Quantity)
                })
                .ToListAsync();
            return Json(monthlySales);
        }

        // GET: AdminDataAnlysis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", order.MemberId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "OrderStatusId", "Name", order.OrderStatusId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "Description", order.PaymentId);
            ViewData["PaymentStatusId"] = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "Name", order.PaymentStatusId);
            ViewData["PromoCodeId"] = new SelectList(_context.PromoCodes, "PromoCodeId", "Code", order.PromoCodeId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "ShippingId", "Description", order.ShippingId);
            ViewData["ShippingStatusId"] = new SelectList(_context.ShippingStatuses, "ShippingStatusId", "Name", order.ShippingStatusId);
            return View(order);
        }

        // POST: AdminDataAnlysis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,MemberId,PaymentStatusId,PaymentId,ShippingStatusId,ShippingId,OrderStatusId,PromoCodeId,RecipientName,RecipientAddress,BillRecipientName,BillRecipientAddress,CreationTime,CompletionTime")] Order order)
        {
            if (id != order.OrderId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", order.MemberId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "OrderStatusId", "Name", order.OrderStatusId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "Description", order.PaymentId);
            ViewData["PaymentStatusId"] = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "Name", order.PaymentStatusId);
            ViewData["PromoCodeId"] = new SelectList(_context.PromoCodes, "PromoCodeId", "Code", order.PromoCodeId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "ShippingId", "Description", order.ShippingId);
            ViewData["ShippingStatusId"] = new SelectList(_context.ShippingStatuses, "ShippingStatusId", "Name", order.ShippingStatusId);
            return View(order);
        }

        // GET: AdminDataAnlysis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var order = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderStatus)
                .Include(o => o.Payment)
                .Include(o => o.PaymentStatus)
                .Include(o => o.PromoCode)
                .Include(o => o.Shipping)
                .Include(o => o.ShippingStatus)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null) return NotFound();
            return View(order);
        }

        // POST: AdminDataAnlysis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null) _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
