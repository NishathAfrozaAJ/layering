using System;
using System.Collections.Generic;

namespace layering.Models
{
    public partial class Pur
    {
        public int Puid { get; set; }
        public int? Pid { get; set; }
        public string? Pname { get; set; }
        public int? Purquantity { get; set; }
        public int? TotalPrice { get; set; }
        public int? Cid { get; set; }
        public bool? Flag { get; set; }

        public virtual Customer? CidNavigation { get; set; }
        public virtual Product? PidNavigation { get; set; }
    }
}
