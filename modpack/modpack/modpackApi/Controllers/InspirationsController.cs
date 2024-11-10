using System;
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
    public class InspirationsController : ControllerBase
    {
        private readonly ModPackContext _context;

        public InspirationsController(ModPackContext context)
        {
            _context = context;
        }

        // GET: api/Inspirations
        [HttpGet]
        public async Task<string> GetInspirations()
        {
            return JsonSerializer.Serialize(await _context.Inspirations
                .Include(e => e.Promotion)
              .Select(e => new InspirationDTO
              {
                  InspirationId = e.InspirationId,
                  PromotionId = e.PromotionId,
                  PromotionName = e.Promotion.Name,
                  Name = e.Name,
                  ImageFileName = e.ImageFileName,
                  SalePrice = (float)e.SalePrice,
                  InspirationSpecs = _context.InspirationSpecifications
                    .Where(cat => cat.InspirationId == e.InspirationId)
                    .Select(cat => new InspirationDTO.InspirationSpec
                    {
                        InspirationSpecificationId = cat.InspirationSpecificationId,
                        ComponentId = cat.ComponentId,
                        MaterialId = cat.MaterialId,
                        ColorId = cat.ColorId,
                        Location = cat.Location,
                        SizeX = cat.SizeX,
                        SizeY = cat.SizeY
                    }).ToList(),
                  InspirationComponents = _context.Components
                    .Where(cat => e.InspirationSpecifications.Any(spec => spec.ComponentId == cat.ComponentId))
                    .Select(cat => new InspirationDTO.InspirationComponent
                    {
                        ComponentId = cat.ComponentId,
                        MaterialId = cat.MaterialId,
                        ColorId = cat.ColorId,
                        Name = cat.Name,
                        Category = cat.Category,
                        OriginalPrice = (float)cat.OriginalPrice,
                        FBXFileName = cat.FbxfileName,
                        ImageFileName = cat.ImageFileName,
                        IsCustomized = cat.IsCustomized,
                    }).ToList(),
              }).ToListAsync());
        }

        [HttpGet("Key/{key}")]
        public async Task<string> GetInspirations(string key)
        {
            return JsonSerializer.Serialize(await _context.Inspirations.Include(e => e.Promotion)
                  .Where(e => e.Name.Contains(key) || e.Promotion.Name.Contains(key))
                  .Select(e => new InspirationDTO
                  {
                      InspirationId = e.InspirationId,
                      PromotionId = e.PromotionId,
                      PromotionName = e.Promotion.Name,
                      Name = e.Name,
                      ImageFileName = e.ImageFileName,
                      SalePrice = (float)e.SalePrice,
                      InspirationSpecs = _context.InspirationSpecifications
                      .Where(cat => cat.InspirationId == e.InspirationId)
                      .Select(cat => new InspirationDTO.InspirationSpec
                      {
                          InspirationSpecificationId = cat.InspirationSpecificationId,
                          ComponentId = cat.ComponentId,
                          MaterialId = cat.MaterialId,
                          ColorId = cat.ColorId,
                          Location = cat.Location,
                          SizeX = cat.SizeX,
                          SizeY = cat.SizeY
                      }).ToList(),
                      InspirationComponents = _context.Components
                      .Where(cat => e.InspirationSpecifications.Any(spec => spec.ComponentId == cat.ComponentId))
                      .Select(cat => new InspirationDTO.InspirationComponent
                      {
                          ComponentId = cat.ComponentId,
                          MaterialId = cat.MaterialId,
                          ColorId = cat.ColorId,
                          Name = cat.Name,
                          Category = cat.Category,
                          OriginalPrice = (float)cat.OriginalPrice,
                          FBXFileName = cat.FbxfileName,
                          ImageFileName = cat.ImageFileName,
                          IsCustomized = cat.IsCustomized,
                      }).ToList(),
                  }).ToListAsync());
        }

        // GET: api/Inspirations/5
        [HttpGet("Id/{id}")]
        public async Task<string> GetInspiration(int id)
        {
            return JsonSerializer.Serialize(await _context.Inspirations
                .Include(e => e.Promotion)
                .Where(e => e.InspirationId == id)
                .Select(e => new InspirationDTO
                {
                    InspirationId = e.InspirationId,
                    PromotionId = e.PromotionId,
                    PromotionName = e.Promotion.Name,
                    Name = e.Name,
                    ImageFileName = e.ImageFileName,
                    SalePrice = (float)e.SalePrice,
                    InspirationSpecs = _context.InspirationSpecifications
                    .Where(cat => cat.InspirationId == e.InspirationId)
                    .Select(cat => new InspirationDTO.InspirationSpec
                    {
                        InspirationSpecificationId = cat.InspirationSpecificationId,
                        ComponentId = cat.ComponentId,
                        MaterialId = cat.MaterialId,
                        ColorId = cat.ColorId,
                        Location = cat.Location,
                        SizeX = cat.SizeX,
                        SizeY = cat.SizeY
                    }).ToList(),
                    InspirationComponents = _context.Components
                    .Where(cat => e.InspirationSpecifications.Any(spec => spec.ComponentId == cat.ComponentId))
                    .Select(cat => new InspirationDTO.InspirationComponent
                    {
                        ComponentId = cat.ComponentId,
                        MaterialId = cat.MaterialId,
                        ColorId = cat.ColorId,
                        Name = cat.Name,
                        Category = cat.Category,
                        OriginalPrice = (float)cat.OriginalPrice,
                        FBXFileName = cat.FbxfileName,
                        ImageFileName = cat.ImageFileName,
                        IsCustomized = cat.IsCustomized,
                    }).ToList(),
                }).FirstOrDefaultAsync());
        }

        // POST: api/Inspirations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostInspiration()
        {
            var InspDTO = JsonSerializer.Deserialize<InspirationDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            Inspiration Insp = new Inspiration
            {
                InspirationId = InspDTO.InspirationId,
                PromotionId = InspDTO.PromotionId,
                Name = InspDTO.Name,
                ImageFileName = InspDTO.ImageFileName,
                SalePrice = (decimal)InspDTO.SalePrice,
            };
            try
            {
                _context.Inspirations.Add(Insp);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return "";//靈感失敗
            }
            foreach (var item in InspDTO.InspirationSpecs)
            {
                InspirationSpecification Spec = new InspirationSpecification
                {
                    InspirationId = Insp.InspirationId,
                    ComponentId = item.ComponentId,
                    MaterialId = item.MaterialId,
                    ColorId = item.ColorId,
                    Location = item.Location,
                    SizeX = item.SizeX,
                    SizeY = item.SizeY
                };
                try
                {
                    _context.InspirationSpecifications.Add(Spec);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Inspirations.Remove(Insp);
                    await _context.SaveChangesAsync();
                    return "";//靈感失敗
                }
            }
            return $"新增靈感編號:{Insp.InspirationId}";
        }


        // PUT: api/Inspirations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutInspiration(int id)
        {
            var InspDTO = JsonSerializer.Deserialize<InspirationDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            if (id != InspDTO.InspirationId)
            {
                return "";//靈感失敗
            }
            Inspiration Insp = await _context.Inspirations.FindAsync(id);
            Insp.InspirationId = InspDTO.InspirationId;
            Insp.PromotionId = InspDTO.PromotionId;
            Insp.Name = InspDTO.Name;
            Insp.ImageFileName = InspDTO.ImageFileName;
            Insp.SalePrice = (decimal)InspDTO.SalePrice;
            var missingSpecifications = _context.InspirationSpecifications
                .Where(c => c.InspirationId == id).ToList()
                .Where(c1 => !InspDTO.InspirationSpecs
                .Any(c2 => c2.InspirationSpecificationId == c1.InspirationSpecificationId)).ToList();
            foreach (var item in missingSpecifications)
            {
                var Specifications = await _context.InspirationSpecifications.FindAsync(item.InspirationSpecificationId);
                try
                {
                    _context.InspirationSpecifications.Remove(Specifications);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return "";//靈感失敗
                }
            }
            foreach (var SpecDTO in InspDTO.InspirationSpecs)
            {
                if (SpecDTO.InspirationSpecificationId != 0)
                {
                    InspirationSpecification Spec =new InspirationSpecification();
                    Spec.InspirationSpecificationId = SpecDTO.InspirationSpecificationId;
                    Spec.InspirationId = Insp.InspirationId;
                    Spec.ComponentId = SpecDTO.ComponentId;
                    Spec.MaterialId = SpecDTO.MaterialId;
                    Spec.ColorId = SpecDTO.ColorId;
                    Spec.Location = SpecDTO.Location;
                    Spec.SizeX = SpecDTO.SizeX;
                    Spec.SizeY = SpecDTO.SizeY;
                    _context.InspirationSpecifications.Add(Spec);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    InspirationSpecification Spec = new InspirationSpecification
                    {
                        InspirationId = Insp.InspirationId,
                        ComponentId = SpecDTO.ComponentId,
                        MaterialId = SpecDTO.MaterialId,
                        ColorId = SpecDTO.ColorId,
                        Location = SpecDTO.Location,
                        SizeX = SpecDTO.SizeX,
                        SizeY = SpecDTO.SizeY
                    };
                    _context.InspirationSpecifications.Add(Spec);
                    await _context.SaveChangesAsync();
                }
            }
            _context.Entry(Insp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspirationExists(id))
                {
                    return "";//靈感失敗
                }
                else
                {
                    throw;
                }
            }
            return "更新靈感成功!";
        }

        // DELETE: api/Inspirations/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteInspiration(int id)
        {
            var inspiration = await _context.Inspirations.FindAsync(id);
            if (inspiration == null)
            {
                return "刪除靈感失敗";
            }
            try
            {
                var InspirationSpecifications = _context.InspirationSpecifications.Where(cat => cat.InspirationId == id).ToList();
                foreach (var cat in InspirationSpecifications)
                {
                    try
                    {
                        _context.InspirationSpecifications.Remove(cat);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        return "刪除靈感失敗";
                    }
                }
                _context.Inspirations.Remove(inspiration);
                string content = inspiration.ImageFileName;
                HttpClient client = new HttpClient();
                string imageUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("imageUrl").Value;
                HttpResponseMessage response = await client.PutAsync($"{imageUrl}/Inspiration/imageDelete", new StringContent(content, Encoding.UTF8, "text/plain"));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return "刪除靈感失敗";
            }
            return "刪除靈感成功";
        }

        private bool InspirationExists(int id)
        {
            return _context.Inspirations.Any(e => e.InspirationId == id);
        }
    }
}
