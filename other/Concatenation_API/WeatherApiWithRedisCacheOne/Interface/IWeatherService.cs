namespace WeatherApiWithRedisCacheOne.Interface
{
    public interface IWeatherService
    {
        Task<WeatherResponse?> GetWeatherForTwLocation36HrForecastAsync(string locationZhTw);
    }
}
