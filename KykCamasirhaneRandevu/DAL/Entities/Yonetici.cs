using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
        public class Yonetici
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int YoneticiID { get; set; }

            [MaxLength(70)]
            public string YoneticiAdSoyad { get; set; }

            [MaxLength(100)]
            public string YoneticiEposta { get; set; }

            [MaxLength(100)]
            public string YoneticiSifre { get; set; }

            [MaxLength(11)]
            public string YoneticiTC { get; set; }
    }
} 