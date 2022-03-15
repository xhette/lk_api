using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lk.DbLayer.LkDatabase.Models
{
    public class AbonentDevice
    {
        public int Id { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public DateOnly VerificationPeriod { get; set; }
        public double LastIndication { get; set; }
        public DateOnly IndicationDate { get; set; }
        public int AbonentId { get; set; }
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
    }
}
