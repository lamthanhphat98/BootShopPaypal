using System;
using System.Collections.Generic;

namespace BootShop.Models
{
    public partial class TblColor
    {
        public TblColor()
        {
            TblBoots = new HashSet<TblBoots>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public ICollection<TblBoots> TblBoots { get; set; }
    }
}
