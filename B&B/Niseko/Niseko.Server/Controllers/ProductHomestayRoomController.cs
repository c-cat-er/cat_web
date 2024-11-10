using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Niseko.Server.DTOs;
using Niseko.Server.Models;

namespace Niseko.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductHomestayRoomController : ControllerBase
    {
        private readonly NisekoContext _context;

        public ProductHomestayRoomController(NisekoContext context)
        {
            _context = context;
        }

        //GET: api/ProductHomestayRoom/5
        [HttpGet("{roomID}")]
        public async Task<ActionResult<ProductHomestayDTO>> GetTProductHomestayRoom(int roomID)
        {
            var room = await _context.TProductHomestayRooms
                .Include(r => r.TProductHomestayPrices)
                .Include(r => r.FHomestay)
                    .ThenInclude(h => h.FAddress)
                .Where(r => r.FHomestayRoomID == roomID)
                .Select(r => new ProductHomestayDTO
                {
                    FHomestayID = r.FHomestay.FHomestayID,
                    FHomestayCode = r.FHomestay.FHomestayCode,
                    FHomestayName = r.FHomestay.FHomestayName,
                    FDescription = r.FHomestay.FDescription,
                    FAddressID = r.FHomestay.FAddressID,
                    FAddressName = r.FHomestay.FAddress.FLocationName,
                    ProductHomestayRoomDTO = new List<ProductHomestayRoomDTO>
                    {
                        new ProductHomestayRoomDTO
                        {
                            FHomestayRoomID = r.FHomestayRoomID,
                            FRoomCode = r.FRoomCode,
                            FBathroomCount = r.FBathroomCount,
                            FToiletCount = r.FToiletCount,
                            FQueenBedCount = r.FQueenBedCount,
                            FSingleBedCount = r.FSingleBedCount,
                            FMaxCapacity = r.FMaxCapacity,
                            FHomestayPriceID = r.TProductHomestayPrices.FirstOrDefault().FHomestayPriceID,
                            FIsPeakSeason = r.TProductHomestayPrices.FirstOrDefault().FIsPeakSeason,
                            FStayDays = r.TProductHomestayPrices.FirstOrDefault().FStayDays,
                            FStayPrice = r.TProductHomestayPrices.FirstOrDefault().FStayPrice,
                            // 查找房間圖片
                            FRoomImages = _context.TProductImages
                                .Where(img => img.FProductType == "R" && img.FProductID == r.FHomestayRoomID)
                                .Select(img => img.FImage)
                                .ToList()
                        }
                    },

                    // 查找民宿圖片
                    FHomestayImages = _context.TProductImages
                        .Where(img => img.FProductType == "H" && img.FProductID == r.FHomestay.FHomestayID)
                        .Select(img => img.FImage)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return NotFound(); // 如果找不到房間，返回 404
            }

            return Ok(room);
        }
    }
}
