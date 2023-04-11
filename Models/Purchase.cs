using System;
using System.Collections.Generic;

namespace layering.Models
{
    public partial class Purchase
    {
        public int Pid { get; set; }
        public string? Pname { get; set; }
        public int? Purquantity { get; set; }

        public virtual Product PidNavigation { get; set; } = null!;
    }
}
