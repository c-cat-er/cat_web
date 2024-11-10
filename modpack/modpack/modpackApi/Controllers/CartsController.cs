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
    public class CartsController : ControllerBase
    {
        private readonly ModPackContext _context;

        public CartsController(ModPackContext context)
        {
            _context = context;
        }


        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<List<CartsDTO>> GetCart(int id)
        {
            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.Inspiration)
                .Include(c => c.Customized)
                .Where(c => c.MemberId == id).ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                return null;
            }
            var cartDTOs = new List<CartsDTO>();

            foreach (var cartItem in cartItems)
            {

                var cartDTO = new CartsDTO
                {
                    cartId = cartItem.CartId,
                    memberId = cartItem.MemberId,
                    productId = cartItem.ProductId,
                    inspirationId = cartItem.InspirationId,
                    customizedId = cartItem.CustomizedId,
                    quantity = cartItem.Quantity,
                    price = cartItem.Product?.SalePrice ?? cartItem.Inspiration?.SalePrice ?? cartItem.Customized?.SalePrice,
                    name = cartItem.Product?.Name ?? cartItem.Inspiration?.Name ?? cartItem.Customized?.Name,
                    ImageFileName = cartItem.Product?.ImageFileName ?? cartItem.Inspiration?.ImageFileName ?? cartItem.Customized?.ImageFileName,
                    ImageFile = cartItem.ProductId != null ? "product" : cartItem.InspirationId != null ? "Inspiration" : cartItem.CustomizedId != null ? "Customized" : null,
                };

                cartDTOs.Add(cartDTO);
            }

            return cartDTOs;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutCart(int id, CartsDTO cDTO)
        {
            if (id != cDTO.cartId)
            {
                return "修改數量失敗";
            }

            Cart cartItem = await _context.Carts.FindAsync(id);

            cartItem.Quantity = cDTO.quantity;

            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return "更新數量失敗";
                }
                else
                {
                    throw;
                }
            }
            return "更新數量成功";
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostCart(Cart cart)
        {
            _context.Carts.Add(cart);
            try
            {
                await _context.SaveChangesAsync();
                return "已加入購物車";
            }
            catch (DbUpdateConcurrencyException)
            {
                return "加入購物車失敗";
            }
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return "刪除產品失敗";
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return "產品刪除成功";
        }

        // DELETE: api/Carts/Clear{memberId}
        [HttpDelete("Clear{memberId}")]
        public async Task<string> ClearCart(int memberId)
        {
            var cartsToRemove = await _context.Carts.Where(cart => cart.MemberId == memberId).ToListAsync();

            if (cartsToRemove == null)
            {
                return "該會員的購物車為空，無需刪除";
            }

            _context.Carts.RemoveRange(cartsToRemove);
            await _context.SaveChangesAsync();

            return "產品清空成功";
        }

        // POST: api/Carts/DeleteMultiple
        [HttpDelete("deletePurchasedProduct")]
        public async Task<string> deletePurchasedProduct(List<int> cartIds)
        {
            foreach (int cartId in cartIds)
            {
                var cart = await _context.Carts.FindAsync(cartId);
                if (cart != null)
                {
                    _context.Carts.Remove(cart);
                }
            }
            await _context.SaveChangesAsync();
            return "多个购物车项目删除成功";
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}
