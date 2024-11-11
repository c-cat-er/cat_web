using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTrain1.Services
{
    public class MyService
    {//依賴於 ApiService 並進行一些數據處理。

        private readonly ApiService _apiService;

        public MyService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> GetProcessedDataAsync(string endpoint)
        {
            ///通過 endpoint 向外部 API 發送請求以獲取數據 {"key":"value"}
            var apiResponse = await _apiService.GetApiResponseAsync(endpoint);
            ///需要的數據處理
            var processedData = $"Processed: {apiResponse}";
            return processedData;
        }
    }
}
