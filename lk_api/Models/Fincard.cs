using lk.DbLayer.LkDatabase.Models;

namespace lk.API.Models
{
    public class Fincard
    {
        public int AbonentId { get; set; }
        public List<AbonentAccural> Accurals { get; set; }
        public decimal Debt { get; set; }
        public decimal Prepayment { get; set; }
        public decimal Payment { get; set; }

        public static explicit operator Fincard (AbonentFincard fincard)
        {
            if (fincard == null)
                return null;
            else return new Fincard
            {
                AbonentId = fincard.AbonentId,
                Debt = fincard.Debt,
                Prepayment = fincard.Prepayment,
                Payment = fincard.Payment,
                Accurals = fincard.Accurals.Select(c => (AbonentAccural)c).ToList(),
            };
        }
    }
}
