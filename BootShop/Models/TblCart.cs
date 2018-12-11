using System;
using System.Collections.Generic;

namespace BootShop.Models
{
    public partial class TblCart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string SizeName { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }

        public TblBoots Product { get; set; }
        public TblUser User { get; set; }
    }
}
