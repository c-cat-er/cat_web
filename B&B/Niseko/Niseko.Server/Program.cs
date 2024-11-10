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
            ///����ñ�o�̡G�o�O���o�XJWT������A�q�`�O�A�����ε{���C�A�ݭn�N��]�m���A�����ε{�������Ѳũ�URL�C�Ҧp�A�A�����ε{�������}�Ϊ̤@�Ӱߤ@�����ѲšC
            ValidIssuer = configuration["Jwt:Issuer"],
            ///���Ī��[���G�o�O������JWT���w�������C�q�`�O�A��API�A�Ȫ����Ѳũ�URL�C�p�G�A��API���h�Ө����A�A�i�H�]�m�h�Ӧ��Ī��[���C
            ValidAudience = configuration["Jwt:Audience"],
            ///ñ�o��ñ�W�K�_�G�o�O�Ω�ñ�p�M����JWT���K�_�C�o�O�@�ӹ�ٱK�_�]�ϥάۦP���K�_�i��ñ�p�M���ҡ^�ΫD��ٱK�_�]�ϥΤ��_�M�p�_��^�C�A�ݭn��ܤ@�Ӧw�����K�_�ñN���ഫ��SymmetricSecurityKey��RsaSecurityKey��H�C
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:JwtToken"] ?? throw new InvalidOperationException("Jwt Token not configured")
                )
            )
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //�T�O�ϥ��ݩʦW�٪���l�j�p�g�A���� JSON �ǦC�ƩM�ϧǦC�ƾɭP�j�p�g���ǰt�Afor System.Text.JSON
        options.JsonSerializerOptions.PropertyNamingPolicy = null;

        //�䴩�`���ޥ�
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        //import JSON �ǦC����
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
                "http://localhost:5038", "https://localhost:5038", ///server address for �ĤT��n�J
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
