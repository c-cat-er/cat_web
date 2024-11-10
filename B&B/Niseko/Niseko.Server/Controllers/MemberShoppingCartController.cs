using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Niseko.Server.DTOs;
using Niseko.Server.Models;
using System.Globalization;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberShoppingCartController : ControllerBase
    {
        private readonly NisekoContext _context;

        public MemberShoppingCartController(NisekoContext context)
        {
            _context = context;
        }

        [HttpGet("homestay/shoppingCartId/{shoppingCartId}")]
        public async Task<ActionResult<MemberShoppingCartHomestayDTO>> GetTMemberShoppingCartShoppingCartID(int shoppingCartId)
        {
            //k-p, 非外鍵關聯的多表 include list
            var cartItem = await _context.TMemberShoppingCarts
                .Where(c => c.FShoppingCartID == shoppingCartId)
                .Include(c => c.TMemberShoppingCartHomestays)
                .Select(c => new MemberShoppingCartHomestayDTO
                {
                    FShoppingCartID = c.FShoppingCartID,
                    FMemberID = c.FMemberID,
                    FProductType = c.FProductType,
                    FProductID = c.FProductID,
                    FPrice = c.FPrice,
                    FRemark = c.FRemark,
                    FStartDatetime = c.TMemberShoppingCartHomestays.FirstOrDefault().FStartDatetime.ToString("yyyy-MM-dd HH:mm:ss"),
                    FEndDatetime = c.TMemberShoppingCartHomestays.FirstOrDefault().FEndDatetime.ToString("yyyy-MM-dd HH:mm:ss"),
                    FPickupLocationID = c.TMemberShoppingCartHomestays.FirstOrDefault().FPickupLocationID,
                    FDropoffLocationID = c.TMemberShoppingCartHomestays.FirstOrDefault().FDropoffLocationID,
                })
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                //return NotFound();
                return Ok(new List<MemberShoppingCartHomestayDTO>()); //返回空列表
            }

            return Ok(cartItem);
        }

        //[HttpGet("homestay/memberId/{memberId}")]
        //public async Task<ActionResult<IEnumerable<MemberShoppingCartBothDTO>>> GetTMemberShoppingCartMemberID(int memberId)
        //{//只傳回購物車資料不包含產品信息
        //    var cartItems = await _context.TMemberShoppingCarts
        //        .Where(c => c.FMemberID == memberId)
        //        .Include(c => c.TMemberShoppingCartCourses)
        //        .Include(c => c.TMemberShoppingCartHomestays)
        //        .Select(c => new MemberShoppingCartBothDTO
        //        {
        //            FShoppingCartID = c.FShoppingCartID,
        //            FMemberID = c.FMemberID,
        //            FProductType = c.FProductType,
        //            FProductID = c.FProductID,
        //            FPrice = c.FPrice,
        //            FRemark = c.FRemark,
        //            //处理民宿数据，如果有多条记录，此仅示例加载第一条
        //            FStartDatetime = c.TMemberShoppingCartHomestays.FirstOrDefault().FStartDatetime.ToString("yyyy-MM-dd HH:mm:ss"),
        //            FEndDatetime = c.TMemberShoppingCartHomestays.FirstOrDefault().FEndDatetime.ToString("yyyy-MM-dd HH:mm:ss"),
        //            FPickupLocationID = c.TMemberShoppingCartHomestays.FirstOrDefault().FPickupLocationID,
        //            FDropoffLocationID = c.TMemberShoppingCartHomestays.FirstOrDefault().FDropoffLocationID,
        //            //处理课程数据，如果有多条记录，此仅示例加载第一条
        //            FLocationID = c.TMemberShoppingCartCourses.FirstOrDefault().FLocationID,
        //            FDays = c.TMemberShoppingCartCourses.FirstOrDefault().FDays,
        //            FPeopleCount = c.TMemberShoppingCartCourses.FirstOrDefault().FPeopleCount,
        //        })
        //        .ToListAsync();

        //    if (cartItems == null || cartItems.Count == 0)
        //    {
        //        //return NotFound();
        //        return Ok(new List<MemberShoppingCartBothDTO>());
        //    }

        //    return Ok(cartItems);
        //}

        [HttpGet("homestay/memberId/{memberId}")]
        public async Task<ActionResult<IEnumerable<MemberShoppingCartBothDTO>>> GetTMemberShoppingCartMemberID(int memberId)
        {//傳回購物車資料和產品信息
            var cartItems = await _context.TMemberShoppingCarts
                .Where(c => c.FMemberID == memberId)
                .Include(c => c.TMemberShoppingCartCourses)
                .Include(c => c.TMemberShoppingCartHomestays)
                .Select(c => new MemberShoppingCartBothDTO
                {
                    FShoppingCartID = c.FShoppingCartID,
                    FMemberID = c.FMemberID,
                    FProductType = c.FProductType,
                    FProductID = c.FProductID,
                    FPrice = c.FPrice,
                    FRemark = c.FRemark,
                    // 民宿数据
                    FStartDatetime = c.TMemberShoppingCartHomestays.FirstOrDefault().FStartDatetime.ToString("yyyy-MM-dd HH:mm:ss"),
                    FEndDatetime = c.TMemberShoppingCartHomestays.FirstOrDefault().FEndDatetime.ToString("yyyy-MM-dd HH:mm:ss"),
                    FPickupLocationID = c.TMemberShoppingCartHomestays.FirstOrDefault().FPickupLocationID,
                    FDropoffLocationID = c.TMemberShoppingCartHomestays.FirstOrDefault().FDropoffLocationID,
                    // 课程数据
                    FLocationID = c.TMemberShoppingCartCourses.FirstOrDefault().FLocationID,
                    FDays = c.TMemberShoppingCartCourses.FirstOrDefault().FDays,
                    FPeopleCount = c.TMemberShoppingCartCourses.FirstOrDefault().FPeopleCount,
                })
                .ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                return Ok(new List<MemberShoppingCartBothDTO>());
            }

            // 获取每个民宿类产品的详细房间信息
            foreach (var item in cartItems)
            {
                if (item.FProductType == "H")
                {
                    // 根据 FProductID (roomID) 查询房间详情
                    var room = await _context.TProductHomestayRooms
                        .Include(r => r.TProductHomestayPrices)
                        .Include(r => r.FHomestay)
                            .ThenInclude(h => h.FAddress)
                        .Where(r => r.FHomestayRoomID == item.FProductID) // 使用 FProductID 作為 roomID
                        .Select(r => new
                        {
                            FHomestayName = r.FHomestay.FHomestayName,
                            FRoomCode = r.FRoomCode,
                            FRoomImages = _context.TProductImages
                                .Where(img => img.FProductType == "R" && img.FProductID == r.FHomestayRoomID)
                                .Select(img => img.FImage)
                                .ToList()
                        })
                        .FirstOrDefaultAsync();

                    if (room != null)
                    {
                        item.FHomestayName = room.FHomestayName;
                        item.FRoomCode = room.FRoomCode;
                        item.FRoomImages = room.FRoomImages;
                    }
                }
            }

            return Ok(cartItems);
        }

        // POST: api/MemberShoppingCart/homestay
        [HttpPost("homestay")]
        public async Task<ActionResult<MemberShoppingCartHomestayDTO>> PostTMemberShoppingCartHomestay
            ([FromBody] MemberShoppingCartHomestayDTO dmsh)
        {
            try
            {
                var ms = new TMemberShoppingCart
                {
                    FMemberID = dmsh.FMemberID,
                    FProductType = dmsh.FProductType,
                    FProductID = dmsh.FProductID,
                    FPrice = dmsh.FPrice,
                    FRemark = dmsh.FRemark,
                };

                _context.TMemberShoppingCarts.Add(ms);
                await _context.SaveChangesAsync();

                var msh = new TMemberShoppingCartHomestay
                {
                    FShoppingCartID = ms.FShoppingCartID,
                    FPickupLocationID = dmsh.FPickupLocationID,
                    FDropoffLocationID = dmsh.FDropoffLocationID,
                    FStartDatetime = TurnToDatetime(dmsh.FStartDatetime), //
                    FEndDatetime = TurnToDatetime(dmsh.FEndDatetime),
                };

                _context.TMemberShoppingCartHomestays.Add(msh);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTMemberShoppingCartShoppingCartID", new { shoppingCartId = ms.FShoppingCartID }, msh);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST: api/MemberShoppingCart/course
        [HttpPost("course")]
        public async Task<ActionResult<MemberShoppingCartCourseDTO>> PostTMemberShoppingCartCourse([FromBody] MemberShoppingCartCourseDTO dmsc)
        {
            var ms = new TMemberShoppingCart
            {
                FMemberID = dmsc.FMemberID,
                FProductType = dmsc.FProductType,
                FProductID = dmsc.FProductID,
                FPrice = dmsc.FPrice,
                FRemark = dmsc.FRemark,
            };

            _context.TMemberShoppingCarts.Add(ms);
            await _context.SaveChangesAsync();

            var msc = new TMemberShoppingCartCourse
            {
                FShoppingCartID = ms.FShoppingCartID,
                //FLocation = dmsc.FLocationID,
                FDays = dmsc.FDays,
                FPeopleCount = dmsc.FPeopleCount,
            };

            _context.TMemberShoppingCartCourses.Add(msc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTMemberShoppingCartShoppingCartID", new { shoppingCartId = ms.FShoppingCartID }, msc);
        }

        public static DateTime TurnToDatetime(string d)
        {//string -> datetime

            //k-p,use 集合接收多種字串
            string[] formats = ["yyyy-MM-dd HH:mm:ss", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.fffZ"];
            if (DateTime.TryParseExact(d, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDatetime))
            {
                return parsedDatetime;
            }
            else
            {
                throw new FormatException($"日期时间格式无效: {d}");
            }
        }

        public static string ProcessDatetime(string d)
        {//string -> string，暫不用
            //解析 UTC 时间字符串为 DateTime 对象
            if (DateTime.TryParseExact(d, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime parsedDatetime))
            {
                //将 DateTime 对象转换为本地时间
                DateTime localDatetime = parsedDatetime.ToLocalTime();

                //将本地时间格式化为 yyyymmddHHmm 格式的字符串
                return localDatetime.ToString("yyyyMMddHHmm");
            }
            else
            {
                throw new FormatException($"日期时间格式无效: {d}");
            }
        }

        [HttpDelete("homestay/shoppingCartId/{shoppingCartId}")]
        public async Task<IActionResult> DeleteTMemberShoppingCartHomestay(int shoppingCartId)
        {
            try
            {
                // 查找購物車項目
                var cartItem = await _context.TMemberShoppingCarts
                    .Include(c => c.TMemberShoppingCartHomestays)
                    .FirstOrDefaultAsync(c => c.FShoppingCartID == shoppingCartId);

                if (cartItem == null)
                {
                    return NotFound($"購物車項目未找到，ID: {shoppingCartId}");
                }

                // 刪除民宿相關記錄
                var homestayItems = cartItem.TMemberShoppingCartHomestays.ToList();
                if (homestayItems.Count != 0)
                {
                    _context.TMemberShoppingCartHomestays.RemoveRange(homestayItems);
                }

                // 刪除購物車項目
                _context.TMemberShoppingCarts.Remove(cartItem);
                await _context.SaveChangesAsync();

                return NoContent(); //返回 204 No Content 表示刪除成功
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest($"刪除時發生錯誤: {ex.Message}");
            }
        }
    }
}
