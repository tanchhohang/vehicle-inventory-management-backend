using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using vehicle_management_backend.Data;
using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Implementations;
using vehicle_management_backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>((options) => { options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")); }
);

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPartsService, PartsService>();
builder.Services.AddScoped<ISalesService, SalesService>();

// register vendor service
builder.Services.AddScoped<IVendorService, VendorService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173", "http://localhost:5176")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddIdentity<Users, IdentityRole<long>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors("AllowFrontend");

// app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();