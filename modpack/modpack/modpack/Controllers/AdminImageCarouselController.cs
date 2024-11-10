using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using modpack.Models;
using modpack.ViewModels;
using modpack.Wrap;
using Microsoft.AspNetCore.SignalR;
using modpack.Service;
using X.PagedList;

namespace modpack.Controllers
{
    public class AdminImageCarouselController : Controller
    {
        private readonly ModPackContext _context;
        private IWebHostEnvironment _environment;
        private readonly IHubContext<ImageCarouselHub> _hubContext;
        private readonly ILogger<AdminImageCarouselController> _logger;

        public AdminImageCarouselController(ModPackContext context, IWebHostEnvironment environment,
            IHubContext<ImageCarouselHub> hubContext, ILogger<AdminImageCarouselController> logger)
        {
            _context = context;
            _environment = environment;
            _hubContext = hubContext;
            _logger = logger;
        }

        // GET: AdminCarouselImage
        public async Task<IActionResult> List(string searchStringDescription, int page = 1, int pageSize = 10)
        {
            IQueryable<CImageCarouselViewModel> caVM = _context.ImageCarousels
                .Select(carousel => new CImageCarouselViewModel
                {
                    aImageCarouselID = carousel.ImageCarouselId,
                    aImageFile = carousel.Image,
                    aImageDescription = carousel.Description
                });

            if (!string.IsNullOrEmpty(searchStringDescription))
                caVM = caVM.Where(a => a.aImageDescription.Contains(searchStringDescription));
            var result = await caVM.ToPagedListAsync(page, pageSize);

            if (result.Count == 0) ViewBag.NoSearchResults = true;
                else ViewBag.NoSearchResults = false;
            return View(result);
        }

        // GET: AdminCarouselImage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var imageCarousel = await _context.ImageCarousels
                .FirstOrDefaultAsync(m => m.ImageCarouselId == id);
            if (imageCarousel == null) return NotFound();
            return View(imageCarousel);
        }

        // GET: AdminCarouselImage/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: AdminCarouselImage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CImageCarouselViewModel iVM)
        {
            if (iVM.photo == null || iVM.photo.Length == 0)
            {
                ModelState.AddModelError("photo", "尚未選擇圖片");
                return View(iVM);
            }
            if (iVM.aImageDescription == null || iVM.aImageDescription.Length == 0)
            {
                ModelState.AddModelError("aImageDescription", "尚未輸入相關文字");
                return View(iVM);
            }
            string photoName = string.Concat("carousel", Guid.NewGuid().ToString().AsSpan(0, 3), ".jpeg");
            using var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, "images/carousel", photoName), FileMode.Create);
            await iVM.photo.CopyToAsync(fileStream);
            
            ImageCarousel newImage = new()
            {
                Image = photoName,
                Description = iVM.aImageDescription
            };
            _context.Add(newImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        // GET: AdminCarouselImage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var im = await _context.ImageCarousels.FindAsync(id);
            var vm = new CImageCarouselViewModel
            {
                aImageCarouselID = im.ImageCarouselId,
                aImageFile = im.Image,
                aImageDescription = im.Description
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CImageCarouselViewModel iVM)
        {
            ///int imageCarouselId = iVM.aImageCarouselID;
            ImageCarousel icEdit = await _context.ImageCarousels.FindAsync(iVM.aImageCarouselID);
            if (icEdit != null)
            {
                if (iVM.photo != null)
                {
                    string photoName = string.Concat("carousel", Guid.NewGuid().ToString().AsSpan(0, 5), ".jpeg");
                    icEdit.Image = photoName;
                    using var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, "images/carousel", photoName), FileMode.Create);
                    await iVM.photo.CopyToAsync(fileStream);
                }
                icEdit.Description = iVM.aImageDescription;
                await _context.SaveChangesAsync();

                var imagePaths = await _context.ImageCarousels.Select(ic => ic.Image).ToListAsync();
                var descriptions = await _context.ImageCarousels.Select(ic => ic.Description).ToListAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveImagePaths", imagePaths, descriptions);
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var imageCarousel = await _context.ImageCarousels
                .FirstOrDefaultAsync(m => m.ImageCarouselId == id);
            if (imageCarousel == null) return NotFound();
            var cWAdmin = new CWrapAdministrator
            {
                ImageCarouselID = imageCarousel.ImageCarouselId,
                ImageFile = imageCarousel.Image,
                Description = imageCarousel.Description
            };
            return View(cWAdmin);
        }

        // POST: AdminCarouselImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageCarousel = await _context.ImageCarousels.FindAsync(id);
            if (imageCarousel != null) _context.ImageCarousels.Remove(imageCarousel);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        // GET: /imagesTwo/{ids}
        [HttpGet("AdminImageCarousel/imagesTwo")]
        public async Task<IActionResult> GetImageTwo([FromQuery] string ids)
        {
            try
            {
                var idArray = ids.Split(',').Select(id => int.Parse(id)).ToList();
                var productList = await _context.Products
                    .Include(p => p.Status)
                    .Include(p => p.Promotion)
                    .Where(p => idArray.Contains(p.ProductId))
                    .Take(2)
                    .Select(p => new Product
                    {
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Category = p.Category,
                        ImageFileName = p.ImageFileName,
                        StatusId = p.StatusId,
                        PromotionId = p.PromotionId,
                        Promotion = p.Promotion,
                        OriginalPrice = p.OriginalPrice,
                        SalePrice = p.SalePrice,
                    })
                    .ToListAsync();
                ///return Json(productList);
                return Ok(JsonSerializer.Serialize(productList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"找不到圖片: {ex.Message}");
            }
        }

        private bool ImageCarouselExists(int id)
        {
            return _context.ImageCarousels.Any(e => e.ImageCarouselId == id);
        }
    }
}
