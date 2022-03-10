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
    }
}

