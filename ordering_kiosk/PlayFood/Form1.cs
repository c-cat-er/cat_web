using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using ZXing;
using System.Diagnostics;

namespace PlayFood
{
    public partial class Form1 : Form
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        string strDBConnectionString = "";      // 資料庫連線字串

        /**/
        FormShoppingCart ShoppingCartInstance;        // 建立 ShoppingCart 實例

        /* 動態生成元件 */
        public static Button btn會員頭像;
        TabControl tabControl美食;
        TabPage tabPage熱食;
        TabPage tabPage冷食;
        TabPage tabPage點心;
        FlowLayoutPanel flowLayoutPanel熱食;
        FlowLayoutPanel flowLayoutPanel冷食;
        FlowLayoutPanel flowLayoutPanel點心;
        ListBox listBox已選清單;
        GroupBox groupBox結帳;
        public Label lbl折扣前價格;


        /* 變數 */
        string image_修改後的檔名 = "";
        bool is點選 = false;

        /* 集合 */
        // key: 選項名,  value: 價格
        

        public static Dictionary<string, int> dict熱食所有名稱價格 = new Dictionary<string, int>()
            {
            };

        //List<Button> listBtn全小吃 = new List<Button>();
        //List<string> listBtn已選小吃 = new List<string>();
        
        public Form1()
        {
            InitializeComponent();
            Size = new Size(1033, 741);
            BackColor = Color.LightGoldenrodYellow;
            Activated += FormMain_Activated;
            this.Activated += Form_VisibleChanged;
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {// 在窗体激活时执行的操作
            if (GlobalVar.is會員登入)
            {
                btn會員頭像.Text = "";
                string 修改後的會員頭像檔名 = GlobalVar.image_dir會員頭像 + @"\" + GlobalVar.會員頭像;
                Image 完整圖檔路徑 = Image.FromFile(修改後的會員頭像檔名);
                Image 縮放後的圖像 = ScaleImage(完整圖檔路徑, btn會員頭像.Width, btn會員頭像.Height);     // 等比例缩放图像以适应按钮大小
                btn會員頭像.Image = 縮放後的圖像;
            }
        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {// 在表單激活事件中根據靜態變數更新按鈕狀態
            // 在表單可見性更改時執行的代碼
            if (this.Visible)
            {// 表單變得可見
                foreach (Button btn in tabControl美食.Controls.OfType<Button>())
                {
                    string strBtn = btn.Text.ToString();
                    string strBtn小吃名 = strBtn.Split('-')[0].Trim();
                    if (GlobalVar.dict熱食已選名稱數量.Keys.Contains(strBtn小吃名))
                    {
                        btn.Tag = "selected";
                        btn.BackColor = Color.LightGreen;
                    }
                }
            }
            else
            {
                // 表單變得不可見
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scsb.DataSource = @"."; //伺服器名稱
            scsb.InitialCatalog = "playfood"; //資料庫名稱
            scsb.IntegratedSecurity = true;        // k-p, true 指 windows 驗證。false 指 SQLServer 驗證
            strDBConnectionString = scsb.ConnectionString;      // k-p, ConnectionString 是 SqlConnectionStringBuilder 類的一個屬性，
                                                                // 包含了用於建立到 SQL Server 的連接所需的信息，例如數據庫名稱、用戶名、密碼等。

            ShoppingCartInstance = new FormShoppingCart();  // FormShoppingCart 的實例

            if (GlobalVar.is管理者登入 == false)
            {
                MessageBox.Show("請先登入");
                Close();
            }
            
            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(451, 20);
            lbl名稱.Size = new Size(130, 40);
            lbl名稱.Text = "食在好玩";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            btn會員頭像 = new Button();
            btn會員頭像.Location = new Point(50, 90);
            btn會員頭像.Size = new Size(50, 50);
            btn會員頭像.BackColor = Color.LightPink;
            btn會員頭像.Text = "點我註冊";
            btn會員頭像.Font = new Font("微軟正黑體", 12);
            btn會員頭像.Click += new EventHandler(btn會員登入_Click);
            Controls.Add(btn會員頭像);

            FlowLayoutPanel flowLayoutPanel主菜單按鈕 = new FlowLayoutPanel();
            flowLayoutPanel主菜單按鈕.Location = new Point(120, 90);
            flowLayoutPanel主菜單按鈕.Size = new Size(400, 60);
            Controls.Add(flowLayoutPanel主菜單按鈕);

            Button btn熱食 = new Button();
            btn熱食.Size = new Size(120, flowLayoutPanel主菜單按鈕.Height - 2);
            btn熱食.Text = "超人氣熱食";
            btn熱食.Font = new Font("微軟正黑體", 14);
            btn熱食.BackColor = Color.LightSalmon;
            btn熱食.Click += new EventHandler(btn熱食_Click);
            flowLayoutPanel主菜單按鈕.Controls.Add(btn熱食);

            Button btn冷食 = new Button();
            btn冷食.Size = new Size(120, flowLayoutPanel主菜單按鈕.Height - 2);
            btn冷食.Text = "超人氣冷食";
            btn冷食.Font = new Font("微軟正黑體", 14);
            btn冷食.BackColor = Color.LightSalmon;
            btn冷食.Click += new EventHandler(btn冷食_Click);
            flowLayoutPanel主菜單按鈕.Controls.Add(btn冷食);

            Button btn點心 = new Button();
            btn點心.Size = new Size(120, flowLayoutPanel主菜單按鈕.Height - 2);
            btn點心.Text = "超人氣點心";
            btn點心.Font = new Font("微軟正黑體", 14);
            btn點心.BackColor = Color.LightSalmon;
            btn點心.Click += new EventHandler(btn點心_Click);
            flowLayoutPanel主菜單按鈕.Controls.Add(btn點心);

            tabControl美食 = new TabControl();
            tabControl美食.Location = new Point(50, 167);
            tabControl美食.Size = new Size(510, 490);
            tabControl美食.ItemSize = new Size(1, 1);
            Controls.Add(tabControl美食);

            tabPage熱食 = new TabPage();
            tabPage熱食.Size = new Size(100, 50);
            tabPage熱食.BackColor = Color.LightYellow;
            tabControl美食.Controls.Add(tabPage熱食);

            tabPage冷食 = new TabPage();
            tabPage冷食.BackColor = Color.LightYellow;
            tabControl美食.Controls.Add(tabPage冷食);

            tabPage點心 = new TabPage();
            tabPage點心.BackColor = Color.LightYellow;
            tabControl美食.Controls.Add(tabPage點心);

            flowLayoutPanel熱食 = new FlowLayoutPanel();
            flowLayoutPanel熱食.Size = new Size(502, 490);
            flowLayoutPanel熱食.AutoScroll = true;
            tabPage熱食.Controls.Add(flowLayoutPanel熱食);

            flowLayoutPanel冷食 = new FlowLayoutPanel();
            flowLayoutPanel冷食.Size = new Size(502, 490);
            flowLayoutPanel冷食.AutoScroll = true;
            tabPage冷食.Controls.Add(flowLayoutPanel冷食);

            flowLayoutPanel點心 = new FlowLayoutPanel();
            flowLayoutPanel點心.Size = new Size(502, 490);
            flowLayoutPanel點心.AutoScroll = true;
            tabPage點心.Controls.Add(flowLayoutPanel點心);


            /* 動態生成 [btn] */
            // 從資料庫連線到按鈕
            using (SqlConnection con = new SqlConnection(strDBConnectionString))
            {
                con.Open();
                string sql熱食 = "SELECT Hname, Hprice, Himg FROM HotFood";
                using (SqlCommand command = new SqlCommand(sql熱食, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        string name = reader["Hname"].ToString();
                        string price = reader["Hprice"].ToString();
                        string imagePath = Path.Combine(GlobalVar.image_dir熱食, reader["Himg"].ToString());

                        Button btn = new Button();
                        btn.BackColor = Color.LightGray;
                        btn.Size = new Size(flowLayoutPanel熱食.Width / 3 - 10, flowLayoutPanel熱食.Height / 4);
                        btn.Text = $"{name} - {price} 元";
                        btn.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                        btn.ForeColor = Color.White;
                        btn.Padding = new Padding(0, 64, 0, 0);     // 設定文字內邊距，左邊距，上邊距，右邊距，下邊距
                        btn.Tag = "yetSelected";
                        btn.FlatAppearance.BorderColor = Color.Green;
                        btn.FlatAppearance.BorderSize = 2;

                        btn.BackgroundImage = Image.FromFile(imagePath);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        btn.Paint += new PaintEventHandler(btn_Paint繪製名稱);
                        btn.Paint += new PaintEventHandler(btn_Paint繪製紅圈);
                        btn.MouseEnter += new EventHandler(btn_MouseEnter);
                        btn.MouseLeave += new EventHandler(btn_MouseLeave);
                        btn.Click += new EventHandler(btn_Click);

                        flowLayoutPanel熱食.Controls.Add(btn);
                    }
                }

                string sql冷食 = "SELECT Cname, Cprice, Cimg FROM ColdFood";
                using (SqlCommand command = new SqlCommand(sql冷食, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        string name = reader["Cname"].ToString();
                        string price = reader["Cprice"].ToString();
                        string imagePath = Path.Combine(GlobalVar.image_dir冷食, reader["Cimg"].ToString());

                        Button btn = new Button();
                        btn.BackColor = Color.LightGray;
                        btn.Size = new Size(flowLayoutPanel冷食.Width / 3 - 10, flowLayoutPanel冷食.Height / 4);
                        btn.Text = $"{name} - {price} 元";
                        btn.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                        btn.ForeColor = Color.White;
                        btn.Padding = new Padding(0, 64, 0, 0);     // 設定文字內邊距，左邊距，上邊距，右邊距，下邊距
                        btn.Tag = "yetSelected";
                        btn.FlatAppearance.BorderColor = Color.Green;
                        btn.FlatAppearance.BorderSize = 2;

                        btn.BackgroundImage = Image.FromFile(imagePath);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        btn.Paint += new PaintEventHandler(btn_Paint繪製名稱);
                        btn.Paint += new PaintEventHandler(btn_Paint繪製紅圈);
                        btn.MouseEnter += new EventHandler(btn_MouseEnter);
                        btn.MouseLeave += new EventHandler(btn_MouseLeave);
                        btn.Click += new EventHandler(btn_Click);

                        flowLayoutPanel冷食.Controls.Add(btn);
                    }
                }

                string sql點心 = "SELECT Dname, Dprice, Dimg FROM DessertFood";
                using (SqlCommand command = new SqlCommand(sql點心, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        string name = reader["Dname"].ToString();
                        string price = reader["Dprice"].ToString();
                        string imagePath = Path.Combine(GlobalVar.image_dir點心, reader["Dimg"].ToString());

                        Button btn = new Button();
                        btn.BackColor = Color.LightGray;
                        btn.Size = new Size(flowLayoutPanel點心.Width / 3 - 10, flowLayoutPanel點心.Height / 4);
                        btn.Text = $"{name} - {price} 元";
                        btn.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                        btn.ForeColor = Color.White;
                        btn.Padding = new Padding(0, 64, 0, 0);
                        btn.Tag = "yetSelected";
                        btn.FlatAppearance.BorderColor = Color.Green;
                        btn.FlatAppearance.BorderSize = 2;

                        btn.BackgroundImage = Image.FromFile(imagePath);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;

                        返回菜單顯示紅圈圈();

                        btn.Paint += new PaintEventHandler(btn_Paint繪製名稱);
                        btn.Paint += new PaintEventHandler(btn_Paint繪製紅圈);
                        btn.MouseEnter += new EventHandler(btn_MouseEnter);
                        btn.MouseLeave += new EventHandler(btn_MouseLeave);
                        btn.Click += new EventHandler(btn_Click);

                        flowLayoutPanel點心.Controls.Add(btn);
                    }
                }
            }

            GroupBox groupBox已選清單 = new GroupBox();
            groupBox已選清單.Location = new Point(614, 90);
            groupBox已選清單.Size = new Size(360, 410);
            groupBox已選清單.Text = "已選清單";
            groupBox已選清單.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox已選清單);

            listBox已選清單 = new ListBox();
            listBox已選清單.Location = new Point(30, 50);
            listBox已選清單.Size = new Size(300, 250);
            listBox已選清單.Text = "預購列表好空空 \t\r\n(つд⊂)";
            foreach (var kvp in GlobalVar.dict熱食已選名稱數量)
            {
                listBox已選清單.Items.Add($"{kvp.Key} - {kvp.Value} 份 - 共 {kvp.Value * GlobalVar.dict所有名稱價格[kvp.Key]} 元");
            }
            groupBox已選清單.Controls.Add(listBox已選清單);

            FlowLayoutPanel flowLayoutPanel菜單按鈕 = new FlowLayoutPanel();
            flowLayoutPanel菜單按鈕.Location = new Point(30, 300);
            flowLayoutPanel菜單按鈕.Size = new Size(300, 100);
            groupBox已選清單.Controls.Add(flowLayoutPanel菜單按鈕);

            List<string> listTextBtn = new List<string>()   // 菜單按鈕文字集合
            { "重選", "數量加", "數量減" };

            List<EventHandler> actions = new List<EventHandler>();
            actions.Add(new EventHandler(btn重選_Click));
            actions.Add(new EventHandler(btn數量加_Click));
            actions.Add(new EventHandler(btn數量減_Click));

            int i = 0;
            foreach (string str in listTextBtn)
            {
                Button btn = new Button();
                btn.Size = new Size(flowLayoutPanel菜單按鈕.Width / 3 - 6, flowLayoutPanel菜單按鈕.Height / 2 - 8);
                btn.Text = str;
                btn.BackColor = Color.LightSalmon;
                btn.Font = new Font("微軟正黑體", 12);
                btn.Click += new EventHandler(actions[i]);
                flowLayoutPanel菜單按鈕.Controls.Add(btn);
                i++;
            }

            groupBox結帳 = new GroupBox();
            groupBox結帳.Location = new Point(600, 550);
            groupBox結帳.Size = new Size(380, 100);
            groupBox結帳.Text = "結帳";
            groupBox結帳.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox結帳);

            lbl折扣前價格 = new Label();
            lbl折扣前價格.Location = new Point(25, 40);
            lbl折扣前價格.Size = new Size(140, 35);
            lbl折扣前價格.Text = $"折扣前價格: 0 元";
            lbl折扣前價格.Font = new Font("微軟正黑體", 12);
            groupBox結帳.Controls.Add(lbl折扣前價格);
            更新初始價格();

            Button btn至購物車 = new Button();
            btn至購物車.Location = new Point(180, 30);
            btn至購物車.Size = new Size(160, 50);
            btn至購物車.BackColor = Color.LightBlue;
            btn至購物車.Click += new EventHandler(btn結帳_Click);
            btn至購物車.Text = "前往折扣與抽獎";
            groupBox結帳.Font = new Font("微軟正黑體", 14);
            groupBox結帳.Controls.Add(btn至購物車);
        }

        public void btn_Paint繪製名稱(object sender, PaintEventArgs e)
        {// 使用混和(blending)，繪製 [btn小吃名] 下方的灰色方框來顯示文字
            Button btn = (Button)sender;

            // 設定方框的透明度，這裡的 200 是透明度的值（0 完全透明，255 完全不透明）
            int alpha = 200;

            // 調整文字的顏色和背景色，使文字更白更清晰
            Color textColor = Color.White;
            Color backColor = Color.FromArgb(alpha, Color.DarkGray);

            // 先將文字繪製到臨時的 Bitmap 上，並添加上邊距
            TextRenderer.DrawText(
                e.Graphics,
                btn.Text,
                btn.Font,
                new Rectangle(0, 80, btn.Width, btn.Height - 80),
                textColor,
                backColor,
                TextFormatFlags.Top | TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak);

            // 如果需要邊框，可以取消下一行的註解
            // e.Graphics.DrawRectangle(new Pen(Color.Black), rectangle);
        }

        void btn會員登入_Click(object sender, EventArgs e)
        {
            FormLoginMember formLoginMember = new FormLoginMember();
            formLoginMember.ShowDialog();
        }

        private Image ScaleImage(Image sourceImage, int targetWidth, int targetHeight)
        {// 等比例缩放图像以适应按钮大小
            // 确保源图像和目标尺寸都有效
            if (sourceImage == null || targetWidth <= 0 || targetHeight <= 0)
            {
                return null;
            }

            // 计算等比例缩放后的尺寸
            int newWidth, newHeight;
            if (sourceImage.Width > sourceImage.Height)
            {
                newWidth = targetWidth;
                newHeight = (int)(sourceImage.Height * ((float)targetWidth / sourceImage.Width));
            }
            else
            {
                newWidth = (int)(sourceImage.Width * ((float)targetHeight / sourceImage.Height));
                newHeight = targetHeight;
            }

            // 创建目标图像并进行缩放
            Bitmap scaledImage = new Bitmap(targetWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceImage, 0, 0, newWidth, newHeight);
            }

            return scaledImage;
        }

