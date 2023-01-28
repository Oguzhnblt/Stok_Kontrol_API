using System.ComponentModel.DataAnnotations.Schema;

namespace Stok_Kontrol_API.Entities.Entities
{
    public class OrderDetails : BaseEntity
    {
        [ForeignKey("Siparis")]
        public int OrderID { get; set; }

        [ForeignKey("Urun")]
        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }

        public virtual Order Siparis { get; set; }
        public virtual Product Urun { get; set; }

    }
}
