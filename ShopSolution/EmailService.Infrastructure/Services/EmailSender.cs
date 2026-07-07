using EmailService.Application.Common.Interfaces;
using EmailService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EmailService.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendOrderConfirmationAsync(string to, int orderId)
    {
        var record = new EmailRecord(to, $"Order {orderId} Confirmed",
            $"Your order #{orderId} has been confirmed successfully!");

        _logger.LogInformation("=== EMAIL SENT ===");
        _logger.LogInformation("To: {To}", record.To);
        _logger.LogInformation("Subject: {Subject}", record.Subject);
        _logger.LogInformation("Body: {Body}", record.Body);
        _logger.LogInformation("==================");

        return Task.CompletedTask;
    }
}
