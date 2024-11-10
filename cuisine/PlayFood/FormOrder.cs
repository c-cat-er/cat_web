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
using System.Windows.Forms.DataVisualization.Charting;

namespace PlayFood
{
    public partial class FormOrder : Form
    {
        FormMain formMainInstrance;

        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        string strDBConnectionString = "";      // 資料庫連線字串

        List<int> SearchOIDs = new List<int>();  // 暫存訂單編號ID
        int DGV筆數 = 0;      // DataGridView 的資料筆數

        ComboBox cmb搜尋一;
        TextBox txt搜尋一;
        ComboBox cmb搜尋二;
        TextBox txt搜尋二;
        ListBox listBox訂單;
        Label lbl筆數;
        TextBox txtOID;
        TextBox txt訂購人姓名;
        TextBox txt訂單編號;
        TextBox txt訂購人電話;
        TextBox txt訂購人地址;
        DateTimePicker dtp下訂日;
        TextBox txt訂單項目;
        TextBox txt訂單金額;
        DateTimePicker dtp預計出貨日;
        DateTimePicker dtp預計到貨日;
        FlowLayoutPanel flowLayoutPanel訂單資料修改按鈕;
        Chart chart營業額;

        public FormOrder()
        {
            InitializeComponent();
            Size = new Size(1033, 741);
            BackColor = Color.LightSteelBlue;
        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'playfoodDataSet.Orders' 資料表。您可以視需要進行移動或移除。
            this.ordersTableAdapter.Fill(this.playfoodDataSet.Orders);

            scsb.DataSource = @"."; //伺服器名稱
            scsb.InitialCatalog = "playfood"; //資料庫名稱
            scsb.IntegratedSecurity = true;        // k-p, true 指 windows 驗證。false 指 SQLServer 驗證
            strDBConnectionString = scsb.ConnectionString;      // k-p, ConnectionString 是 SqlConnectionStringBuilder 類的一個屬性，
                                                                // 包含了用於建立到 SQL Server 的連接所需的信息，例如數據庫名稱、用戶名、密碼等。


            if (GlobalVar.is管理者登入 == false)
            {
                MessageBox.Show("請先登入");
                Close();
            }

            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(426, 20);
            lbl名稱.Size = new Size(180, 40);
            lbl名稱.Text = "訂單管理中心";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Location = new Point(50, 90);
            flowLayoutPanel.Size = new Size(140, 300);
            Controls.Add(flowLayoutPanel);

            List<string> listBtnStr訂單管理系統 = new List<string>()
            { "所有訂單", "訂單資料修改", "數據分析", "返回中心" };

            List<EventHandler> listEH訂單管理系統 = new List<EventHandler>()
            {
                new EventHandler(btn所有訂單_Click), new EventHandler(btn訂單資料修改_Click),
                new EventHandler(btn數據分析_Click), new EventHandler(btn返回中心_Click)
            };

            int i = 0;
            foreach (string str in listBtnStr訂單管理系統)
            {
                Button btn = new Button();
                btn.Size = new Size(130, 50);
                btn.BackColor = Color.LightYellow;
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 13);
                btn.Click += listEH訂單管理系統[i];
                flowLayoutPanel.Controls.Add(btn);
                i++;
            }


            /* 右邊 */
            產生訂單資料列表DataGridView();

            /* 上面 */
            GroupBox groupBox = new GroupBox();
            groupBox.Location = new Point(35, 10);
            groupBox.Size = new Size(680, 100);
            groupBox.Text = "快速搜尋";
            groupBox.Font = new Font("微軟正黑體", 15);
            tabPage訂單資料修改.Controls.Add(groupBox);

            cmb搜尋一 = new ComboBox();
            cmb搜尋一.Location = new Point(20, 35);
            cmb搜尋一.Size = new Size(120, 20);
            cmb搜尋一.Font = new Font("微軟正黑體", 11);
            cmb搜尋一.DropDownStyle = ComboBoxStyle.DropDownList;
            groupBox.Controls.Add(cmb搜尋一);

            cmb搜尋一.Items.Add("訂購人姓名");
            cmb搜尋一.Items.Add("訂購人電話");
            cmb搜尋一.Items.Add("訂購人地址");
            cmb搜尋一.Items.Add("訂單項目");
            cmb搜尋一.Items.Add("訂單金額");
            cmb搜尋一.SelectedIndex = 0;

