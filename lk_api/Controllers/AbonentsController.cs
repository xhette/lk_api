using lk_api.LkDatabase.ApiModels;
using lk_api.LkDatabase.Models;
using lk_api.UsersDatabase;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lk_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonentsController : ControllerBase
    {
        private readonly lkDbContext _context;
        private readonly UserManager<User> _userManager;

        public AbonentsController(lkDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Abonents
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonent>>> GetAbonents()
        {
            return await _context.Abonents.ToListAsync();
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("abonentinfo")]
        public async Task<ActionResult<AbonentInfo>> GetAbonentInfo()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
            }

            User? user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не найден");

            var abonent = _context.Abonents.Where(a => a.PersonalNumber == user.PhoneNumber).FirstOrDefault();

            if (abonent == null)
            {
                return NotFound();
            }

            AbonentInfo abonentInfo = (AbonentInfo)abonent;

            return abonentInfo;
        }

        [Authorize(Roles = "user")]
        [HttpPut]
        [Route("abonentinfo")]
        public async Task<ActionResult> PutAbonent(AbonentInfo abonent)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
            }
            User? user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не найден");

            int id = _context.Abonents.Where(a => a.PersonalNumber == user.PhoneNumber).FirstOrDefault().Id;

            if (id != abonent.Id)
            {
                return BadRequest();
            }

            _context.Entry(abonent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonentExists(id))
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Abonent>> PostAbonent(AbonentInfo abonent)
        {
            _context.Abonents.Add((Abonent)abonent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbonent", new { id = abonent.Id }, abonent);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbonent(int id)
        {
            var abonent = await _context.Abonents.FindAsync(id);
            if (abonent == null)
            {
                return NotFound();
            }

            _context.Abonents.Remove(abonent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbonentExists(int id)
        {
            return _context.Abonents.Any(e => e.Id == id);
        }
    }
}
