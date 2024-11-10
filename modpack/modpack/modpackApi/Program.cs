
using Microsoft.EntityFrameworkCore;
using modpackApi.Models;
using modpackApi.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using modpackApi.Services;

namespace modpackApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //抓取前面網址
            string? xx = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("Kestrel").GetSection("Endpoints").GetSection("Http").GetSection("Url").Value;

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ModPackContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("modpack"));
            });

            ///AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHttpClient();

            ///JWT (登入用)
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ///有效簽發者）：這是指發出JWT的實體，通常是你的應用程式。你需要將其設置為你的應用程式的標識符或URL。例如，你的應用程式的網址或者一個唯一的標識符。
                        ValidIssuer = "your_issuer",
                        ///有效的觀眾）：這是指接收JWT的預期受眾。通常是你的API服務的標識符或URL。如果你的API有多個受眾，你可以設置多個有效的觀眾。
                        ValidAudience = "your_audience",
                        ///簽發者簽名密鑰）：這是用於簽署和驗證JWT的密鑰。這是一個對稱密鑰（使用相同的密鑰進行簽署和驗證）或非對稱密鑰（使用公鑰和私鑰對）。你需要選擇一個安全的密鑰並將其轉換為SymmetricSecurityKey或RsaSecurityKey對象。
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
                    };
                });

            ///CSV (客服用)
            builder.Services.AddSingleton<ICsvFileReader, CsvFileReader>();

            ///CORS.
            string CorsPolicy = "AllowAny";
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(name: CorsPolicy, policy =>
                {
                    policy.WithOrigins("*").WithHeaders("*").WithMethods("*");
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication(); ///JWT.
            app.UseCors(CorsPolicy);
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
