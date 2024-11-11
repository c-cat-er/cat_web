using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Niseko.Server.DTOs;
using Niseko.Server.Models;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductHomestaysController : ControllerBase
    {
        private readonly NisekoContext _context;

        public ProductHomestaysController(NisekoContext context)
        {
            _context = context;
        }

        // GET: api/ProductHomestays
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TProductHomestay>>> GetTProductHomestays()
        //{//不支援分頁功能
        //    var currentMonth = DateTime.Now.Month; //get當前月份

        //    //if use 實體m
        //    //return await _context.TProductHomestays
        //    //    .Include(m => m.FAddress)
        //    //    .Include(m => m.TProductHomestayRooms)
        //    //        .ThenInclude(r => r.TProductHomestayPrices)
        //    //    .ToListAsync();

        //    //if use dto
        //    var h = await _context.TProductHomestays
        //        .Include(h => h.FAddress)
        //        .Include(h => h.TProductHomestayRooms)
        //            .ThenInclude(r => r.TProductHomestayPrices)
        //        .Include(h => h.TProductHomestayRooms)
        //            .ThenInclude(r => r.TProductHomestayImages)
        //        .ToListAsync();

        //    var hd = h.Select(h => new ProductHomestayDTO
        //    {
        //        FHomestayID = h.FHomestayID,
        //        FHomestayCode = h.FHomestayCode,
        //        FHomestayName = h.FHomestayName,
        //        FDescription = h.FDescription,
        //        FAddressName = h.FAddress.FLocationName,
        //        ProductHomestayRoomDTO = h.TProductHomestayRooms.Select(r => {
        //            //k-p, 判斷當前月份是否屬於旺季
        //            bool isPeakSeason = currentMonth == 12 || currentMonth == 1 || currentMonth == 2 || currentMonth == 3;
        //            var selectedPrice = r.TProductHomestayPrices
        //                .FirstOrDefault(p => p.FIsPeakSeason == isPeakSeason);

        //            return new ProductHomestayRoomDTO
        //            {
        //                FHomestayRoomID = r.FHomestayRoomID,
        //                FRoomCode = r.FRoomCode,
        //                FToiletCount = r.FToiletCount,
        //                FQueenBedCount = r.FQueenBedCount,
        //                FSingleBedCount = r.FSingleBedCount,
        //                FMaxCapacity = r.FMaxCapacity,
        //                FImage = r.TProductHomestayImages.FirstOrDefault()?.FImage ?? "default.jpg",
        //                FHomestayPriceID = selectedPrice?.FHomestayPriceID ?? 0, //若無選擇結果則設為0
        //                FIsPeakSeason = selectedPrice?.FIsPeakSeason ?? false,
        //                FStayDays = selectedPrice?.FStayDays ?? 0,
        //                FStayPrice = selectedPrice?.FStayPrice ?? 0m
        //            };
        //        }).ToList()
        //    }).ToList();

        //    return Ok(hd);
        //}

        // GET: api/ProductHomestays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TProductHomestay>>>GetTProductHomestays
            (int page = 1, int perPage = 6)
        {//支援分頁功能
            var currentMonth = DateTime.Now.Month; //get當月
            var skip = (page - 1) * perPage; //計算偏移量

            var h = await _context.TProductHomestays
                .Include(h => h.FAddress)
                .Include(h => h.TProductHomestayRooms)
                    .ThenInclude(r => r.TProductHomestayPrices)
                .Skip(skip) //跳過前面的數據
                .Take(perPage) //只取所需數量
                .ToListAsync();

            var hd = h.Select(h => new ProductHomestayDTO
            {
                FHomestayID = h.FHomestayID,
                FHomestayCode = h.FHomestayCode,
                FHomestayName = h.FHomestayName,
                FDescription = h.FDescription,
                FAddressID = h.FAddressID,
                FAddressName = h.FAddress.FLocationName,
                ProductHomestayRoomDTO = h.TProductHomestayRooms.Select(r =>
                {
                    bool isPeakSeason = currentMonth == 12 || currentMonth == 1 || currentMonth == 2 || currentMonth == 3;
                    var selectedPrice = r.TProductHomestayPrices.FirstOrDefault(p => p.FIsPeakSeason == isPeakSeason);

                    return new ProductHomestayRoomDTO
                    {
                        FHomestayRoomID = r.FHomestayRoomID,
                        FRoomCode = r.FRoomCode,
                        FToiletCount = r.FToiletCount,
                        FQueenBedCount = r.FQueenBedCount,
                        FSingleBedCount = r.FSingleBedCount,
                        FMaxCapacity = r.FMaxCapacity,
                        FHomestayPriceID = selectedPrice?.FHomestayPriceID ?? 0,
                        FIsPeakSeason = selectedPrice?.FIsPeakSeason ?? false,
                        FStayDays = selectedPrice?.FStayDays ?? 0,
                        FStayPrice = selectedPrice?.FStayPrice ?? 0m,
                        // 查找房間圖片
                        FRoomImages = _context.TProductImages
                            .Where(img => img.FProductType == "R" && img.FProductID == r.FHomestayRoomID)
                            .Select(img => img.FImage)
                            .ToList()
                    };
                }).ToList(),

                // 查找民宿圖片
                FHomestayImages = _context.TProductImages
                    .Where(img => img.FProductType == "H" && img.FProductID == h.FHomestayID)
                    .Select(img => img.FImage)
                    .ToList()
            }).ToList();

            // 返回數據和總數
            var totalRooms = await _context.TProductHomestays.CountAsync();
            return Ok(new { TotalCount = totalRooms, Values = hd });
        }

        // PUT: api/ProductHomestays/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTProductHomestay(int id, TProductHomestay tProductHomestay)
        {
            if (id != tProductHomestay.FHomestayID)
            {
                return BadRequest();
            }

            _context.Entry(tProductHomestay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TProductHomestayExists(id))
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

        // POST: api/ProductHomestays
        [HttpPost]
        public async Task<ActionResult<TProductHomestay>> PostTProductHomestay(TProductHomestay tProductHomestay)
        {
            _context.TProductHomestays.Add(tProductHomestay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTProductHomestay", new { id = tProductHomestay.FHomestayID }, tProductHomestay);
        }

        // DELETE: api/ProductHomestays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTProductHomestay(int id)
        {
            var tProductHomestay = await _context.TProductHomestays.FindAsync(id);
            if (tProductHomestay == null)
            {
                return NotFound();
            }

            _context.TProductHomestays.Remove(tProductHomestay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TProductHomestayExists(int id)
        {
            return _context.TProductHomestays.Any(e => e.FHomestayID == id);
        }
    }
}
