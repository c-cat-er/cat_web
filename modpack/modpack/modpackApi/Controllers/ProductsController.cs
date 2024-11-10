using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    public class ProductsController : ControllerBase
    {
        private readonly ModPackContext _context;

        public ProductsController(ModPackContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<string> GetProducts()
        {
            return JsonSerializer.Serialize(await _context.Products
                  .Include(e => e.Promotion)
                  .Include(e => e.Status)
                  .Select(e => new ProductsDTO
                  {
                      ProductId = e.ProductId,
                      PromotionId = e.PromotionId,
                      StatusId = e.StatusId,
                      PromotionName = e.Promotion.Name,
                      StatusName = e.Status.Name,
                      Name = e.Name,
                      Category = e.Category,
                      ImageFileName = e.ImageFileName,
                      OriginalPrice = (float)e.OriginalPrice,
                      SalePrice = (float)e.SalePrice,
                  }).ToListAsync());
        }

        [HttpGet("Key/{key}")]
        public async Task<string> GetProducts(string key)
        {
            return JsonSerializer.Serialize(await _context.Products.Include(e => e.Promotion).Include(e => e.Status)
                    .Where(e => e.Name.Contains(key) || e.Category.Contains(key) || e.Promotion.Name.Contains(key) || e.Status.Name.Contains(key))
                    .Select(e => new ProductsDTO
                    {
                        ProductId = e.ProductId,
                        PromotionId = e.PromotionId,
                        StatusId = e.StatusId,
                        PromotionName = e.Promotion.Name,
                        StatusName = e.Status.Name,
                        Name = e.Name,
                        Category = e.Category,
                        ImageFileName = e.ImageFileName,
                        OriginalPrice = (float)e.OriginalPrice,
                        SalePrice = (float)e.SalePrice
                    }).ToListAsync());
        }

        [HttpGet("Id/{id}")]
        public async Task<string> GetProducts(int id)
        {
            return JsonSerializer.Serialize(await _context.Products.Include(e => e.Promotion).Include(e => e.Status)
           .Where(e => e.ProductId == id)
           .Select(e => new ProductsDTO
           {
               ProductId = e.ProductId,
               PromotionId = e.PromotionId,
               StatusId = e.StatusId,
               PromotionName = e.Promotion.Name,
               StatusName = e.Status.Name,
               Name = e.Name,
               Category = e.Category,
               ImageFileName = e.ImageFileName,
               OriginalPrice = (float)e.OriginalPrice,
               SalePrice = (float)e.SalePrice
           }).FirstOrDefaultAsync());
        }

        [HttpGet("Init")]
        public async Task<string> initGet()
        {
            InitProduct initProduct = new InitProduct
            {
                ComponentName = new List<string>(),
                ComponentCategory = new List<string>(),
                PromotionName = new List<string>(),
                StatusName = new List<string>()
            };
            foreach (var item in from p in _context.Components select p)
            {
                initProduct.ComponentName.Add(item.Name);
                initProduct.ComponentCategory.Add(item.Category);
            }
            foreach (var item in from p in _context.Promotions select p)
                initProduct.PromotionName.Add(item.Name);
            foreach (var item in from p in _context.Statuses select p)
                initProduct.StatusName.Add(item.Name);
            var returnJson = JsonSerializer.Serialize(initProduct);
            return returnJson;
        }


        public class InitProduct
        {
            public List<string> ComponentName { get; set; }
            public List<string> ComponentCategory { get; set; }
            public List<string> PromotionName { get; set; }
            public List<string> StatusName { get; set; }
        }
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutProducts(int id)
        {
            var ProdDTO = JsonSerializer.Deserialize<ProductsDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            if (id != ProdDTO.ProductId)
            {
                return "更新產品失敗!";
            }
            Product Prod = await _context.Products.FindAsync(id);
            Prod.PromotionId = ProdDTO.PromotionId;
            Prod.StatusId = ProdDTO.StatusId;
            Prod.Name = ProdDTO.Name;
            Prod.Category = ProdDTO.Category;
            Prod.ImageFileName = ProdDTO.ImageFileName;
            Prod.OriginalPrice = (decimal)ProdDTO.OriginalPrice;
            Prod.SalePrice = (decimal)ProdDTO.SalePrice;
            _context.Entry(Prod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return "更新產品失敗!";
                }
                else
                {
                    throw;
                }
            }
            return "更新產品成功!";
        }
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostProducts()
        {
            var ProdDTO = JsonSerializer.Deserialize<ProductsDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            Product Prod = new Product
            {
                ProductId = ProdDTO.ProductId,
                PromotionId = ProdDTO.PromotionId,
                StatusId = ProdDTO.StatusId,
                Name = ProdDTO.Name,
                Category = ProdDTO.Category,
                ImageFileName = ProdDTO.ImageFileName,
                OriginalPrice = (decimal)ProdDTO.OriginalPrice,
                SalePrice = (decimal)ProdDTO.SalePrice
            };
            _context.Products.Add(Prod);
            await _context.SaveChangesAsync();
            return $"新增產品編號:{Prod.ProductId}";
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteProducts(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return "刪除產品失敗";
            }
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return "刪除產品失敗";
            }
            return "刪除產品成功";
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
