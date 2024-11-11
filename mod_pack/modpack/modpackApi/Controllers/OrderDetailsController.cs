using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modpackApi.DTO;
using modpackApi.Models;

namespace modpackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly ModPackContext _context;

        public OrderDetailsController(ModPackContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<List<OrderDetailDTO>> GetOrderDetail(int id)
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Inspiration)
                .Include(od => od.Customized)
                .Where(od => od.OrderId == id)
                .ToListAsync();

            if (orderDetails.Count == 0)
            {
                return null;
            }

            var odDTOs = new List<OrderDetailDTO>();

            foreach (var orderDetail in orderDetails)
            {
                var odDTO = new OrderDetailDTO
                {
                    detailsId = orderDetail.DetailsId,
                    orderId = orderDetail.OrderId,
                    name = orderDetail.Product?.Name ?? orderDetail.Inspiration?.Name ?? orderDetail.Customized?.Name,
                    quantity = orderDetail.Quantity,
                    unitPrice = orderDetail.UnitPrice,
                    ImageFileName = orderDetail.Product?.ImageFileName ?? orderDetail.Inspiration?.ImageFileName ?? orderDetail.Customized?.ImageFileName,
                    subtotal = orderDetail.UnitPrice * orderDetail.Quantity,
                    ImageFile = orderDetail.ProductId != null ? "product" : orderDetail.InspirationId != null ? "Inspiration" : orderDetail.CustomizedId != null ? "Customized" : null,
                };

                odDTOs.Add(odDTO);
            }
            return odDTOs;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.DetailsId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(List<OrderDetail> orderDetail)
        {
            foreach (var item in orderDetail)
            {
                _context.OrderDetails.Add(item);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.DetailsId == id);
        }
    }
}
