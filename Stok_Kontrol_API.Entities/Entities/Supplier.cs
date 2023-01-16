using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stok_Kontrol_API.Entities.Entities
{
    public class Supplier : BaseEntity
    {
        public Supplier()
        {
            Ürünler = new List<Product>();
        }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual List<Product> Ürünler { get; set; }
    }
}
