using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modpackApi.DTO;
using modpackApi.Services;

namespace modpackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRecordsDataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public ServiceRecordsDataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public IActionResult ProcessData()
        {
            try
            {
                ///讀取 CSV 文件
                var filePath1 = "客服紀錄_客服紀錄類別表.csv";
                var filePath2 = "客服紀錄_客服會員提問表.csv";
                var filePath3 = "客服紀錄_客服員工回覆表.csv";
                var processedData1 = _dataService.ProcessCsvFile(filePath1);
                var processedData2 = _dataService.ProcessCsvFile(filePath2);
                var processedData3 = _dataService.ProcessCsvFile(filePath3);

                ///封裝處理後的數據
                var responseData = new ServiceRecordsDataDTO
                {
                    ProcessedData1 = processedData1,
                    ProcessedData2 = processedData2,
                    ProcessedData3 = processedData3
                };
                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
