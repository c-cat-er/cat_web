namespace modpackApi.Services
{
    public interface ICsvFileReader
    {///CSV 檔案處理接口.

        IEnumerable<string[]> ReadCsvFile(string filePath);
        ///該方法用於從 CSV 文件中讀取數據。
        ///使用接口的好處是，它定義了一個標準的方法，任何實現這個接口的類都可以按照這個方法的規範來實現。
        ///舉例來說，你可以有不同的實現方式來讀取 CSV 文件，例如使用 TextFieldParser、CsvHelper、StreamReader 等等。如果你的代碼中使用了 ICsvFileReader 接口，那麼只需要修改你的 DI 配置，而不需要修改客戶端代碼，就可以輕鬆地切換到不同的實現方式。
        ///這樣做的好處是可以使代碼更加靈活和可擴展，並且使單元測試更容易進行，因為你可以輕鬆地模擬 ICsvFileReader 接口的實現來進行測試。

        ///IEnumerable<string[]> 代表的是從 CSV 檔案中讀取的數據的內容，而不是 CSV 檔案的路徑。

        /* 舉例
        IEnumerable<string[]> 可能是

        [
            ["apple", "banana", "orange"],
            ["car", "bus", "train", "plane"],
            ["cat", "dog"]
        ]

        IEnumerable<string> 可能是

        [ "apple", "banana", "orange", "car", "bus", "train", "plane", "cat", "dog"]

         */

        ///string[] 指每個元素都是一個字符串數組。
    }
}
