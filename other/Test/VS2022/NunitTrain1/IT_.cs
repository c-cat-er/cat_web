using Microsoft.EntityFrameworkCore;
using Moq;
using NunitTrain1.Porcess;
using NunitTrain1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NunitTrain1
{
    public class IT_
    {//Integration Testing (集成測試)。
        //需安裝
        ///Moq

        //步驟
        ///1. 設置測試環境：確保測試環境與生產環境相似，包括配置、依賴的服務等。
        ///2. 模擬外部 API：使用 Mock 伺服器或測試伺服器模擬外部 API 的響應。
        ///3. 編寫測試代碼：撰寫測試代碼，測試應用程序與外部 API 的交互。
        ///4. 運行測試：運行測試，驗證應用程序是否正確處理 API 響應。

        ///private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private MockHttpMessageHandler _httpMessageHandlerMock; ///改用自定義的方法
        private HttpClient _httpClient;
        private ApiService _apiService;
        private MyService _myService;

        [SetUp]
        public void Setup()
        {//設置模擬的 HttpClient 和服務類

            ///使用 Moq 庫來模擬 HTTP 請求的行為，而不需要實際向外部 API 發送請求。
            _httpMessageHandlerMock = new MockHttpMessageHandler();

            ///使用模擬的 HttpMessageHandler 創建一個 HttpClient 對象
            _httpClient = new HttpClient(_httpMessageHandlerMock)
            {
                BaseAddress = new Uri("http://testapi.com/")
            };

            ///使用 HttpClient 創建一個 ApiService 和 MyService 對象
            _apiService = new ApiService(_httpClient);
            _myService = new MyService(_apiService);
        }

        [Test]
        public async Task TestGetProcessedDataAsync_Success()
        {//模擬成功的 API 響應，並驗證 MyService 的數據處理邏輯

            // Arrange
            var expectedApiResponse = "{\"key\":\"value\"}";
            var expectedProcessedData = $"Processed: {expectedApiResponse}";

            // 使用自定义的 MockHttpMessageHandler 设置 SendAsyncFunc
            _httpMessageHandlerMock.SendAsyncFunc = (request, cancellationToken) =>
                Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedApiResponse)
                });

            // Act
            var result = await _myService.GetProcessedDataAsync("api/test");

            // Assert
            Assert.That(result, Is.EqualTo(expectedProcessedData));
        }

        [Test]
        public async Task TestGetProcessedDataAsync_ApiFailure()
        {//模擬失敗的 API 響應，並驗證 MyService 是否正確處理異常情況

            // Arrange
            _httpMessageHandlerMock.SendAsyncFunc = (request, cancellationToken) =>
                Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            // Act & Assert
            Assert.ThrowsAsync<HttpRequestException>(async () => await _myService.GetProcessedDataAsync("api/test"));
        }

        [TearDown]
        public void TearDown()
        {
            //釋放 HttpClient 的資源
            _httpClient.Dispose();
        }
    }
}
