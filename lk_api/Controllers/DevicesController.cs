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

            


            return devices;
        }
    }
}
