using lk.DbLayer.LkDatabase.Models;

namespace lk_api.LkDatabase.Models
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

        public static explicit operator DeviceInfo (AbonentDevice device)
        {
            if (device == null)
                return null;
            else return new DeviceInfo
            {
                Id = device.Id,
                AbonentId = device.AbonentId,
                DeviceNumber = device.DeviceNumber,
                VerificationPeriod = device.VerificationPeriod,
                LastIndication = device.LastIndication,
                IndicationDate = device.IndicationDate,
                TypeId = device.TypeId,
                TypeName = device.TypeName,
            };
        }
    }
}
