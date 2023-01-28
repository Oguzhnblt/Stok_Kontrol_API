using Stok_Kontrol_API.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stok_Kontrol_API.Entities.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            SiparisDetaylari = new List<OrderDetails>();
        }
        [ForeignKey("Kullanıcı")]
        public int UserID { get; set; }


        // Navigation Properties
        public virtual User Kullanıcı { get; set; }
        public Status Status { get; set; }
        public virtual List<OrderDetails> SiparisDetaylari { get; set; }
    }
}
