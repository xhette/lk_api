using System;
using System.Collections.Generic;

namespace lk_db.LkDatabase.Models
{
    public partial class UsersTariff
    {
        public int AbonentId { get; set; }
        public int TariffId { get; set; }

        public virtual Abonent Abonent { get; set; } = null!;
        public virtual Tariff Tariff { get; set; } = null!;
    }
}
