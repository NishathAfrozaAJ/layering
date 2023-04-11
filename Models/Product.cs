using System;
using System.Collections.Generic;

namespace layering.Models
{
    public partial class Product
    {
        public Product()
        {
            Purs = new HashSet<Pur>();
        }

        public int Pid { get; set; }
        public string? Pname { get; set; }
        public double? Pcost { get; set; }
        public int? Pquantity { get; set; }

        public virtual Purchase? Purchase { get; set; }
        public virtual ICollection<Pur> Purs { get; set; }
    }
}
