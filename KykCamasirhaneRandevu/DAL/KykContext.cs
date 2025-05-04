using Microsoft.EntityFrameworkCore;
using KykCamasirhaneRandevu.DAL.Entities;

namespace KykCamasirhaneRandevu.DAL
{
    public class KykContext : DbContext
    {
        public KykContext(DbContextOptions<KykContext> options) : base(options)
        {
        }

        public DbSet<Anket> Anketler { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Yonetici> Yoneticiler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<Mesaj> Mesajlar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Anket tablosu için konfigürasyon
            modelBuilder.Entity<Anket>(entity =>
            {
                entity.ToTable("Anketler");
                entity.HasKey(e => e.AnketId);
                entity.Property(e => e.AnketTarihi).IsRequired();
                entity.Property(e => e.GirisKolayligiPuani).IsRequired();
                entity.Property(e => e.RandevuIslemiPuani).IsRequired();
                entity.Property(e => e.PerformansPuani).IsRequired();
                entity.Property(e => e.ArayuzPuani).IsRequired();
                entity.Property(e => e.GenelMemnuniyetPuani).IsRequired();
                entity.Property(e => e.OneriPuani).IsRequired();
            });
        }
    }
} 