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
        public DbSet<Anket> Anketler { get; set; }
        public DbSet<Mesaj> Mesajlar { get; set; }
        public DbSet<CezaSuresi> CezaSuresi { get; set; }
        public DbSet<EmailAyarlari> EmailAyarlari { get; set; }
        public DbSet<RandevuHatirlatma> RandevuHatirlatma { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Öğrenci entity konfigürasyonu
            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.HasKey(e => e.OgrenciID);
                entity.Property(e => e.OgrenciAdSoyad).IsRequired().HasMaxLength(50);
                entity.Property(e => e.OgrenciTC).IsRequired().HasMaxLength(11);
                entity.Property(e => e.OgrenciSifre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.OgrenciEposta).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Oda_YatakNo).IsRequired().HasMaxLength(20);
            });

            // Randevu - Ogrenci ilişkisi
            modelBuilder.Entity<Randevu>(entity =>
            {
                entity.HasKey(e => e.RandevuID);
                entity.HasOne(r => r.Ogrenci)
                    .WithMany(o => o.Randevular)
                    .HasForeignKey(r => r.OgrenciID)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Mesaj - Ogrenci ilişkisi
            modelBuilder.Entity<Mesaj>(entity =>
            {
                entity.HasKey(e => e.MesajID);
                entity.HasOne(m => m.Ogrenci)
                    .WithMany(o => o.Mesajlar)
                    .HasForeignKey(m => m.OgrenciID)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Anket tablosu için konfigürasyon
            modelBuilder.Entity<Anket>(entity =>
            {
                entity.ToTable("Anketler");
                entity.HasKey(a => a.AnketId);
                entity.Property(a => a.GirisKolayligiPuani).IsRequired();
                entity.Property(a => a.RandevuIslemiPuani).IsRequired();
                entity.Property(a => a.PerformansPuani).IsRequired();
                entity.Property(a => a.ArayuzPuani).IsRequired();
                entity.Property(a => a.GenelMemnuniyetPuani).IsRequired();
                entity.Property(a => a.OneriPuani).IsRequired();
            });

            // CezaSuresi entity konfigürasyonu
            modelBuilder.Entity<CezaSuresi>(entity =>
            {
                entity.HasKey(e => e.CezaSuresiID);
            });

            // EmailAyarlari entity konfigürasyonu
            modelBuilder.Entity<EmailAyarlari>(entity =>
            {
                entity.HasKey(e => e.EmailAyarlariID);
                entity.Property(e => e.SmtpServer).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SmtpPort).IsRequired();
                entity.Property(e => e.SmtpUsername).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SmtpPassword).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FromEmail).IsRequired().HasMaxLength(100);
            });
        }
    }
}
