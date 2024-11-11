using System.Text.Json;
using System.Web;
using WeatherApiWithRedisCacheOne.Interface;

namespace WeatherApiWithRedisCacheOne.Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        private const string _authorizationKey = "氣象局會員授權碼 ";
        private IHttpClientFactory _factory;
        
        public OpenWeatherMapService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<WeatherResponse?> GetWeatherForTwLocation36HrForecastAsync(string locationZhTw)
        {
            //一般天氣預報-今明36小時天氣預報
            var getNext36HrWeatherForecastApiUrl = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001";
            var httpClient = _factory.CreateClient();
            string requestUrl =
                $"{getNext36HrWeatherForecastApiUrl}?Authorization={_authorizationKey}&locationName={HttpUtility.UrlEncode(locationZhTw)}";
            var response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonSerializer.Deserialize<WeatherResponse>(await response.Content.ReadAsStringAsync());
                return responseObject;
            }
            throw new Exception($"StatusCode:{response.StatusCode}, Message:{(await response.Content.ReadAsStringAsync())}");

        }
    }
}
