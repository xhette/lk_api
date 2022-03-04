#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lk_api;
using lk_api.LkDatabase.Models;

namespace lk_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypesController : ControllerBase
    {
        private readonly lkDbContext _context;

        public DeviceTypesController(lkDbContext context)
        {
            _context = context;
        }

        // GET: api/DeviceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceType>>> GetDeviceTypes()
        {
            return await _context.DeviceTypes.ToListAsync();
        }

        // GET: api/DeviceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceType>> GetDeviceType(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);

            if (deviceType == null)
            {
                return NotFound();
            }

            return deviceType;
        }

        // PUT: api/DeviceTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceType(int id, DeviceType deviceType)
        {
            if (id != deviceType.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceTypeExists(id))
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

        // POST: api/DeviceTypes
        [HttpPost]
        public async Task<ActionResult<DeviceType>> PostDeviceType(DeviceType deviceType)
        {
            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceType", new { id = deviceType.Id }, deviceType);
        }

        // DELETE: api/DeviceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceType(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound();
            }

            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceTypeExists(int id)
        {
            return _context.DeviceTypes.Any(e => e.Id == id);
        }
    }
}
