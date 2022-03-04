using System;
using System.Collections.Generic;

namespace lk_api.LkDatabase.Models
{
    public partial class Company
    {
        public Company()
        {
            Tariffs = new HashSet<Tariff>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Tariff> Tariffs { get; set; }
    }
}
