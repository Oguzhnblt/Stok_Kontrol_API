using System.ComponentModel.DataAnnotations.Schema;

namespace Stok_Kontrol_API.Entities.Entities
{
    public class BaseEntity
    {
        [Column(Order = 1)] // Bütün entitylerde bu kolon 1. sırada olacak şekilde ayarlandı.
        public int ID { get; set; }
        public bool isActive { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
