using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Niseko.Server.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

///DbContext
builder.Services.AddDbContext<NisekoContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ///有效簽發者：這是指發出JWT的實體，通常是你的應用程式。你需要將其設置為你的應用程式的標識符或URL。例如，你的應用程式的網址或者一個唯一的標識符。
            ValidIssuer = configuration["Jwt:Issuer"],
            ///有效的觀眾：這是指接收JWT的預期受眾。通常是你的API服務的標識符或URL。如果你的API有多個受眾，你可以設置多個有效的觀眾。
            ValidAudience = configuration["Jwt:Audience"],
            ///簽發者簽名密鑰：這是用於簽署和驗證JWT的密鑰。這是一個對稱密鑰（使用相同的密鑰進行簽署和驗證）或非對稱密鑰（使用公鑰和私鑰對）。你需要選擇一個安全的密鑰並將其轉換為SymmetricSecurityKey或RsaSecurityKey對象。
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:JwtToken"] ?? throw new InvalidOperationException("Jwt Token not configured")
                )
            )
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //確保使用屬性名稱的原始大小寫，防止 JSON 序列化和反序列化導致大小寫不匹配，for System.Text.JSON
        options.JsonSerializerOptions.PropertyNamingPolicy = null;

        //支援循環引用
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        //import JSON 序列化檔
        //options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///ASP.NET default HttpClient
builder.Services.AddHttpClient();

///CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "https://localhost:5173", ///client address
                "http://localhost:5038", "https://localhost:5038", ///server address for 第三方登入
                "https://localhost:7199", "http://localhost:85", "https://localhost:85") ///IIS address
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

///Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
