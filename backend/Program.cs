using Microsoft.EntityFrameworkCore;
using Context;
 using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


// Redis bağlantısı oluşturma
var redisConnection = ConnectionMultiplexer.Connect("localhost");
var database = redisConnection.GetDatabase();


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


