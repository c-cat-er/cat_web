namespace modpackApi.Services
{
    public interface IDataService
    {///數據處理接口.

        //IEnumerable<IDataService> GetAll();

        IEnumerable<string[]> ProcessCsvFile(string filePath);
    }
}