        void btn熱食_Click(object sender, EventArgs e)
        {
            tabControl美食.SelectedIndex = tabControl美食.TabPages.IndexOf(tabPage熱食);
        }

        void btn冷食_Click(object sender, EventArgs e)
        {
            tabControl美食.SelectedIndex = tabControl美食.TabPages.IndexOf(tabPage冷食);
        }

        void btn點心_Click(object sender, EventArgs e)
        {
            tabControl美食.SelectedIndex = tabControl美食.TabPages.IndexOf(tabPage點心);
        }

        void btn_MouseEnter(object sender, EventArgs e)
        {
            // k-p
            if (sender is Button btn)
            {
                if (btn.BackColor == Color.LightGreen)
                {
                    btn.BackColor = Color.LightGreen;
                }
                else
                {
                    btn.BackColor = Color.LightYellow;
                }
            }
        }

        void btn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.BackColor == Color.LightGreen)    // k-p, 邏輯
                {
                    btn.BackColor = Color.LightGreen;
                    is點選 = !is點選;
                }
                else
                {
                    btn.BackColor = Color.LightGray;
                }
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            is點選 = !is點選;
            Button clickBtn = sender as Button;     // k-p

            if (clickBtn != null)
            {
                if (clickBtn.Tag.ToString() == "yetSelected")
                {// 如果按鈕狀態是未選擇，則變更為已選擇
                    clickBtn.BackColor = Color.LightGreen;
                    clickBtn.Tag = "selected";

                    string[] parts = clickBtn.Text.ToString().Split('-');
                    if (parts.Length == 2)
                    {
                        string 食物名 = parts[0].Trim();
                        int 單價;
                        Match match = Regex.Match(parts[1], @"\d+");
                        if (match.Success)
                        {
                            if (Int32.TryParse(match.Value, out 單價))
                            {
                                GlobalVar.dict熱食已選名稱數量.Add(食物名, 1);
                                listBox已選清單.Items.Add($"{食物名} - 1 份 - 共 {單價} 元");
                                clickBtn.BackColor = Color.LightGreen;
                            }
                        }
                    }
                }
                else
                {// 如果按鈕狀態是已選擇，則變更為未選擇
                    clickBtn.BackColor = Color.LightGray;
                    clickBtn.Tag = "yetSelected";

                    string[] parts = clickBtn.Text.ToString().Split('-');
                    Console.WriteLine(parts.Length + 1);
                    if (parts.Length == 2)
                    {
                        string 食物名 = parts[0].Trim();
                        GlobalVar.dict熱食已選名稱數量.Remove(食物名);
                        for (int i = listBox已選清單.Items.Count - 1; i >= 0; i--)
                        {
                            if (listBox已選清單.Items[i].ToString().Contains(食物名))
                            {
                                listBox已選清單.Items.RemoveAt(i);
                            }
                        }

                        clickBtn.BackColor = Color.LightGreen;
                    }
                }
            }

            更新初始價格();
        }

        public void btn_Paint繪製紅圈(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            // ... 其他绘制逻辑

            // 繪製紅色圓圈
            if ((string)btn.Tag == "selected")
            {
                using (Pen pen = new Pen(Color.Red, 2)) // 2 是边框的宽度
                {
                    int ellipseWidth = 80;  // 橢圆的宽度
                    int ellipseHeight = 50; // 橢圆的高度
                    int x = (btn.Width - ellipseWidth) / 2;
                    int y = (btn.Height) / 2;

                    e.Graphics.DrawEllipse(pen, x, y, ellipseWidth, ellipseHeight);
                }
            }
            else
            {
                // 取消紅圈圈，繪製相同尺寸但颜色透明的圓
                using (Pen pen = new Pen(Color.Transparent, 2)) // 2 是边框的宽度
                {
                    int ellipseWidth = 80;  // 橢圆的宽度
                    int ellipseHeight = 50; // 橢圆的高度
                    int x = (btn.Width - ellipseWidth) / 2;
                    int y = (btn.Height) / 2;

                    e.Graphics.DrawEllipse(pen, x, y, ellipseWidth, ellipseHeight);
                }
            }
        }

        void 尋找和標記已選項目()
        {
            foreach (Button btn in flowLayoutPanel熱食.Controls.OfType<Button>())
            {
                string[] parts = btn.Text.ToString().Split('-');
                if (parts.Length == 2)
                {
                    string 食物名 = parts[0].Trim();
                    if (GlobalVar.dict熱食已選名稱數量.ContainsKey(食物名))
                    {
                        btn.Tag = "selected";
                    }
                }
            }

            foreach (Button btn in flowLayoutPanel冷食.Controls.OfType<Button>())
            {
                string[] parts = btn.Text.ToString().Split('-');
                if (parts.Length == 2)
                {
                    string 食物名 = parts[0].Trim();
                    if (GlobalVar.dict熱食已選名稱數量.ContainsKey(食物名))
                    {
                        btn.Tag = "selected";
                    }
                }
            }

            foreach (Button btn in flowLayoutPanel點心.Controls.OfType<Button>())
            {
                string[] parts = btn.Text.ToString().Split('-');
                if (parts.Length == 2)
                {
                    string 食物名 = parts[0].Trim();
                    if (GlobalVar.dict熱食已選名稱數量.ContainsKey(食物名))
                    {
                        btn.Tag = "selected";
                    }
                }
            }
        }

        private void btn重選_Click(object sender, EventArgs e)
        {
            foreach (Button btn in flowLayoutPanel熱食.Controls.OfType<Button>())
            {
                btn.BackColor = Color.LightGray;
                btn.Tag = "yetSelected";
            }

            foreach (Button btn in flowLayoutPanel冷食.Controls.OfType<Button>())
            {
                btn.BackColor = Color.LightGray;
                btn.Tag = "yetSelected";
            }

            foreach (Button btn in flowLayoutPanel點心.Controls.OfType<Button>())
            {
                btn.BackColor = Color.LightGray;
                btn.Tag = "yetSelected";
            }

            GlobalVar.dict熱食已選名稱數量.Clear();
            listBox已選清單.Items.Clear();
            更新初始價格();
        }

        private void btn數量加_Click(object sender, EventArgs e)
        {
            /* 點擊，增加 [lbl數量] +1 */
            // 獲取選定項目的內容
            if (listBox已選清單.SelectedIndex >= 0)
            {
                // 解析選定項目的內容
                string strSelectedItem = listBox已選清單.SelectedItem.ToString();
                string[] parts = strSelectedItem.Split('-');
                if (parts.Length == 3)
                {
                    string 小吃名 = parts[0].Trim();

                    // 提取數量
                    int 數量 = 0;
                    Match match數量 = Regex.Match(parts[1], @"\d+");
                    if (match數量.Success)
                    {
                        if (int.TryParse(match數量.Value, out 數量))
                        {
                            listBox已選清單.Items.RemoveAt(listBox已選清單.SelectedIndex);
                            數量 += 1;
                            GlobalVar.dict熱食已選名稱數量[小吃名] = 數量;
                            listBox已選清單.Items.Add($"{小吃名} - {數量} 份 - 共 {數量 * GlobalVar.dict所有名稱價格[小吃名]} 元");

                            // 設定選擇為新添加的項目
                            listBox已選清單.SelectedIndex = listBox已選清單.Items.Count - 1;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("請先選擇一個項目");
            }

            更新初始價格();
        }

        private void btn數量減_Click(object sender, EventArgs e)
        {
            // 點擊，減少 [lbl數量] -1
            /* 點擊，增加 [lbl數量] +1 */
            // 獲取選定項目的內容
            if (listBox已選清單.SelectedIndex >= 0)
            {
                // 解析選定項目的內容
                string strSelectedItem = listBox已選清單.SelectedItem.ToString();
                string[] parts = strSelectedItem.Split('-');
                if (parts.Length == 3)
                {
                    string 小吃名 = parts[0].Trim();

                    // 提取數量
                    int 數量 = 0;
                    Match match數量 = Regex.Match(parts[1], @"\d+");
                    if (match數量.Success)
                    {
                        if (int.TryParse(match數量.Value, out 數量))
                        {
                            if (數量 > 1)
                            {
                                listBox已選清單.Items.RemoveAt(listBox已選清單.SelectedIndex);
                                數量 -= 1;
                                GlobalVar.dict熱食已選名稱數量[小吃名] = 數量;
                                listBox已選清單.Items.Add($"{小吃名} - {數量} 份 - 共 {數量 * GlobalVar.dict所有名稱價格[小吃名]} 元");

                                // 設定選擇為新添加的項目
                                listBox已選清單.SelectedIndex = listBox已選清單.Items.Count - 1;
                            }
                            else
                            {
                                // 移除按鈕點選狀態
                                foreach (Button btn in flowLayoutPanel熱食.Controls.OfType<Button>())
                                {
                                    if (btn.Text.ToString() == $"{小吃名} - {GlobalVar.dict所有名稱價格[小吃名]} 元" && btn.BackColor == Color.LightGreen)
                                    {
                                        listBox已選清單.Items.RemoveAt(listBox已選清單.SelectedIndex);
                                        GlobalVar.dict熱食已選名稱數量.Remove(小吃名);
                                        btn.BackColor = Color.LightGray;
                                        btn.Tag = "yetSelected";
                                        btn_Paint繪製紅圈(btn, new PaintEventArgs(btn.CreateGraphics(), btn.ClientRectangle));
                                    }
                                }

                                foreach (Button btn in flowLayoutPanel冷食.Controls.OfType<Button>())
                                {
                                    if (btn.Text.ToString() == $"{小吃名} - {GlobalVar.dict所有名稱價格[小吃名]} 元" && btn.BackColor == Color.LightGreen)
                                    {
                                        listBox已選清單.Items.RemoveAt(listBox已選清單.SelectedIndex);
                                        GlobalVar.dict熱食已選名稱數量.Remove(小吃名);
                                        btn.BackColor = Color.LightGray;
                                        btn.Tag = "yetSelected";
                                        btn_Paint繪製紅圈(btn, new PaintEventArgs(btn.CreateGraphics(), btn.ClientRectangle));
                                    }
                                }

                                foreach (Button btn in flowLayoutPanel點心.Controls.OfType<Button>())
                                {
                                    if (btn.Text.ToString() == $"{小吃名} - {GlobalVar.dict所有名稱價格[小吃名]} 元" && btn.BackColor == Color.LightGreen)
                                    {
                                        listBox已選清單.Items.RemoveAt(listBox已選清單.SelectedIndex);
                                        GlobalVar.dict熱食已選名稱數量.Remove(小吃名);
                                        btn.BackColor = Color.LightGray;
                                        btn.Tag = "yetSelected";
                                        btn_Paint繪製紅圈(btn, new PaintEventArgs(btn.CreateGraphics(), btn.ClientRectangle));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("請先選擇一個項目");
            }

            更新初始價格();
        }

        public void 更新初始價格()
        {
            int 折扣前熱食價格 = 0;
            foreach (var kvp in GlobalVar.dict熱食已選名稱數量)
            {
                // 使用 kvp.Key 獲取字典中的鍵（食物名稱）
                // 使用 kvp.Value 獲取字典中的值（數量）
                折扣前熱食價格 += kvp.Value * GlobalVar.dict所有名稱價格[kvp.Key];
            }

            lbl折扣前價格.Text = $"折扣前價格: {折扣前熱食價格} 元";
        }

        private void btn結帳_Click(object sender, EventArgs e)
        {
            if (listBox已選清單.Items.Count > 0)
            {
                FormShoppingCart shoppingCart = new FormShoppingCart();
                //shoppingCart.ShowDialog();    // k-p, 使用 ShowDialog() 控制權會移到 shoppingCart 上，因此會無法對 Form1 做操作
                shoppingCart.Show();    // k-p, 若要搭配 Hide() 需使用 Show()
                Hide();
            }
            else
            {
                MessageBox.Show("菜單無餐點");
            }
        }

        void 返回菜單顯示紅圈圈()
        {
            foreach (Button btn in flowLayoutPanel熱食.Controls.OfType<Button>())
            {
                string[] parts = btn.Text.ToString().Split('-');
                if (parts.Length == 2)
                {
                    string 食物名 = parts[0].Trim();
                    if (GlobalVar.dict熱食已選名稱數量.ContainsKey(食物名))
                    {
                        btn.Tag = "selected";
                    }
                }
            }

            foreach (Button btn in flowLayoutPanel冷食.Controls.OfType<Button>())
            {
                string[] parts = btn.Text.ToString().Split('-');
                if (parts.Length == 2)
                {
                    string 食物名 = parts[0].Trim();
                    if (GlobalVar.dict熱食已選名稱數量.ContainsKey(食物名))
                    {
                        btn.Tag = "selected";
                    }
                }
            }

            foreach (Button btn in flowLayoutPanel點心.Controls.OfType<Button>())
            {
                string[] parts = btn.Text.ToString().Split('-');
                if (parts.Length == 2)
                {
                    string 食物名 = parts[0].Trim();
                    if (GlobalVar.dict熱食已選名稱數量.ContainsKey(食物名))
                    {
                        btn.Tag = "selected";
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // k-p, 用戶關閉表單時確認是否關閉
            DialogResult R = MessageBox.Show("您確認要關閉表單?", "關閉表單確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (R == DialogResult.Yes)
            { // 關閉
                e.Cancel = false;
            }
            else
            { // 不關閉
                e.Cancel = true;
            }
        }
    }
}
