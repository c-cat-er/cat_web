using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFood
{
    internal class GlobalVar
    {
        public static string strDBConnectionString = "";    // 資料庫連線字串
        public static string image_dir熱食 = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\iSpan\MidtermWork\PlayFood\img熱食";      // k-p, 圖檔路徑，記得變更
        public static string image_dir冷食 = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\iSpan\MidtermWork\PlayFood\img冷食";      // k-p, 圖檔路徑，記得變更
        public static string image_dir點心 = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\iSpan\MidtermWork\PlayFood\img點心";      // k-p, 圖檔路徑，記得變更
        public static string image_dir會員頭像 = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\iSpan\MidtermWork\PlayFood\img會員頭像";      // k-p, 圖檔路徑，記得變更

        /*  */
        public static Dictionary<string, int> dict所有名稱價格 = new Dictionary<string, int>()
            {
                { "鴛鴦鍋", 140 }, { "肉燥飯", 30 }, { "米糕", 35 }, { "阿婆魯麵", 50 }, { "烤餅", 30 }, { "碗粿", 45 },
                { "擔仔麵", 55 }, { "鴨肉飯", 60 }, { "蚵仔煎", 55 }, { "蒸餃", 70 }, { "台北金峰滷肉飯", 40 }, { "台北欣葉三杯雞", 60 },
                { "鹹粥", 50 }, {"牛肉麵", 80}, {"台北師大夜市蔥抓餅", 25}, {"台北JamesKitchen虱目魚", 80}, {"台北鴨肉扁鵝肉", 70}, { "赤肉羹", 50}, { "虱目魚湯", 60},
                { "台北林東芳牛肉麵", 80},
                { "杏仁豆腐", 60}, { "豆花", 30}, { "韓國冰酥", 70}, { "霜淇淋", 30}, { "包心粉圓芒果冰", 70}, { "凍芒果西米露", 60},
                { "珍珠奶茶", 40},
                { "白糖粿", 30}, { "鹹焦糖爆米花百香果雪貝", 80}, { "獨角獸熊棉花糖", 80}, { "豬血糕", 30}
            };

        public static Dictionary<string, int> dict熱食已選名稱數量 = new Dictionary<string, int>();     // 已選的


        public static bool is管理者登入 = false;
        public static bool is會員登入 = false;

        public static bool is折扣 = false;
        public static bool is外帶 = false;
        public static bool is買購物袋 = false;
        public static int 最終價格 = 0;

        /* 物件 */
        //public static string lbl折扣前價格;

        /* 變數 */
        public static string 管理者資訊 = "";
        public static string 管理者姓名 = "";
        public static int 管理者id = 0;
        public static int 管理者權限 = 0;
        public static string 管理者職稱 = "";

        public static string 會員資訊 = "";
        public static string 會員姓名 = "";
        public static int 會員id = 0;
        public static string 會員電話 = "";
        public static string 會員地址 = "";
        public static string 會員Email = "";
        public static string 會員生日 = "";
        public static int 會員等級 = 0;
        public static int 會員點數 = 0;
        public static string 會員頭像 = "";
        public static string 會員職稱 = "";


    }
}
