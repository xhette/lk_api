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
    }
}
