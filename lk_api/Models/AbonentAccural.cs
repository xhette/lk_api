using lk_db.LkDatabase.Models;

namespace lk.API.Models
{
    public class AbonentAccural
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Debt { get; set; }
        public decimal Prepayment { get; set; }
        public decimal Payment { get; set; }
        public decimal Fine { get; set; }
        public int AbonentId { get; set; }

        public static explicit operator AbonentAccural(Accural accural)
        {
            if (accural == null)
                return null;
            else return new AbonentAccural
            {
                Id = accural.Id,
                AbonentId = accural.AbonentId,
                Year = accural.Year,
                Month = accural.Month,
                Debt = accural.Debt,
                Prepayment = accural.Prepayment,
                Payment = accural.Payment,
                Fine = accural.Fine,
            };
        }
    }
}
