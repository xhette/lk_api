using lk_api.LkDatabase.Models;

using lk_db;
using lk_db.LkDatabase.Models;
using lk_api.UsersDatabase;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lk.DbLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using lk.API.Models;

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

        [Authorize]
        [HttpGet]
        [Route("Info")]
        public async Task<ActionResult<AbonentInfo>> GetAbonentInfo()
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

        [Authorize]
        [HttpGet]
        [Route("Info/Fincard")]
        public async Task<ActionResult<Fincard>> GetFincard()
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

            var abonentResult = await dbRepository.GetFincard(user.AbonentId.Value);

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
                Fincard fincard = (Fincard)abonentResult.InnerObject;
                return fincard;
            }

        }

        [Authorize]
        [HttpPut]
        [Route("Info")]
        public async Task<ActionResult> PutAbonent(AbonentInfo abonent)
        {
            if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated || HttpContext.User.Identity.Name == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (user == null || !user.AbonentId.HasValue)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователь не найден");
            }

            var abonentResult = await dbRepository.ChangeAbonent(user.AbonentId.Value, (Abonent)abonent);

            if (abonentResult.ResultCode == ResultCodeEnum.Error || abonentResult.InnerObject == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, abonentResult.InnerMessage);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, "Изменения успешно сохранены");
            }
        }

        //[Authorize]
        //[HttpPut]
        //[Route("Info/Tariffs")]
        //public async Task<ActionResult<IEnumerable<AbonentTariff>>> GetTariffs()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return StatusCode(StatusCodes.Status401Unauthorized, "Пользователь не авторизован");
        //    }

        //    var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        //    if (user == null || !user.AbonentId.HasValue)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Пользователь не найден");
        //    }

        //    var tariffsResult = await dbRepository.GetTariffs(user.AbonentId.Value);

        //    if (tariffsResult == null)
        //    {
        //        return NotFound();
        //    }
        //    if (tariffsResult.ResultCode == ResultCodeEnum.Error || tariffsResult.InnerObject == null)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, tariffsResult.InnerMessage);
        //    }
        //    else
        //    {
        //        var tariffs = tariffsResult.InnerObject.Select(c => (AbonentTariff)c).ToList();
        //    }
        //}
    }
}
