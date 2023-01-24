using System.ComponentModel.DataAnnotations.Schema;

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
        public int SupplierID { get; set; }
        public virtual Supplier? Tedarikci { get; set; }

        public virtual List<OrderDetails>? SiparisDetayları { get; set; }
    }
}
