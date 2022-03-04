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
    public class NotifficationsController : ControllerBase
    {
        private readonly lkDbContext _context;

        public NotifficationsController(lkDbContext context)
        {
            _context = context;
        }

        // GET: api/Notiffications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notiffication>>> GetNotiffications()
        {
            return await _context.Notiffications.ToListAsync();
        }

        // GET: api/Notiffications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notiffication>> GetNotiffication(int id)
        {
            var notiffication = await _context.Notiffications.FindAsync(id);

            if (notiffication == null)
            {
                return NotFound();
            }

            return notiffication;
        }

        // PUT: api/Notiffications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotiffication(int id, Notiffication notiffication)
        {
            if (id != notiffication.Id)
            {
                return BadRequest();
            }

            _context.Entry(notiffication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotifficationExists(id))
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

        // POST: api/Notiffications
        [HttpPost]
        public async Task<ActionResult<Notiffication>> PostNotiffication(Notiffication notiffication)
        {
            _context.Notiffications.Add(notiffication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotiffication", new { id = notiffication.Id }, notiffication);
        }

        // DELETE: api/Notiffications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotiffication(int id)
        {
            var notiffication = await _context.Notiffications.FindAsync(id);
            if (notiffication == null)
            {
                return NotFound();
            }

            _context.Notiffications.Remove(notiffication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotifficationExists(int id)
        {
            return _context.Notiffications.Any(e => e.Id == id);
        }
    }
}
