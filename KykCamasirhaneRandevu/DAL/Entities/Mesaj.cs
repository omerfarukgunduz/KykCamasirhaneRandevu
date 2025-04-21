using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class Mesaj
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MesajID { get; set; }

        [ForeignKey("Ogrenci")]
        public int? OgrenciID { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "İçerik alanı zorunludur.")]
        public string Icerik { get; set; }

        public DateTime Tarih { get; set; }
        public bool Okundu { get; set; }
    }
} 