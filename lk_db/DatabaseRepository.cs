using lk.DbLayer.LkDatabase.Models;

using lk_db;
using lk_db.LkDatabase.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lk.DbLayer
{
    public class DatabaseRepository
    {
        private readonly lkDbContext dbContext;
        public DatabaseRepository(lkDbContext context)
        {
            dbContext = context;
        }

        public DatabaseRepository(string connectionString)
        {
            dbContext = new lkDbContext(connectionString);
        }

        public async Task<DbRepoResult<Abonent>> GetAbonent(int id)
        {
            var abonent = await dbContext.Abonents.FindAsync(id);

            if (abonent == null)
            {
                return new DbRepoResult<Abonent>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = "Абонент не найден",
                    InnerObject = null
                };
            }

            return new DbRepoResult<Abonent>
            {
                ResultCode = ResultCodeEnum.Ok,
                InnerMessage = "",
                InnerObject = abonent
            };
        }

        public async Task<DbRepoResult<Abonent>> ChangeAbonent(int id, Abonent abonent)
        {
            if (id != abonent.Id)
            {
                return new DbRepoResult<Abonent>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = "Абонент не найден",
                };
            }

            dbContext.Entry(abonent).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new DbRepoResult<Abonent>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = ex.Message,
                    InnerObject = abonent
                };
            }

            return new DbRepoResult<Abonent>
            {
                ResultCode = ResultCodeEnum.Ok,
                InnerMessage = "",
                InnerObject = abonent
            };
        }

        // TO-DO: хуйня переделывай
        public async Task<DbRepoResult<IEnumerable<Tariff>>> GetTariffs(int abonentId)
        {
            try
            {
                var tariffs = await dbContext.Tariffs.ToListAsync();

                return new DbRepoResult<IEnumerable<Tariff>>
                {
                    InnerMessage = "",
                    InnerObject = tariffs.Where(a => a.Id == abonentId).ToList(),
                    ResultCode = ResultCodeEnum.Ok,
                };
            }
            catch (Exception ex)
            {
                return new DbRepoResult<IEnumerable<Tariff>>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = ex.Message,
                    InnerObject = null
                };
            }
        }
        public async Task<DbRepoResult<AbonentFincard>> GetFincard(int abonentId)
        {
            try
            {
                var fincard = await dbContext.Accurals.Where(a => a.Id == abonentId).ToListAsync();

                AbonentFincard abonentFincard = new AbonentFincard(fincard);
                abonentFincard.AbonentId = abonentId;

                if (fincard.Count > 0)
                {
                   var fincardLast = fincard.OrderBy(c => c.Year).OrderBy(c => c.Month).FirstOrDefault();

                    abonentFincard.Prepayment = fincardLast.Prepayment;
                    abonentFincard.Payment = fincardLast.Payment;
                    abonentFincard.Debt = fincardLast.Debt;
                }

                return new DbRepoResult<AbonentFincard>
                {
                    ResultCode = ResultCodeEnum.Ok,
                    InnerMessage = "",
                    InnerObject = abonentFincard
                };
            }
            catch (Exception ex)
            {
                return new DbRepoResult<AbonentFincard>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = ex.Message,
                    InnerObject = null
                };
            }
        }

        public async Task<DbRepoResult<IEnumerable<AbonentDevice>>> GetDevices(int abonentId)
        {
            try
            {
                var devices = await dbContext.Devices.Where(d => d.AbonentId == abonentId)
                    .Join(dbContext.DeviceTypes, d => d.Type, t => t.Id, (d, t) => new AbonentDevice
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

                return new DbRepoResult<IEnumerable<AbonentDevice>>
                {
                    ResultCode = ResultCodeEnum.Ok,
                    InnerMessage = "",
                    InnerObject = devices
                };
            }
            catch (Exception ex)
            {
                return new DbRepoResult<IEnumerable<AbonentDevice>>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = ex.Message,
                    InnerObject = null
                };
            }
        }

        public async Task<DbRepoResult<Device>> ChangeDevice(Device device, int id)
        {
            if (id != device.Id)
            {
                return new DbRepoResult<Device>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = "П/У не найден",
                };
            }

            dbContext.Entry(device).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new DbRepoResult<Device>
                {
                    ResultCode = ResultCodeEnum.Error,
                    InnerMessage = ex.Message,
                    InnerObject = device
                };
            }

            return new DbRepoResult<Device>
            {
                ResultCode = ResultCodeEnum.Ok,
                InnerMessage = "",
                InnerObject = device
            };
        }
    }
}

