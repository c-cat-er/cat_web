using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTrain1.Porcess
{
    internal class MockHttpMessageHandler : HttpMessageHandler
    {//因為 HttpMessageHandler.SendAsync 是受保護的方法，所以不能直接使用 Moq 來模擬它。
     //需要創建一個繼承自 HttpMessageHandler 的自定義處理程序，並重寫 SendAsync 方法。

        //定義一個委託 Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> 型別的屬性 SendAsyncFunc。
        //這個屬性用來存放一個函數，該函數接受 HttpRequestMessage 和 CancellationToken 作為參數，並返回一個 Task<HttpResponseMessage>。在測試中，我們可以設置這個函數來模擬 SendAsync 的行為。
        public Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> SendAsyncFunc { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {//重寫 HttpMessageHandler 的 SendAsync 方法，在 HttpClient 發送 HTTP 請求時被調用。

            //SendAsync 方法並不是直接發送請求，而是調用 SendAsyncFunc 屬性中存放的函數。這意味著我們可以在測試中設置 SendAsyncFunc 來模擬不同的 HTTP 響應，而不需要實際發送請求。
            return SendAsyncFunc(request, cancellationToken);
        }
    }
}