            txt搜尋一 = new TextBox();
            txt搜尋一.Location = new Point(150, 35);
            txt搜尋一.Multiline = true;
            txt搜尋一.Size = new Size(120, 23);
            txt搜尋一.Font = new Font("微軟正黑體", 11);
            groupBox.Controls.Add(txt搜尋一);

            cmb搜尋二 = new ComboBox();
            cmb搜尋二.Location = new Point(300, 35);
            cmb搜尋二.Size = new Size(120, 20);
            cmb搜尋二.Font = new Font("微軟正黑體", 11);
            cmb搜尋二.DropDownStyle = ComboBoxStyle.DropDownList;
            groupBox.Controls.Add(cmb搜尋二);

            cmb搜尋二.Items.Add("訂購人姓名");
            cmb搜尋二.Items.Add("訂購人電話");
            cmb搜尋二.Items.Add("訂購人地址");
            cmb搜尋二.Items.Add("訂單項目");
            cmb搜尋一.Items.Add("訂單金額");
            cmb搜尋二.SelectedIndex = 1;

            txt搜尋二 = new TextBox();
            txt搜尋二.Location = new Point(430, 35);
            txt搜尋二.Multiline = true;
            txt搜尋二.Size = new Size(120, 23);
            txt搜尋二.Font = new Font("微軟正黑體", 11);
            groupBox.Controls.Add(txt搜尋二);

            Label lbl開始日 = new Label();
            lbl開始日.Location = new Point(30, 65);
            lbl開始日.Size = new Size(90, 25);
            lbl開始日.Text = "預計出貨日";
            lbl開始日.Font = new Font("微軟正黑體", 11);
            groupBox.Controls.Add(lbl開始日);

            Label lbl結束日 = new Label();
            lbl結束日.Location = new Point(310, 65);
            lbl結束日.Size = new Size(90, 25);
            lbl結束日.Text = "預計到貨日";
            lbl結束日.Font = new Font("微軟正黑體", 11);
            groupBox.Controls.Add(lbl結束日);

            dtp預計出貨日 = new DateTimePicker();
            dtp預計出貨日.Location = new Point(140, 65);
            dtp預計出貨日.Size = new Size(140, 25);
            dtp預計出貨日.Value = new DateTime(1900, 1, 1); // 2023年12月31日下午3:30:00
            dtp預計出貨日.Font = new Font("微軟正黑體", 10);
            groupBox.Controls.Add(dtp預計出貨日);

            dtp預計到貨日 = new DateTimePicker();
            dtp預計到貨日.Location = new Point(420, 65);
            dtp預計到貨日.Size = new Size(140, 25);
            dtp預計到貨日.Value = DateTime.Today;  // k-p, 今日
            dtp預計到貨日.Font = new Font("微軟正黑體", 10);
            groupBox.Controls.Add(dtp預計到貨日);

            Button btn清空欄位 = new Button();
            btn清空欄位.Location = new Point(580, 30);
            btn清空欄位.Size = new Size(70, 30);
            btn清空欄位.Text = "清空欄位";
            btn清空欄位.Font = new Font("微軟正黑體", 12);
            btn清空欄位.Click += new EventHandler(btn清空搜尋欄位_Click);
            groupBox.Controls.Add(btn清空欄位);

            Button btn搜尋 = new Button();
            btn搜尋.Location = new Point(580, 65);
            btn搜尋.Size = new Size(70, 30);
            btn搜尋.Text = "搜尋";
            btn搜尋.Font = new Font("微軟正黑體", 12);
            btn搜尋.Click += new EventHandler(btn搜尋_Click);
            groupBox.Controls.Add(btn搜尋);


            /* 中間 */
            dgv訂單資料修改列表.SelectionChanged += dgv訂單資料修改列表_SelectionChanged;

            lbl筆數 = new Label();
            lbl筆數.Location = new Point(200, 450);
            lbl筆數.Size = new Size(120, 25);
            lbl筆數.Text = $"共 ? 筆 / 第 1 筆";
            lbl筆數.Font = new Font("微軟正黑體", 12);
            tabPage訂單資料修改.Controls.Add(lbl筆數);
            顯示資料筆數();


