using System;
using System.Collections;
using System.Collections.Generic;
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
    public class ComponentsController : ControllerBase
    {
        private readonly ModPackContext _context;

        public ComponentsController(ModPackContext context)
        {
            _context = context;
        }

        // GET: api/Components
        [HttpGet]
        public async Task<string> GetComponents()
        {
            return JsonSerializer.Serialize(await _context.Components
                .Include(e => e.Material)
                .Include(e => e.Color)
                .Include(e => e.Status)
               .Select(e => new ComponentsDTO
               {
                   ComponentID = e.ComponentId,
                   MateriaName = e.Material.Name,
                   MateriaFileName=e.Material.ImageFileName,
                   ColorName = e.Color.Name,
                   ColorRGB=e.Color.Rgb,
                   StatusName = e.Status.Name,
                   Name = e.Name,
                   Category = e.Category,
                   OriginalPrice = (float)e.OriginalPrice,
                   FBXFileName = e.FbxfileName,
                   ImageFileName = e.ImageFileName,
                   IsCustomized = e.IsCustomized,
                   productCategories = _context.Categories
                    .Where(cat => cat.ComponentId == e.ComponentId)
                    .Select(cat => new ComponentsDTO.ProductCategory
                    {
                        CategoryID = cat.CategoryId,
                        Name = cat.Name
                    }).ToList()
               }).ToListAsync());
        }

        // GET: api/Components/5
        [HttpGet("Key/{key}")]
        public async Task<string> GetComponent(string key)
        {
            return JsonSerializer.Serialize(await _context.Components
                .Include(e => e.Material)
                .Include(e => e.Color)
                .Include(e => e.Status)
            .Include(e => e.Categories)
                .Where(e =>
                    e.Material.Name.Contains(key) ||
               e.Color.Name.Contains(key) ||
               e.Status.Name.Contains(key) ||
                e.Category.Contains(key) ||
               e.Categories.Any(cat => cat.Name.Contains(key)))
               .Select(e => new ComponentsDTO
               {
                   ComponentID = e.ComponentId,
                   MateriaName = e.Material.Name,
                   MateriaFileName = e.Material.ImageFileName,
                   ColorName = e.Color.Name,
                   ColorRGB = e.Color.Rgb,
                   StatusName = e.Status.Name,
                   Name = e.Name,
                   Category = e.Category,
                   OriginalPrice = (float)e.OriginalPrice,
                   FBXFileName = e.FbxfileName,
                   ImageFileName = e.ImageFileName,
                   IsCustomized = e.IsCustomized,
                   productCategories = _context.Categories
                    .Where(cat => cat.ComponentId == e.ComponentId)
                    .Select(cat => new ComponentsDTO.ProductCategory
                    {
                        CategoryID = cat.CategoryId,
                        Name = cat.Name
                    }).ToList()
               }).ToListAsync());
        }
        [HttpGet("Id/{id}")]
        public async Task<string> GetComponent(int id)
        {
            return JsonSerializer.Serialize(await _context.Components
                        .Include(e => e.Material)
                        .Include(e => e.Color)
                        .Include(e => e.Status)
                        .Where(e => e.ComponentId == id)
                       .Select(e => new ComponentsDTO
                       {
                           ComponentID = e.ComponentId,
                           MateriaName = e.Material.Name,
                           MateriaFileName = e.Material.ImageFileName,
                           ColorName = e.Color.Name,
                           ColorRGB = e.Color.Rgb,
                           StatusName = e.Status.Name,
                           Name = e.Name,
                           Category = e.Category,
                           OriginalPrice = (float)e.OriginalPrice,
                           FBXFileName = e.FbxfileName,
                           ImageFileName = e.ImageFileName,
                           IsCustomized = e.IsCustomized,
                           productCategories = _context.Categories
                            .Where(cat => cat.ComponentId == e.ComponentId)
                            .Select(cat => new ComponentsDTO.ProductCategory
                            {
                                CategoryID = cat.CategoryId,
                                Name = cat.Name
                            }).ToList()
                       }).FirstAsync());
        }
        // POST: api/Components
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostComponent()
        {
            var CompDTO = JsonSerializer.Deserialize<ComponentsDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            Component Comp = new Component
            {
                MaterialId = CompDTO.MaterialID,
                ColorId = CompDTO.ColorID,
                StatusId = CompDTO.StatusID,
                Name = CompDTO.Name,
                Category = CompDTO.Category,
                OriginalPrice = (decimal)CompDTO.OriginalPrice,
                FbxfileName = CompDTO.FBXFileName,
                ImageFileName = "",
                IsCustomized = CompDTO.IsCustomized,
            };
            try
            {
                _context.Components.Add(Comp);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return "新增產品失敗!";
            }
            if (CompDTO.productCategories!=null)
            {
                foreach (var item in CompDTO.productCategories)
                {
                    Category cat = new Category
                    {
                        ComponentId = Comp.ComponentId,
                        Name = item.Name,
                    };
                    try
                    {
                        _context.Categories.Add(cat);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _context.Components.Remove(Comp);
                        await _context.SaveChangesAsync();
                        return "新增產品類別失敗!";
                    }
                }
            }
            return $"新增產品編號:{Comp.ComponentId}";
        }

        // PUT: api/Component/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutComponent(int id)
        {
            var CompDTO = JsonSerializer.Deserialize<ComponentsDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            if (id != CompDTO.ComponentID)
            {
                return "更新產品失敗!";
            }
            Component Comp = await _context.Components.FindAsync(id);
            Comp.ComponentId = CompDTO.ComponentID;
            Comp.MaterialId = CompDTO.MaterialID;
            Comp.ColorId = CompDTO.ColorID;
            Comp.StatusId = CompDTO.StatusID;
            Comp.Name = CompDTO.Name;
            Comp.Category = CompDTO.Category;
            Comp.OriginalPrice = (decimal)CompDTO.OriginalPrice;
            Comp.FbxfileName = CompDTO.FBXFileName;
            Comp.ImageFileName = Comp.ImageFileName;
            Comp.IsCustomized = CompDTO.IsCustomized;
            if (CompDTO.productCategories!=null)
            {
                var missingCategories = _context.Categories
                .Where(c => c.ComponentId == id).ToList()
                .Where(c1 => !CompDTO.productCategories
                .Any(c2 => c2.CategoryID == c1.CategoryId)).ToList();
                foreach (var item in missingCategories)
                {
                    var component = await _context.Categories.FindAsync(item.CategoryId);
                    try
                    {
                        _context.Categories.Remove(component);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return "更新產品失敗!";
                    }
                }
                foreach (var categoryDTO in CompDTO.productCategories)
                {
                    if (categoryDTO.CategoryID != 0)
                    {
                        Category cat = await _context.Categories.FindAsync(categoryDTO.CategoryID);
                        if (cat != null)
                        {
                            cat.CategoryId = categoryDTO.CategoryID;
                            cat.ComponentId = Comp.ComponentId;
                            cat.Name = categoryDTO.Name;
                            _context.Entry(cat).State = EntityState.Modified;
                            try
                            {
                                await _context.SaveChangesAsync();

                            }
                            catch (Exception ex)
                            {

                                throw;
                            }

                        }
                        else
                        {
                            return "更新產品失敗!";
                        }
                    }
                    else
                    {
                        Category cat = new Category
                        {
                            ComponentId = Comp.ComponentId,
                            Name = categoryDTO.Name,
                        };
                        _context.Categories.Add(cat);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            _context.Entry(Comp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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


        [HttpPut("image/{id}")]
        public async Task<string> PutComponentimage(int id)
        {
            string ImageFileName = new StreamReader(Request.Body).ReadToEndAsync().Result;
            Component Comp = await _context.Components.FindAsync(id);
            Comp.ImageFileName = ImageFileName;
            _context.Entry(Comp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
                {
                    return "更新圖片失敗!";
                }
                else
                {
                    throw;
                }
            }
            return "更新圖片成功!";
        }

        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteComponent(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return "刪除元件失敗";
            }
            try
            {
                var categories = _context.Categories.Where(cat => cat.ComponentId == id).ToList();
                foreach (var cat in categories)
                {
                    try
                    {
                        _context.Categories.Remove(cat);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        return "刪除元件失敗";
                    }
                }
                _context.Components.Remove(component);
                await _context.SaveChangesAsync();
                string content = component.ImageFileName;
                HttpClient client = new HttpClient();
                string imageUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("imageUrl").Value;
                HttpResponseMessage response = await client.PutAsync($"{imageUrl}/Components/imageDelete", new StringContent(content, Encoding.UTF8, "text/plain"));
            }
            catch (DbUpdateException ex)
            {
                return "刪除元件失敗";
            }
            return "刪除元件成功";
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.ComponentId == id);
        }
    }
}
