using lk.DbLayer.LkDatabase.Models;

using lk_db.LkDatabase.Models;

namespace lk_api.LkDatabase.Models
{
    public class AbonentTariff
    {
        public int Id { get; set; }
        public int AbonentId { get; set; }
        public int CompanyId { get; set; }
        public string TariffName { get; set; } = null!;
        public decimal? Payment { get; set; }
        public string? Unit { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyEmail { get; set; }

        public static explicit operator AbonentTariff (TariffView tariff)
        {
            if (tariff == null)
                return null;
            else return new AbonentTariff
            {
                Id = tariff.Id,
                AbonentId = tariff.AbonentId,
                TariffName = string.IsNullOrEmpty(tariff.TariffName) ? "" : tariff.TariffName,
                Payment = tariff.Payment,
                Unit = tariff.Unit,
                CompanyId = tariff.CompanyId,
                CompanyName = tariff.CompanyName,
                CompanyAddress = tariff.CompanyAddress,
                CompanyPhone = tariff.CompanyPhone,
                CompanyEmail = tariff.CompanyEmail,
            };
        }
    }
}
