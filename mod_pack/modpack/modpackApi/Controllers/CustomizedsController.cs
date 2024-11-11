using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
    public class CustomizedsController : ControllerBase
    {
        private readonly ModPackContext _context;

        public CustomizedsController(ModPackContext context)
        {
            _context = context;
        }

        // GET: api/Customizeds
        [HttpGet]
        public async Task<string> GetCustomizeds()
        {
            return JsonSerializer.Serialize(await _context.Customizeds
             .Include(e => e.Promotion)
               .Include(e => e.Member)
             .Select(e => new CustomizedDTO
             {
                 CustomizedId = e.CustomizedId,
                 MemberId = e.MemberId,
                 MemberName = e.Member.Name,
                 PromotionId = e.PromotionId,
                 PromotionName = e.Promotion.Name,
                 Name = e.Name,
                 ImageFileName = e.ImageFileName,
                 SalePrice = (float)e.SalePrice,
                 CustomizedSpecs = _context.CustomizedSpecifications
                 .Where(cat => cat.CustomizedId == e.CustomizedId)
                 .Select(cat => new CustomizedDTO.CustomizedSpec
                 {
                     CustomizedSpecificationId = cat.CustomizedSpecificationId,
                     ComponentId = cat.ComponentId,
                     MaterialId = cat.MaterialId,
                     ColorId = cat.ColorId,
                     Location = cat.Location,
                     SizeX = cat.SizeX,
                     SizeY = cat.SizeY
                 }).ToList(),
                 CustomizedComponents = _context.Components
                 .Where(cat => e.CustomizedSpecifications.Any(spec => spec.ComponentId == cat.ComponentId))
                 .Select(cat => new CustomizedDTO.CustomizedComponent
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
        public async Task<string> GetCustomized(string key)
        {
            return JsonSerializer.Serialize(await _context.Customizeds.Include(e => e.Promotion).Include(e => e.Member)
                 .Where(e => e.Name.Contains(key) || e.Promotion.Name.Contains(key) || e.Member.Name.Contains(key))
                 .Select(e => new CustomizedDTO
                 {
                     CustomizedId = e.CustomizedId,
                     MemberId = e.MemberId,
                     MemberName = e.Member.Name,
                     PromotionId = e.PromotionId,
                     PromotionName = e.Promotion.Name,
                     Name = e.Name,
                     ImageFileName = e.ImageFileName,
                     SalePrice = (float)e.SalePrice,
                     CustomizedSpecs = _context.CustomizedSpecifications
                     .Where(cat => cat.CustomizedId == e.CustomizedId)
                     .Select(cat => new CustomizedDTO.CustomizedSpec
                     {
                         CustomizedSpecificationId = cat.CustomizedSpecificationId,
                         ComponentId = cat.ComponentId,
                         MaterialId = cat.MaterialId,
                         ColorId = cat.ColorId,
                         Location = cat.Location,
                         SizeX = cat.SizeX,
                         SizeY = cat.SizeY
                     }).ToList(),
                     CustomizedComponents = _context.Components
                     .Where(cat => e.CustomizedSpecifications.Any(spec => spec.ComponentId == cat.ComponentId))
                     .Select(cat => new CustomizedDTO.CustomizedComponent
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

        // GET: api/Customizeds/5
        [HttpGet("Id/{id}")]
        public async Task<string> GetCustomized(int id)
        {
            return JsonSerializer.Serialize(await _context.Customizeds
                .Include(e => e.Promotion)
                .Include(e => e.Member)
                .Where(e => e.CustomizedId == id)
                .Select(e => new CustomizedDTO
                {
                    CustomizedId = e.CustomizedId,
                    MemberId = e.MemberId,
                    MemberName = e.Member.Name,
                    PromotionId = e.PromotionId,
                    PromotionName = e.Promotion.Name,
                    Name = e.Name,
                    ImageFileName = e.ImageFileName,
                    SalePrice = (float)e.SalePrice,
                    CustomizedSpecs = _context.CustomizedSpecifications
                    .Where(cat => cat.CustomizedId == e.CustomizedId)
                    .Select(cat => new CustomizedDTO.CustomizedSpec
                    {
                        CustomizedSpecificationId = cat.CustomizedSpecificationId,
                        ComponentId = cat.ComponentId,
                        MaterialId = cat.MaterialId,
                        ColorId = cat.ColorId,
                        Location = cat.Location,
                        SizeX = cat.SizeX,
                        SizeY = cat.SizeY
                    }).ToList(),
                    CustomizedComponents = _context.Components
                    .Where(cat => e.CustomizedSpecifications.Any(spec => spec.ComponentId == cat.ComponentId))
                    .Select(cat => new CustomizedDTO.CustomizedComponent
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

        // POST: api/Customizeds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostCustomized()
        {
            var CustDTO = JsonSerializer.Deserialize<CustomizedDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            Customized Cust = new Customized
            {
                CustomizedId = CustDTO.CustomizedId,
                MemberId = CustDTO.MemberId,
                PromotionId = CustDTO.PromotionId,
                Name = CustDTO.Name,
                ImageFileName = CustDTO.ImageFileName,
                SalePrice = (decimal)CustDTO.SalePrice,
            };
            try
            {
                _context.Customizeds.Add(Cust);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return "";//客製化失敗
            }
            foreach (var item in CustDTO.CustomizedSpecs)
            {
                CustomizedSpecification Spec = new CustomizedSpecification
                {
                    CustomizedId = Cust.CustomizedId,
                    ComponentId = item.ComponentId,
                    MaterialId = item.MaterialId,
                    ColorId = item.ColorId,
                    Location = item.Location,
                    SizeX = item.SizeX,
                    SizeY = item.SizeY
                };
                try
                {
                    _context.CustomizedSpecifications.Add(Spec);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Customizeds.Remove(Cust);
                    await _context.SaveChangesAsync();
                    return "";//客製化失敗
                }
            }
            return $"新增客製化成功";
        }

        // PUT: api/Customizeds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutCustomized(int id)
        {
            var CustDTO = JsonSerializer.Deserialize<CustomizedDTO>(new StreamReader(Request.Body).ReadToEndAsync().Result);
            if (id != CustDTO.CustomizedId)
            {
                return "";//客製化失敗
            }
            Customized Cust = await _context.Customizeds.FindAsync(id);
            Cust.CustomizedId = CustDTO.CustomizedId;
            Cust.MemberId = CustDTO.MemberId;
            Cust.PromotionId = CustDTO.PromotionId;
            Cust.Name = CustDTO.Name;
            Cust.ImageFileName = CustDTO.ImageFileName;
            Cust.SalePrice = (decimal)CustDTO.SalePrice;
            var x = _context.CustomizedSpecifications
                .Where(c => c.CustomizedId == id).ToList();
            var y = x.Where(c1 => !CustDTO.CustomizedSpecs.Any(c2 => c2.CustomizedSpecificationId == c1.CustomizedSpecificationId)).ToList();
            var missingSpecifications = _context.CustomizedSpecifications
                .Where(c => c.CustomizedId == id).ToList()
                .Where(c1 => !CustDTO.CustomizedSpecs
                .Any(c2 => c2.CustomizedSpecificationId == c1.CustomizedSpecificationId)).ToList();
            foreach (var item in missingSpecifications)
            {
                try
                {
                    _context.CustomizedSpecifications.Remove(item);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return "";//客製化失敗
                }
            }
            foreach (var SpecDTO in CustDTO.CustomizedSpecs)
            {
                if (SpecDTO.CustomizedSpecificationId != 0)
                {
                    CustomizedSpecification spec=new CustomizedSpecification();
                    spec.CustomizedId = Cust.CustomizedId;
                    spec.ComponentId = SpecDTO.ComponentId;
                    spec.MaterialId = SpecDTO.MaterialId;
                    spec.ColorId = SpecDTO.ColorId;
                    spec.Location = SpecDTO.Location;
                    spec.SizeX = SpecDTO.SizeX;
                    spec.SizeY = SpecDTO.SizeY;
                    _context.CustomizedSpecifications.Add(spec);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    CustomizedSpecification Spec = new CustomizedSpecification
                    {
                        CustomizedId = Cust.CustomizedId,
                        ComponentId = SpecDTO.ComponentId,
                        MaterialId = SpecDTO.MaterialId,
                        ColorId = SpecDTO.ColorId,
                        Location = SpecDTO.Location,
                        SizeX = SpecDTO.SizeX,
                        SizeY = SpecDTO.SizeY
                    };
                    _context.CustomizedSpecifications.Add(Spec);
                    await _context.SaveChangesAsync();
                }
            }
            _context.Entry(Cust).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomizedExists(id))
                {
                    return "";//客製化失敗
                }
                else
                {
                    throw;
                }
            }
            return "更新客製化成功!";
        }

        // DELETE: api/Customizeds/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteCustomized(int id)
        {
            var Customized = await _context.Customizeds.FindAsync(id);
            if (Customized == null)
            {
                return null;
            }
            try
            {
                var CustomizedSpecifications = _context.CustomizedSpecifications.Where(cat => cat.CustomizedId == id).ToList();
                foreach (var cat in CustomizedSpecifications)
                {
                    try
                    {
                        _context.CustomizedSpecifications.Remove(cat);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        return null;
                    }
                }
                _context.Customizeds.Remove(Customized);
                await _context.SaveChangesAsync();
                string content = Customized.ImageFileName;
                HttpClient client = new HttpClient();
                string imageUrl = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("imageUrl").Value;
                HttpResponseMessage response = await client.PutAsync($"{imageUrl}/Customized/imageDelete", new StringContent(content, Encoding.UTF8, "text/plain"));
            }
            catch (DbUpdateException ex)
            {
                return null;
            }
            return "刪除客製化成功";
        }

        private bool CustomizedExists(int id)
        {
            return _context.Customizeds.Any(e => e.CustomizedId == id);
        }

        [HttpGet("Init")]
        public async Task<string> initGet()
        {
            InitunityCustomizeds initunityCustomizeds = new InitunityCustomizeds();
            foreach (var item in from p in _context.Components select p)
            {
                if (item.IsCustomized == true)
                {
                    initunityCustomizeds.ComponentID.Add(item.ComponentId);
                    initunityCustomizeds.ComponentCategory.Add(item.Category);
                    initunityCustomizeds.ComponentFbxfileName.Add(item.FbxfileName);
                }
            }
            foreach (var item in from p in _context.Materials select p)
            {
                initunityCustomizeds.MaterialID.Add(item.MaterialId);
                initunityCustomizeds.MaterialFileName.Add(item.ImageFileName);
            }
            foreach (var item in from p in _context.Colors select p)
            {
                initunityCustomizeds.ColorID.Add(item.ColorId);
                initunityCustomizeds.ColorRGB.Add(item.Rgb);
            }
            var returnJson = JsonSerializer.Serialize(initunityCustomizeds);
            return returnJson;
        }
        public class InitunityCustomizeds
        {
            public List<int> ComponentID { get; set; } = new List<int>();
            public List<string> ComponentCategory { get; set; } = new List<string>();
            public List<string> ComponentFbxfileName { get; set; } = new List<string>();
            public List<int> MaterialID { get; set; } = new List<int>();
            public List<string> MaterialFileName { get; set; } = new List<string>();
            public List<int> ColorID { get; set; } = new List<int>();
            public List<string> ColorRGB { get; set; } = new List<string>();
        }
    }
}
