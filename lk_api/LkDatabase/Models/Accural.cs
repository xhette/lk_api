using System;
using System.Collections.Generic;

namespace lk_api.LkDatabase.Models
{
    public partial class Accural
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Debt { get; set; }
        public decimal Prepayment { get; set; }
        public decimal Payment { get; set; }
        public decimal Fine { get; set; }
        public int AbonentId { get; set; }

        public virtual Abonent Abonent { get; set; } = null!;
    }
}
