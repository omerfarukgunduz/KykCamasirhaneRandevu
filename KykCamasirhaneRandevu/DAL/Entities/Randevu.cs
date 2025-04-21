using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class Randevu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RandevuID { get; set; }

        public int? OgrenciID { get; set; }

        [Required]
        public DateTime RandevuTarihi { get; set; }

        [Required]
        [Range(1, 35, ErrorMessage = "Makine numarası 1-35 arasında olmalıdır.")]
        public int MakineNo { get; set; }

        public bool Kurutma { get; set; } = true;

        public int TeslimeKalanSure { get; set; } = 180;

        public bool? RandevuGerceklesti { get; set; } = null;

        public virtual Ogrenci? Ogrenci { get; set; }
    }
} 