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
    public class AccuralsController : ControllerBase
    {
        private readonly lkDbContext _context;

        public AccuralsController(lkDbContext context)
        {
            _context = context;
        }

        // GET: api/Accurals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accural>>> GetAccurals()
        {
            return await _context.Accurals.ToListAsync();
        }

        // GET: api/Accurals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accural>> GetAccural(int id)
        {
            var accural = await _context.Accurals.FindAsync(id);

            if (accural == null)
            {
                return NotFound();
            }

            return accural;
        }

        // PUT: api/Accurals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccural(int id, Accural accural)
        {
            if (id != accural.Id)
            {
                return BadRequest();
            }

            _context.Entry(accural).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccuralExists(id))
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

        // POST: api/Accurals
        [HttpPost]
        public async Task<ActionResult<Accural>> PostAccural(Accural accural)
        {
            _context.Accurals.Add(accural);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccural", new { id = accural.Id }, accural);
        }

        // DELETE: api/Accurals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccural(int id)
        {
            var accural = await _context.Accurals.FindAsync(id);
            if (accural == null)
            {
                return NotFound();
            }

            _context.Accurals.Remove(accural);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccuralExists(int id)
        {
            return _context.Accurals.Any(e => e.Id == id);
        }
    }
}
