using System;
using System.Collections.Generic;

namespace lk_db.LkDatabase.Models
{
    public partial class Tariff
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string TariffName { get; set; } = null!;
        public decimal? Payment { get; set; }
        public string? Unit { get; set; }

        public virtual Company Company { get; set; } = null!;
    }
}
