#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using lk.DbLayer;

using lk_api.LkDatabase.Models;
using lk_api.UsersDatabase;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lk_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly DatabaseRepository dbRepository;

        public TariffsController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            dbRepository = new DatabaseRepository(configuration.GetConnectionString("LkDbConnection"));
        }

        // GET: api/Tariffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbonentTariff>>> GetTariffs()
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

            var abonentResult = await dbRepository.GetTariffs(user.AbonentId.Value);

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
                List<AbonentTariff> devices = abonentResult.InnerObject.Select(c => (AbonentTariff)c).ToList();
                return devices;
            }
        }
    }
}
