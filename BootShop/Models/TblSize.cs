using System;
using System.Collections.Generic;

namespace BootShop.Models
{
    public partial class TblSize
    {
        public TblSize()
        {
            TblBoots = new HashSet<TblBoots>();
        }

        public int SizeId { get; set; }
        public string SizeName { get; set; }

        public ICollection<TblBoots> TblBoots { get; set; }
    }
}
