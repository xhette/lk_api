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
using lk_api.LkDatabase.ApiModels;

namespace lk_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private readonly lkDbContext _context;

        public TariffsController(lkDbContext context)
        {
            _context = context;
        }

        // GET: api/Tariffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbonentTariff>>> GetTariffs()
        {
            var tariffs = _context.Tariffs.Join(_context.UsersTariffs, 
                t => t.Id, 
                u => u.TariffId, (t, u) => new { 
                    t.Id, 
                    u.AbonentId,
                    t.CompanyId,
                    t.TariffName,
                    t.Payment,
                    t.Unit
                });

            return await tariffs.Join(_context.Companies, t => t.CompanyId, c => c.Id, (t, c) => new AbonentTariff
            {
                Id = t.Id,
                AbonentId = t.AbonentId,
                CompanyId = t.CompanyId,
                TariffName = t.TariffName,
                Payment = t.Payment,
                Unit = t.Unit,
                CompanyName = c.Name,
                CompanyAddress = c.Address,
                CompanyEmail = c.Email,
                CompanyPhone = c.Phone,
            }).ToListAsync();
        }
    }
}
