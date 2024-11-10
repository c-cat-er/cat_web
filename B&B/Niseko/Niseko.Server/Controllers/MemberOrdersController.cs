using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Niseko.Server.DTOs;
using Niseko.Server.Models;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberOrdersController : ControllerBase
    {
        private readonly NisekoContext _context;

        public MemberOrdersController(NisekoContext context)
        {
            _context = context;
        }

        //[HttpPost("ProcessEcpayPayment")]
        //public IActionResult ProcessEcpayPayment([FromBody] MemberOrderHomestayDTO paymentRequest)
        //{
        //    try
        //    {
        //        // 使用 DTO 中的数据库相关字段保存订单信息到数据库
        //        var FOrderCode = SaveOrderToDatabase(paymentRequest);

        //        //1. 准备支付参数
        //        var paymentData = new Dictionary<string, string>
        //        {
        //            {"MerchantID", paymentRequest.MerchantID},
        //            {"MerchantTradeNo", FOrderCode}, //使用FOrderCode賦值
        //            {"MerchantTradeDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},
        //            {"PaymentType", "aio"},
        //            {"TotalAmount", paymentRequest.FFinalAmount.ToString()},
        //            {"TradeDesc", paymentRequest.FRemarks}, // 使用 FRemarks 作为 TradeDesc
        //            {"ItemName", paymentRequest.FRemarks}, // 使用 FRemarks 作为 ItemName
        //            {"ReturnURL", "你的回调URL"},
        //            {"ChoosePayment", "ALL"},
        //            {"EncryptType", "1"},
        //        };

        //        // HashKey 和 HashIV 是从绿界取得的密钥，确保妥善保存
        //        var hashKey = "你的HashKey";
        //        var hashIV = "你的HashIV";

        //        //2. 生成 CheckMacValue
        //        var checkMacValue = GenerateCheckMacValue(paymentData, hashKey, hashIV);
        //        paymentData.Add("CheckMacValue", checkMacValue);

        //        //3. 构建支付请求链接
        //        var paymentUrl = "https://payment.ecpay.com.tw/Cashier/AioCheckOut/V5";
        //        var queryString = BuildQueryString(paymentData);
        //        var fullPaymentUrl = $"{paymentUrl}?{queryString}";

        //        return Ok(new { paymentUrl = fullPaymentUrl });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}

        //public string SaveOrderToDatabase(MemberOrderHomestayDTO orderDto)
        //{
        //    // Step 1: Save Order (TOrder)
        //    var order = new TOrder
        //    {
        //        FMemberID = orderDto.FMemberID,
        //        FOrderCode = GenerateUniqueTradeNo(),
        //        FCreationDatetime = DateTime.Now,
        //        FInitialAmount = orderDto.FInitialAmount,
        //        FFinalAmount = orderDto.FFinalAmount,
        //        FIsCheckedIn = false // 默认值
        //    };
        //    _context.TOrders.Add(order);
        //    _context.SaveChanges();

        //    // Step 2: Save Order Detail (TOrderDetail)
        //    var orderDetail = new TOrderDetail
        //    {
        //        FOrderID = order.FOrderID,
        //        FProductType = orderDto.FProductType,
        //        FProductID = orderDto.FProductID,
        //        FOrderDetailAmount = orderDto.FOrderDetailAmount,
        //        FRemarks = orderDto.FRemarks
        //    };
        //    _context.TOrderDetails.Add(orderDetail);
        //    _context.SaveChanges();

        //    // Step 3: Save Order Detail Homestay (TOrderDetailHomestay)
        //    if (orderDto.FProductType == "H") // Assuming "H" is for Homestay
        //    {
        //        var orderDetailHomestay = new TOrderDetailHomestay
        //        {
        //            FOrderDetailID = orderDetail.FOrderDetailID,
        //            FPickupLocationID = orderDto.FPickupLocationID,
        //            FDropoffLocationID = orderDto.FDropoffLocationID,
        //            FStartDatetime = DateTime.Parse(orderDto.FStartDatetime),
        //            FEndDatetime = DateTime.Parse(orderDto.FEndDatetime)
        //        };
        //        _context.TOrderDetailHomestays.Add(orderDetailHomestay);
        //        _context.SaveChanges();
        //    }

        //    // Step 4: Save Payment Record (TOrderPaymentRecord)
        //    var paymentRecord = new TOrderPaymentRecord
        //    {
        //        FOrderID = order.FOrderID,
        //        FPaymentMethodID = orderDto.FPaymentMethodID,
        //        FPaymentDatetime = DateTime.Now,
        //        FPaymentAmount = orderDto.FPaymentAmount,
        //        FPaymentStatusID = 1 // Assuming 1 is the default status for "Pending"
        //    };
        //    _context.TOrderPaymentRecords.Add(paymentRecord);
        //    _context.SaveChanges();

        //    // Return the unique order code for payment processing
        //    return order.FOrderCode;
        //}

        //public string GenerateUniqueTradeNo()
        //{//生成唯一訂單編號 (O+任意9個字)
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    var random = new Random();
        //    var result = new char[9];

        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        result[i] = chars[random.Next(chars.Length)];
        //    }

        //    return "O" + new string(result);
        //}

        //private string GenerateCheckMacValue(Dictionary<string, string> parameters, string hashKey, string hashIV)
        //{//生成 CheckMacValue 的方法
        //    var sortedParams = parameters
        //        .OrderBy(p => p.Key)
        //        .Select(p => $"{p.Key}={p.Value}")
        //        .ToList();

        //    var rawData = $"HashKey={hashKey}&{string.Join("&", sortedParams)}&HashIV={hashIV}";

        //    // 使用 SHA256 进行哈希计算
        //    var encodedData = HttpUtility.UrlEncode(rawData).ToLower();
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(encodedData));
        //        var checkMacValue = BitConverter.ToString(hash).Replace("-", string.Empty).ToUpper();
        //        return checkMacValue;
        //    }
        //}

        //private string BuildQueryString(Dictionary<string, string> parameters)
        //{//构建查询字符串的方法
        //    return string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
        //}





        //GET: api/MemberOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TOrder>>> GetTOrders()
        {
            return await _context.TOrders.ToListAsync();
        }

        //GET: api/MemberOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TOrder>> GetTOrder(int id)
        {
            var tOrder = await _context.TOrders.FindAsync(id);

            if (tOrder == null)
            {
                return NotFound();
            }

            return tOrder;
        }

        //PUT: api/MemberOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTOrder(int id, TOrder tOrder)
        {
            if (id != tOrder.FOrderID)
            {
                return BadRequest();
            }

            _context.Entry(tOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/MemberOrders
        [HttpPost]
        public async Task<ActionResult<TOrder>> PostTOrder(TOrder tOrder)
        {
            _context.TOrders.Add(tOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTOrder", new { id = tOrder.FOrderID }, tOrder);
        }

        //DELETE: api/MemberOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTOrder(int id)
        {
            var tOrder = await _context.TOrders.FindAsync(id);
            if (tOrder == null)
            {
                return NotFound();
            }

            _context.TOrders.Remove(tOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TOrderExists(int id)
        {
            return _context.TOrders.Any(e => e.FOrderID == id);
        }
    }
}