            /* 中右 */
            FlowLayoutPanel flowLayoutPanel標籤 = new FlowLayoutPanel();
            flowLayoutPanel標籤.Location = new Point(325, 131);
            flowLayoutPanel標籤.Size = new Size(100, 320);
            tabPage訂單資料修改.Controls.Add(flowLayoutPanel標籤);

            List<string> listLblStr = new List<string>()
                        {
                            "ID", "訂單編號", "訂購人姓名", "訂購人電話", "訂購人地址", "訂單項目", "", "訂單金額",
                            "下訂日", "預計出貨日", "預計到貨日"
                        };

            int j = 0;
            foreach (string str in listLblStr)
            {
                Label lbl = new Label();
                lbl.Size = new Size(90, 29);
                lbl.Text = str;
                lbl.Font = new Font("微軟正黑體", 12);
                flowLayoutPanel標籤.Controls.Add(lbl);
                j++;
            }

            FlowLayoutPanel flowLayoutPanel輸入框 = new FlowLayoutPanel();
            flowLayoutPanel輸入框.Location = new Point(440, 125);
            flowLayoutPanel輸入框.Size = new Size(320, 320);
            tabPage訂單資料修改.Controls.Add(flowLayoutPanel輸入框);

            txtOID = new TextBox();
            txtOID.Size = new Size(170, 22);
            txtOID.ReadOnly = true;  // k-p, 只讀
            txtOID.Text = "(只讀)";
            flowLayoutPanel輸入框.Controls.Add(txtOID);

            txt訂單編號 = new TextBox();
            txt訂單編號.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txt訂單編號);

