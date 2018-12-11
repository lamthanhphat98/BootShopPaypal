using System;
using System.Collections.Generic;

namespace BootShop.Models
{
    public partial class TblBoots
    {
        public TblBoots()
        {
            TblCart = new HashSet<TblCart>();
            TblOrderDetails = new HashSet<TblOrderDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Picture { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public bool? Sale { get; set; }
        public bool? Sex { get; set; }
        public int? Quantity { get; set; }

        public TblColor Color { get; set; }
        public TblSize Size { get; set; }
        public ICollection<TblCart> TblCart { get; set; }
        public ICollection<TblOrderDetails> TblOrderDetails { get; set; }
    }
}
