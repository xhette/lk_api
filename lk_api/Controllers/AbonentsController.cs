using lk_api.LkDatabase.Models;

using lk_db;
using lk_db.LkDatabase.Models;
using lk_api.UsersDatabase;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lk.DbLayer;

namespace lk_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonentsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly DatabaseRepository dbRepository;

        public AbonentsController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            dbRepository = new DatabaseRepository(configuration.GetConnectionString("LkDbConnection"));
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("Info")]
        public async Task<ActionResult<AbonentInfo>> GetAbonentInfo()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated || User.Identity.Name == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null || !user.AbonentId.HasValue)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователь не найден");
            }

            var abonentResult = await dbRepository.GetAbonent(user.AbonentId.Value);

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
                AbonentInfo abonentInfo = (AbonentInfo)abonentResult.InnerObject;
                return abonentInfo;
            }

        }

        [Authorize(Roles = "user")]
        [HttpPut]
        [Route("Info")]
        public async Task<ActionResult> PutAbonent(AbonentInfo abonent)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated || User.Identity.Name == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null || !user.AbonentId.HasValue)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователь не найден");
            }

            var abonentResult = await dbRepository.ChangeAbonent(user.AbonentId.Value);

            if (abonentResult.ResultCode == ResultCodeEnum.Error || abonentResult.InnerObject == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, abonentResult.InnerMessage);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, "Изменения успешно сохранены");
            }
        }
    }
}
