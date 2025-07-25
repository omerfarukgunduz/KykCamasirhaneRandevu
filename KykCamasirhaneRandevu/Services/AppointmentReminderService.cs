using KykCamasirhaneRandevu.DAL;
using KykCamasirhaneRandevu.DAL.Context;
using KykCamasirhaneRandevu.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KykCamasirhaneRandevu.Services
{
    public class AppointmentReminderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppointmentReminderService> _logger;
        private readonly EmailService _emailService;

        public AppointmentReminderService(
            IServiceProvider serviceProvider,
            ILogger<AppointmentReminderService> logger,
            EmailService emailService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<KykContext>();
                        
                        // Hatırlatma süresini veritabanından al
                        var hatirlatmaAyari = await dbContext.RandevuHatirlatma.FirstOrDefaultAsync();
                        if (hatirlatmaAyari == null)
                        {
                            _logger.LogWarning("Hatırlatma ayarı bulunamadı. Lütfen yönetici panelinden ayarları yapılandırın.");
                            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                            continue;
                        }

                        var simdi = DateTime.Now;
                        var hatirlatmaZamani = simdi.AddMinutes(hatirlatmaAyari.HatirlatmaSuresiDakika);

                        // Hatırlatma zamanına yaklaşan randevuları bul
                        var yaklasanRandevular = await dbContext.Randevular
                            .Include(r => r.Ogrenci)
                            .Where(r => r.RandevuTarihi >= simdi && 
                                      r.RandevuTarihi <= hatirlatmaZamani &&
                                      r.OgrenciID != null) // Sadece öğrenci atanmış randevuları al
                            .ToListAsync();

                        foreach (var randevu in yaklasanRandevular)
                        {
                            try
                            {
                                if (randevu.Ogrenci == null || string.IsNullOrEmpty(randevu.Ogrenci.OgrenciEposta))
                                {
                                    _logger.LogWarning($"Randevu ID: {randevu.RandevuID} için öğrenci bilgisi veya e-posta adresi bulunamadı.");
                                    continue;
                                }

                                var emailIcerik = $@"
                                    <html>
                                    <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                                        <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                                            <h2 style='color: #2c3e50;'>Çamaşırhane Randevu Hatırlatması</h2>
                                            <p>Sayın {randevu.Ogrenci.OgrenciAdSoyad},</p>
                                            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                                                <p>Çamaşırhaneye <strong>{randevu.RandevuTarihi:dd.MM.yyyy HH:mm}</strong> tarihinde randevunuz bulunmaktadır. Gelemeyecekseniz lütfen randevunuzu iptal etmeyi unutmayınız.</p>
                                            </div>
                                            <div style='background-color: #e8f4f8; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                                                <h3 style='color: #34495e; margin-top: 0;'>Randevu Detayları</h3>
                                                <ul style='list-style-type: none; padding-left: 0;'>
                                                    <li style='margin-bottom: 8px;'><strong>Tarih:</strong> {randevu.RandevuTarihi:dd.MM.yyyy HH:mm}</li>
                                                    <li style='margin-bottom: 8px;'><strong>Makine No:</strong> {randevu.MakineNo}</li>
                                                </ul>
                                            </div>
                                            <p style='color: #666; font-size: 0.9em;'>Randevunuza zamanında gelmenizi rica ederiz.</p>
                                            <p style='color: #666; font-size: 0.9em; margin-top: 20px; border-top: 1px solid #ddd; padding-top: 15px;'>Bu otomatik bir hatırlatma e-postasıdır.</p>
                                        </div>
                                    </body>
                                    </html>";

                                await _emailService.SendEmailAsync(
                                    randevu.Ogrenci.OgrenciEposta,
                                    "Çamaşırhane Randevu Hatırlatması",
                                    emailIcerik
                                );

                                _logger.LogInformation($"Hatırlatma e-postası gönderildi: {randevu.Ogrenci.OgrenciEposta}");
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"Hatırlatma e-postası gönderilirken hata oluştu: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Randevu hatırlatma servisi çalışırken hata oluştu: {ex.Message}");
                }

                // Her dakika kontrol et
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
} 