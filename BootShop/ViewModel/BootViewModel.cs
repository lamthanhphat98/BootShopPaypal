using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootShop.ViewModel
{
    public class BootViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Double? Price { get; set; }
        public String Picture { get; set; }
        public Boolean? Sex { get; set; }
        public Boolean? Sale { get; set; }
        public String Description { get; set; }
     
    }
}