            txt訂購人姓名 = new TextBox();
            txt訂購人姓名.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txt訂購人姓名);

            txt訂購人電話 = new TextBox();
            txt訂購人電話.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txt訂購人電話);

            txt訂購人地址 = new TextBox();
            txt訂購人地址.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txt訂購人地址);

            txt訂單項目 = new TextBox();
            txt訂單項目.Multiline = true;
            txt訂單項目.Size = new Size(300, 50);
            txt訂單項目.ScrollBars = ScrollBars.Both;
            flowLayoutPanel輸入框.Controls.Add(txt訂單項目);

            txt訂單金額 = new TextBox();
            txt訂單金額.Size = new Size(170, 50);
            flowLayoutPanel輸入框.Controls.Add(txt訂單金額);

            dtp下訂日 = new DateTimePicker();
            dtp下訂日.Size = new Size(170, 22);
            dtp下訂日.Value = new DateTime(1900, 1, 1);
            flowLayoutPanel輸入框.Controls.Add(dtp下訂日);

            dtp預計出貨日 = new DateTimePicker();
            dtp預計出貨日.Size = new Size(170, 22);
            dtp預計出貨日.Value = new DateTime(1900, 1, 1);
            flowLayoutPanel輸入框.Controls.Add(dtp預計出貨日);

            dtp預計到貨日 = new DateTimePicker();
            dtp預計到貨日.Size = new Size(170, 22);
            dtp預計到貨日.Value = DateTime.Today;
            flowLayoutPanel輸入框.Controls.Add(dtp預計到貨日);

            /* 下面 */
            List<string> listBtnStr訂單資料修改 = new List<string>()
                        { "重新整理", "清空欄位", "新增訂單", "儲存訂單", "延遲出貨", "刪除訂單" };

            List<EventHandler> listEventHandler訂單資料修改 = new List<EventHandler>()
                {
                new EventHandler(btn重新整理_Click), new EventHandler(btn清空欄位_Click),
                new EventHandler(btn新增訂單_Click), new EventHandler(btn儲存訂單_Click),
                new EventHandler(btn延遲出貨_Click),new EventHandler(btn刪除訂單_Click)
                };

            flowLayoutPanel訂單資料修改按鈕 = new FlowLayoutPanel();
            flowLayoutPanel訂單資料修改按鈕.Location = new Point(35, 480);
            flowLayoutPanel訂單資料修改按鈕.Size = new Size(750, 45);
            tabPage訂單資料修改.Controls.Add(flowLayoutPanel訂單資料修改按鈕);

            int l = 0;
            foreach (string str in listBtnStr訂單資料修改)
            {
                Button btn = new Button();
                btn.Size = new Size(100, 40);
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 14);
                btn.Click += listEventHandler訂單資料修改[l];
                flowLayoutPanel訂單資料修改按鈕.Controls.Add(btn);
                l++;
            }


            /* 圖表 */
            
            Label lbl營業額 = new Label();
            lbl營業額.Location = new Point(50, 50);
            lbl營業額.Size = new Size(100, 30);
            lbl營業額.Text = "營業額";
            lbl營業額.Font = new Font("微軟正黑體", 12);
            tabPage數據分析.Controls.Add(lbl營業額);

            chart營業額 = new Chart();
            chart營業額.Location = new Point(200, 50);
            chart營業額.Size = new Size(500, 300);
            chart營業額.BackColor = Color.LightBlue;
            tabPage數據分析.Controls.Add(chart營業額);
            讀取資料表生成圖();
        }

        void 讀取資料表生成圖()
        {
            DataTable dataTable = new DataTable();

            // 添加列
            dataTable.Columns.Add("下訂日", typeof(DateTime)); // 修改列类型为 DateTime
            dataTable.Columns.Add("訂單金額", typeof(int));

            using (SqlConnection con = new SqlConnection(strDBConnectionString))
            {
                con.Open();
                string strSQL = "SELECT 下訂日, 訂單金額 FROM Orders";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataReader reader = cmd.ExecuteReader();

                // 檢查是否有資料
                if (reader.HasRows)
                {
                    // 读取数据库数据并添加到dataTable中
                    while (reader.Read())
                    {
                        // 假设数据库中的日期和營收金額列的名称是下訂日和訂單金額
                        DateTime date = Convert.ToDateTime(reader["下訂日"]); // 使用 Convert.ToDateTime 转换为 DateTime 类型
                        int revenue = Convert.ToInt32(reader["訂單金額"]);
                        dataTable.Rows.Add(date, revenue);
                    }
                }
                else
                {
                    MessageBox.Show("No data found in Orders table.");
                }
            }
            // 創建一個新的系列
            Series series = new Series("營業額");
            chart營業額.Series.Add(series);

            // 添加 ChartArea
            ChartArea chartArea = new ChartArea();
            chart營業額.ChartAreas.Add(chartArea);

            // 將資料表繫結到圖表
            chart營業額.DataSource = dataTable;

            // 設置X軸和Y軸的數據綁定關係
            chart營業額.Series[0].XValueMember = "下訂日";
            chart營業額.Series[0].YValueMembers = "訂單金額";

            // 設置圖表屬性
            chart營業額.Titles.Add("營業額圖表");
            chart營業額.ChartAreas[0].AxisX.Title = "下訂日";
            chart營業額.ChartAreas[0].AxisY.Title = "訂單金額";
            chart營業額.Series[0].ChartType = SeriesChartType.Column;

            // 設置顏色
            chart營業額.Series[0].Color = Color.Blue;
            foreach (DataPoint point in chart營業額.Series[0].Points)
            {
                point.Color = Color.Blue;
            }

            // 設置X軸的範圍
            chart營業額.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
            chart營業額.ChartAreas[0].AxisX.Minimum = DateTime.Parse("2022-12-01").ToOADate();
            chart營業額.ChartAreas[0].AxisX.Maximum = DateTime.Parse("2022-12-05").ToOADate();
            chart營業額.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
            chart營業額.ChartAreas[0].AxisX.Interval = 1;

            // 設置Y軸的範圍
            chart營業額.ChartAreas[0].AxisY.Minimum = 0;
            chart營業額.ChartAreas[0].AxisY.Maximum = 30;

            // 更新圖表
            chart營業額.DataBind();
        }

        void btn所有訂單_Click(object sender, EventArgs e)
        {
            tabControl訂單.SelectedIndex = tabControl訂單.TabPages.IndexOf(tabPage所有訂單);
        }

        void 產生訂單資料列表DataGridView()
        {
            SqlConnection con = new SqlConnection(strDBConnectionString);
            con.Open();
            string strSQL = "select * from Orders";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows == true)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                Console.WriteLine($"DGV筆數:{dt.Rows.Count}");
                DGV筆數 = dt.Rows.Count;
                dgv訂單資料列表.DataSource = dt;
                dgv訂單資料修改列表.DataSource = dt;
            }
            else
            {
                MessageBox.Show("找不到資料");
            }
            reader.Close();
            con.Close();
        }

        private void dgv訂單資料列表_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.RowIndex < DGV筆數))
            {
                string strSelectID = dgv訂單資料列表.Rows[e.RowIndex].Cells[0].Value.ToString();
                int selectOID = 0;
                Int32.TryParse(strSelectID, out selectOID);

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = "select * from Orders where OID = @SearchOID";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchOID", selectOID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    txtOID.Text = reader["OID"].ToString();
                    txt訂單編號.Text = reader["訂單編號"].ToString();
                    txt訂購人姓名.Text = reader["訂購人姓名"].ToString();
                    txt訂購人電話.Text = reader["訂購人電話"].ToString();
                    txt訂購人地址.Text = reader["訂購人地址"].ToString();
                    dtp下訂日.Value = Convert.ToDateTime(reader["下訂日"]);
                    txt訂單項目.Text = reader["訂單項目"].ToString();
                    txt訂單金額.Text = reader["訂單金額"].ToString();
                    dtp預計出貨日.Value = Convert.ToDateTime(reader["預計出貨日"]);
                    dtp預計到貨日.Value = Convert.ToDateTime(reader["預計到貨日"]);
                }
                else
                {
                    MessageBox.Show("查無此訂單");
                    清空欄位();
                }
                reader.Close();
                con.Close();
            }
        }

        void btn數據分析_Click(object sender, EventArgs e)
        {
            tabControl訂單.SelectedIndex = tabControl訂單.TabPages.IndexOf(tabPage數據分析);
        }

        void btn清空搜尋欄位_Click(object sender, EventArgs e)
        {
            txt搜尋一.Clear();
            txt搜尋二.Clear();
            dtp預計出貨日.Value = new DateTime(1900, 1, 1);
            dtp預計到貨日.Value = DateTime.Today;
        }

        private void dgv訂單資料修改列表_SelectionChanged(object sender, EventArgs e)
        {
            顯示資料筆數();
        }

        void 清空欄位()
        {
            txtOID.Clear();
            txt訂購人姓名.Clear();
            txt訂購人電話.Clear();
            txt訂購人地址.Clear();
            dtp下訂日.Value = DateTime.Now;
            txt訂單項目.Clear();
            txt訂單金額.Clear();
            dtp預計出貨日.Value = new DateTime(1900, 1, 1);
            dtp預計到貨日.Value = DateTime.Today;
        }

        void btn訂單資料修改_Click(object sender, EventArgs e)
        {
            tabControl訂單.SelectedIndex = tabControl訂單.TabPages.IndexOf(tabPage訂單資料修改);
        }

        void btn返回中心_Click(object sender, EventArgs e)
        {
            Close();
            formMainInstrance = new FormMain();
            formMainInstrance.Show();
        }

        void btn搜尋_Click(object sender, EventArgs e)
        {
            if (txt搜尋一.Text != "" || txt搜尋二.Text != "")
            {
                SearchOIDs.Clear();
                string str欄位名稱一 = cmb搜尋一.SelectedItem.ToString();
                string str欄位名稱二 = cmb搜尋二.SelectedItem.ToString();

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = $"select * from Orders where {str欄位名稱一} like @SearchKeyWordOne AND {str欄位名稱二} like @SearchKeyWordTwo;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchKeyWordOne", $"{txt搜尋一.Text}%");
                cmd.Parameters.AddWithValue("@SearchKeyWordTwo", $"{txt搜尋二.Text}%");
                SqlDataReader reader = cmd.ExecuteReader();

                // 檢查 DataTable 是否已經存在，若不存在則建立一個新的 DataTable
                DataTable dataTable = new DataTable();

                if (reader.HasRows)
                {
                    // 將資料加入 DataTable
                    dataTable.Load(reader);

                    // 將 DataTable 綁定到 DataGridView
                    dgv訂單資料修改列表.DataSource = dataTable;
                    dgv訂單資料修改列表.Rows[0].Selected = true;

                    // 將 reader 重置到第一條記錄
                    reader.Close();
                    reader = cmd.ExecuteReader();

                    // 讀取第一條資料
                    if (reader.Read() == true)
                    {
                        txtOID.Text = reader["OID"].ToString();
                        txt訂購人姓名.Text = reader["訂購人姓名"].ToString();
                        txt訂購人電話.Text = reader["訂購人電話"].ToString();
                        txt訂購人地址.Text = reader["訂購人地址"].ToString();
                        dtp下訂日.Value = Convert.ToDateTime(reader["下訂日"]);
                        txt訂單項目.Text = reader["訂單項目"].ToString();
                        txt訂單金額.Text = reader["訂單金額"].ToString();
                        dtp預計出貨日.Value = Convert.ToDateTime(reader["預計出貨日"]);
                        dtp預計到貨日.Value = Convert.ToDateTime(reader["預計到貨日"]);

                        foreach (DataRow row in dataTable.Rows)     // 將符合條件的 OID 加入 SearchOIDs
                        {
                            SearchOIDs.Add((int)row["OID"]);
                        }
                    }
                    else
                    {
                        MessageBox.Show("查無此訂單");
                        清空欄位();
                    }
                }
                else
                {
                    MessageBox.Show("查無此訂單");
                    清空欄位();
                }

                reader.Close();
                con.Close();
            }
            else
            {
                MessageBox.Show("請輸入查詢關鍵字");
            }
        }

        void 顯示資料筆數()
        {
            /* 使用 using 關鍵字可以確保在 using 塊結束時自動關閉連線 */
            using (SqlConnection con = new SqlConnection(strDBConnectionString))
            {
                con.Open();

                // 使用 COUNT 聚合函數來計算總筆數
                string strSQL = "SELECT COUNT(*) FROM Orders;";
                SqlCommand cmd = new SqlCommand(strSQL, con);

                int 總資料筆數 = (int)cmd.ExecuteScalar();
                int 目前第幾筆 = dgv訂單資料修改列表.CurrentRow?.Index + 1 ?? 0;     // k-p

                lbl筆數.Text = $"第{目前第幾筆}筆/共{總資料筆數}筆";
            } // 在這裡自動關閉連線
        }

        void btn重新整理_Click(object sender, EventArgs e)
        {
            ((DataTable)dgv訂單資料列表.DataSource)?.Clear();     // k-p
            ((DataTable)dgv訂單資料修改列表.DataSource)?.Clear();
            產生訂單資料列表DataGridView();
        }

        private void btn清空欄位_Click(object sender, EventArgs e)
        {
            清空欄位();
        }

        void btn新增訂單_Click(object sender, EventArgs e)
        {
            if ((txt訂購人姓名.Text != "") && (txt訂購人電話.Text != "") && (txt訂購人地址.Text != "") &&
                (dtp下訂日.Value.ToString() != "") && (txt訂單項目.Text != "") && (txt訂單金額.Text != "") &&
                (dtp預計出貨日.Value.ToString() != "") && (dtp預計到貨日.Value.ToString() != ""))
            {
                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();

                string strSQL = "insert into Orders (訂購人姓名, 訂購人電話, 訂購人地址, 下訂日, 訂單項目, 訂單金額, 預計出貨日, 預計到貨日)" +
                    "values(@NewName, @NewPhone, @NewAddress, @NewDate, @NewItem, @NewRevenue, @NewShippingDate, @NewDeliveryDate);";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@NewName", txt訂購人姓名.Text);
                cmd.Parameters.AddWithValue("@NewPhone", txt訂購人電話.Text);
                cmd.Parameters.AddWithValue("@NewAddress", txt訂購人地址.Text);
                cmd.Parameters.AddWithValue("@NewDate", dtp下訂日.Value);
                cmd.Parameters.AddWithValue("@NewItem", txt訂單項目.Text);
                cmd.Parameters.AddWithValue("@NewRevenue", txt訂單金額.Text);
                cmd.Parameters.AddWithValue("@NewShippingDate", dtp預計出貨日.Value);
                cmd.Parameters.AddWithValue("@NewDeliveryDate", dtp預計到貨日.Value);

                // k-p, 此段與法會顯示同 SQLServer 的 (1 個資料列受到影響) 顯示
                int row = cmd.ExecuteNonQuery();    // k-p, ExecuteNonQuery() 指只做執行不做查詢
                con.Close();

                MessageBox.Show($"({row}個資料列受到影響)");
            }
            else
            {
                MessageBox.Show("欄位資料不齊全");
            }
        }

        void btn儲存訂單_Click(object sender, EventArgs e)
        {
            int intOID;
            Int32.TryParse(txtOID.Text, out intOID);

            if ((txt訂購人姓名.Text != "") && (txt訂購人電話.Text != "") && (txt訂購人地址.Text != "") &&
                (txt訂單項目.Text != "") && (dtp下訂日.Value.ToString() != "") &&
                (dtp預計出貨日.Value.ToString() != "") && (dtp預計到貨日.Value.ToString() != ""))
            {
                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = "update Orders set 訂購人姓名=@NewName, 訂購人電話=@NewPhone, 訂購人地址=@NewAddress," +
                    "下訂日=@NewDate, 訂單項目=@NewItem, 訂單金額=@NewRevenue, 預計出貨日=@NewShippingDate, 預計到貨日=@NewDeliveryDate where OID=@SearchOID;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchOID", intOID);
                cmd.Parameters.AddWithValue("@NewName", txt訂購人姓名.Text);
                cmd.Parameters.AddWithValue("@NewPhone", txt訂購人電話.Text);
                cmd.Parameters.AddWithValue("@NewAddress", txt訂購人地址.Text);
                cmd.Parameters.AddWithValue("@NewDate", dtp下訂日.Value);
                cmd.Parameters.AddWithValue("@NewItem", txt訂單項目.Text);
                cmd.Parameters.AddWithValue("@NewRevenue", txt訂單金額.Text);
                cmd.Parameters.AddWithValue("@NewShippingDate", dtp預計出貨日.Value);
                cmd.Parameters.AddWithValue("@NewDeliveryDate", dtp預計到貨日.Value);

                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show($"({rows}個資料列受到影響)");
            }
            else
            {
                MessageBox.Show("欄位資料不齊全");
            }
        }

        void btn延遲出貨_Click(object sender, EventArgs e)
        {

        }

        void btn刪除訂單_Click(object sender, EventArgs e)
        {
            int intOID = 0;
            if (Int32.TryParse(txtOID.Text, out intOID))
            {
                DialogResult R = MessageBox.Show("您確認要刪除此筆資料?", "刪除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (R == DialogResult.Yes)
                {
                    if (intOID > 0)
                    {
                        SqlConnection con = new SqlConnection(strDBConnectionString);
                        con.Open();
                        string strSQL = "delete from Orders where OID = @DeleteOID;";
                        SqlCommand cmd = new SqlCommand(strSQL, con);
                        cmd.Parameters.AddWithValue("@DeleteOID", intOID);

                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        清空欄位();
                        MessageBox.Show($"資料已刪除, {rows}筆資料受影響");
                    }
                }
            }
            else
            {
                MessageBox.Show("請先選擇欲刪除資料");
            }
        }

        private void dgv訂單資料修改列表_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.RowIndex < DGV筆數))
            {
                string strSelectOID = dgv訂單資料修改列表.Rows[e.RowIndex].Cells[0].Value.ToString();
                int selectOID = 0;
                Int32.TryParse(strSelectOID, out selectOID);

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = "select * from Orders where OID = @SearchOID";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchOID", selectOID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    txtOID.Text = reader["OID"].ToString();
                    txt訂購人姓名.Text = reader["訂購人姓名"].ToString();
                    txt訂購人電話.Text = reader["訂購人電話"].ToString();
                    txt訂購人地址.Text = reader["訂購人地址"].ToString();
                    dtp下訂日.Value = Convert.ToDateTime(reader["下訂日"]);
                    txt訂單項目.Text = reader["訂單項目"].ToString();
                    txt訂單金額.Text = reader["訂單金額"].ToString();
                    dtp預計出貨日.Value = Convert.ToDateTime(reader["預計出貨日"]);
                    dtp預計到貨日.Value = Convert.ToDateTime(reader["預計到貨日"]);
                }
                else
                {
                    MessageBox.Show("查無此人");
                    清空欄位();
                }
                reader.Close();
                con.Close();
            }
        }

        private void tabPage數據分析_Click(object sender, EventArgs e)
        {

        }
    }
}
