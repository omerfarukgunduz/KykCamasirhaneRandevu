using Microsoft.AspNetCore.Mvc;
using KykCamasirhaneRandevu.DAL.Context;
using KykCamasirhaneRandevu.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<IActionResult> Login(string ogrenciNo, string sifre)
        {
            if (string.IsNullOrEmpty(ogrenciNo) || string.IsNullOrEmpty(sifre))
            {
                ViewBag.Error = "Öğrenci numarası ve şifre alanları boş bırakılamaz!";
                return View();
            }

            var ogrenci = await _context.Ogrenciler
                .FirstOrDefaultAsync(o => o.OgrenciTC == ogrenciNo && o.OgrenciSifre == sifre);

            if (ogrenci != null)
            {
                // Başarılı giriş
                // TODO: Session veya Cookie işlemleri burada yapılabilir
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Başarısız giriş
                ViewBag.Error = "Öğrenci numarası veya şifre hatalı!";
                return View();
            }
        }
    }
} 