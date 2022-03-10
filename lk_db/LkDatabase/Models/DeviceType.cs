using System;
using System.Collections.Generic;

namespace lk_db.LkDatabase.Models
{
    public partial class DeviceType
    {
        public DeviceType()
        {
            Devices = new HashSet<Device>();
        }

        public int Id { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
