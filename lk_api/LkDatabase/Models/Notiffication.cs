using System;
using System.Collections.Generic;

namespace lk_api.LkDatabase.Models
{
    public partial class Notiffication
    {
        public int Id { get; set; }
        public DateOnly? NotifficationDate { get; set; }
        public TimeOnly? NotifficationTime { get; set; }
        public string? Text { get; set; }
        public bool? ReadStatus { get; set; }
        public int? AbonentId { get; set; }

        public virtual Abonent? Abonent { get; set; }
    }
}
