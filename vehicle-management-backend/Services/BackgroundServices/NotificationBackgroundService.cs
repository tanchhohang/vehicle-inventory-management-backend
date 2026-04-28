using Microsoft.EntityFrameworkCore;
using vehicle_management_backend.Data;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Services.BackgroundServices;

public class NotificationBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<NotificationBackgroundService> _logger;

    public NotificationBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<NotificationBackgroundService> logger
    )
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken
    )
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();
                var emailService = scope.ServiceProvider
                    .GetRequiredService<IEmailService>();

                await CheckLowStock(context, emailService);
                await SendOverdueCreditReminders(context, emailService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Notification service error.");
            }

            // Run every 24 hours
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    private async Task CheckLowStock(
        AppDbContext context,
        IEmailService emailService
    )
    {
        var lowStockParts = await context.Parts
            .Where(p => p.StockQuantity < 10)
            .ToListAsync();

        if (!lowStockParts.Any()) return;

        var admins = await context.Users
            .Where(u => u.Role == "Admin")
            .ToListAsync();

        var partsHtml = string.Join("", lowStockParts.Select(p =>
            $"<tr><td>{p.Name}</td><td>{p.StockQuantity}</td></tr>"
        ));

        var html = $@"
            <h2>Low Stock Alert</h2>
            <p>The following parts have fewer than 10 units:</p>
            <table border='1' cellpadding='8' cellspacing='0'>
                <tr><th>Part</th><th>Stock</th></tr>
                {partsHtml}
            </table>";

        foreach (var admin in admins)
        {
            if (string.IsNullOrEmpty(admin.Email)) continue;
            await emailService.SendEmailAsync(
                admin.Email,
                "Low Stock Alert - Vehicle Parts Center",
                html
            );
            _logger.LogInformation(
                "Low stock alert sent to {Email}", admin.Email
            );
        }
    }

    private async Task SendOverdueCreditReminders(
        AppDbContext context,
        IEmailService emailService
    )
    {
        var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

        var overdueSales = await context.Sales
            .Include(s => s.Customer)
            .Where(s => !s.IsPaid && s.SaleDate <= oneMonthAgo)
            .ToListAsync();

        var grouped = overdueSales
            .Where(s => !string.IsNullOrEmpty(s.Customer.Email))
            .GroupBy(s => s.Customer);

        foreach (var group in grouped)
        {
            var customer = group.Key;
            var totalOwed = group.Sum(s => s.TotalAmount);

            var html = $@"
                <h2>Payment Reminder</h2>
                <p>Dear {customer.FirstName},</p>
                <p>You have <strong>Rs. {totalOwed:F2}</strong> in 
                unpaid credit balance(s) overdue by more than 1 month.</p>
                <p>Please settle your dues at your earliest convenience.</p>
                <p>Thank you,<br/>Vehicle Parts Center</p>";

            await emailService.SendEmailAsync(
                customer.Email!,
                "Payment Reminder - Vehicle Parts Center",
                html
            );
            _logger.LogInformation(
                "Overdue reminder sent to {Email}", customer.Email
            );
        }
    }
}