using Microsoft.AspNetCore.Mvc;
using KykCamasirhaneRandevu.DAL.Context;
using KykCamasirhaneRandevu.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using KykCamasirhaneRandevu.Services;

namespace KykCamasirhaneRandevu.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly KykContext _context;

        public OgrenciController(KykContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SifreAl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SifreAl(string eposta)
        {
            if (string.IsNullOrEmpty(eposta))
            {
                ViewBag.Error = "E-posta adresi boş bırakılamaz!";
                return View();
            }

            var ogrenci = await _context.Ogrenciler
                .FirstOrDefaultAsync(o => o.OgrenciEposta == eposta);

            if (ogrenci == null)
            {
                ViewBag.Error = "Bu e-posta adresi ile kayıtlı öğrenci bulunamadı!";
                return View();
            }

            try
            {
                var emailService = HttpContext.RequestServices.GetRequiredService<EmailService>();
                var konu = "KYK Çamaşırhane Randevu - Şifre Hatırlatma";
                var icerik = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                            <h2 style='color: #2c3e50;'>KYK Çamaşırhane Şifre Bilgisi</h2>
                            <p>Sayın {ogrenci.OgrenciAdSoyad},</p>
                            <p>Şifre alma talebiniz alınmıştır. Şifreniz aşağıda belirtilmiştir:</p>
                            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                                <strong>Şifreniz:</strong> {ogrenci.OgrenciSifre}
                            </div>
                            <p>Güvenliğiniz için giriş yaptıktan sonra şifrenizi değiştirmenizi öneririz.</p>
                            <p style='color: #666; font-size: 0.9em;'>Bu e-posta {DateTime.Now:dd.MM.yyyy HH:mm} tarihinde gönderilmiştir.</p>
                        </div>
                    </body>
                    </html>";

                await emailService.SendEmailAsync(eposta, konu, icerik);
                ViewBag.Success = "Şifreniz e-posta adresinize gönderildi.";
            }
            catch (Exception)
            {
                ViewBag.Error = "E-posta gönderilirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string ogrenciTC, string sifre)
        {
            if (string.IsNullOrEmpty(ogrenciTC) || string.IsNullOrEmpty(sifre))
            {
                ViewBag.Error = "TC numarası ve şifre alanları boş bırakılamaz!";
                return View();
            }

            var ogrenci = await _context.Ogrenciler
                .FirstOrDefaultAsync(o => o.OgrenciTC == ogrenciTC && o.OgrenciSifre == sifre);

            if (ogrenci != null)
            {
                HttpContext.Session.SetString("OgrenciAdSoyad", ogrenci.OgrenciAdSoyad);
                HttpContext.Session.SetInt32("OgrenciID", ogrenci.OgrenciID);
                return RedirectToAction("Randevular");
            }
            else
            {
                ViewBag.Error = "TC numarası veya şifre hatalı!";
                return View();
            }
        }

        public async Task<IActionResult> Randevular()
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var bugun = DateTime.Today;
            var randevular = await _context.Randevular
                .Where(r => r.OgrenciID == null && r.RandevuTarihi >= bugun)
                .OrderBy(r => r.RandevuTarihi)
                .ToListAsync();

            return View(randevular);
        }

        public async Task<IActionResult> Randevularim()
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var bugun = DateTime.Today;
            var randevular = await _context.Randevular
                .Where(r => r.OgrenciID == ogrenciId && r.RandevuTarihi >= bugun)
                .OrderBy(r => r.RandevuTarihi)
                .ToListAsync();

            return View(randevular);
        }

        [HttpPost]
        public async Task<IActionResult> RandevuIptal(int id)
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null || randevu.OgrenciID != ogrenciId)
            {
                TempData["ErrorMessage"] = "Bu randevu bulunamadı veya size ait değil!";
                return RedirectToAction(nameof(Randevularim));
            }

            randevu.OgrenciID = null;
            _context.Update(randevu);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Randevunuz başarıyla iptal edildi.";
            return RedirectToAction(nameof(Randevularim));
        }

        public IActionResult SifreGuncelle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SifreGuncelle(string mevcutSifre, string yeniSifre, string yeniSifreTekrar)
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var ogrenci = await _context.Ogrenciler.FindAsync(ogrenciId);
            if (ogrenci == null)
            {
                return NotFound();
            }

            if (ogrenci.OgrenciSifre != mevcutSifre)
            {
                ModelState.AddModelError("mevcutSifre", "Mevcut şifre hatalı!");
                return View();
            }

            if (yeniSifre != yeniSifreTekrar)
            {
                ModelState.AddModelError("yeniSifreTekrar", "Yeni şifreler eşleşmiyor!");
                return View();
            }

            ogrenci.OgrenciSifre = yeniSifre;
            _context.Update(ogrenci);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi.";
            return RedirectToAction(nameof(SifreGuncelle));
        }

        public IActionResult OneriSikayet()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OneriSikayet(string baslik, string icerik)
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var mesaj = new Mesaj
            {
                OgrenciID = ogrenciId.Value,
                Baslik = baslik,
                Icerik = icerik,
                Tarih = DateTime.Now,
                Okundu = false
            };

            _context.Mesajlar.Add(mesaj);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Öneri/şikayetiniz başarıyla gönderildi.";
            return RedirectToAction(nameof(OneriSikayet));
        }

        public IActionResult Anketler()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnketDegerlendir(int GirisKolayligiPuani, int RandevuIslemiPuani, 
            int PerformansPuani, int ArayuzPuani, int GenelMemnuniyetPuani, int OneriPuani)
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            // Aylık anket kontrolü
            var bugun = DateTime.Now;
            var ayBasi = new DateTime(bugun.Year, bugun.Month, 1);
            var aySonu = ayBasi.AddMonths(1).AddDays(-1);

            var aylikAnketVarMi = await _context.Anketler
                .AnyAsync(a => a.OgrenciID == ogrenciId && 
                             a.AnketTarihi >= ayBasi && 
                             a.AnketTarihi <= aySonu);

            if (aylikAnketVarMi)
            {
                TempData["ErrorMessage"] = "Bu ay için zaten bir anket doldurdunuz. Bir sonraki ay tekrar deneyebilirsiniz.";
                return RedirectToAction(nameof(Anketler));
            }

            var anket = new Anket
            {
                GirisKolayligiPuani = GirisKolayligiPuani,
                RandevuIslemiPuani = RandevuIslemiPuani,
                PerformansPuani = PerformansPuani,
                ArayuzPuani = ArayuzPuani,
                GenelMemnuniyetPuani = GenelMemnuniyetPuani,
                OneriPuani = OneriPuani,
                AnketTarihi = DateTime.Now,
                OgrenciID = ogrenciId
            };

            _context.Anketler.Add(anket);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Anket değerlendirmeniz başarıyla kaydedildi.";
            return RedirectToAction(nameof(Anketler));
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> RandevuAl(int id, bool kurutma)
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var ogrenci = await _context.Ogrenciler.FindAsync(ogrenciId);
            if (ogrenci == null)
            {
                return RedirectToAction("Login");
            }

            // Ceza kontrolü
            if (ogrenci.CezaDurumu && ogrenci.CezaBitisTarihi > DateTime.Now)
            {
                TempData["ErrorMessage"] = $"Ceza süreniz dolmamıştır. Ceza süreniz {ogrenci.CezaBitisTarihi.Value.ToString("dd.MM.yyyy HH:mm")} tarihinde sona erecektir.";
                return RedirectToAction(nameof(Randevular));
            }
            else if (ogrenci.CezaDurumu)
            {
                // Ceza süresi dolmuşsa ceza durumunu false yap
                ogrenci.CezaDurumu = false;
                ogrenci.CezaBitisTarihi = null;
                _context.Update(ogrenci);
                await _context.SaveChangesAsync();
            }

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }

            // Randevu dolu mu kontrol et
            if (randevu.OgrenciID != null)
            {
                TempData["ErrorMessage"] = "Bu randevu dolu!";
                return RedirectToAction(nameof(Randevular));
            }

            // Aynı tarihte başka randevu var mı kontrol et
            var ayniTarihRandevu = await _context.Randevular
                .FirstOrDefaultAsync(r => r.OgrenciID == ogrenciId && 
                                        r.RandevuTarihi.Date == randevu.RandevuTarihi.Date);
            
            if (ayniTarihRandevu != null)
            {
                TempData["ErrorMessage"] = $"Bu tarihte zaten bir randevunuz bulunmaktadır!";
                return RedirectToAction(nameof(Randevular));
            }

            // Randevuyu öğrenciye ata
            randevu.OgrenciID = ogrenciId;
            randevu.Kurutma = kurutma;
            randevu.Ogrenci = ogrenci;
            _context.Update(randevu);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Randevunuz başarıyla alındı!";
            return RedirectToAction(nameof(Randevular));
        }

        public async Task<IActionResult> Mesajlarim()
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var mesajlar = await _context.Mesajlar
                .Where(m => m.OgrenciID == ogrenciId)
                .OrderByDescending(m => m.Tarih)
                .ToListAsync();

            return View(mesajlar);
        }

        public async Task<IActionResult> MesajCevap(int id)
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var mesaj = await _context.Mesajlar
                .FirstOrDefaultAsync(m => m.MesajID == id && m.OgrenciID == ogrenciId);

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
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var mesaj = await _context.Mesajlar
                .FirstOrDefaultAsync(m => m.MesajID == id && m.OgrenciID == ogrenciId);

            if (mesaj == null)
            {
                return NotFound();
            }

            // Cevap mesajını oluştur
            var cevapMesaji = new Mesaj
            {
                OgrenciID = ogrenciId,
                Baslik = "Re: " + mesaj.Baslik,
                Icerik = cevap,
                Tarih = DateTime.Now,
                Okundu = false
            };

            _context.Mesajlar.Add(cevapMesaji);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi.";
            return RedirectToAction(nameof(Mesajlarim));
        }

        public async Task<IActionResult> Duyurular()
        {
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciID");
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            var duyurular = await _context.Duyurular
                .OrderByDescending(d => d.DuyuruTarihi)
                .ToListAsync();

            return View(duyurular);
        }
    }
} 