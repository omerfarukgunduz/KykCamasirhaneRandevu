using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class Mesaj
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MesajID { get; set; }

        [MaxLength(70)]
        public string OgrenciAdSoyad { get; set; }

        [MaxLength(100)]
        public string OgrenciEposta { get; set; }

        [MaxLength(100)]
        public string Konu { get; set; }

        public string MesajIcerik { get; set; }

        public bool Cevaplandi { get; set; } = false;
    }
} 