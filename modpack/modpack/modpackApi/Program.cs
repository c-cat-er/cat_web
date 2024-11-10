
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
            //����e�����}
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

            ///JWT (�n�J��)
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ///����ñ�o�̡^�G�o�O���o�XJWT������A�q�`�O�A�����ε{���C�A�ݭn�N��]�m���A�����ε{�������Ѳũ�URL�C�Ҧp�A�A�����ε{�������}�Ϊ̤@�Ӱߤ@�����ѲšC
                        ValidIssuer = "your_issuer",
                        ///���Ī��[���^�G�o�O������JWT���w�������C�q�`�O�A��API�A�Ȫ����Ѳũ�URL�C�p�G�A��API���h�Ө����A�A�i�H�]�m�h�Ӧ��Ī��[���C
                        ValidAudience = "your_audience",
                        ///ñ�o��ñ�W�K�_�^�G�o�O�Ω�ñ�p�M����JWT���K�_�C�o�O�@�ӹ�ٱK�_�]�ϥάۦP���K�_�i��ñ�p�M���ҡ^�ΫD��ٱK�_�]�ϥΤ��_�M�p�_��^�C�A�ݭn��ܤ@�Ӧw�����K�_�ñN���ഫ��SymmetricSecurityKey��RsaSecurityKey��H�C
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
                    };
                });

            ///CSV (�ȪA��)
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
