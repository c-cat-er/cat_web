using QueryMaskInfo.Models;

namespace QueryMaskInfo.Services
{
    public class MaskService
    {
        //k-p, 可以創建建構式或唯讀式的 HttpClient
        public HttpClient Client { get; }

        public MaskService(HttpClient client)
        {
            //其他資料來源
            //健康保險資料開放服務：https://data.nhi.gov.tw

            client.BaseAddress = new Uri("https://quality.data.gov.tw/"); ///url 亦可寫在 appsettings.json
            client.DefaultRequestHeaders.Add("Accept", "application/json"); ///接收 json data
            client.DefaultRequestHeaders.Add("User-Agent", "QueryMaskInfo");
            Client = client; ///透過建構式注入 HttpClient 執行個體
        }

        public async Task<IEnumerable<MaskInfo>> GetMaskInfo()
        {///發出 GetAsync() 請求取回資料並與 MaskInfo 物件繫結

            //HTTP request to API-endpoint url
            //資料集已下架
            var response = await Client.GetAsync("dq_download_json.php?nid=116285&md5_url=2150b333756e64325bdbc4a5fd45fad1");
            response.EnsureSuccessStatusCode();

            //ReadAsStreamAsync: 從回應內容中讀取 Stream (資料流)
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<MaskInfo>>(responseStream);
        }
    }
}
