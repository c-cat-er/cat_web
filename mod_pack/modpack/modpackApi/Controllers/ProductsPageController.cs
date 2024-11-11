using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modpackApi.DTO;
using modpackApi.Models;

namespace modpackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsPageController : ControllerBase
    {
        private readonly ModPackContext _context;

        public ProductsPageController(ModPackContext context)
        {
            _context = context;
        }

        [HttpGet("product/{id}")]
        public async Task<string> GetProducts(int id)
        {
            return JsonSerializer.Serialize(await _context.Products
                    .Include(e => e.Promotion)
                    .Where(e => e.ProductId == id)
                    .Select(e => new ProductsPageDTO
                    {
                        ProductId = e.ProductId,
                        PromotionId = e.PromotionId,
                        PromotionName = e.Promotion.Name,
                        Category = e.Category,
                        Name = e.Name,
                        ImageFileName = e.ImageFileName,
                        SalePrice = (float)e.SalePrice,
                        relatedproductsId = _context.Products.Where(p => p.Category == e.Category).Select(p => p.ProductId).ToList(),
                        relatedproductsName = _context.Products.Where(p => p.Category == e.Category).Select(p => p.Name).ToList(),
                        relatedproductsImageFileName = _context.Products.Where(p => p.Category == e.Category).Select(p => p.ImageFileName).ToList(),
                        relatedproductsSalePrice = _context.Products.Where(p => p.Category == e.Category).Select(p =>(float)p.SalePrice).ToList(),
                    }).FirstOrDefaultAsync());
        }

        [HttpGet("memberproduct/{id}/{memberid}")]//客製化產品
        public async Task<string> GetmemberProductss(int id, int memberid)
        {
            var inspirationName = _context.Inspirations
                .Where(insp => insp.InspirationId == id)
                .Select(e => e.Name)
                .FirstOrDefault();
            var query = _context.Customizeds
                .Where(cust => cust.Name.Contains(inspirationName) && cust.MemberId == memberid);
            return JsonSerializer.Serialize(await _context.Inspirations
                .Include(e => e.Promotion)
                .Where(e => e.InspirationId == id)
                .Select(e => new ProductsPageDTO
                {
                    ProductId = e.InspirationId,
                    PromotionId = e.PromotionId,
                    PromotionName = e.Promotion.Name,
                    Name = e.Name,
                    ImageFileName = e.ImageFileName,
                    SalePrice = (float)e.SalePrice,
                    Customizedid = query.Select(Result => Result.CustomizedId).ToList(),
                    CustomizedName=query.Select(Result => Result.Name).ToList(),
                    CustomizedSalePrice = query.Select(Result => (float)Result.SalePrice).ToList(),
                    CustomizedImageFileName = query.Select(Result => Result.ImageFileName).ToList(),
                }).FirstOrDefaultAsync());
        }
        [HttpGet("product/member/{id}/{memberid}")]//已購買產品
        public async Task<string> GetmemberProductssAndPurchasedproducts(int id, int memberid)
        {
            var purchasedProducts = await _context.Orders
                .Where(o => o.MemberId == memberid)
                .SelectMany(o => o.OrderDetails.Select(od => od.Product))
                .ToListAsync();
            purchasedProducts.RemoveAll(item => item == null);
            return JsonSerializer.Serialize(await _context.Products
                .Include(e => e.Promotion)
                .Where(e => e.ProductId == id)
                .Select(e => new ProductsPageDTO
                {
                    ProductId = e.ProductId,
                    PromotionId = e.PromotionId,
                    PromotionName = e.Promotion.Name,
                    Name = e.Name,
                    Category = e.Category,
                    ImageFileName = e.ImageFileName,
                    SalePrice = (float)e.SalePrice,
                    relatedproductsId = _context.Products.Where(p => p.Category == e.Category).Select(p => p.ProductId).ToList(),
                    relatedproductsName = _context.Products.Where(p => p.Category == e.Category).Select(p => p.Name).ToList(),
                    relatedproductsImageFileName = _context.Products.Where(p => p.Category == e.Category).Select(p => p.ImageFileName).ToList(),
                    relatedproductsSalePrice = _context.Products.Where(p => p.Category == e.Category).Select(p => (float)p.SalePrice).ToList(),
                    purchasedproductsId = purchasedProducts.Select(p => p.ProductId).ToList(),
                    purchasedproductsName = purchasedProducts.Select(p => p.Name).ToList(),
                    purchasedproductsImageFileName = purchasedProducts.Select(p => p.ImageFileName).ToList(),
                    purchasedproductsSalePrice = purchasedProducts.Select(p => (float)p.SalePrice).ToList(),
                }).FirstOrDefaultAsync());
        }

        [HttpGet("Init")]
        public async Task<string> initGet()
        {
            InitunityProductsPage initunityProductsPage = new InitunityProductsPage();
            foreach (var item in from p in _context.Components select p)
            {
                if (item.IsCustomized==true)
                {
                    initunityProductsPage.ComponentID.Add(item.ComponentId);
                    initunityProductsPage.ComponentColorID.Add(item.ColorId);
                    initunityProductsPage.ComponentMaterialID.Add(item.MaterialId);
                    initunityProductsPage.ComponentName.Add(item.Name);
                    initunityProductsPage.ComponentCategory.Add(item.Category);
                    initunityProductsPage.ComponentImageFileName.Add(item.ImageFileName);
                    initunityProductsPage.ComponentFbxfileName.Add(item.FbxfileName);
                    initunityProductsPage.ComponentOriginalPrice.Add((float)item.OriginalPrice);
                }
            }
            foreach (var item in from p in _context.Categories select p)
            {
                initunityProductsPage.CategoryComponentID.Add(item.ComponentId);
                initunityProductsPage.CategoryName.Add(item.Name);
            }
            foreach (var item in from p in _context.Promotions select p)
            {
                initunityProductsPage.PromotionID.Add(item.PromotionId);
                initunityProductsPage.PromotionName.Add(item.Name);
            }
            foreach (var item in from p in _context.Materials select p)
            {
                initunityProductsPage.MaterialID.Add(item.MaterialId);
                initunityProductsPage.MaterialName.Add(item.Name);
                initunityProductsPage.MaterialFileName.Add(item.ImageFileName);
            }
            foreach (var item in from p in _context.Colors select p)
            {
                initunityProductsPage.ColorID.Add(item.ColorId);
                initunityProductsPage.ColorName.Add(item.Name);
                initunityProductsPage.ColorRGB.Add(item.Rgb);
            }
            var returnJson = JsonSerializer.Serialize(initunityProductsPage);
            return returnJson;
        }
        public class InitunityProductsPage
        {
            public List<int> ComponentID { get; set; } = new List<int>();
            public List<int> ComponentColorID { get; set; } = new List<int>();
            public List<int> ComponentMaterialID { get; set; } = new List<int>();
            public List<string> ComponentName { get; set; } = new List<string>();
            public List<string> ComponentCategory { get; set; } = new List<string>();
            public List<string> ComponentImageFileName { get; set; } = new List<string>();
            public List<string> ComponentFbxfileName { get; set; } = new List<string>();
            public List<float> ComponentOriginalPrice { get; set; } = new List<float>();
            public List<int> CategoryComponentID { get; set; } = new List<int>();
            public List<string> CategoryName { get; set; } = new List<string>();
            public List<int> PromotionID { get; set; } = new List<int>();
            public List<string> PromotionName { get; set; } = new List<string>();
            public List<int> MaterialID { get; set; } = new List<int>();
            public List<string> MaterialName { get; set; } = new List<string>();
            public List<string> MaterialFileName { get; set; } = new List<string>();
            public List<int> ColorID { get; set; } = new List<int>();
            public List<string> ColorName { get; set; } = new List<string>();
            public List<string> ColorRGB { get; set; } = new List<string>();
        }
    }
}
