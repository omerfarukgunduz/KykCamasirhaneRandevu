using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KykCamasirhaneRandevu.DAL.Entities
{
    public class EmailAyarlari
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailAyarlariID { get; set; }

        [Required]
        [MaxLength(100)]
        public string SmtpServer { get; set; }

        [Required]
        public int SmtpPort { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string SmtpUsername { get; set; }

        [Required]
        [MaxLength(100)]
        public string SmtpPassword { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string FromEmail { get; set; }

        public DateTime SonGuncellemeTarihi { get; set; } = DateTime.Now;
    }
} 