#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using lk_api.LkDatabase.Models;
using lk_db;
using lk_api.UsersDatabase;
using lk.DbLayer;

namespace lk_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly DatabaseRepository dbRepository;

        public DevicesController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            dbRepository = new DatabaseRepository(configuration.GetConnectionString("LkDbConnection"));
        }

        [Authorize]
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

            var abonentResult = await dbRepository.GetDevices(user.AbonentId.Value);

            if (abonentResult == null)
            {
                return NotFound();
            }

            if (abonentResult.ResultCode == ResultCodeEnum.Error || abonentResult.InnerObject == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, abonentResult.InnerMessage);
            }
            else
            {
                List<DeviceInfo> devices = abonentResult.InnerObject.Select(c => (DeviceInfo)c).ToList();
                return devices;
            }
        }
    }
}
