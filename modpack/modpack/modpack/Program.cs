using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using modpack.Controllers;
using modpack.Data;
using modpack.Models;
using modpack.Profiles;
using modpack.Service;
using System.Configuration;

namespace modpack
{
    //github test
    public class Program
    {
        public static void Main(string[] args)
        {
            //§ì¨ú«e­±ºô§}
            string? xx = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("Kestrel").GetSection("Endpoints").GetSection("Http").GetSection("Url").Value;
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ModPackContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("modpack"));
            });

            ///API URL
            var configuration = builder.Configuration;
            var apiUrl = configuration.GetValue<string>("ApiUrl");
            builder.Services.AddSingleton(apiUrl);

            ///AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHttpClient();


            string CorsPolicy = "AllowAny";
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(name: CorsPolicy, policy =>
                {
                    policy.WithOrigins("http://localhost:7252");
                    policy.WithMethods("GET", "POST");
                    policy.AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            // 自訂，註冊 session.
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(3);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // 自訂，註冊 cookie.
            builder.Services.AddAuthentication("YourAuthenticationScheme")
                .AddCookie("YourCookieAuthenticationScheme", options =>
                {
                    options.Cookie.Name = "YourCookieName"; // 設定Cookie的名稱
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // 設定Cookie的過期時間
                    options.LoginPath = "/Account/Login"; // 登入頁面的路徑
                    options.AccessDeniedPath = "/Account/AccessDenied"; // 拒絕訪問的路徑
                    options.SlidingExpiration = true; // 啟用滑動過期
                });
            builder.Services.AddSignalR();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(CorsPolicy);
            app.UseHttpsRedirection();
            var provider = new FileExtensionContentTypeProvider();

            provider.Mappings[".data"] = "application/binary";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

            app.UseRouting();
            app.UseSession();   //. 啟用 session.
            app.UseAuthorization();     //. 啟用 cookie.

            app.MapControllers();
            app.MapControllerRoute(
                name: "Admin",
                pattern: "{controller=AdminUser}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ImageCarouselHub>("/imageCarouselHub");
                endpoints.MapHub<ServiceRecordsHub>("/serviceRecordsHub");
            });

            app.Run();
        }
    }
}
