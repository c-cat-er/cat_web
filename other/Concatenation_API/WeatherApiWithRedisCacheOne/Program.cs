
namespace WeatherApiWithRedisCacheOne
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ///DI
            builder.Services.AddTransient<IWeatherService, OpenWeatherMapService>();

            ///HttpClient
            builder.Services.AddHttpClient();

            ///Cache
            builder.Services.AddOutputCache();

            var app = builder.Build();

            app.UseHttpsRedirection();

            ///
            app.UseOutputCache();

            app.MapGet("/weather", async (string city, IWeatherService weatherService) =>
                {
                    var weather = await weatherService.GetWeatherForTwLocation36HrForecastAsync(city);
                    return weather is null ? Results.NotFound() : Results.Ok(weather);
                })
                .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5))) //??
                .WithName("GetWeatherForecast")
                .WithOpenApi();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
