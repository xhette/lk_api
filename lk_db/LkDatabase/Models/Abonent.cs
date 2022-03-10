using System;
using System.Collections.Generic;

namespace lk_db.LkDatabase.Models
{
    public partial class Abonent
    {
        public Abonent()
        {
            Accurals = new HashSet<Accural>();
            Devices = new HashSet<Device>();
            Notiffications = new HashSet<Notiffication>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string PersonalNumber { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool? SendReceiptEmail { get; set; }
        public bool? SendReceiptPost { get; set; }

        public virtual ICollection<Accural> Accurals { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Notiffication> Notiffications { get; set; }
    }
}
