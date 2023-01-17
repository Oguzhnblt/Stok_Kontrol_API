using Stok_Kontrol_API.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stok_Kontrol_API.Entities.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            SiparişDetayları = new List<OrderDetails>();
        }
        [ForeignKey("Kullanıcı")]
        public int UserID { get; set; }
        public Status Status { get; set; }  
        public virtual User Kullanıcı { get; set; }
        public virtual List<OrderDetails> SiparişDetayları { get; set; }
    }
}
