using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stok_Kontrol_API.Entities.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short? Stock { get; set; }
        public DateTime? ExpireDate { get; set; }


        // Navigation Properties

        [ForeignKey("Kategori")]
        public int CategoryID { get; set; }
        public virtual Category? Kategori { get; set; }

        [ForeignKey("Tedarikci")]
        public string SupplierID { get; set; }
        public virtual Supplier? Tedarikçi { get; set; }

        public virtual List<OrderDetails> SiparişDetayları { get; set; }
    }
}
