using System;
using System.Collections.Generic;

namespace lk_db.LkDatabase.Models
{
    public partial class Device
    {
        public int Id { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public DateOnly VerificationPeriod { get; set; }
        public double LastIndication { get; set; }
        public DateOnly IndicationDate { get; set; }
        public int AbonentId { get; set; }
        public int Type { get; set; }

        public virtual Abonent Abonent { get; set; } = null!;
        public virtual DeviceType TypeNavigation { get; set; } = null!;
    }
}
