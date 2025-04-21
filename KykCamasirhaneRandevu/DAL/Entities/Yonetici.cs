using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
        public class Yonetici
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int YoneticiID { get; set; }

            [Required]
            [MaxLength(50)]
            public string YoneticiAdSoyad { get; set; }

            [MaxLength(100)]
            public string YoneticiEposta { get; set; }

            [Required]
            [MaxLength(50)]
            public string YoneticiSifre { get; set; }

            [Required]
            [MaxLength(11)]
            public string YoneticiTC { get; set; }

            public int VarsayilanCezaSuresiDakika { get; set; } = 2; // Varsayılan 2 dakika
        }
} 