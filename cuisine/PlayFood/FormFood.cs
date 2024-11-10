using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayFood
{
    public partial class FormFood : Form
    {
        FormMain formMainInstrance;

        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

        public List<string> listHotFoodProductName = new List<string>();
        public List<string> listColdFoodProductName = new List<string>();
        public List<string> listDessertFoodProductName = new List<string>();

        public List<int> listHotFoodPrice = new List<int>();
        public List<int> listColdFoodPrice = new List<int>();
        public List<int> listDessertFoodPrice = new List<int>();

        List<int> listHID = new List<int>();
        List<int> listCID = new List<int>();
        List<int> listDID = new List<int>();

        Label lbl管理者資訊;
        ComboBox cmb搜尋;
        TextBox txt搜尋;

        ListView listView商品展示熱食;
        ListView listView商品展示冷食;
        ListView listView商品展示點心;

        public int selectID = 0;

        string image_修改後的檔名 = "";
        bool is修改圖檔 = false;

        public FormFood()
        {
            InitializeComponent();
            Size = new Size(1033, 741);
            BackColor = Color.LightSteelBlue;
        }

        private void FormFood_Load(object sender, EventArgs e)
        {
            if (GlobalVar.is管理者登入 == false)
            {
                MessageBox.Show("請先登入");
                Close();
            }

            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(446, 20);
            lbl名稱.Size = new Size(180, 40);
            lbl名稱.Text = "商品管理中心";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            lbl管理者資訊 = new Label();
            lbl管理者資訊.Location = new Point(150, 60);
            lbl管理者資訊.AutoSize = false;
            lbl管理者資訊.Size = new Size(160, 30);
            lbl管理者資訊.Text = $"使用者:{GlobalVar.管理者姓名} 權限:{GlobalVar.管理者權限}";
            lbl管理者資訊.Font = new Font("微軟正黑體", 12);
            Controls.Add(lbl管理者資訊);


            /* 搜尋 */
            GroupBox groupBoxn搜尋 = new GroupBox();
            groupBoxn搜尋.Location = new Point(50, 80);
            groupBoxn搜尋.Size = new Size(400, 80);
            groupBoxn搜尋.Text = "快速搜尋";
            groupBoxn搜尋.Font = new Font("微軟正黑體", 15);
            Controls.Add(groupBoxn搜尋);

            cmb搜尋 = new ComboBox();
            cmb搜尋.Location = new Point(20, 30);
            cmb搜尋.Size = new Size(100, 40);
            cmb搜尋.Font = new Font("微軟正黑體", 12);
            cmb搜尋.DropDownStyle = ComboBoxStyle.DropDownList;
            groupBoxn搜尋.Controls.Add(cmb搜尋);

            cmb搜尋.Items.Add("商品名稱");
            cmb搜尋.Items.Add("商品價格");
            cmb搜尋.Items.Add("商品產地");
            cmb搜尋.SelectedIndex = 0;

            txt搜尋 = new TextBox();
            txt搜尋.Location = new Point(130, 30);
            txt搜尋.Size = new Size(120, 30);
            txt搜尋.Font = new Font("微軟正黑體", 9);
            groupBoxn搜尋.Controls.Add(txt搜尋);

            Button btn搜尋 = new Button();
            btn搜尋.Location = new Point(280, 30);
            btn搜尋.Size = new Size(80, 40);
            btn搜尋.BackColor = Color.LightYellow;
            btn搜尋.Text = "搜尋";
            btn搜尋.Font = new Font("微軟正黑體", 14);
            btn搜尋.Click += new EventHandler(btn搜尋_Click);
            groupBoxn搜尋.Controls.Add(btn搜尋);


            /* 按鈕 */
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Location = new Point(500, 88);
            flowLayoutPanel.Size = new Size(520, 100);
            Controls.Add(flowLayoutPanel);

            List<string> listBtnStr商品管理系統 = new List<string>()
            { "圖片模式", "列表模式", "新增商品", "重新整理", "返回中心" };

            List<EventHandler> listEventHandler會員管理系統 = new List<EventHandler>()
            {
                new EventHandler(btn圖片模式_Click), new EventHandler(btn列表模式_Click),
                new EventHandler(btn新增商品_Click), new EventHandler(btn重新整理_Click),
                new EventHandler(btn返回中心_Click)
            };

            int i = 0;
            foreach (string str in listBtnStr商品管理系統)
            {
                Button btn = new Button();
                btn.Size = new Size(120, 40);
                btn.BackColor = Color.LightYellow;
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 14);
                btn.Click += listEventHandler會員管理系統[i];
                flowLayoutPanel.Controls.Add(btn);
                i++;
            }


            /* 商品頁 */
            TabControl tabControl美食 = new TabControl();
            tabControl美食.Location = new Point(50, 180);
            tabControl美食.Size = new Size(900, 480);
            Controls.Add(tabControl美食);

            TabPage tabPage熱食 = new TabPage();
            tabPage熱食.Text = "熱食";
            tabControl美食.Controls.Add(tabPage熱食);

            TabPage tabPage冷食 = new TabPage();
            tabPage冷食.Text = "冷食";
            tabControl美食.Controls.Add(tabPage冷食);

            TabPage tabPage點心 = new TabPage();
            tabPage點心.Text = "點心";
            tabControl美食.Controls.Add(tabPage點心);

            listView商品展示熱食 = new ListView();
            listView商品展示熱食.Size = new Size(950, 480);
            listView商品展示熱食.ItemActivate += new EventHandler(listView商品展示熱食_ItemActivate);
            tabPage熱食.Controls.Add(listView商品展示熱食);

            listView商品展示冷食 = new ListView();
            listView商品展示冷食.Size = new Size(900, 480);
            listView商品展示冷食.ItemActivate += new EventHandler(listView商品展示冷食_ItemActivate);
            tabPage冷食.Controls.Add(listView商品展示冷食);

            listView商品展示點心 = new ListView();
            listView商品展示點心.Size = new Size(900, 480);
            listView商品展示點心.ItemActivate += new EventHandler(listView商品展示點心_ItemActivate);
            tabPage點心.Controls.Add(listView商品展示點心);


            scsb.DataSource = @".";
            scsb.InitialCatalog = "playfood";
            scsb.IntegratedSecurity = true;
            GlobalVar.strDBConnectionString = scsb.ConnectionString;

            讀取熱食商品資料庫();
            讀取冷食商品資料庫();
            讀取點心商品資料庫();
            顯示熱食listView_圖片模式();
            顯示冷食listView_圖片模式();
            顯示點心listView_圖片模式();
        }

        void 讀取熱食商品資料庫()
        {
            SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
            con.Open();
            string strSQL熱食 = "select top 200 * from HotFood;";
            SqlCommand cmd = new SqlCommand(strSQL熱食, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read() == true)
            {
                listHID.Add((int)reader["HID"]);
                listHotFoodProductName.Add((string)reader["Hname"]);
                listHotFoodPrice.Add((int)reader["Hprice"]);
                string image_name = (string)reader["Himg"];
                string 完整圖檔路徑 = $"{GlobalVar.image_dir熱食}\\{image_name}";
                Image img產品圖檔 = Image.FromFile(完整圖檔路徑);
                imageList熱食商品圖檔.Images.Add(img產品圖檔);
                count += 1;
            }

            reader.Close();
            con.Close();
            Console.WriteLine($"讀取{count}筆資料");
        }

        void 讀取冷食商品資料庫()
        {
            SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
            con.Open();
            string strSQL冷食 = "select top 200 * from ColdFood;";
            SqlCommand cmd = new SqlCommand(strSQL冷食, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read() == true)
            {
                listCID.Add((int)reader["CID"]);
                listColdFoodProductName.Add((string)reader["Cname"]);
                listColdFoodPrice.Add((int)reader["Cprice"]);
                string image_name = (string)reader["Cimg"];
                string 完整圖檔路徑 = $"{GlobalVar.image_dir冷食}\\{image_name}";
                Image img產品圖檔 = Image.FromFile(完整圖檔路徑);
                imageList冷食商品圖檔.Images.Add(img產品圖檔);
                count += 1;
            }

            reader.Close();
            con.Close();
            Console.WriteLine($"讀取{count}筆資料");
        }

        void 讀取點心商品資料庫()
        {
            SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
            con.Open();
            string strSQL點心 = "select top 200 * from DessertFood;";
            SqlCommand cmd = new SqlCommand(strSQL點心, con);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read() == true)
            {
                listDID.Add((int)reader["DID"]);
                listDessertFoodProductName.Add((string)reader["Dname"]);
                listDessertFoodPrice.Add((int)reader["Dprice"]);
                string image_name = (string)reader["Dimg"];
                string 完整圖檔路徑 = $"{GlobalVar.image_dir點心}\\{image_name}";
                Image img產品圖檔 = Image.FromFile(完整圖檔路徑);
                imageList點心商品圖檔.Images.Add(img產品圖檔);
                count += 1;
            }

            reader.Close();
            con.Close();
            Console.WriteLine($"讀取{count}筆資料");
        }

        void 顯示熱食listView_圖片模式()
        {
            listView商品展示熱食.Clear();
            listView商品展示熱食.View = View.LargeIcon;//LargeIcon, Tile, List, SmallIcon

            imageList熱食商品圖檔.ImageSize = new Size(listView商品展示熱食.Width / 5 - 7, listView商品展示熱食.Height / 4);
            listView商品展示熱食.LargeImageList = imageList熱食商品圖檔; //LargeIcon, Tile
            listView商品展示熱食.SmallImageList = imageList熱食商品圖檔; //SmallIcon, List

            for (int i = 0; i < imageList熱食商品圖檔.Images.Count; i += 1)
            {
                ListViewItem item熱食 = new ListViewItem();
                item熱食.ImageIndex = i;
                item熱食.Text = $"{listHotFoodProductName[i]} {listHotFoodPrice[i]}元";
                item熱食.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item熱食.Tag = listHID[i];
                listView商品展示熱食.Items.Add(item熱食);
            }
        }

        void 顯示冷食listView_圖片模式()
        {
            listView商品展示冷食.Clear();
            listView商品展示冷食.View = View.LargeIcon;//LargeIcon, Tile, List, SmallIcon

            imageList冷食商品圖檔.ImageSize = new Size(listView商品展示冷食.Width / 5 - 7, listView商品展示冷食.Height / 4);
            listView商品展示冷食.LargeImageList = imageList冷食商品圖檔; //LargeIcon, Tile
            listView商品展示冷食.SmallImageList = imageList冷食商品圖檔; //SmallIcon, List

            for (int i = 0; i < imageList冷食商品圖檔.Images.Count; i += 1)
            {
                ListViewItem item冷食 = new ListViewItem();
                item冷食.ImageIndex = i;
                item冷食.Text = $"{listColdFoodProductName[i]} {listColdFoodPrice[i]}元";
                item冷食.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item冷食.Tag = listCID[i];
                listView商品展示冷食.Items.Add(item冷食);
            }
        }

        void 顯示點心listView_圖片模式()
        {
            listView商品展示點心.Clear();
            listView商品展示點心.View = View.LargeIcon;//LargeIcon, Tile, List, SmallIcon

            imageList點心商品圖檔.ImageSize = new Size(listView商品展示點心.Width / 5 - 7, listView商品展示點心.Height / 4);
            listView商品展示點心.LargeImageList = imageList點心商品圖檔; //LargeIcon, Tile
            listView商品展示點心.SmallImageList = imageList點心商品圖檔; //SmallIcon, List

            for (int i = 0; i < imageList點心商品圖檔.Images.Count; i += 1)
            {
                ListViewItem item點心 = new ListViewItem();
                item點心.ImageIndex = i;
                item點心.Text = $"{listDessertFoodProductName[i]} {listDessertFoodPrice[i]}元";
                item點心.Font = new Font("微軟正黑體", 14, FontStyle.Regular);
                item點心.Tag = listDID[i];
                listView商品展示點心.Items.Add(item點心);
            }
        }

        private void btn圖片模式_Click(object sender, EventArgs e)
        {
            顯示熱食listView_圖片模式();
            顯示冷食listView_圖片模式();
            顯示點心listView_圖片模式();
        }

        void 顯示熱食ListView_列表模式()
        {
            listView商品展示熱食.Clear();
            listView商品展示熱食.LargeImageList = null;
            listView商品展示熱食.SmallImageList = null;
            listView商品展示熱食.View = View.Details;
            listView商品展示熱食.Columns.Add("ID", 100);
            listView商品展示熱食.Columns.Add("商品名稱", 200);
            listView商品展示熱食.Columns.Add("商品價格", 100);
            listView商品展示熱食.GridLines = true;
            listView商品展示熱食.FullRowSelect = true;

            for (int i = 0; i < listHID.Count; i += 1)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                listItem.Tag = listHID[i];
                listItem.Text = listHID[i].ToString();
                listItem.SubItems.Add(listHotFoodProductName[i]);
                listItem.SubItems.Add(listHotFoodPrice[i].ToString());
                listItem.ForeColor = Color.DarkBlue;
                if (i % 2 == 0)
                {
                    listItem.BackColor = Color.LightGray;
                }
                else
                {
                    listItem.BackColor = Color.White;
                }
                listView商品展示熱食.Items.Add(listItem);
            }
        }

        void 顯示冷食ListView_列表模式()
        {
            listView商品展示冷食.Clear();
            listView商品展示冷食.LargeImageList = null;
            listView商品展示冷食.SmallImageList = null;
            listView商品展示冷食.View = View.Details;
            listView商品展示冷食.Columns.Add("ID", 100);
            listView商品展示冷食.Columns.Add("商品名稱", 200);
            listView商品展示冷食.Columns.Add("商品價格", 100);
            listView商品展示冷食.GridLines = true;
            listView商品展示冷食.FullRowSelect = true;

            for (int i = 0; i < listCID.Count; i += 1)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                listItem.Tag = listCID[i];
                listItem.Text = listCID[i].ToString();
                listItem.SubItems.Add(listColdFoodProductName[i]);
                listItem.SubItems.Add(listColdFoodPrice[i].ToString());
                listItem.ForeColor = Color.DarkBlue;
                if (i % 2 == 0)
                {
                    listItem.BackColor = Color.LightGray;
                }
                else
                {
                    listItem.BackColor = Color.White;
                }
                listView商品展示冷食.Items.Add(listItem);
            }
        }

        void 顯示點心ListView_列表模式()
        {
            listView商品展示點心.Clear();
            listView商品展示點心.LargeImageList = null;
            listView商品展示點心.SmallImageList = null;
            listView商品展示點心.View = View.Details;
            listView商品展示點心.Columns.Add("ID", 100);
            listView商品展示點心.Columns.Add("商品名稱", 200);
            listView商品展示點心.Columns.Add("商品價格", 100);
            listView商品展示點心.GridLines = true;
            listView商品展示點心.FullRowSelect = true;

            for (int i = 0; i < listDID.Count; i += 1)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                listItem.Tag = listDID[i];
                listItem.Text = listDID[i].ToString();
                listItem.SubItems.Add(listDessertFoodProductName[i]);
                listItem.SubItems.Add(listDessertFoodPrice[i].ToString());
                listItem.ForeColor = Color.DarkBlue;
                if (i % 2 == 0)
                {
                    listItem.BackColor = Color.LightGray;
                }
                else
                {
                    listItem.BackColor = Color.White;
                }
                listView商品展示點心.Items.Add(listItem);
            }
        }

        private void btn列表模式_Click(object sender, EventArgs e)
        {
            顯示熱食ListView_列表模式();
            顯示冷食ListView_列表模式();
            顯示點心ListView_列表模式();
        }

        private void btn新增商品_Click(object sender, EventArgs e)
        {
            FormFoodDetails myFormDetail = new FormFoodDetails();
            myFormDetail.ShowDialog();
        }

        private void btn重新整理_Click(object sender, EventArgs e)
        {
            重新載入熱食資料();
            重新載入冷食資料();
            重新載入點心資料();
        }

        void 重新載入熱食資料()
        {
            listHID.Clear();
            listHotFoodPrice.Clear();
            listHotFoodProductName.Clear();
            imageList熱食商品圖檔.Images.Clear();
            讀取熱食商品資料庫();

            if (listView商品展示熱食.View == View.Details)
            {
                顯示熱食ListView_列表模式();
            }
            else
            {
                顯示熱食listView_圖片模式();
            }
        }

        void 重新載入冷食資料()
        {
            listCID.Clear();
            listColdFoodPrice.Clear();
            listColdFoodProductName.Clear();
            imageList冷食商品圖檔.Images.Clear();
            讀取冷食商品資料庫();

            if (listView商品展示冷食.View == View.Details)
            {
                顯示冷食ListView_列表模式();
            }
            else
            {
                顯示冷食listView_圖片模式();
            }
        }

        void 重新載入點心資料()
        {
            listDID.Clear();
            listDessertFoodPrice.Clear();
            listDessertFoodProductName.Clear();
            imageList點心商品圖檔.Images.Clear();
            讀取點心商品資料庫();

            if (listView商品展示點心.View == View.Details)
            {
                顯示點心ListView_列表模式();
            }
            else
            {
                顯示點心listView_圖片模式();
            }
        }

        private void listView商品展示熱食_ItemActivate(object sender, EventArgs e)
        {
            FormFoodDetails myFormDetail = new FormFoodDetails();
            myFormDetail.selectHID = (int)listView商品展示熱食.SelectedItems[0].Tag;
            myFormDetail.ShowDialog();
        }

        private void listView商品展示冷食_ItemActivate(object sender, EventArgs e)
        {
            FormFoodDetails myFormDetail = new FormFoodDetails();
            myFormDetail.selectHID = (int)listView商品展示冷食.SelectedItems[0].Tag;
            myFormDetail.ShowDialog();
        }

        private void listView商品展示點心_ItemActivate(object sender, EventArgs e)
        {
            FormFoodDetails myFormDetail = new FormFoodDetails();
            myFormDetail.selectHID = (int)listView商品展示點心.SelectedItems[0].Tag;
            myFormDetail.ShowDialog();
        }

        void btn返回中心_Click(object sender, EventArgs e)
        {
            Close();
            formMainInstrance = new FormMain();
            formMainInstrance.Show();
        }

        void btn搜尋_Click(object sender, EventArgs e)
        {
            /*
            if (txt搜尋.Text != "")
            {
                listBox會員.Items.Clear();
                SearchPIDs.Clear();
                string str欄位名稱 = cmb搜尋一.SelectedItem.ToString();

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = $"select * from Persons where {str欄位名稱} like @SearchKeyWord;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchKeyWord", $"{txt搜尋一.Text}%");
                SqlDataReader reader = cmd.ExecuteReader();

                int count = 0;
                while (reader.Read() == true)
                {
                    SearchPIDs.Add((int)reader["PID"]);   // k-p, 索引值對應
                    listBox會員.Items.Add($"PID: {reader["PID"]}, 姓名: {reader["姓名"]}, 電話: {reader["電話"]}");
                    count++;
                }

                if (count == 0)
                {
                    MessageBox.Show("查無此人");
                }

                reader.Close();
                con.Close();
            }
            else
            {
                MessageBox.Show("請輸入查詢關鍵字");
            }*/
        }
    }
}
