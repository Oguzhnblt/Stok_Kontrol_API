using System.ComponentModel.DataAnnotations.Schema;

namespace Stok_Kontrol_API.Entities.Entities
{
    public class OrderDetails : BaseEntity
    {
        [ForeignKey("Sipariş")]
        public int OrderID { get; set; }

        [ForeignKey("Ürün")]
        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }

        public virtual Order Sipariş { get; set; }
        public virtual Product Ürün { get; set; }
    }
}
