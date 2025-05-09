using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class Anket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnketId { get; set; }

        public int GirisKolayligiPuani { get; set; }
        public int RandevuIslemiPuani { get; set; }
        public int PerformansPuani { get; set; }
        public int ArayuzPuani { get; set; }
        public int GenelMemnuniyetPuani { get; set; }
        public int OneriPuani { get; set; }

        [Required]
        public DateTime AnketTarihi { get; set; } = DateTime.Now;

        public int? OgrenciID { get; set; }
        [ForeignKey("OgrenciID")]
        public virtual Ogrenci? Ogrenci { get; set; }
    }
} 