using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayFood
{
    public partial class FormShoppingCart : Form
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        string strDBConnectionString = "";

        /**/
        Form1 form1Instance;        // 建立 Form1 實例

        /* 動態創建元件 */
        FlowLayoutPanel flowLayoutPanel購物清單;
        FlowLayoutPanel flowLayoutPanel結帳;
        Label lbl折扣後價格;
        public static Label lbl最終價格;
        int 折扣後價格;
        int 最終價格;
        public static TextBox txt折扣;

        public static CheckBox chk外帶;
        public static CheckBox chk買購物袋;

        /* 變數 */
        bool is點選 = false;

        public FormShoppingCart()
        {
            InitializeComponent();
            Size = new Size(1033, 741);
            BackColor = Color.LightGoldenrodYellow;
        }

        private void FormShoppingCart_Load(object sender, EventArgs e)
        {
            scsb.DataSource = @".";
            scsb.InitialCatalog = "playfood";
            scsb.IntegratedSecurity = true;
            strDBConnectionString = scsb.ConnectionString;
            CheckMemberLogin();

            if (GlobalVar.is管理者登入 == false)
            {
                MessageBox.Show("請先登入");
                Close();
            }

            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(451, 20);
            lbl名稱.Size = new Size(130, 40);
            lbl名稱.Text = "購物車";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            GroupBox groupBox已選清單 = new GroupBox();
            groupBox已選清單.Location = new Point(50, 90);
            groupBox已選清單.Size = new Size(600, 550);
            groupBox已選清單.Text = "已選清單";
            groupBox已選清單.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox已選清單);

            flowLayoutPanel購物清單 = new FlowLayoutPanel();
            flowLayoutPanel購物清單.Location = new Point(50, 40);
            flowLayoutPanel購物清單.Size = new Size(500, 470);
            flowLayoutPanel購物清單.AutoScroll = true;
            groupBox已選清單.Controls.Add(flowLayoutPanel購物清單);
            顯示已購清單圖片();

            form1Instance = new Form1();    // k-p, 初始化 Form1 實例
            form1Instance.lbl折扣前價格 = new Label();
            form1Instance.lbl折扣前價格.Location = new Point(10, 515);
            form1Instance.lbl折扣前價格.Size = new Size(160, 20);
            form1Instance.更新初始價格();
            form1Instance.lbl折扣前價格.Font = new Font("微軟正黑體", 10);
            groupBox已選清單.Controls.Add(form1Instance.lbl折扣前價格);

            lbl折扣後價格 = new Label();
            lbl折扣後價格.Location = new Point(200, 515);
            lbl折扣後價格.Size = new Size(160, 20);
            lbl折扣後價格.Text = $"折扣後價格: 尚未折扣";
            lbl折扣後價格.Font = new Font("微軟正黑體", 10);
            groupBox已選清單.Controls.Add(lbl折扣後價格);

            lbl最終價格 = new Label();
            lbl最終價格.Location = new Point(400, 515);
            lbl最終價格.Size = new Size(160, 20);
            lbl最終價格.Text = $"lbl最終價格: ";
            lbl最終價格.Font = new Font("微軟正黑體", 10);
            groupBox已選清單.Controls.Add(lbl最終價格);

            GroupBox groupBox折扣 = new GroupBox();
            groupBox折扣.Location = new Point(680, 90);
            groupBox折扣.Size = new Size(300, 150);
            groupBox折扣.Text = "折扣";
            groupBox折扣.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox折扣);

            txt折扣 = new TextBox();
            txt折扣.Location = new Point(30, 50);
            txt折扣.Size = new Size(100, 100);
            txt折扣.Font = new Font("微軟正黑體", 9);
            groupBox折扣.Controls.Add(txt折扣);

            Button btn折扣 = new Button();
            btn折扣.Location = new Point(160, 30);
            btn折扣.Size = new Size(100, 40);
            btn折扣.BackColor = Color.LightSalmon;
            btn折扣.Text = "折扣";
            btn折扣.Font = new Font("微軟正黑體", 14);
            btn折扣.Click += new EventHandler(btn折扣_Click);
            groupBox折扣.Controls.Add(btn折扣);

            Button btn更換折扣 = new Button();
            btn更換折扣.Location = new Point(160, 80);
            btn更換折扣.Size = new Size(100, 40);
            btn更換折扣.BackColor = Color.LightSalmon;
            btn更換折扣.Text = "更換折扣";
            btn更換折扣.Font = new Font("微軟正黑體", 14);
            btn更換折扣.Click += new EventHandler(btn更換折扣_Click);
            groupBox折扣.Controls.Add(btn更換折扣);

            GroupBox groupBox其他可選 = new GroupBox();
            groupBox其他可選.Location = new Point(680, 270);
            groupBox其他可選.Size = new Size(300, 90);
            groupBox其他可選.Text = "其他可選";
            groupBox其他可選.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox其他可選);

            chk外帶 = new CheckBox();
            chk外帶.Location = new Point(30, 40);
            chk外帶.Size = new Size(80, 30);
            chk外帶.Text = "外帶";
            chk外帶.Font = new Font("微軟正黑體", 14);
            chk外帶.CheckedChanged += new EventHandler(chk外帶_CheckedChanged);
            groupBox其他可選.Controls.Add(chk外帶);

            chk買購物袋 = new CheckBox();
            chk買購物袋.Location = new Point(120, 40);
            chk買購物袋.Size = new Size(180, 30);
            chk買購物袋.Text = "買購物袋 + 2 元";
            chk買購物袋.Font = new Font("微軟正黑體", 14);
            chk買購物袋.CheckedChanged += new EventHandler(chk買購物袋_CheckedChanged);
            groupBox其他可選.Controls.Add(chk買購物袋);

            GroupBox groupBox結帳 = new GroupBox();
            groupBox結帳.Location = new Point(680, 390);
            groupBox結帳.Size = new Size(300, 200);
            groupBox結帳.Text = "結帳";
            groupBox結帳.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBox結帳);

            flowLayoutPanel結帳 = new FlowLayoutPanel();
            flowLayoutPanel結帳.Location = new Point(30, 50);
            flowLayoutPanel結帳.Size = new Size(240, 120);
            groupBox結帳.Controls.Add(flowLayoutPanel結帳);

            //List<Button> listBtn結帳 = new List<Button>();

            List<string> listStr結帳 = new List<string>()
            { "移除品項", "返回菜單", "前往付款" };

            List<EventHandler> listEventHandler = new List<EventHandler>()
            {
                new EventHandler(btn移除品項_Click), new EventHandler(btn返回菜單_Click),
                new EventHandler(btn前往付款_Click)
            };

            int k = 0;
            foreach (string str in listStr結帳)
            {
                Button btn = new Button();
                btn.Location = new Point(30, 30);
                btn.Size = new Size(flowLayoutPanel結帳.Width / 2 - 10, flowLayoutPanel結帳.Height / 2 - 10);
                btn.BackColor = Color.LightBlue;
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 13);
                btn.Click += listEventHandler[k];
                flowLayoutPanel結帳.Controls.Add(btn);
                k++;
            }
        }

        private void CheckMemberLogin()
        {
            if (GlobalVar.is會員登入 == false)
            {
                FormCustomMessageBox formCustomMessageBox = new FormCustomMessageBox();
                formCustomMessageBox.ShowDialog();

                switch (formCustomMessageBox.Result)
                {
                    case "Register":
                        // 做註冊相關的事情
                        break;
                    case "Login":
                        FormLoginMember formLoginMember = new FormLoginMember();
                        formLoginMember.ShowDialog();
                        break;
                    case "Cancel":
                        // 什麼也不做
                        Close();  // 如果取消，關閉 FormPay
                        break;
                }
            }
        }

        void 顯示已購清單圖片()
        {
            form1Instance = new Form1();
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
                        int price = (int)reader["Hprice"];
                        if (GlobalVar.dict熱食已選名稱數量.Keys.Contains(name))
                        {
                            string imagePath = Path.Combine(GlobalVar.image_dir熱食, reader["Himg"].ToString());
                            Button btn = new Button();
                            btn.BackColor = Color.LightGreen;
                            btn.Size = new Size(flowLayoutPanel購物清單.Width / 3 - 10, flowLayoutPanel購物清單.Height / 4);
                            btn.Text = $"{name} - {GlobalVar.dict熱食已選名稱數量[name]} 份 - {GlobalVar.dict熱食已選名稱數量[name] * price} 元";
                            btn.Font = new Font("微軟正黑體", 11);
                            btn.ForeColor = Color.White;
                            btn.Padding = new Padding(0, 64, 0, 0);
                            btn.Click += new EventHandler(btn_Click);

                            btn.BackgroundImage = Image.FromFile(imagePath);
                            btn.BackgroundImageLayout = ImageLayout.Stretch;

                            btn.Paint += new PaintEventHandler(form1Instance.btn_Paint繪製名稱);
                            flowLayoutPanel購物清單.Controls.Add(btn);
                        }
                    }
                }

                string sql冷食 = "SELECT Cname, Cprice, Cimg FROM ColdFood";
                using (SqlCommand command = new SqlCommand(sql冷食, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        string name = reader["Cname"].ToString();
                        int price = (int)reader["Cprice"];
                        if (GlobalVar.dict熱食已選名稱數量.Keys.Contains(name))
                        {
                            string imagePath = Path.Combine(GlobalVar.image_dir冷食, reader["Cimg"].ToString());
                            Button btn = new Button();
                            btn.BackColor = Color.LightGreen;
                            btn.Size = new Size(flowLayoutPanel購物清單.Width / 3 - 10, flowLayoutPanel購物清單.Height / 4);
                            btn.Text = $"{name} - {GlobalVar.dict熱食已選名稱數量[name]} 份 - {GlobalVar.dict熱食已選名稱數量[name] * price} 元";
                            btn.Font = new Font("微軟正黑體", 11);
                            btn.ForeColor = Color.White;
                            btn.Padding = new Padding(0, 64, 0, 0);
                            btn.Click += new EventHandler(btn_Click);

                            btn.BackgroundImage = Image.FromFile(imagePath);
                            btn.BackgroundImageLayout = ImageLayout.Stretch;

                            btn.Paint += new PaintEventHandler(form1Instance.btn_Paint繪製名稱);
                            flowLayoutPanel購物清單.Controls.Add(btn);
                        }
                    }
                }

                string sql點心 = "SELECT Dname, Dprice, Dimg FROM DessertFood";
                using (SqlCommand command = new SqlCommand(sql點心, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        string name = reader["Dname"].ToString();
                        int price = (int)reader["Dprice"];
                        if (GlobalVar.dict熱食已選名稱數量.Keys.Contains(name))
                        {
                            string imagePath = Path.Combine(GlobalVar.image_dir點心, reader["Dimg"].ToString());
                            Button btn = new Button();
                            btn.BackColor = Color.LightGreen;
                            btn.Size = new Size(flowLayoutPanel購物清單.Width / 3 - 10, flowLayoutPanel購物清單.Height / 4);
                            btn.Text = $"{name} - {GlobalVar.dict熱食已選名稱數量[name]} 份 - {GlobalVar.dict熱食已選名稱數量[name] * price} 元";
                            btn.Font = new Font("微軟正黑體", 11);
                            btn.ForeColor = Color.White;
                            btn.Padding = new Padding(0, 64, 0, 0);
                            btn.Click += new EventHandler(btn_Click);

                            btn.BackgroundImage = Image.FromFile(imagePath);
                            btn.BackgroundImageLayout = ImageLayout.Stretch;

                            btn.Paint += new PaintEventHandler(form1Instance.btn_Paint繪製名稱);
                            flowLayoutPanel購物清單.Controls.Add(btn);
                        }
                    }
                }
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            is點選 = !is點選;
            if (sender is Button clickBtn)
            {
                if (is點選 && clickBtn.BackColor == Color.LightGreen)
                {
                    clickBtn.BackColor = Color.LightBlue;
                }
                else
                {
                    clickBtn.BackColor = Color.LightGreen;
                }
            }

            //is點選 = !is點選;
        }

        void btn折扣_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.is折扣)
            {
                if (txt折扣.Text != "")
                {
                    計算折扣後價格();
                    GlobalVar.is折扣 = !GlobalVar.is折扣;
                    MessageBox.Show($"您以使用 <{txt折扣.Text} 折扣碼>，若您想改使用其他 <折扣碼> 請先點擊 [更換折扣]");
                }
                else
                {
                    MessageBox.Show("您尚未輸入折扣碼");
                }
            }
            else
            {
                MessageBox.Show("您已使用過折扣，若要更換折扣碼請先點擊 [更換折扣]");
            }

            計算最終價格();
        }

        void 計算折扣後價格()
        {
            // 獲取 Form1 的最後一次更新的初始價格
            string[] parts = form1Instance.lbl折扣前價格.Text.Split(':');

            if (parts.Length == 2)
            {
                // 移除 "元"
                Match match = Regex.Match(parts[1], @"\d+");
                if (match.Success)
                {
                    int 原始價格 = Int32.Parse(match.Value);
                    // 先檢查折扣碼是否為空
                    if (string.IsNullOrEmpty(txt折扣.Text))
                    {
                        折扣後價格 = 原始價格;
                    }
                    else
                    {
                        switch (txt折扣.Text.ToLower())
                        {
                            case "aa":
                                折扣後價格 = Convert.ToInt32(Math.Round(原始價格 * 0.9));
                                break;

                            case "bb":
                                折扣後價格 = Convert.ToInt32(Math.Round(原始價格 * 0.8));
                                break;

                            default:
                                MessageBox.Show("未知折扣代碼");
                                折扣後價格 = Convert.ToInt32(原始價格);
                                return;
                        }
                    }

                    lbl折扣後價格.Text = $"折扣後價格: {折扣後價格} 元";
                }
                else
                {
                    Console.WriteLine("數字格式不正確");
                    // 在這裡可以處理轉換失敗的情況，例如設定一個預設值或顯示錯誤訊息。
                }
            }
            else
            {
                MessageBox.Show("價格格式不正確");
            }
        }

        void btn更換折扣_Click(object sender, EventArgs e)
        {
            GlobalVar.is折扣 = false;
            txt折扣.Text = "";
            計算折扣後價格();
            計算最終價格();
        }

        private bool isChkdHandled外帶 = false;

        private void chk外帶_CheckedChanged(object sender, EventArgs e)
        {
            if (!isChkdHandled外帶)
            {
                isChkdHandled外帶 = true;

                if (txt折扣.Text != "" && GlobalVar.is外帶 == false)
                {
                    if (chk外帶.Checked)
                    {
                        GlobalVar.is外帶 = true;
                    }
                    else
                    {
                        GlobalVar.is外帶 = false;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("您是否有折扣需要使用?", "關閉表單確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        chk外帶.Checked = false;
                    }
                    else
                    {
                        chk外帶.Checked = true;
                    }
                }
            }

            isChkdHandled外帶 = false;
            計算折扣後價格();
            計算最終價格();
        }

        private bool isChkHandled買購物袋 = false;

        private void chk買購物袋_CheckedChanged(object sender, EventArgs e)
        {
            if (!isChkHandled買購物袋)
            {
                isChkHandled買購物袋 = true;

                if (txt折扣.Text != "" && GlobalVar.is買購物袋 == false)
                {
                    if (chk外帶.Checked)
                    {
                        GlobalVar.is買購物袋 = true;
                    }
                    else
                    {
                        GlobalVar.is買購物袋 = false;
                        計算折扣後價格();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("您是否有折扣需要使用?", "關閉表單確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        chk買購物袋.Checked = false;
                    }
                    else
                    {
                        chk買購物袋.Checked = true;
                        計算折扣後價格();
                    }
                }
            }

            isChkHandled買購物袋 = false;
            計算最終價格();
        }

        void btn移除品項_Click(object sender, EventArgs e)
        {
            // k-p, 複製一份按鈕清單，以免在迴圈中刪除按鈕時集合被修改
            var buttonsToRemove = new List<Button>(flowLayoutPanel購物清單.Controls.OfType<Button>());

            bool foundSelectedButton = false;
            foreach (var clickBtn in buttonsToRemove)
            {
                if (clickBtn.BackColor == Color.LightBlue)
                {
                    flowLayoutPanel購物清單.Controls.Remove(clickBtn);
                    //form1Instance.

                    // 解析選定項目的內容
                    string strClickBtn = clickBtn.Text.ToString();
                    string[] parts = strClickBtn.Split('-');
                    if (parts.Length == 3)
                    {
                        string 小吃名 = parts[0].Trim();
                        GlobalVar.dict熱食已選名稱數量.Remove(小吃名);
                    }

                    foundSelectedButton = true;
                }
            }

            if (!foundSelectedButton)
            {
                MessageBox.Show("請先點選任一小吃");
            }

            form1Instance.更新初始價格();
            計算折扣後價格();
            計算最終價格();
        }

        void btn返回菜單_Click(object sender, EventArgs e)
        {
            // 顯示隱藏的 form1
            form1Instance.Show();   // k-p, 這裡使用已經存在的實例
            Close();
        }

        void 計算最終價格()
        {
            // 獲取 Form1 的最後一次更新的初始價格
            string[] parts = lbl折扣後價格.Text.Split(':');

            if (parts.Length == 2)
            {
                // 移除 "元"
                Match match = Regex.Match(parts[1], @"\d+");
                if (match.Success)
                {
                    折扣後價格 = Int32.Parse(match.Value);
                }

                if (chk買購物袋.Checked == true)
                {
                    GlobalVar.is買購物袋 = true;
                }
                else
                {
                    GlobalVar.is買購物袋 = false;
                }

                if (GlobalVar.is買購物袋)
                {
                    最終價格 = 折扣後價格 + 2;
                }
                else
                {
                    最終價格 = 折扣後價格;
                }
            }

            lbl最終價格.Text = string.Format("最終價格: {0}元", 最終價格);
        }

        void btn前往付款_Click(object sender, EventArgs e)
        {

            // 檢查 flowLayoutPanel 購物清單中是否有 Button
            bool hasButtons = flowLayoutPanel購物清單.Controls.OfType<Button>().Any();

            if (hasButtons)
            {
                FormPay formPay = new FormPay();
                formPay.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("您已無任何可購買美食");
            }
        }

        private void FormOrderList_FormClosing(object sender, FormClosingEventArgs e)
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
