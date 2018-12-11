using System;
using System.Collections.Generic;

namespace BootShop.Models
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderDetails = new HashSet<TblOrderDetails>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime? OrderDate { get; set; }

        public TblUser User { get; set; }
        public ICollection<TblOrderDetails> TblOrderDetails { get; set; }
    }
}
