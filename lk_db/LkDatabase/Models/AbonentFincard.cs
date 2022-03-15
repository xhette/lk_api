using lk_db.LkDatabase.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lk.DbLayer.LkDatabase.Models
{
    public class AbonentFincard
    {
        public int AbonentId { get; set; }
        public List<Accural> Accurals { get; set; }
        public decimal Debt { get; set; }
        public decimal Prepayment { get; set; }
        public decimal Payment { get; set; }

        public AbonentFincard()
        {
            Accurals = new List<Accural>();
        }

        public AbonentFincard(List<Accural> accurals)
        {
            Accurals = new List<Accural>(accurals);
        }
    }
}
