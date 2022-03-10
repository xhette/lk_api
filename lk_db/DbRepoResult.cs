using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lk.DbLayer
{
    public class DbRepoResult<T> where T : class
    {
        public ResultCodeEnum ResultCode { get; set; }
        public string InnerMessage { get; set; }
        public T? InnerObject { get; set; }
    }
}
