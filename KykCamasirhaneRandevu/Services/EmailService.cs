using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using KykCamasirhaneRandevu.DAL.Context;
using KykCamasirhaneRandevu.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace KykCamasirhaneRandevu.Services
{
    public class EmailService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IServiceProvider serviceProvider, ILogger<EmailService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<KykContext>();
                
                var emailAyarlari = await dbContext.EmailAyarlari.FirstOrDefaultAsync();
                if (emailAyarlari == null)
                {
                    _logger.LogError("E-posta ayarları bulunamadı!");
                    return;
                }

                var smtpClient = new SmtpClient(emailAyarlari.SmtpServer)
                {
                    Port = emailAyarlari.SmtpPort,
                    Credentials = new NetworkCredential(emailAyarlari.SmtpUsername, emailAyarlari.SmtpPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailAyarlari.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"E-posta başarıyla gönderildi: {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"E-posta gönderilirken hata oluştu: {ex.Message}");
                throw;
            }
        }
    }
} 