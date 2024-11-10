using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using modpackFront.Data;
using modpackFront.Models;
using Microsoft.AspNetCore.Authentication.Cookies; // �ޤJ Cookie �������Ҫ��R�W�Ŷ�

namespace modpackFront
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //����e�����}
            // ��l�ưt�m�ɮ�Ū��
            string? xx = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetSection("Kestrel").GetSection("Endpoints").GetSection("Http").GetSection("Url").Value;
            var builder = WebApplication.CreateBuilder(args);

            // �t�m��Ʈw�W�U��
            builder.Services.AddDbContext<ModPackContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("modpack"));
            });

            // �]�w��ӷ��귽�@��(CORS)�F��
            string CorsPolicy = "AllowAny";
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(name: CorsPolicy, policy =>
                {
                    policy.WithOrigins("http://localhost:7251");
                    policy.WithMethods("GET", "POST");
                    policy.AllowCredentials()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
                });
            });

            // �t�m���ε{������Ʈw�W�U��M�ҥ~����
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // �]�w�w�]�������Ҥ覡�� Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // �s�W Cookie �������ҪA��
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login"; // ���w�n�J���|
                    options.LogoutPath = "/Login/Logout"; // ���w�n�X���|
                    // �b���i�H�]�w��h Cookie �ﶵ�A�Ҧp�L���ɶ���
                });

            // �s�W MVC ����P���Ϫ����
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization().AddViewLocalization();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // �t�m HTTP �ШD�B�z�޹D
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // �ҥ� CORS
            app.UseCors(CorsPolicy);

            // �ҥ� HTTPS ���w�V
            app.UseHttpsRedirection();

            // �]�w�R�A�ɮת����e����
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".data"] = "application/binary";
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

            // �t�m����
            app.UseRouting();

            // �ҥΨ�������
            app.UseAuthentication();
            // �ҥα��v
            app.UseAuthorization();

            // �t�m MVC ����
            app.MapControllers();
            app.MapControllerRoute(
                name: "Home",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            // �Ұ����ε{��
            app.Run();
        }
    }
}
