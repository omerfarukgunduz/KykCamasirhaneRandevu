using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class RandevuSaati
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RandevuSaatiID { get; set; }

        [Required]
        public DayOfWeek Gun { get; set; }

        [Required]
        public TimeSpan Saat { get; set; }

        [Required]
        public bool Aktif { get; set; } = true;
    }
} 