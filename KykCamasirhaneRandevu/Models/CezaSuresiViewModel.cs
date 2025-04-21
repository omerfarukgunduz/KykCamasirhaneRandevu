using System.ComponentModel.DataAnnotations;

namespace KykCamasirhaneRandevu.Models
{
    public class CezaSuresiViewModel
    {
        [Required(ErrorMessage = "Ceza süresi boş bırakılamaz")]
        [Range(1, 14400, ErrorMessage = "Ceza süresi 1-14400 dakika (10 gün) arasında olmalıdır")]
        public int Dakika { get; set; }
    }
} 