namespace modpackApi.Services
{
    public class DataService : IDataService
    {///數據處理

        private readonly ICsvFileReader _csvFileReader;

        public DataService(ICsvFileReader csvFileReader)
        {
            _csvFileReader = csvFileReader;
        }

        public IEnumerable<string[]> ProcessCsvFile(string filePath)
        {
            return _csvFileReader.ReadCsvFile(filePath);

            // 做一些數據處理，例如清理、轉換等
            //數據清理(刪除重複數據、填補缺失值、修正錯誤值等操作)

            //特徵提取(將原始文本數據轉換為模型可用的特徵表示形式。這可能包括分詞、詞向量化、TF-IDF 轉換等操作。)

            //數據轉換(對特徵進行進一步的轉換和處理，以滿足模型的要求。這可能包括標準化、正規化、PCA 等操作。)

            //數據拆分(將處理後的數據集分為訓練集和測試集。訓練集用於訓練模型，測試集用於評估模型的性能。)

        }
    }
}
