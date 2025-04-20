using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class Duyuru
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DuyuruID { get; set; }

        [Required]
        [MaxLength(200)]
        public string DuyuruKonu { get; set; }

        [Required]
        public string DuyuruMetin { get; set; }

        [Required]
        public DateTime DuyuruTarihi { get; set; } = DateTime.Now;
    }
} 