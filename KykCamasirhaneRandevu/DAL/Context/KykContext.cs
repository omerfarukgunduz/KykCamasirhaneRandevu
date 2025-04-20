using Microsoft.EntityFrameworkCore;
using KykCamasirhaneRandevu.DAL.Entities;

namespace KykCamasirhaneRandevu.DAL.Context
{
    public class KykContext : DbContext
    {
        public KykContext(DbContextOptions<KykContext> options) : base(options)
        {
        }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Yonetici> Yoneticiler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
