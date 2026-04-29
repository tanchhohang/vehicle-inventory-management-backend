using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using vehicle_management_backend.Data;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;

    public EmailService(IConfiguration config, AppDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody
    )
    {
        var settings = _config.GetSection("EmailSettings");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(
            settings["SenderName"],
            settings["SenderEmail"]
        ));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = htmlBody };

        using var client = new SmtpClient();
        await client.ConnectAsync(
            settings["SmtpHost"],
            int.Parse(settings["SmtpPort"]!),
            MailKit.Security.SecureSocketOptions.StartTls
        );
        await client.AuthenticateAsync(
            settings["SenderEmail"],
            settings["SenderPassword"]
        );
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public async Task SendInvoiceEmailAsync(long saleId)
    {
        var sale = await _context.Sales
            .Include(s => s.SaleItems)
                .ThenInclude(si => si.Part)
            .Include(s => s.Customer)
            .FirstOrDefaultAsync(s => s.Id == saleId);

        if (sale == null)
            throw new Exception("Sale not found.");

        if (string.IsNullOrEmpty(sale.Customer.Email))
            throw new Exception("Customer has no email address.");

        var itemsHtml = string.Join("", sale.SaleItems.Select(si =>
            $"<tr><td>{si.Part.Name}</td>" +
            $"<td>{si.Quantity}</td>" +
            $"<td>Rs. {si.UnitPrice:F2}</td>" +
            $"<td>Rs. {si.Quantity * si.UnitPrice:F2}</td></tr>"
        ));

        var html = $@"
            <h2>Invoice #{sale.Id}</h2>
            <p>Date: {sale.SaleDate:yyyy-MM-dd}</p>
            <p>Customer: {sale.Customer.FirstName} {sale.Customer.LastName}</p>
            <table border='1' cellpadding='8' cellspacing='0'>
                <tr>
                    <th>Part</th><th>Qty</th>
                    <th>Unit Price</th><th>Total</th>
                </tr>
                {itemsHtml}
            </table>
            <h3>Grand Total: Rs. {sale.TotalAmount:F2}</h3>
            <p>Payment Status: {(sale.IsPaid ? "Paid" : "Credit (Unpaid)")}</p>
            <p>Thank you for your business!</p>";

        await SendEmailAsync(
            sale.Customer.Email,
            $"Invoice #{sale.Id} - Vehicle Parts Center",
            html
        );
    }
}