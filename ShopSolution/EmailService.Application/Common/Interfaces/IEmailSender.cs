namespace EmailService.Application.Common.Interfaces;

public interface IEmailSender
{
    Task SendOrderConfirmationAsync(string to, int orderId);
}
