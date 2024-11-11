using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTrain1.Services
{
    public class ApiService
    {//與外部 API 交互的類

        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetApiResponseAsync(string url)
        {//傳回狀態碼字串

            ///發送 HTTP GET 請求到指定的 URL
            var response = await _httpClient.GetAsync(url);
            ///檢查狀態碼，若失敗引發 HttpRequestException
            response.EnsureSuccessStatusCode();

            ///讀取回應內容
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
