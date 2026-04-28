namespace vehicle_management_backend.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string htmlBody);
    Task SendInvoiceEmailAsync(long saleId);
}