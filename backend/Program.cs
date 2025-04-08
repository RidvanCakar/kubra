using Microsoft.EntityFrameworkCore;
using Context;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Redis Bağlantısı
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

// PostgreSQL bağlantı dizesini al ve eCommerceContext'i yapılandır
builder.Services.AddDbContext<eCommerceContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS politikasını ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin() // Bütün origin'lere izin ver
               .AllowAnyMethod() // Bütün HTTP metotlarına izin ver
               .AllowAnyHeader()); // Bütün header'lara izin ver
});

// Swagger/OpenAPI desteği ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// API Controller'larını ekle
builder.Services.AddControllers();

var app = builder.Build();

// Geliştirme ortamında hata ayıklamayı etkinleştir
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // Swagger'ı etkinleştir
    app.UseSwaggerUI(); // Swagger UI'yi yapılandır
}

// CORS middleware'ini ekle
app.UseCors("AllowAllOrigins");

// API Controller'larını yönlendir
app.MapControllers();

// Uygulama çalıştır
app.Run();

// Helper method to get products from PostgreSQL (will use DI for DbContext)
async Task<List<Product>> GetUrunlerFromPostgreSQL(IConnectionMultiplexer redis, eCommerceContext context)
{
    var redisKey = "product:10";
    var database = redis.GetDatabase();
    var cachedData = await database.StringGetAsync(redisKey);

    if (!cachedData.IsNullOrEmpty)
    {
        var urunler = JsonSerializer.Deserialize<List<Product>>(cachedData);
        Console.WriteLine("Veri Redis'ten alındı:");
        foreach (var urun in urunler)
        {
            Console.WriteLine($"ID: {urun.Id}, Ad: {urun.Name}");
        }
        return urunler;
    }
    else
    {
        var urunler = await context.Products.Take(10).ToListAsync();
        Console.WriteLine("Veri DB'den alındı");
        foreach (var urun in urunler)
        {
            Console.WriteLine($"ID: {urun.Id}, Ad: {urun.Name}");
        }
        
        await database.StringSetAsync(redisKey, JsonSerializer.Serialize(urunler), TimeSpan.FromHours(1)); // 1 saat boyunca sakla
        return urunler;
    }
}
