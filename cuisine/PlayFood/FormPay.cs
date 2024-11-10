using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PlayFood
{
    public partial class FormPay : Form
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        string strDBConnectionString = "";      // 資料庫連線字串

        Button btn會員頭像;
        TextBox txt姓名;
        TextBox txt電話;
        TextBox txt地址;
        Button btn會員登入;
        PictureBox pictureBoxQRCode;
        PictureBox pictureBox付款效果;
        bool hasShownMessageBox旋轉的 = false;

        Panel panel信用卡;

        public FormPay()
        {
            InitializeComponent();
            Size = new Size(800, 600);
            BackColor = Color.LightYellow;
            Activated += FormPay_Activated;
        }

        private void FormPay_Activated(object sender, EventArgs e)
        {// 在窗体激活时执行的操作
            if (GlobalVar.is會員登入)
            {
                btn會員頭像.Text = "";
                string 修改後的會員頭像檔名 = GlobalVar.image_dir會員頭像 + @"\" + GlobalVar.會員頭像;
                Image 完整圖檔路徑 = Image.FromFile(修改後的會員頭像檔名);
                Image 縮放後的圖像 = ScaleImage(完整圖檔路徑, btn會員頭像.Width, btn會員頭像.Height);     // 等比例缩放图像以适应按钮大小
                btn會員頭像.Image = 縮放後的圖像;

                using (SqlConnection con = new SqlConnection(strDBConnectionString))
                {
                    con.Open();
                    string sql會員資料 = $"SELECT 會員姓名, 會員電話, 會員地址, 會員等級, 會員點數 FROM Members WHERE MID = {GlobalVar.會員id};";
                    using (SqlCommand command = new SqlCommand(sql會員資料, con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            txt姓名.Text = reader["會員姓名"].ToString();
                            txt電話.Text = reader["會員電話"].ToString();
                            txt地址.Text = reader["會員地址"].ToString();
                            int 等級 = Convert.ToInt32(reader["會員等級"]);
                            int 點數 = Convert.ToInt32(reader["會員點數"]);

                            txt姓名.ReadOnly = true;
                            txt電話.ReadOnly = true;
                        }
                    }
                }

                btn會員登入 = new Button();
                btn會員登入.Visible = false;
            }
        }

        private void FormPay_Load(object sender, EventArgs e)
        {
            scsb.DataSource = @"."; //伺服器名稱
            scsb.InitialCatalog = "playfood"; //資料庫名稱
            scsb.IntegratedSecurity = true;        // k-p, true 指 windows 驗證。false 指 SQLServer 驗證
            strDBConnectionString = scsb.ConnectionString;      // k-p, ConnectionString 是 SqlConnectionStringBuilder 類的一個屬性，

            if (GlobalVar.is管理者登入 == false)
            {
                MessageBox.Show("請先登入");
                Close();
            }

            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(335, 20);
            lbl名稱.Size = new Size(130, 40);
            lbl名稱.Text = "快速付款";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            GroupBox groupBox訂購人資訊 = new GroupBox();
            groupBox訂購人資訊.Location = new Point(50, 90);
            groupBox訂購人資訊.Size = new Size(275, 260);
            groupBox訂購人資訊.Text = "訂購人資訊";
            groupBox訂購人資訊.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox訂購人資訊);

            btn會員頭像 = new Button();
            btn會員頭像.Location = new Point(30, 30);
            btn會員頭像.Size = new Size(50, 50);
            btn會員頭像.BackColor = Color.LightPink;
            btn會員頭像.Text = "點我登入";
            btn會員頭像.Font = new Font("微軟正黑體", 12);
            btn會員頭像.Click += new EventHandler(btn會員登入_Click);
            groupBox訂購人資訊.Controls.Add(btn會員頭像);

            if (GlobalVar.is會員登入)
            {
                btn會員頭像.Text = "";
                string 修改後的會員頭像檔名 = GlobalVar.image_dir會員頭像 + @"\" + GlobalVar.會員頭像;
                Image 完整圖檔路徑 = Image.FromFile(修改後的會員頭像檔名);
                Image 縮放後的圖像 = ScaleImage(完整圖檔路徑, btn會員頭像.Width, btn會員頭像.Height);     // 等比例缩放图像以适应按钮大小
                btn會員頭像.Image = 縮放後的圖像;
            }

            FlowLayoutPanel flowLayoutPanel訂購人資訊標籤 = new FlowLayoutPanel();
            flowLayoutPanel訂購人資訊標籤.Location = new Point(20, 90);
            flowLayoutPanel訂購人資訊標籤.Size = new Size(100, 150);
            groupBox訂購人資訊.Controls.Add(flowLayoutPanel訂購人資訊標籤);

            List<string> listStr訂購人標籤 = new List<string>()
            {
                "訂購人姓名", "訂購人電話" , "取收地址"
            };

            int i = 0;
            foreach (string str in listStr訂購人標籤)
            {
                Label lbl = new Label();
                lbl.Size = new Size(120, 45);
                lbl.Text = str;
                lbl.Font = new Font("微軟正黑體", 13);
                flowLayoutPanel訂購人資訊標籤.Controls.Add(lbl);
                i++;
            }

            FlowLayoutPanel flowLayoutPanel訂購人資訊輸入框 = new FlowLayoutPanel();
            flowLayoutPanel訂購人資訊輸入框.Location = new Point(130, 90);
            flowLayoutPanel訂購人資訊輸入框.Size = new Size(130, 140);
            groupBox訂購人資訊.Controls.Add(flowLayoutPanel訂購人資訊輸入框);

            txt姓名 = new TextBox();
            txt姓名.Size = new Size(110, 25);
            flowLayoutPanel訂購人資訊輸入框.Controls.Add(txt姓名);

            txt電話 = new TextBox();
            txt電話.Size = new Size(110, 25);
            flowLayoutPanel訂購人資訊輸入框.Controls.Add(txt電話);

            txt地址 = new TextBox();
            txt地址.Size = new Size(110, 60);
            txt地址.Multiline = true;
            flowLayoutPanel訂購人資訊輸入框.Controls.Add(txt地址);


            /*  */
            GroupBox groupBox付款方式 = new GroupBox();
            groupBox付款方式.Location = new Point(375, 90);
            groupBox付款方式.Size = new Size(270, 160);
            groupBox付款方式.Text = "付款方式";
            groupBox付款方式.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox付款方式);

            FlowLayoutPanel flowLayoutPanel付款方式 = new FlowLayoutPanel();
            flowLayoutPanel付款方式.Location = new Point(20, 40);
            flowLayoutPanel付款方式.Size = new Size(235, 120);
            groupBox付款方式.Controls.Add(flowLayoutPanel付款方式);

            List<string> listStr付款方式 = new List<string>()
            {
                "信用卡", "街口支付", "QRCode"
            };

            List<EventHandler> listEH詢問會員 = new List<EventHandler>()
            {
                new EventHandler(btn信用卡_Click), new EventHandler(btn街口支付_Click),
                new EventHandler(btnQRCode_Click),
            };

            int j = 0;
            foreach (string str in listStr付款方式)
            {
                Button btn = new Button();
                btn.Size = new Size(110, 45);
                btn.BackColor = Color.LightSalmon;
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 14);
                btn.Click += listEH詢問會員[j];
                flowLayoutPanel付款方式.Controls.Add(btn);
                j++;
            }
            
            pictureBoxQRCode = new PictureBox();
            pictureBoxQRCode.Location = new Point(375, 280);
            pictureBoxQRCode.Size = new Size(150, 150);
            pictureBoxQRCode.BackColor = Color.LightGray;
            Controls.Add(pictureBoxQRCode);
            
            panel信用卡 = new Panel();
            panel信用卡.Location = new Point(50, 380);
            panel信用卡.Size = new Size(400, 150);
            panel信用卡.BackColor = Color.LightGray;
            panel信用卡.Visible = false;
            Controls.Add(panel信用卡);

            Label lbl信用卡卡號 = new Label();
            lbl信用卡卡號.Location = new Point(20, 20);
            lbl信用卡卡號.Size = new Size(110, 25);
            lbl信用卡卡號.Text = "信用卡卡號：";
            lbl信用卡卡號.Font = new Font("微軟正黑體", 12);
            panel信用卡.Controls.Add(lbl信用卡卡號);

            Label lbl到期日 = new Label();
            lbl到期日.Location = new Point(20, 50);
            lbl到期日.Size = new Size(80, 25);
            lbl到期日.Text = "到期日：";
            lbl到期日.Font = new Font("微軟正黑體", 12);
            panel信用卡.Controls.Add(lbl到期日);

            Label lbl月 = new Label();
            lbl月.Location = new Point(200, 50);
            lbl月.Size = new Size(50, 25);
            lbl月.Text = "月(Month)";
            lbl月.Font = new Font("微軟正黑體", 12);
            panel信用卡.Controls.Add(lbl月);

            Label lbl年 = new Label();
            lbl年.Location = new Point(320, 50);
            lbl年.Size = new Size(60, 25);
            lbl年.Text = "年(Year)";
            lbl年.Font = new Font("微軟正黑體", 12);
            panel信用卡.Controls.Add(lbl年);

            Label lbl末三碼 = new Label();
            lbl末三碼.Location = new Point(20, 80);
            lbl末三碼.Size = new Size(80, 25);
            lbl末三碼.Text = "末三碼：";
            lbl末三碼.Font = new Font("微軟正黑體", 12);
            panel信用卡.Controls.Add(lbl末三碼);

            Label lbl驗證碼 = new Label();
            lbl驗證碼.Location = new Point(20, 110);
            lbl驗證碼.Size = new Size(80, 25);
            lbl驗證碼.Text = "驗證碼：";
            lbl驗證碼.Font = new Font("微軟正黑體", 12);
            panel信用卡.Controls.Add(lbl驗證碼);

            TextBox txt信用卡卡號一 = new TextBox();
            txt信用卡卡號一.Location = new Point(140, 20);
            txt信用卡卡號一.Size = new Size(50, 20);
            txt信用卡卡號一.Multiline = true;
            txt信用卡卡號一.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(txt信用卡卡號一);

            TextBox txt信用卡卡號二 = new TextBox();
            txt信用卡卡號二.Location = new Point(195, 20);
            txt信用卡卡號二.Size = new Size(50, 20);
            txt信用卡卡號二.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(txt信用卡卡號二);

            TextBox txt信用卡卡號三 = new TextBox();
            txt信用卡卡號三.Location = new Point(250, 20);
            txt信用卡卡號三.Size = new Size(50, 20);
            txt信用卡卡號三.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(txt信用卡卡號三);

            TextBox txt信用卡卡號四 = new TextBox();
            txt信用卡卡號四.Location = new Point(305, 20);
            txt信用卡卡號四.Size = new Size(50, 20);
            txt信用卡卡號四.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(txt信用卡卡號四);

            ComboBox cmb月 = new ComboBox();
            cmb月.Location = new Point(140, 50);
            cmb月.Size = new Size(50, 20);
            cmb月.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(cmb月);

            for (int k = 1; k <= 12; k++)
            {
                cmb月.Items.Add(k);
            }
            cmb月.SelectedIndex = 0;

            ComboBox cmb年 = new ComboBox();
            cmb年.Location = new Point(260, 50);
            cmb年.Size = new Size(50, 20);
            cmb年.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(cmb年);

            for (int l = 1; l <= 24; l++)
            {
                cmb年.Items.Add(l);
            }
            cmb年.SelectedIndex = 0;

            TextBox txt末三碼 = new TextBox();
            txt末三碼.Location = new Point(140, 80);
            txt末三碼.Size = new Size(50, 20);
            txt末三碼.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(txt末三碼);

            TextBox txt驗證碼 = new TextBox();
            txt驗證碼.Location = new Point(140, 110);
            txt驗證碼.Size = new Size(70, 20);
            txt驗證碼.Font = new Font("微軟正黑體", 11);
            panel信用卡.Controls.Add(txt驗證碼);


            //
            Button btn付款完成 = new Button();
            btn付款完成.Location = new Point(540, 280);
            btn付款完成.Size = new Size(105, 100);
            btn付款完成.BackColor = Color.LightBlue;
            btn付款完成.Text = "開始支付 / 付款完成";
            btn付款完成.Click += new EventHandler(btn付款完成_Click);
            btn付款完成.Font = new Font("微軟正黑體", 14);
            Controls.Add(btn付款完成);

            Button btn輸出訂單 = new Button();
            btn輸出訂單.Location = new Point(540, 400);
            btn輸出訂單.Size = new Size(105, 50);
            btn輸出訂單.BackColor = Color.LightBlue;
            btn輸出訂單.Text = "輸出訂單";
            btn輸出訂單.Click += new EventHandler(btn輸出訂單_Click);
            btn輸出訂單.Font = new Font("微軟正黑體", 14);
            Controls.Add(btn輸出訂單);

            /*
            pictureBox付款效果 = new PictureBox();
            pictureBox付款效果.Location = new Point(350, 250);
            pictureBox付款效果.Size = new Size(100, 100);
            pictureBox付款效果.Paint += pictureBox付款效果_Paint;
            pictureBox付款效果.Invalidate();    // 在非同步動畫更新時，使用 Invalidate 觸發 pictureBox 重新繪製
            Controls.Add(pictureBox付款效果);
            pictureBoxQRCode.SendToBack(); // 確保 QR Code 在 Z 軸上處於最底層
            pictureBox付款效果.BringToFront();   // 將 PictureBox 顯示在最上層*/
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

        void btn會員登入_Click(object sender, EventArgs e)
        {
            FormLoginMember formLoginMember = new FormLoginMember();
            formLoginMember.ShowDialog();
        }

        void btn信用卡_Click(object sender, EventArgs e)
        {
            pictureBoxQRCode.Visible = false;
            panel信用卡.Visible = true;
        }

        void btn街口支付_Click(object sender, EventArgs e)
        {

        }

        void btnQRCode_Click(object sender, EventArgs e)
        {
            pictureBoxQRCode.Visible = true;
            panel信用卡.Visible = false;
        }

        
        private void pictureBox付款效果_Paint(object sender, PaintEventArgs e)
        {/*
            // 自定義繪製邏輯，例如繪製透明的圓圈
            using (Graphics g = e.Graphics)
            {
                g.Clear(Color.Transparent);

                int circleSize = 50;
                int alpha = 128; // 0 (完全透明) 到 255 (完全不透明)
                using (Brush brush = new SolidBrush(Color.FromArgb(alpha, Color.Blue)))
                {
                    g.FillEllipse(brush, pictureBox付款效果.ClientRectangle);
                }
            }*/
        }

        void btn輸出訂單_Click(object sender, EventArgs e)
        {
            string str檔案目錄 = @"C:\Users\User\Desktop\iSpan\SecondMidterm\PlayFood\訂單";
            Random myRnd = new Random();
            int rndNum = myRnd.Next(10000, 100000);
            string str檔名 = DateTime.Now.ToString("yyMMddHHmmss") + rndNum + "訂購檔.txt";
            string str完整檔案路徑 = str檔案目錄 + @"\" + str檔名;

            Console.WriteLine(str完整檔案路徑);
            //EXE: 時間格式字串 am pm? 星期幾? 第幾周? 

            SaveFileDialog sfd = new SaveFileDialog();  // k-p, 建立檔案儲存對話，讓用戶確認
            sfd.InitialDirectory = str檔案目錄;          // k-p, InitialDirectory 初始目錄
            sfd.FileName = str檔名;
            sfd.Filter = "文字檔|*.txt";                // k-p, 限制用戶僅能使用特定副檔名

            DialogResult R = sfd.ShowDialog();         // k-p, 顯示檔案儲存對話窗

            if (R == DialogResult.OK)
            {
                str完整檔案路徑 = sfd.FileName;
            }
            else
            {
                return; // 結束方法，不儲存
            }

            // 訂單內容輸出
            List<string> list訂單資訊 = new List<string>();
            list訂單資訊.Add("************ 食在好玩 訂購單 ************");
            list訂單資訊.Add("=========================================");
            list訂單資訊.Add("------------ << 訂購人資料 >> ------------");
            list訂單資訊.Add($"訂購人: {GlobalVar.會員資訊}");
            list訂單資訊.Add($"訂購時間: {DateTime.Now}");

            if (GlobalVar.is外帶)
            {
                DateTime 預計出貨日 = DateTime.Now;
                DateTime 預計到貨日 = 預計出貨日.AddHours(1); // 假設預計出貨後 1 小時到貨
                list訂單資訊.Add($"預計出貨日: {預計出貨日}");
                list訂單資訊.Add($"預計到貨日: {預計到貨日}");
            }
            list訂單資訊.Add("------------- << 訂購品項 >> -------------");

            int 總價格 = 0;
            foreach (var kvp in GlobalVar.dict熱食已選名稱數量)
            {
                // 從 list 取值
                string 品項名稱 = (string)kvp.Key;
                int 數量 = (int)kvp.Value;
                int 價格 = (int)kvp.Value * GlobalVar.dict所有名稱價格[kvp.Key];
                總價格 += 價格;

                string strInfo1 = string.Format("{0} - {1} 份 - 共 {2} 元", 品項名稱, 數量, 價格);
                list訂單資訊.Add(strInfo1);
            }

            string strInfo2 = string.Format("所有總價格:{0}元", 總價格);
            list訂單資訊.Add(strInfo2);

            list訂單資訊.Add("------------- << 折扣 >> -------------");
            if (GlobalVar.is折扣)
            {
                string[] parts = FormShoppingCart.lbl最終價格.Text.ToString().Split(':');
                if (parts.Length == 2)
                {
                    // 移除 "元"
                    Match match = Regex.Match(parts[1], @"\d+");
                    if (match.Success)
                    {
                        GlobalVar.最終價格 = Int32.Parse(match.Value);
                        list訂單資訊.Add($"已使用折扣: {FormShoppingCart.txt折扣.Text} 碼，" +
                            $"共折抵了 {總價格 - GlobalVar.最終價格} 元");
                    }
                }
            }

            list訂單資訊.Add("=========================================");
            // k-p, 根據 chk外帶 和 chk買購物袋 的勾選狀態動態生成訂購單的一行
            if (FormShoppingCart.chk外帶.Checked || FormShoppingCart.chk買購物袋.Checked)
            {
                string strInfo3 = string.Format("{0}\n{1}", FormShoppingCart.chk外帶.Checked ? FormShoppingCart.chk外帶.Text : "", FormShoppingCart.chk買購物袋.Checked ? FormShoppingCart.chk買購物袋.Text : "");
                list訂單資訊.Add(strInfo3);
            }

            list訂單資訊.Add("----------------------------------------");
            list訂單資訊.Add($"{FormShoppingCart.lbl最終價格.Text}");
            list訂單資訊.Add("=========================================");
            list訂單資訊.Add("*************** 謝謝光臨 *****************");

            System.IO.File.WriteAllLines(str完整檔案路徑, list訂單資訊, Encoding.UTF8);   // k-p, 輸出檔案
            MessageBox.Show("訂購單儲存成功");

            Close();
        }

        private async void btn付款完成_Click(object sender, EventArgs e)
        {
            if ((txt姓名.Text != "") && (txt電話.Text != "") && (txt地址.Text != ""))
            {/*
                pictureBox付款效果.Invalidate(); // 觸發重新繪製

                // 啟動計時器，五秒後隱藏 pictureBox付款效果
                Timer timer = new Timer();
                timer.Interval = 5000; // 5秒
                timer.Tick += (timerSender, timerE) =>
                {
                    pictureBox付款效果.Visible = false;
                    timer.Stop();

                    // 顯示消息
                    if (!hasShownMessageBox旋轉的)
                    {
                        MessageBox.Show("付款完成");
                        hasShownMessageBox旋轉的 = true;
                    }
                };
                timer.Start();

                // 在這裡加入你的非同步動畫邏輯
                // 例如，使用 Task.Delay 來模擬非同步操作
                await Task.Delay(5000);*/


                MessageBox.Show("支付完成，若是現場的客人請等候叫號取餐，若是線上訂購的客人，請點擊 [輸出訂單] 等候配送與取餐。\n感謝您的購物，歡迎下次光臨。");

                // 輸出訂單
                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                int 總價格 = 0;
                List<string> list訂單資訊 = new List<string>();

                foreach (var kvp in GlobalVar.dict熱食已選名稱數量)
                {
                    // 從 list 取值
                    string 品項名稱 = (string)kvp.Key;
                    int 數量 = (int)kvp.Value;
                    int 價格 = 數量 * GlobalVar.dict所有名稱價格[品項名稱];
                    總價格 += 價格;
                    string strInfo1 = string.Format("{0} - {1} 份 - 共 {2} 元", 品項名稱, 數量, 價格);
                    list訂單資訊.Add(strInfo1);
                }

                string 訂單項目 = string.Join(", ", list訂單資訊); // 將 list 轉為字串
                string[] parts = FormShoppingCart.lbl最終價格.Text.Split(':');
                if (parts.Length == 2)
                {
                    // 移除 "元"
                    Match match = Regex.Match(parts[1], @"\d+");
                    if (match.Success)
                    {
                        GlobalVar.最終價格 = Int32.Parse(match.Value);
                    }

                    int 訂單金額 = (int)GlobalVar.最終價格;
                    DateTime 下訂日 = DateTime.Now;
                    DateTime 預計出貨日 = 下訂日;
                    DateTime 預計到貨日 = 預計出貨日.AddHours(1); // 假設預計出貨後 1 小時到貨

                    string strSQL = "insert into Orders (訂購人姓名, 訂購人電話, 訂購人地址, 下訂日, 訂單項目, 訂單金額, 預計出貨日, 預計到貨日)" +
                        "values(@NewName, @NewPhone, @NewAddress, @NewDate, @NewItem, @NewRevenue, @NewShippingDate, @NewDeliveryDate);";

                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@NewName", txt姓名.Text);
                    cmd.Parameters.AddWithValue("@NewPhone", txt電話.Text);
                    cmd.Parameters.AddWithValue("@NewAddress", txt地址.Text);
                    cmd.Parameters.AddWithValue("@NewDate", 下訂日);
                    cmd.Parameters.AddWithValue("@NewItem", 訂單項目);
                    cmd.Parameters.AddWithValue("@NewRevenue", 訂單金額);
                    cmd.Parameters.AddWithValue("@NewShippingDate", 預計出貨日);
                    cmd.Parameters.AddWithValue("@NewDeliveryDate", 預計到貨日);

                    int row = cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show($"({row}個資料列受到影響)");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("請登入，或是填寫姓名、電話和地址");
            }
        }
    }
}
