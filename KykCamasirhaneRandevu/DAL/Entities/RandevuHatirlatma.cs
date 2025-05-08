using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class RandevuHatirlatma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RandevuHatirlatmaID { get; set; }

        [Required]
        [Range(1, 1440, ErrorMessage = "Hatırlatma süresi 1-1440 dakika arasında olmalıdır.")]
        public int HatirlatmaSuresiDakika { get; set; }

        public DateTime SonGuncellemeTarihi { get; set; } = DateTime.Now;
    }
} 