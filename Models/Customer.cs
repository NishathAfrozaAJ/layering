using System;
using System.Collections.Generic;

namespace layering.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Purs = new HashSet<Pur>();
        }

        public int Cid { get; set; }
        public string? Cname { get; set; }
        public string? Pass { get; set; }
        public string? Cpass { get; set; }

        public virtual ICollection<Pur> Purs { get; set; }
    }
}
