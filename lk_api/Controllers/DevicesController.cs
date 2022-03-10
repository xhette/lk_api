#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using lk_api.LkDatabase.Models;
using lk_db;
using lk_api.UsersDatabase;

namespace lk_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly lkDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DevicesController(lkDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceInfo>>> GetDevices()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
            }

            User user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не найден");
            }

            var abonent = _context.Abonents.Where(a => a.PersonalNumber == user.PhoneNumber).FirstOrDefault();

            if (abonent == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Абонент не найден");
            }

            var devices = await _context.Devices.Where(d => d.AbonentId == abonent.Id)
                .Join(_context.DeviceTypes, d => d.Type, t => t.Id, (d,t) => new DeviceInfo
                {
                    Id = d.Id,
                    AbonentId = d.AbonentId,
                    DeviceNumber = d.DeviceNumber,
                    IndicationDate = d.IndicationDate,
                    LastIndication = d.LastIndication,
                    VerificationPeriod = d.VerificationPeriod,
                    TypeId = d.Type,
                    TypeName = t.TypeName
                }).ToListAsync();


            return devices;
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
