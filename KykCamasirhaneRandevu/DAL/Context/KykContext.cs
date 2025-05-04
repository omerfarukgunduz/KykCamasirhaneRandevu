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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Randevu - Ogrenci ilişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Ogrenci)
                .WithMany(o => o.Randevular)
                .HasForeignKey(r => r.OgrenciID)
                .OnDelete(DeleteBehavior.SetNull);

            // Mesaj - Ogrenci ilişkisi
            modelBuilder.Entity<Mesaj>()
                .HasOne(m => m.Ogrenci)
                .WithMany()
                .HasForeignKey(m => m.OgrenciID)
                .OnDelete(DeleteBehavior.SetNull);

            // Anket tablosu için konfigürasyon
            modelBuilder.Entity<Anket>()
                .ToTable("Anketler")
                .HasKey(a => a.AnketId);

            modelBuilder.Entity<Anket>()
                .Property(a => a.GirisKolayligiPuani)
                .IsRequired();

            modelBuilder.Entity<Anket>()
                .Property(a => a.RandevuIslemiPuani)
                .IsRequired();

            modelBuilder.Entity<Anket>()
                .Property(a => a.PerformansPuani)
                .IsRequired();

            modelBuilder.Entity<Anket>()
                .Property(a => a.ArayuzPuani)
                .IsRequired();

            modelBuilder.Entity<Anket>()
                .Property(a => a.GenelMemnuniyetPuani)
                .IsRequired();

            modelBuilder.Entity<Anket>()
                .Property(a => a.OneriPuani)
                .IsRequired();
        }
    }
}
