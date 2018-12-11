using System;
using System.Collections.Generic;

namespace BootShop.Models
{
    public partial class TblOrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public TblOrder Order { get; set; }
        public TblBoots Product { get; set; }
    }
}
