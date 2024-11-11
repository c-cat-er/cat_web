using Microsoft.VisualBasic.FileIO;

namespace modpackApi.Services
{
    public class CsvFileReader : ICsvFileReader
    {///CSV 檔案處理.

        public IEnumerable<string[]> ReadCsvFile(string filePath)
        {///使用 TextFieldParser 來解析 CSV 檔案的每一行，並以字串陣列的形式返回每一行的資料。

            ///使用 TextFieldParser 類別來解析 CSV 檔案。
            ///TextFieldParser 類別是 .NET Framework 提供的一個工具，用於從文字檔案中讀取資料，特別是 CSV 檔案。

            ///創建 TextFieldParser 物件，並指定要解析的 CSV 檔案路徑
            using TextFieldParser parser = new(filePath);
            ///設定 TextFieldParser 物件的 TextFieldType 屬性為 FieldType.Delimited，這表示要解析的資料是由特定的分隔符號分隔的。
            parser.TextFieldType = FieldType.Delimited;
            ///設置分隔符號.
            parser.SetDelimiters("﹏");

            ///讀取 CSV 檔案中的每一行
            while (!parser.EndOfData)
                ///使用 ReadFields 方法來讀取 CSV 檔案的下一行，並將它作為字串陣列返回。
                ///yield return 用於將讀取的每一行回傳給呼叫方，並保持方法的狀態，使得在下一次迭代時可以繼續從上一次停止的地方繼續執行。
                ///這使得程式碼可以以流式的方式處理大型 CSV 檔案，而不需要一次性將整個檔案讀入記憶體中。
                yield return parser.ReadFields();
        }
    }
}
