namespace lk_api.LkDatabase.ApiModels
{
    public class DeviceInfo
    {
        public int Id { get; set; }
        public string DeviceNumber { get; set; } = null!;
        public DateOnly VerificationPeriod { get; set; }
        public double LastIndication { get; set; }
        public DateOnly IndicationDate { get; set; }
        public int AbonentId { get; set; }
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
    }
}
