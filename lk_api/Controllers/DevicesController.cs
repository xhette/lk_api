#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using lk_api.LkDatabase.Models;
using lk_db;
using lk_api.UsersDatabase;
using lk.DbLayer;
using lk_db.LkDatabase.Models;

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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddLastIndication(double lastIndication, int deviceId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (user == null || !user.AbonentId.HasValue)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователь не найден");
            }

            var abonentResult = await dbRepository.GetDevices(user.AbonentId.Value);

            if (abonentResult == null)
            {
                return NotFound();
            }

            if (abonentResult.ResultCode == ResultCodeEnum.Error || abonentResult.InnerObject == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при поиске П/У. " + abonentResult.InnerMessage);
            }
            else
            {
                DeviceInfo device = abonentResult.InnerObject.Select(c => (DeviceInfo)c).ToList().Find(c => c.Id == deviceId);

                if (device == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при поиске П/У.");
                }

                device.LastIndication = lastIndication;

                var result = await dbRepository.ChangeDevice((Device)device, deviceId);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (abonentResult.ResultCode == ResultCodeEnum.Error || abonentResult.InnerObject == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, abonentResult.InnerMessage);
                }

                return StatusCode(StatusCodes.Status200OK);
            }
        }
    }
}
