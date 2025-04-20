using Microsoft.AspNetCore.Mvc;
using KykCamasirhaneRandevu.DAL.Context;
using KykCamasirhaneRandevu.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace KykCamasirhaneRandevu.Controllers
{
    public class YoneticiController : Controller
    {
        private readonly KykContext _context;

        public YoneticiController(KykContext context)
        {
            _context = context;
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
                return RedirectToAction("OgrenciListele");
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
                _context.Ogrenciler.Add(ogrenci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(OgrenciListele));
            }
            return View(ogrenci);
        }

        public async Task<IActionResult> OgrenciDuzenle(int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> OgrenciDuzenlePost(int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Ogrenci>(ogrenci, "",
                o => o.OgrenciTC, o => o.OgrenciAdSoyad, o => o.OgrenciEposta, 
                o => o.Oda_YatakNo, o => o.CezaDurumu))
            {
                try
                {
                    await _context.SaveChangesAsync();
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
            var randevular = await _context.Randevular
                .Include(r => r.Ogrenci)
                .OrderByDescending(r => r.RandevuTarihi)
                .ToListAsync();
            return View(randevular);
        }

        [HttpPost]
        public async Task<IActionResult> RandevuOnayla(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.RandevuOnayDurumu = true;
                _context.Update(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(RandevuListele));
        }

        [HttpPost]
        public async Task<IActionResult> RandevuReddet(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.RandevuOnayDurumu = false;
                _context.Update(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(RandevuListele));
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
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.RandevuGerceklesti = false;
                _context.Update(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(RandevuListele));
        }
    }
} 