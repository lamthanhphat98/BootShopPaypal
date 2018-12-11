using System;
using System.Collections.Generic;

namespace BootShop.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblCart = new HashSet<TblCart>();
            TblOrder = new HashSet<TblOrder>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }

        public ICollection<TblCart> TblCart { get; set; }
        public ICollection<TblOrder> TblOrder { get; set; }
    }
}
