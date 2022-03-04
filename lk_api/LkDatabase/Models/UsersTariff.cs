using System;
using System.Collections.Generic;

namespace lk_api.LkDatabase.Models
{
    public partial class UsersTariff
    {
        public int AbonentId { get; set; }
        public int TariffId { get; set; }

        public virtual Abonent Abonent { get; set; } = null!;
        public virtual Tariff Tariff { get; set; } = null!;
    }
}
