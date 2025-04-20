using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class Ogrenci
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OgrenciID { get; set; }

        [Required]
        [MaxLength(11)]
        public string OgrenciTC { get; set; }

        [Required]
        [MaxLength(100)]
        public string OgrenciAdSoyad { get; set; }

        [Required]
        [MaxLength(100)]
        public string OgrenciEposta { get; set; }

        [Required]
        [MaxLength(100)]
        public string OgrenciSifre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Oda_YatakNo { get; set; }

        public bool CezaDurumu { get; set; } = false;

        public virtual ICollection<Randevu>? Randevular { get; set; }
    }
}
