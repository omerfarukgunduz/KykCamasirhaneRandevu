using Microsoft.AspNetCore.Mvc;
using KykCamasirhaneRandevu.DAL.Context;
using KykCamasirhaneRandevu.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.IO;
using KykCamasirhaneRandevu.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace KykCamasirhaneRandevu.Controllers
{
    public class YoneticiController : Controller
    {
        private readonly KykContext _context;
        private readonly IConfiguration _configuration;

        public YoneticiController(KykContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string yoneticiTC, string sifre)
        {
            if (string.IsNullOrEmpty(yoneticiTC) || string.IsNullOrEmpty(sifre))
            {
                ViewBag.Error = "TC numarası ve şifre alanları boş bırakılamaz!";
                return View();
            }

            var yonetici = await _context.Yoneticiler
                .FirstOrDefaultAsync(y => y.YoneticiTC == yoneticiTC && y.YoneticiSifre == sifre);

            if (yonetici != null)
            {
                // Başarılı giriş
                HttpContext.Session.SetString("YoneticiAdSoyad", yonetici.YoneticiAdSoyad);
                HttpContext.Session.SetInt32("YoneticiID", yonetici.YoneticiID);
                return RedirectToAction("RandevuListele");
            }
            else
            {
                // Başarısız giriş
                ViewBag.Error = "TC numarası veya şifre hatalı!";
                return View();
            }
        }

        public async Task<IActionResult> OgrenciListele()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }

        [HttpPost]
        public async Task<IActionResult> OgrenciSil(int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci != null)
            {
                _context.Ogrenciler.Remove(ogrenci);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Öğrenci başarıyla silindi.";
            }
            return RedirectToAction(nameof(OgrenciListele));
        }

        public IActionResult OgrenciEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OgrenciEkle(Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                // Ceza durumunu false ve ceza bitiş tarihini null olarak ayarla
                ogrenci.CezaDurumu = false;
                ogrenci.CezaBitisTarihi = null;
                
                _context.Ogrenciler.Add(ogrenci);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Öğrenci başarıyla eklendi.";
                return RedirectToAction(nameof(OgrenciListele));
            }
            return View(ogrenci);
        }

        public async Task<IActionResult> OgrenciDuzenle(int id)
        {
            var ogrenci = await _context.Ogrenciler
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OgrenciID == id);
                
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> OgrenciDuzenlePost(int id, Ogrenci ogrenci)
        {
            if (id != ogrenci.OgrenciID)
            {
                return NotFound();
            }

            // ModelState'den Randevular ve Mesajlar validasyonlarını kaldır
            ModelState.Remove("Randevular");
            ModelState.Remove("Mesajlar");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingOgrenci = await _context.Ogrenciler.FindAsync(id);
                    if (existingOgrenci == null)
                    {
                        return NotFound();
                    }

                    // Mevcut öğrencinin özelliklerini güncelle
                    existingOgrenci.OgrenciTC = ogrenci.OgrenciTC;
                    existingOgrenci.OgrenciAdSoyad = ogrenci.OgrenciAdSoyad;
                    existingOgrenci.OgrenciEposta = ogrenci.OgrenciEposta;
                    existingOgrenci.Oda_YatakNo = ogrenci.Oda_YatakNo;
                    existingOgrenci.OgrenciSifre = ogrenci.OgrenciSifre;
                    existingOgrenci.CezaDurumu = ogrenci.CezaDurumu;

                    _context.Update(existingOgrenci);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Öğrenci başarıyla güncellendi.";
                    return RedirectToAction(nameof(OgrenciListele));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(e => e.OgrenciID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View("OgrenciDuzenle", ogrenci);
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> DuyuruListele()
        {
            var duyurular = await _context.Duyurular.ToListAsync();
            return View(duyurular);
        }

        public IActionResult DuyuruEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DuyuruEkle(Duyuru duyuru)
        {
            if (ModelState.IsValid)
            {
                duyuru.DuyuruTarihi = DateTime.Now;
                _context.Duyurular.Add(duyuru);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DuyuruListele));
            }
            return View(duyuru);
        }

        public async Task<IActionResult> DuyuruDuzenle(int id)
        {
            var duyuru = await _context.Duyurular.FindAsync(id);
            if (duyuru == null)
            {
                return NotFound();
            }
            return View(duyuru);
        }

        [HttpPost]
        public async Task<IActionResult> DuyuruDuzenle(int id, Duyuru duyuru)
        {
            if (id != duyuru.DuyuruID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duyuru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Duyurular.Any(e => e.DuyuruID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DuyuruListele));
            }
            return View(duyuru);
        }

        [HttpPost]
        public async Task<IActionResult> DuyuruSil(int id)
        {
            var duyuru = await _context.Duyurular.FindAsync(id);
            if (duyuru != null)
            {
                _context.Duyurular.Remove(duyuru);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(DuyuruListele));
        }

        public async Task<IActionResult> YoneticiListele()
        {
            var yoneticiler = await _context.Yoneticiler.ToListAsync();
            return View(yoneticiler);
        }

        public IActionResult YoneticiEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> YoneticiEkle(Yonetici yonetici)
        {
            if (ModelState.IsValid)
            {
                _context.Yoneticiler.Add(yonetici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(YoneticiListele));
            }
            return View(yonetici);
        }

        public async Task<IActionResult> YoneticiDuzenle(int id)
        {
            var yonetici = await _context.Yoneticiler.FindAsync(id);
            if (yonetici == null)
            {
                return NotFound();
            }
            return View(yonetici);
        }

        [HttpPost]
        public async Task<IActionResult> YoneticiDuzenlePost(int id)
        {
            var yonetici = await _context.Yoneticiler.FindAsync(id);
            if (yonetici == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Yonetici>(yonetici, "",
                y => y.YoneticiTC, y => y.YoneticiAdSoyad, y => y.YoneticiEposta))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(YoneticiListele));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Yoneticiler.Any(e => e.YoneticiID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View("YoneticiDuzenle", yonetici);
        }

        [HttpPost]
        public async Task<IActionResult> YoneticiSil(int id)
        {
            // ID'si 1 olan süper yöneticiyi silmeye çalışırsa hata döndür
            if (id == 1)
            {
                TempData["ErrorMessage"] = "Bu yönetici ana yöneticidir ve silinemez!";
                return RedirectToAction(nameof(YoneticiListele));
            }

            var yonetici = await _context.Yoneticiler.FindAsync(id);
            if (yonetici != null)
            {
                _context.Yoneticiler.Remove(yonetici);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(YoneticiListele));
        }

        public async Task<IActionResult> CezaListesi()
        {
            var cezaliOgrenciler = await _context.Ogrenciler
                .Where(o => o.CezaDurumu == true)
                .ToListAsync();
            return View(cezaliOgrenciler);
        }

        [HttpPost]
        public async Task<IActionResult> CezaKaldir(int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci != null)
            {
                ogrenci.CezaDurumu = false;
                _context.Update(ogrenci);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(CezaListesi));
        }

        public IActionResult SifreGuncelle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SifreGuncelle(string mevcutSifre, string yeniSifre, string yeniSifreTekrar)
        {
            var yoneticiId = HttpContext.Session.GetInt32("YoneticiID");
            if (yoneticiId == null)
            {
                return RedirectToAction("Login");
            }

            var yonetici = await _context.Yoneticiler.FindAsync(yoneticiId);
            if (yonetici == null)
            {
                return NotFound();
            }

            // Mevcut şifre kontrolü
            if (yonetici.YoneticiSifre != mevcutSifre)
            {
                ModelState.AddModelError("mevcutSifre", "Mevcut şifre hatalı!");
                return View();
            }

            // Yeni şifrelerin eşleşme kontrolü
            if (yeniSifre != yeniSifreTekrar)
            {
                ModelState.AddModelError("yeniSifreTekrar", "Yeni şifreler eşleşmiyor!");
                return View();
            }

            // Şifreyi güncelle
            yonetici.YoneticiSifre = yeniSifre;
            _context.Update(yonetici);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi.";
            return RedirectToAction(nameof(SifreGuncelle));
        }

        public async Task<IActionResult> RandevuListele()
        {
            try
            {
                var bugun = DateTime.Today;
                var randevular = await _context.Randevular
                    .Include(r => r.Ogrenci)
                    .Where(r => r.RandevuTarihi >= bugun)
                    .OrderByDescending(r => r.OgrenciID != null) // Önce dolu randevular
                    .ThenBy(r => r.RandevuTarihi) // Sonra tarihe göre sırala
                    .ToListAsync();

                return View(randevular);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RandevuListele hatası: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> RandevuGerceklesti(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.RandevuGerceklesti = true;
                _context.Update(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(RandevuListele));
        }

        [HttpPost]
        public async Task<IActionResult> RandevuGerceklesmedi(int id)
        {
            var randevu = await _context.Randevular
                .Include(r => r.Ogrenci)
                .FirstOrDefaultAsync(r => r.RandevuID == id);
            
            if (randevu != null)
            {
                randevu.RandevuGerceklesti = false;
                
                // Öğrenciye ceza ver
                if (randevu.Ogrenci != null)
                {
                    var cezaSuresi = await _context.CezaSuresi.FirstOrDefaultAsync();
                    if (cezaSuresi != null)
                    {
                        randevu.Ogrenci.CezaDurumu = true;
                        randevu.Ogrenci.CezaBitisTarihi = DateTime.Now.AddMinutes(cezaSuresi.CezaSuresiDakika);
                        _context.Update(randevu.Ogrenci);
                    }
                }
                
                _context.Update(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(RandevuListele));
        }

        public IActionResult RandevuEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RandevuEkle(Randevu randevu)
        {
            // Tüm makineler için randevu oluştur
            for (int i = 1; i <= 35; i++)
            {
                var yeniRandevu = new Randevu
                {
                    RandevuTarihi = randevu.RandevuTarihi,
                    MakineNo = i,
                    Kurutma = null
                };
                _context.Randevular.Add(yeniRandevu);
            }
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Randevular başarıyla eklendi.";
            return RedirectToAction(nameof(RandevuListele));
        }

        public async Task<IActionResult> RandevuDuzenle(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            return View(randevu);
        }

        [HttpPost]
        public async Task<IActionResult> RandevuDuzenle(int id, Randevu randevu)
        {
            if (id != randevu.RandevuID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Randevular.Any(e => e.RandevuID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RandevuListele));
            }
            return View(randevu);
        }

        [HttpPost]
        public async Task<IActionResult> RandevuSil(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(RandevuListele));
        }

        public async Task<IActionResult> Mesajlar()
        {
            var mesajlar = await _context.Mesajlar
                .Include(m => m.Ogrenci)
                .OrderByDescending(m => m.Tarih)
                .ToListAsync();
            return View(mesajlar);
        }

        public async Task<IActionResult> MesajCevap(int id)
        {
            var mesaj = await _context.Mesajlar
                .Include(m => m.Ogrenci)
                .FirstOrDefaultAsync(m => m.MesajID == id);

            if (mesaj == null)
            {
                return NotFound();
            }

            // Mesajı okundu olarak işaretle
            mesaj.Okundu = true;
            _context.Update(mesaj);
            await _context.SaveChangesAsync();

            return View(mesaj);
        }

        [HttpPost]
        public async Task<IActionResult> MesajCevap(int id, string cevap)
        {
            var mesaj = await _context.Mesajlar
                .Include(m => m.Ogrenci)
                .FirstOrDefaultAsync(m => m.MesajID == id);

            if (mesaj == null)
            {
                return NotFound();
            }

            // Cevap mesajını oluştur
            var cevapMesaji = new Mesaj
            {
                OgrenciID = mesaj.OgrenciID,
                Baslik = "Re: " + mesaj.Baslik,
                Icerik = cevap,
                Tarih = DateTime.Now,
                Okundu = false
            };

            _context.Mesajlar.Add(cevapMesaji);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi.";
            return RedirectToAction(nameof(Mesajlar));
        }

        public async Task<IActionResult> CezaSuresiAyarla()
        {
            var cezaSuresi = await _context.CezaSuresi.FirstOrDefaultAsync();
            var model = new CezaSuresiViewModel { Dakika = cezaSuresi?.CezaSuresiDakika ?? 0 };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CezaSuresiAyarla(CezaSuresiViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cezaSuresi = await _context.CezaSuresi.FirstOrDefaultAsync();
                if (cezaSuresi == null)
                {
                    cezaSuresi = new CezaSuresi();
                    _context.CezaSuresi.Add(cezaSuresi);
                }
                
                cezaSuresi.CezaSuresiDakika = model.Dakika;
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = $"Ceza süresi {model.Dakika} dakika olarak ayarlandı.";
                return RedirectToAction(nameof(CezaSuresiAyarla));
            }
            return View(model);
        }

        public IActionResult Anketler()
        {
            // Toplam anket sayısı
            ViewBag.ToplamAnket = _context.Anketler.Count();

            // Her bir kategorideki puanların dağılımını hesapla
            ViewBag.GirisKolayligiDagilimi = GetPuanDagilimi(a => a.GirisKolayligiPuani);
            ViewBag.RandevuIslemiDagilimi = GetPuanDagilimi(a => a.RandevuIslemiPuani);
            ViewBag.PerformansDagilimi = GetPuanDagilimi(a => a.PerformansPuani);
            ViewBag.ArayuzDagilimi = GetPuanDagilimi(a => a.ArayuzPuani);
            ViewBag.MemnuniyetDagilimi = GetPuanDagilimi(a => a.GenelMemnuniyetPuani);
            ViewBag.OneriDagilimi = GetPuanDagilimi(a => a.OneriPuani);

            return View();
        }

        private int[] GetPuanDagilimi(Func<Anket, int> puanSelector)
        {
            var dagilim = new int[5];
            var anketler = _context.Anketler.ToList();

            foreach (var anket in anketler)
            {
                var puan = puanSelector(anket);
                if (puan >= 1 && puan <= 5)
                {
                    dagilim[puan - 1]++;
                }
            }

            return dagilim;
        }

        public async Task<IActionResult> Dashboard()
        {
            var bugun = DateTime.Today;
            var model = new DashboardViewModel();

            // Toplam Öğrenci Sayısı
            model.ToplamOgrenci = await _context.Ogrenciler.CountAsync();

            // Toplam Randevu Sayısı
            model.ToplamRandevu = await _context.Randevular.CountAsync();

            // Bugünkü Randevu Sayısı
            model.BugunkuRandevu = await _context.Randevular.CountAsync(r => r.RandevuTarihi.Date == bugun);

            // En Çok Randevu Alan Öğrenci
            var enCokRandevuAlan = await _context.Randevular
                .GroupBy(r => r.OgrenciID)
                .Select(g => new
                {
                    OgrenciID = g.Key,
                    Toplam = g.Count()
                })
                .OrderByDescending(x => x.Toplam)
                .FirstOrDefaultAsync();

            if (enCokRandevuAlan != null)
            {
                var ogrenci = await _context.Ogrenciler.FindAsync(enCokRandevuAlan.OgrenciID);
                if (ogrenci != null)
                {
                    model.EnCokRandevuAlan = ogrenci.OgrenciAdSoyad;
                    model.EnCokRandevuSayisi = enCokRandevuAlan.Toplam;
                }
            }

            // Makinelerin Kullanım Durumu
            model.MakineKullanimListesi = await _context.Randevular
                .GroupBy(r => r.MakineNo)
                .Select(g => new MakineKullanimViewModel
                {
                    MakineNo = g.Key,
                    KullanimSayisi = g.Count()
                })
                .OrderBy(x => x.MakineNo)
                .ToListAsync();

            model.ToplamMakineSayisi = 35; // Toplam makine sayısı
            model.KullanilanMakineSayisi = model.MakineKullanimListesi.Count;

            // Aktif Ceza Alan Öğrenci Sayısı
            model.CezaAlanOgrenci = await _context.Ogrenciler.CountAsync(o => o.CezaDurumu);

            // Kurutma Seçeneği İstatistikleri
            model.KurutmaIstatistikleri = await _context.Randevular
                .GroupBy(r => r.Kurutma)
                .Select(g => new KurutmaIstatistikViewModel
                {
                    Kurutma = g.Key,
                    Sayi = g.Count()
                })
                .ToListAsync();

            model.KurutmaSecildi = model.KurutmaIstatistikleri.FirstOrDefault(x => x.Kurutma == true)?.Sayi ?? 0;
            model.KurutmaSecilmedi = model.KurutmaIstatistikleri.FirstOrDefault(x => x.Kurutma == false)?.Sayi ?? 0;

            // Günlük Kullanım İstatistikleri
            var son7Gun = Enumerable.Range(0, 7)
                .Select(i => DateTime.Today.AddDays(-i))
                .ToList();

            // Önce veritabanından ham verileri al
            var gunlukKullanimHam = await _context.Randevular
                .Where(r => r.RandevuTarihi.Date >= son7Gun.Last() && r.RandevuTarihi.Date <= son7Gun.First())
                .GroupBy(r => r.RandevuTarihi.Date)
                .Select(g => new
                {
                    Tarih = g.Key,
                    RandevuSayisi = g.Count()
                })
                .ToListAsync();

            // Sonra client tarafında formatla
            model.GunlukKullanimListesi = gunlukKullanimHam
                .Select(g => new GunlukKullanimViewModel
                {
                    Gun = g.Tarih.ToString("dd MMMM"),
                    RandevuSayisi = g.RandevuSayisi
                })
                .ToList();

            // Eksik günleri 0 değeri ile doldur
            var tumGunler = son7Gun
                .Select(g => new GunlukKullanimViewModel
                {
                    Gun = g.ToString("dd MMMM"),
                    RandevuSayisi = 0
                })
                .ToList();

            foreach (var gun in tumGunler)
            {
                if (!model.GunlukKullanimListesi.Any(x => x.Gun == gun.Gun))
                {
                    model.GunlukKullanimListesi.Add(gun);
                }
            }

            model.GunlukKullanimListesi = model.GunlukKullanimListesi
                .OrderBy(x => DateTime.ParseExact(x.Gun, "dd MMMM", null))
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> EmailAyarlari()
        {
            var ayarlar = await _context.EmailAyarlari.FirstOrDefaultAsync();
            if (ayarlar == null)
            {
                ayarlar = new EmailAyarlari();
            }
            return View(ayarlar);
        }

        [HttpPost]
        public async Task<IActionResult> EmailAyarlari(EmailAyarlari ayarlar)
        {
            if (ModelState.IsValid)
            {
                var mevcutAyarlar = await _context.EmailAyarlari.FirstOrDefaultAsync();
                if (mevcutAyarlar == null)
                {
                    ayarlar.SonGuncellemeTarihi = DateTime.Now;
                    _context.EmailAyarlari.Add(ayarlar);
                }
                else
                {
                    mevcutAyarlar.SmtpServer = ayarlar.SmtpServer;
                    mevcutAyarlar.SmtpPort = ayarlar.SmtpPort;
                    mevcutAyarlar.SmtpUsername = ayarlar.SmtpUsername;
                    mevcutAyarlar.SmtpPassword = ayarlar.SmtpPassword;
                    mevcutAyarlar.FromEmail = ayarlar.FromEmail;
                    mevcutAyarlar.SonGuncellemeTarihi = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "E-posta ayarları başarıyla güncellendi.";
                return RedirectToAction(nameof(EmailAyarlari));
            }
            return View(ayarlar);
        }

        [HttpGet]
        public async Task<IActionResult> HatirlatmaAyarlari()
        {
            var hatirlatmaAyari = await _context.RandevuHatirlatma.FirstOrDefaultAsync();
            if (hatirlatmaAyari == null)
            {
                hatirlatmaAyari = new RandevuHatirlatma { HatirlatmaSuresiDakika = 5 };
                _context.RandevuHatirlatma.Add(hatirlatmaAyari);
                await _context.SaveChangesAsync();
            }
            return View(hatirlatmaAyari);
        }

        [HttpPost]
        public async Task<IActionResult> HatirlatmaAyarlari(RandevuHatirlatma model)
        {
            if (ModelState.IsValid)
            {
                var hatirlatmaAyari = await _context.RandevuHatirlatma.FirstOrDefaultAsync();
                if (hatirlatmaAyari == null)
                {
                    hatirlatmaAyari = new RandevuHatirlatma();
                    _context.RandevuHatirlatma.Add(hatirlatmaAyari);
                }

                hatirlatmaAyari.HatirlatmaSuresiDakika = model.HatirlatmaSuresiDakika;
                hatirlatmaAyari.SonGuncellemeTarihi = DateTime.Now;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Hatırlatma süresi başarıyla güncellendi.";
                return RedirectToAction(nameof(HatirlatmaAyarlari));
            }
            return View(model);
        }

        public async Task<IActionResult> GecmisRandevular()
        {
            try
            {
                var bugun = DateTime.Today;
                var randevular = await _context.Randevular
                    .Include(r => r.Ogrenci)
                    .Where(r => r.RandevuTarihi < bugun)
                    .OrderByDescending(r => r.RandevuTarihi)
                    .ToListAsync();

                return View(randevular);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GecmisRandevular hatası: {ex.Message}");
                throw;
            }
        }

        public async Task<IActionResult> RandevuSaatleri()
        {
            var randevuSaatleri = await _context.RandevuSaatleri
                .OrderBy(r => r.Gun == DayOfWeek.Sunday ? 7 : (int)r.Gun) // Pazar'ı en sona al
                .ThenBy(r => r.Saat)
                .ToListAsync();

            return View(randevuSaatleri);
        }

        [HttpPost]
        public async Task<IActionResult> RandevuSaatiEkle(DayOfWeek gun, TimeSpan saat)
        {
            var randevuSaati = new RandevuSaati
            {
                Gun = gun,
                Saat = saat,
                Aktif = true
            };

            _context.RandevuSaatleri.Add(randevuSaati);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Randevu saati başarıyla eklendi.";
            return RedirectToAction(nameof(RandevuSaatleri));
        }

        [HttpPost]
        public async Task<IActionResult> RandevuSaatiSil(int id)
        {
            var randevuSaati = await _context.RandevuSaatleri.FindAsync(id);
            if (randevuSaati != null)
            {
                _context.RandevuSaatleri.Remove(randevuSaati);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Randevu saati başarıyla silindi.";
            }
            return RedirectToAction(nameof(RandevuSaatleri));
        }

        [HttpPost]
        public async Task<IActionResult> RandevuSaatiDurumDegistir(int id)
        {
            var randevuSaati = await _context.RandevuSaatleri.FindAsync(id);
            if (randevuSaati != null)
            {
                randevuSaati.Aktif = !randevuSaati.Aktif;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Randevu saati durumu güncellendi.";
            }
            return RedirectToAction(nameof(RandevuSaatleri));
        }

        [HttpPost]
        public async Task<IActionResult> RandevulariOlustur()
        {
            var bugun = DateTime.Today;
            var yarin = bugun.AddDays(1);
            var randevuSaatleri = await _context.RandevuSaatleri
                .Where(r => r.Aktif && r.Gun == yarin.DayOfWeek)
                .ToListAsync();

            foreach (var randevuSaati in randevuSaatleri)
            {
                for (int makineNo = 1; makineNo <= 35; makineNo++)
                {
                    var randevuTarihi = yarin.Date.Add(randevuSaati.Saat);
                    var randevu = new Randevu
                    {
                        RandevuTarihi = randevuTarihi,
                        MakineNo = makineNo,
                        Kurutma = null
                    };
                    _context.Randevular.Add(randevu);
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Yarın için randevular başarıyla oluşturuldu.";
            return RedirectToAction(nameof(RandevuSaatleri));
        }

        [HttpPost]
        public async Task<IActionResult> HaftalikRandevulariOlustur()
        {
            var bugun = DateTime.Today;
            var yarin = bugun.AddDays(1);
            var birHaftaSonra = yarin.AddDays(6); // 7 günlük periyot

            // Aktif randevu saatlerini al
            var randevuSaatleri = await _context.RandevuSaatleri
                .Where(r => r.Aktif)
                .ToListAsync();

            // Her gün için randevuları oluştur
            for (var tarih = yarin; tarih <= birHaftaSonra; tarih = tarih.AddDays(1))
            {
                // O güne ait randevu saatlerini filtrele
                var gununSaatleri = randevuSaatleri.Where(r => r.Gun == tarih.DayOfWeek);

                foreach (var randevuSaati in gununSaatleri)
                {
                    for (int makineNo = 1; makineNo <= 35; makineNo++)
                    {
                        var randevuTarihi = tarih.Date.Add(randevuSaati.Saat);
                        var randevu = new Randevu
                        {
                            RandevuTarihi = randevuTarihi,
                            MakineNo = makineNo,
                            Kurutma = null
                        };
                        _context.Randevular.Add(randevu);
                    }
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Bir haftalık randevular başarıyla oluşturuldu.";
            return RedirectToAction(nameof(RandevuSaatleri));
        }
    }
} 