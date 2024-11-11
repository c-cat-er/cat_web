using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modpackApi.DTO;
using modpackApi.Models;

namespace modpackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreLocationsDTO_RESTController : ControllerBase
    {
        private readonly ModPackContext _context;
        private readonly IMapper _mapper;

        public StoreLocationsDTO_RESTController(ModPackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region List
        // GET: api/StoreLocationsDTO_REST
        
        [HttpGet("LE")]
        public async Task<ActionResult<IEnumerable<StoreLocationDTO>>> GetStoreLocations()
        {///use LINE Select + 不手動 json 序列.

            return await _context.StoreLocations
                .Select(a => new StoreLocationDTO
                {
                    Name = a.Name,
                    ///...
                })
                .ToListAsync();
        }

        [HttpGet("LJ")]
        public async Task<ActionResult<string>> GetStoreLocationsLJ()
        {///use LINE Select + 手動 json 序列.

            return System.Text.Json.JsonSerializer.Serialize(await _context.StoreLocations
                .Select(a => new StoreLocationDTO
                {
                    Name = a.Name,
                    ///...
                })
                .ToListAsync());
        }

        [HttpGet("AE")]
        public async Task<ActionResult<IEnumerable<StoreLocationDTO>>> GetStoreLocationsAE()
        {///use AutoMapper 映射全部屬性 + 不手動 json 序列.

            return await _context.StoreLocations
                .ProjectTo<StoreLocationDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [HttpGet("AJ")]
        public async Task<ActionResult<string>> GetStoreLocationsAJ()
        {///use AutoMapper 映射全部屬性 + 手動 json 序列.

            return System.Text.Json.JsonSerializer.Serialize(await _context.StoreLocations
                .ProjectTo<StoreLocationDTO>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }
        #endregion

        #region GET id
        // GET: api/StoreLocationsDTO_REST/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetStoreLocation(int id)
        {
            var storeLocation = await _context.StoreLocations.FindAsync(id);
            if (storeLocation == null) return NotFound();
            var storeLocationDTO = _mapper.Map<StoreLocationDTO>(storeLocation);
            var serializedStoreLocationDTO = System.Text.Json.JsonSerializer.Serialize(storeLocationDTO);
            return serializedStoreLocationDTO;
        }

        [HttpGet("Keywrod/{keyword}")]
        public async Task<ActionResult<string>> SearchStoreLocation(string keyword)
        {///one keyword.

            return System.Text.Json.JsonSerializer.Serialize(await _context.StoreLocations
                .Where(a => a.Name.Contains(keyword) || a.OfficeTelephone.Contains(keyword) || a.Address.Contains(keyword))
                .ProjectTo<StoreLocationDTO>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }

        [HttpGet("Search/{Name}/{Address}")]
        public async Task<ActionResult<string>> SearchStoreLocation(string Name, string Address)
        {///mult-keyword.

            IQueryable<StoreLocation> query = _context.StoreLocations;

            ///use Code search.
            if (!string.IsNullOrEmpty(Name))
                query = query.Where(a => a.Name.Contains(Name));  //k-p

            ///use Name search.
            if (!string.IsNullOrEmpty(Address))
                query = query.Where(a => a.Address.Contains(Address));

            var result = await query.ToListAsync();
            return System.Text.Json.JsonSerializer.Serialize(result);
        }
        #endregion

        #region PUT / PATCH
        // PUT: api/StoreLocationsDTO_REST/5
        /*
        [HttpPost("{id}")]
        public async Task<IActionResult> PostStoreLocation(int id, [FromBody] StoreLocationDTO storeLocationDTO)
        {
            if (id != storeLocationDTO.StoreLocationId) return BadRequest();
            var existingStoreLocation = await _context.StoreLocations.FindAsync(id);
            if (existingStoreLocation == null) return NotFound();
            
            ///DTO to DB-Model.
            _mapper.Map(storeLocationDTO, existingStoreLocation);
            _context.StoreLocations.Update(existingStoreLocation);
            await _context.SaveChangesAsync();

            return NoContent(); // 返回 204 No Content 表示更新成功
        }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreLocation(int id, [FromBody] StoreLocationDTO storeLocationDTO)
        {
            if (id != storeLocationDTO.StoreLocationId) return BadRequest();
            var existingStoreLocation = await _context.StoreLocations.FindAsync(id);
            if (existingStoreLocation == null) return NotFound();

            ///DTO to DB-Model.
            _mapper.Map(storeLocationDTO, existingStoreLocation);
            _context.StoreLocations.Update(existingStoreLocation);
            await _context.SaveChangesAsync();

            return NoContent(); // 返回 204 No Content 表示更新成功
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<StoreLocationDTO> patchDoc)
        {
            if (patchDoc == null) return BadRequest();
            var storeLocation = await _context.StoreLocations.FindAsync(id);
            if (storeLocation == null) return NotFound();
             
            var storeDto = _mapper.Map<StoreLocationDTO>(storeLocation);

            // JSON Patch 應用到 DB-Model.
            ///patchDoc.ApplyTo(storeLocation, ModelState);  ///error ?
            patchDoc.ApplyTo(storeDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        // POST: api/StoreLocationsDTO_REST
        [HttpPost]
        public async Task<ActionResult<StoreLocationDTO>> PostStoreLocation(StoreLocationDTO storeLocationDTO)
        {
            ///將接收到的 DTO 對象映射為 DB-Model.
            var newStoreLocation = _mapper.Map<StoreLocation>(storeLocationDTO);
            _context.StoreLocations.Add(newStoreLocation);
            await _context.SaveChangesAsync();
            var createdStoreLocationDTO = _mapper.Map<StoreLocationDTO>(newStoreLocation);
            return CreatedAtAction(nameof(GetStoreLocation), new { id = createdStoreLocationDTO.StoreLocationId }, createdStoreLocationDTO);
        }

        // DELETE: api/StoreLocationsDTO_REST/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreLocation(int id)
        {
            var storeLocation = await _context.StoreLocations.FindAsync(id);
            if (storeLocation == null) return NotFound();
            _context.StoreLocations.Remove(storeLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreLocationExists(int id)
        {
            return _context.StoreLocations.Any(e => e.StoreLocationId == id);
        }
    }
}
