using System;

namespace KykCamasirhaneRandevu.Models
{
    public class DashboardViewModel
    {
        public int ToplamOgrenci { get; set; }
        public int ToplamRandevu { get; set; }
        public int BugunkuRandevu { get; set; }
        public List<EnCokRandevuAlanOgrenci> EnCokRandevuAlanOgrenciler { get; set; }
        public int KullanilanMakineSayisi { get; set; }
        public int ToplamMakineSayisi { get; set; }
        public int CezaAlanOgrenci { get; set; }
        public int KurutmaSecildi { get; set; }
        public int KurutmaSecilmedi { get; set; }
        public List<MakineKullanimViewModel> MakineKullanimListesi { get; set; }
        public List<KurutmaIstatistikViewModel> KurutmaIstatistikleri { get; set; }
        public List<GunlukKullanimViewModel> GunlukKullanimListesi { get; set; }
    }

    public class EnCokRandevuAlanOgrenci
    {
        public string OgrenciAdSoyad { get; set; }
        public int RandevuSayisi { get; set; }
    }

    public class MakineKullanimViewModel
    {
        public int MakineNo { get; set; }
        public int KullanimSayisi { get; set; }
    }

    public class KurutmaIstatistikViewModel
    {
        public bool? Kurutma { get; set; }
        public int Sayi { get; set; }
    }

    public class GunlukKullanimViewModel
    {
        public string Gun { get; set; }
        public int RandevuSayisi { get; set; }
    }
} 