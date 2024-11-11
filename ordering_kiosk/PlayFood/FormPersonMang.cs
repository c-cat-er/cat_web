using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayFood
{
    public partial class FormPersonMang : Form
    {
        FormMain formMainInstrance;

        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        string strDBConnectionString = "";      // 資料庫連線字串

        bool is婚姻狀態 = false;
        List<int> SearchPIDs = new List<int>();  // 暫存會員ID
        int DGV筆數 = 0;      // DataGridView 的資料筆數

        ComboBox cmb搜尋一;
        TextBox txt搜尋一;
        ComboBox cmb搜尋二;
        TextBox txt搜尋二;
        DateTimePicker dtp開始日;
        DateTimePicker dtp結束日;

        Label lbl筆數;
        TextBox txtPID;
        TextBox txt姓名;
        TextBox txt電話;
        TextBox txt地址;
        TextBox txtEmail;
        DateTimePicker dtp生日;
        CheckBox chk婚姻狀態;
        TextBox txt權限;
        FlowLayoutPanel flowLayoutPanel管理者資料修改按鈕;

        public FormPersonMang()
        {
            InitializeComponent();
            Size = new Size(1033, 741);
            BackColor = Color.LightSteelBlue;
        }

        private void FormPersonMang_Load(object sender, EventArgs e)
        {
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

            if (GlobalVar.管理者權限 >= 20)
            {
                MessageBox.Show("權限不足");
                Close();
            }

            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(426, 20);
            lbl名稱.Size = new Size(180, 40);
            lbl名稱.Text = "人員管理中心";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Location = new Point(50, 90);
            flowLayoutPanel.Size = new Size(140, 300);
            Controls.Add(flowLayoutPanel);

            List<string> listBtnStr會員管理系統 = new List<string>()
            { "所有會員", "會員資料修改", "返回中心" };

            List<EventHandler> listEventHandler會員管理系統 = new List<EventHandler>()
            {
                new EventHandler(btn所有會員_Click), new EventHandler(btn會員資料修改_Click),
                new EventHandler(btn返回中心_Click)
            };

            int i = 0;
            foreach (string str in listBtnStr會員管理系統)
            {
                Button btn = new Button();
                btn.Size = new Size(130, 50);
                btn.BackColor = Color.LightYellow;
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 13);
                btn.Click += listEventHandler會員管理系統[i];
                flowLayoutPanel.Controls.Add(btn);
                i++;
            }


            /* 右邊 */
            產生會員資料列表DataGridView();

            /* 上面 */
            GroupBox groupBox = new GroupBox();
            groupBox.Location = new Point(35, 10);
            groupBox.Size = new Size(680, 100);
            groupBox.Text = "快速搜尋";
            groupBox.Font = new Font("微軟正黑體", 15);
            tabPage管理者資料修改.Controls.Add(groupBox);

            cmb搜尋一 = new ComboBox();
            cmb搜尋一.Location = new Point(20, 35);
            cmb搜尋一.Size = new Size(120, 20);
            cmb搜尋一.Font = new Font("微軟正黑體", 11);
            cmb搜尋一.DropDownStyle = ComboBoxStyle.DropDownList;
            groupBox.Controls.Add(cmb搜尋一);

            cmb搜尋一.Items.Add("管理者姓名");
            cmb搜尋一.Items.Add("管理者電話");
            cmb搜尋一.Items.Add("管理者地址");
            cmb搜尋一.Items.Add("管理者Email");
            cmb搜尋一.Items.Add("管理者生日");
            cmb搜尋一.Items.Add("管理者權限");
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

            cmb搜尋二.Items.Add("管理者姓名");
            cmb搜尋二.Items.Add("管理者電話");
            cmb搜尋二.Items.Add("管理者地址");
            cmb搜尋二.Items.Add("管理者Email");
            cmb搜尋二.Items.Add("管理者生日");
            cmb搜尋二.Items.Add("管理者權限");
            cmb搜尋二.SelectedIndex = 1;

            txt搜尋二 = new TextBox();
            txt搜尋二.Location = new Point(430, 35);
            txt搜尋二.Multiline = true;
            txt搜尋二.Size = new Size(120, 23);
            txt搜尋二.Font = new Font("微軟正黑體", 11);
            groupBox.Controls.Add(txt搜尋二);

            Label lbl開始日 = new Label();
            lbl開始日.Location = new Point(80, 65);
            lbl開始日.Size = new Size(30, 25);
            lbl開始日.Text = "從";
            lbl開始日.Font = new Font("微軟正黑體", 11);
            groupBox.Controls.Add(lbl開始日);

            Label lbl結束日 = new Label();
            lbl結束日.Location = new Point(360, 65);
            lbl結束日.Size = new Size(30, 25);
            lbl結束日.Text = "到";
            lbl結束日.Font = new Font("微軟正黑體", 11);
            groupBox.Controls.Add(lbl結束日);

            dtp開始日 = new DateTimePicker();
            dtp開始日.Location = new Point(140, 65);
            dtp開始日.Size = new Size(140, 25);
            dtp開始日.Value = new DateTime(1900, 1, 1); // 2023年12月31日下午3:30:00
            dtp開始日.Font = new Font("微軟正黑體", 10);
            groupBox.Controls.Add(dtp開始日);

            dtp結束日 = new DateTimePicker();
            dtp結束日.Location = new Point(420, 65);
            dtp結束日.Size = new Size(140, 25);
            dtp結束日.Value = DateTime.Today;  // k-p, 今日
            dtp結束日.Font = new Font("微軟正黑體", 10);
            groupBox.Controls.Add(dtp結束日);

            Button btn清空欄位 = new Button();
            btn清空欄位.Location = new Point(580, 30);
            btn清空欄位.Size = new Size(70, 30);
            btn清空欄位.Text = "清空欄位";
            btn清空欄位.Font = new Font("微軟正黑體", 12);
            btn清空欄位.Click += new EventHandler(btn清空搜尋欄位_Click);
            groupBox.Controls.Add(btn清空欄位);

            Button btn搜尋 = new Button();
            btn搜尋.Location = new Point(580, 70);
            btn搜尋.Size = new Size(70, 30);
            btn搜尋.Text = "搜尋";
            btn搜尋.Font = new Font("微軟正黑體", 12);
            btn搜尋.Click += new EventHandler(btn搜尋_Click);
            groupBox.Controls.Add(btn搜尋);


            /* 中間 */
            dgv管理者資料修改列表.SelectionChanged += dgv管理者資料修改列表_SelectionChanged;

            lbl筆數 = new Label();
            lbl筆數.Location = new Point(200, 450);
            lbl筆數.Size = new Size(120, 25);
            lbl筆數.Text = $"共 ? 筆 / 第 1 筆";
            lbl筆數.Font = new Font("微軟正黑體", 12);
            tabPage管理者資料修改.Controls.Add(lbl筆數);
            顯示資料筆數();

            /* 中右 */
            FlowLayoutPanel flowLayoutPanel標籤 = new FlowLayoutPanel();
            flowLayoutPanel標籤.Location = new Point(325, 143);
            flowLayoutPanel標籤.Size = new Size(58, 169);
            tabPage管理者資料修改.Controls.Add(flowLayoutPanel標籤);

            List<string> listLblStr = new List<string>()
                        {
                            "ID", "姓名", "電話", "地址", "Email", "生日"
                        };

            int j = 0;
            foreach (string str in listLblStr)
            {
                Label lbl = new Label();
                lbl.Size = new Size(60, 29);
                lbl.Text = str;
                lbl.Font = new Font("微軟正黑體", 12);
                flowLayoutPanel標籤.Controls.Add(lbl);
                j++;
            }

            FlowLayoutPanel flowLayoutPanel輸入框 = new FlowLayoutPanel();
            flowLayoutPanel輸入框.Location = new Point(388, 137);
            flowLayoutPanel輸入框.Size = new Size(271, 172);
            tabPage管理者資料修改.Controls.Add(flowLayoutPanel輸入框);

            txtPID = new TextBox();
            txtPID.Size = new Size(170, 22);
            txtPID.ReadOnly = true;  // k-p, 只讀
            txtPID.Text = "(只讀)";
            flowLayoutPanel輸入框.Controls.Add(txtPID);

            txt姓名 = new TextBox();
            txt姓名.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txt姓名);

            txt電話 = new TextBox();
            txt電話.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txt電話);

            txt地址 = new TextBox();
            txt地址.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txt地址);

            txtEmail = new TextBox();
            txtEmail.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(txtEmail);

            dtp生日 = new DateTimePicker();
            dtp生日.Size = new Size(170, 22);
            flowLayoutPanel輸入框.Controls.Add(dtp生日);

            Label lbl婚姻狀態 = new Label();
            lbl婚姻狀態.Location = new Point(327, 317);
            lbl婚姻狀態.Size = new Size(60, 25);
            lbl婚姻狀態.Text = "婚姻狀態";
            tabPage管理者資料修改.Controls.Add(lbl婚姻狀態);

            chk婚姻狀態 = new CheckBox();
            chk婚姻狀態.Location = new Point(391, 310);
            chk婚姻狀態.Size = new Size(120, 30);
            chk婚姻狀態.Text = "已婚";
            tabPage管理者資料修改.Controls.Add(chk婚姻狀態);

            Label lbl權限 = new Label();
            lbl權限.Location = new Point(327, 375);
            lbl權限.Size = new Size(60, 25);
            lbl權限.Text = "權限";
            tabPage管理者資料修改.Controls.Add(lbl權限);

            txt權限 = new TextBox();
            txt權限.Location = new Point(391, 366);
            txt權限.Size = new Size(170, 22);
            tabPage管理者資料修改.Controls.Add(txt權限);

            /*
            Label lbl性別 = new Label();
            lbl性別.Location = new Point(327, 375);
            lbl權限.Size = new Size(60, 25);
            lbl權限.Text = "權限";
            tabPage管理者資料修改.Controls.Add(lbl權限);

            chk性別 = new CheckBox();
            chk婚姻狀態.Location = new Point(391, 310);
            chk婚姻狀態.Size = new Size(120, 30);
            chk婚姻狀態.Text = "已婚";
            tabPage管理者資料修改.Controls.Add(chk婚姻狀態);*/


            /* 下面 */
            List<string> listBtnStr會員資料修改 = new List<string>()
                { "重新整理", "停權", "清空欄位", "新增資料", "儲存資料", "刪除資料" };

            List<EventHandler> listEventHandler會員資料修改 = new List<EventHandler>()
                {
                new EventHandler(btn重新整理_Click),
                new EventHandler(btn停權_Click), new EventHandler(btn清空欄位_Click),
                new EventHandler(btn新增資料_Click),
                new EventHandler(btn儲存資料_Click), new EventHandler(btn刪除資料_Click)
                };

            flowLayoutPanel管理者資料修改按鈕 = new FlowLayoutPanel();
            flowLayoutPanel管理者資料修改按鈕.Location = new Point(35, 480);
            flowLayoutPanel管理者資料修改按鈕.Size = new Size(750, 45);
            tabPage管理者資料修改.Controls.Add(flowLayoutPanel管理者資料修改按鈕);

            int l = 0;
            foreach (string str in listBtnStr會員資料修改)
            {
                Button btn = new Button();
                btn.Size = new Size(100, 40);
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 14);
                btn.Click += listEventHandler會員資料修改[l];
                flowLayoutPanel管理者資料修改按鈕.Controls.Add(btn);
                l++;
            }
        }

        void btn所有會員_Click(object sender, EventArgs e)
        {
            tabControl會員.SelectedIndex = tabControl會員.TabPages.IndexOf(tabPage所有管理者);
        }

        void 產生會員資料列表DataGridView()
        {
            SqlConnection con = new SqlConnection(strDBConnectionString);
            con.Open();
            string strSQL = "select * from Persons";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows == true)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                Console.WriteLine($"DGV筆數:{dt.Rows.Count}");
                DGV筆數 = dt.Rows.Count;
                dgv管理者資料列表.DataSource = dt;
                dgv管理者資料修改列表.DataSource = dt;
            }
            else
            {
                MessageBox.Show("找不到資料");
            }
            reader.Close();
            con.Close();
        }

        void 清空欄位()
        {
            txtPID.Clear();
            txt姓名.Clear();
            txt電話.Clear();
            txt地址.Clear();
            txtEmail.Clear();
            dtp生日.Value = DateTime.Now;
            chk婚姻狀態.Checked = false;
            txt權限.Clear();
        }

        void btn會員資料修改_Click(object sender, EventArgs e)
        {
            tabControl會員.SelectedIndex = tabControl會員.TabPages.IndexOf(tabPage管理者資料修改);
        }

        void btn返回中心_Click(object sender, EventArgs e)
        {
            Close();
            formMainInstrance = new FormMain();
            formMainInstrance.Show();
        }

        void btn清空搜尋欄位_Click(object sender, EventArgs e)
        {
            txt搜尋一.Clear();
            txt搜尋二.Clear();
            dtp開始日.Value = new DateTime(1900, 1, 1);
            dtp結束日.Value = DateTime.Now;
        }

        void btn搜尋_Click(object sender, EventArgs e)
        {/*
            if (txt搜尋一.Text != "" || txt搜尋二.Text != "")
            {
                DataTable dataTable = new DataTable(); // 新增 DataTable 用於存放資料
                SearchPIDs.Clear();

                string str欄位名稱一 = cmb搜尋一.SelectedItem.ToString();
                string str欄位名稱二 = cmb搜尋二.SelectedItem.ToString();

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();

                string strSQL = $"select * from Persons where {str欄位名稱一} like @SearchKeyWordOne AND {str欄位名稱二} like @SearchKeyWordTwo;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchKeyWordOne", $"{txt搜尋一.Text}%");
                cmd.Parameters.AddWithValue("@SearchKeyWordTwo", $"{txt搜尋二.Text}%");
                SqlDataReader reader = cmd.ExecuteReader();

                // 將資料加入 DataTable
                dataTable.Load(reader);

                // 結束資料庫連線
                reader.Close();
                con.Close();

                // 將 DataTable 綁定到 DataGridView
                dgv管理者資料修改列表.DataSource = dataTable;

                if (dgv管理者資料修改列表.Rows.Count > 0)
                {
                    dgv管理者資料修改列表.Rows[0].Selected = true;

                    // 如果需要，根據所選行來填充其他文本框
                    txtPID.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["PID"].Value.ToString();
                    txt姓名.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者姓名"].Value.ToString();
                    txt電話.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者電話"].Value.ToString();
                    txt地址.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者地址"].Value.ToString();
                    txtEmail.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者Email"].Value.ToString();
                    dtp生日.Value = Convert.ToDateTime(dgv管理者資料修改列表.SelectedRows[0].Cells["管理者生日"].Value);
                    chk婚姻狀態.Checked = (bool)dgv管理者資料修改列表.SelectedRows[0].Cells["管理者婚姻狀態"].Value;
                    txt權限.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者權限"].Value.ToString();
                    txt點數.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者點數"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("查無此人");
                    清空欄位();
                }
            }
            else
            {
                MessageBox.Show("請輸入查詢關鍵字");
            }
            */
            if (txt搜尋一.Text != "" || txt搜尋二.Text != "" || dtp開始日.Value.ToString() != "" || dtp結束日.Value.ToString() != "")
            {
                SearchPIDs.Clear();
                string str欄位名稱一 = cmb搜尋一.SelectedItem.ToString();
                string str欄位名稱二 = cmb搜尋二.SelectedItem.ToString();
                //string sql婚姻狀態語法 = is婚姻狀態 ? "and 婚姻狀態 = 1" : "and 婚姻狀態 = 0";

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = $"select * from Persons where {str欄位名稱一} like @SearchKeyWordOne AND {str欄位名稱二} like @SearchKeyWordTwo" +
                    $" AND (管理者生日 >= @StartBirth and 管理者生日 <= @EndBirth) ;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchKeyWordOne", $"{txt搜尋一.Text}%");
                cmd.Parameters.AddWithValue("@SearchKeyWordTwo", $"{txt搜尋二.Text}%");
                cmd.Parameters.AddWithValue("@StartBirth", dtp開始日.Value);
                cmd.Parameters.AddWithValue("@EndBirth", dtp結束日.Value);
                SqlDataReader reader = cmd.ExecuteReader();

                // 檢查 DataTable 是否已經存在，若不存在則建立一個新的 DataTable
                DataTable dataTable = new DataTable();

                if (reader.HasRows)
                {
                    // 將資料加入 DataTable
                    dataTable.Load(reader);

                    // 將 DataTable 綁定到 DataGridView
                    dgv管理者資料修改列表.DataSource = dataTable;
                    dgv管理者資料修改列表.Rows[0].Selected = true;

                    // 如果需要，根據所選行來填充其他文本框
                    txtPID.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["PID"].Value.ToString();
                    txt姓名.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者姓名"].Value.ToString();
                    txt電話.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者電話"].Value.ToString();
                    txt地址.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者地址"].Value.ToString();
                    txtEmail.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者Email"].Value.ToString();
                    dtp生日.Value = Convert.ToDateTime(dgv管理者資料修改列表.SelectedRows[0].Cells["管理者生日"].Value);
                    chk婚姻狀態.Checked = (bool)dgv管理者資料修改列表.SelectedRows[0].Cells["管理者婚姻狀態"].Value;
                    txt權限.Text = dgv管理者資料修改列表.SelectedRows[0].Cells["管理者權限"].Value.ToString();

                    foreach (DataRow row in dataTable.Rows)     // 將符合條件的 PID 加入 SearchPIDs
                    {
                        SearchPIDs.Add((int)row["PID"]);
                    }
                }
                else
                {
                    MessageBox.Show("查無此人");
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
                string strSQL = "SELECT COUNT(*) FROM Persons;";
                SqlCommand cmd = new SqlCommand(strSQL, con);

                int 總資料筆數 = (int)cmd.ExecuteScalar();
                int 目前第幾筆 = dgv管理者資料修改列表.CurrentRow?.Index + 1 ?? 0;

                lbl筆數.Text = $"第{目前第幾筆}筆/共{總資料筆數}筆";
            } // 在這裡自動關閉連線
        }

        private void dgv管理者資料修改列表_SelectionChanged(object sender, EventArgs e)
        {
            顯示資料筆數();
        }

        void btn升權_Click(object sender, EventArgs e)
        {
            int intID = 0;
            if (Int32.TryParse(txtPID.Text, out intID))
            {
                int 權限 = Int32.Parse(txt權限.Text);
                if (權限 != 10)
                {
                    權限 += 10;
                }
                using (SqlConnection con = new SqlConnection(strDBConnectionString))
                {
                    con.Open();
                    string sql = "UPDATE Persons SET 管理者權限 = @NewAuthority WHERE PID = @PersonPID";     // 更新資料庫中的 "權限" 欄位值
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NewAuthority", 權限);
                        cmd.Parameters.AddWithValue("@PersonPID", intID);

                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show($"該筆資料已升權, {rows}筆資料受影響");
                    }
                }
            }
        }

        void btn降權_Click(object sender, EventArgs e)
        {
            int intID = 0;
            if (Int32.TryParse(txtPID.Text, out intID))
            {
                int 權限 = Int32.Parse(txt權限.Text) + 10;

                using (SqlConnection con = new SqlConnection(strDBConnectionString))
                {
                    con.Open();

                    string sql = "UPDATE Persons SET 管理者權限 = @NewAuthority WHERE PID = @PersonPID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NewAuthority", 權限);
                        cmd.Parameters.AddWithValue("@PersonPID", intID);

                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        //清空欄位();
                        MessageBox.Show($"該筆資料已降權, {rows}筆資料受影響");
                    }
                }
            }
        }

        void btn重新整理_Click(object sender, EventArgs e)
        {
            ((DataTable)dgv管理者資料列表.DataSource)?.Clear();
            ((DataTable)dgv管理者資料修改列表.DataSource)?.Clear();
            產生會員資料列表DataGridView();
        }

        void btn停權_Click(object sender, EventArgs e)
        {
            int intID = 0;
            if (Int32.TryParse(txtPID.Text, out intID))
            {
                // 在這裡進行停權的邏輯，例如將權限值設為 0
                int newAuthority = 0;

                // 更新資料庫中的 "權限" 欄位值
                using (SqlConnection con = new SqlConnection(strDBConnectionString))
                {
                    con.Open();

                    string sql = "UPDATE Persons SET 管理者權限 = @NewAuthority WHERE PID = @PersonPID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NewAuthority", newAuthority);
                        cmd.Parameters.AddWithValue("@PersonPID", intID);

                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        //清空欄位();
                        MessageBox.Show($"資料已刪除, {rows}筆資料受影響");
                    }
                }
            }
        }

        private void btn清空欄位_Click(object sender, EventArgs e)
        {
            清空欄位();
        }

        void btn新增資料_Click(object sender, EventArgs e)
        {
            if ((txt姓名.Text != "") && (txt電話.Text != "") && (txt地址.Text != "") && (txtEmail.Text.Contains("@")) &&
                (dtp生日.Value.ToString() != "") && (int.TryParse(txt權限.Text, out _)))
            {
                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();

                string strSQL = "insert into Persons (管理者姓名, 管理者電話, 管理者地址, 管理者Email, 管理者生日, 管理者婚姻狀態, 管理者權限)" +
                    "values(@NewName, @NewPhone, @NewAddress, @NewEmail, @NewBirth, @NewMarriage, @NewAuthority);";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@NewName", txt姓名.Text);
                cmd.Parameters.AddWithValue("@NewPhone", txt電話.Text);
                cmd.Parameters.AddWithValue("@NewAddress", txt地址.Text);
                cmd.Parameters.AddWithValue("@NewEmail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@NewBirth", dtp生日.Value);
                cmd.Parameters.AddWithValue("@NewMarriage", chk婚姻狀態.Checked);
                int intAuthority = 0;
                Int32.TryParse(txt權限.Text, out intAuthority);
                cmd.Parameters.AddWithValue("@NewAuthority", intAuthority);

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

        void btn儲存資料_Click(object sender, EventArgs e)
        {
            int intID;
            Int32.TryParse(txtPID.Text, out intID);

            if ((txt姓名.Text != "") && (txt電話.Text != "") && (txt地址.Text != "") && (txtEmail.Text.Contains("@")) &&
                (dtp生日.Value.ToString() != "") && (int.TryParse(txt權限.Text, out _)))
            {
                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = "update Persons set 管理者姓名=@NewName, 管理者電話=@NewPhone, 管理者地址=@NewAddress, 管理者Email=@NewEmail," +
                    "管理者生日=@NewBirth, 管理者婚姻狀態=@NewMarriage, 管理者權限 = @NewAuthority where PID=@SearchPID;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchPID", intID);
                cmd.Parameters.AddWithValue("@NewName", txt姓名.Text);
                cmd.Parameters.AddWithValue("@NewPhone", txt電話.Text);
                cmd.Parameters.AddWithValue("@NewAddress", txt地址.Text);
                cmd.Parameters.AddWithValue("@NewEmail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@NewBirth", dtp生日.Value);
                cmd.Parameters.AddWithValue("@NewMarriage", chk婚姻狀態.Checked);
                int intAuthority = 0;
                Int32.TryParse(txt權限.Text, out intAuthority);
                cmd.Parameters.AddWithValue("@NewAuthority", intAuthority);

                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show($"({rows}個資料列受到影響)");
            }
            else
            {
                MessageBox.Show("欄位資料不齊全");
            }
        }

        void btn刪除資料_Click(object sender, EventArgs e)
        {
            int intID = 0;
            if (Int32.TryParse(txtPID.Text, out intID))
            {
                DialogResult R = MessageBox.Show("您確認要刪除此筆資料?", "刪除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (R == DialogResult.Yes)
                {
                    if (intID > 0)
                    {
                        SqlConnection con = new SqlConnection(strDBConnectionString);
                        con.Open();
                        string strSQL = "delete from Persons where PID = @DeletePID;";
                        SqlCommand cmd = new SqlCommand(strSQL, con);
                        cmd.Parameters.AddWithValue("@DeletePID", intID);

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


        private void dgv管理者資料列表_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.RowIndex < DGV筆數))
            {
                string strSelectID = dgv管理者資料列表.Rows[e.RowIndex].Cells[0].Value.ToString();
                int selectPID = 0;
                Int32.TryParse(strSelectID, out selectPID);

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = "select * from Persons where PID = @SearchPID";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchPID", selectPID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    txtPID.Text = reader["PID"].ToString();
                    txt姓名.Text = reader["管理者姓名"].ToString();
                    txt電話.Text = reader["管理者電話"].ToString();
                    txt地址.Text = reader["管理者地址"].ToString();
                    txtEmail.Text = reader["管理者Email"].ToString();
                    dtp生日.Value = Convert.ToDateTime(reader["管理者生日"]);
                    chk婚姻狀態.Checked = (bool)reader["管理者婚姻狀態"];
                    txt權限.Text = reader["管理者權限"].ToString();
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

        private void dgv管理者資料修改列表_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.RowIndex < DGV筆數))
            {
                string strSelectID = dgv管理者資料修改列表.Rows[e.RowIndex].Cells[0].Value.ToString();
                int selectPID = 0;
                Int32.TryParse(strSelectID, out selectPID);

                SqlConnection con = new SqlConnection(strDBConnectionString);
                con.Open();
                string strSQL = "select * from Persons where PID = @SearchPID";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchPID", selectPID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    txtPID.Text = reader["PID"].ToString();
                    txt姓名.Text = reader["管理者姓名"].ToString();
                    txt電話.Text = reader["管理者電話"].ToString();
                    txt地址.Text = reader["管理者地址"].ToString();
                    txtEmail.Text = reader["管理者Email"].ToString();
                    dtp生日.Value = Convert.ToDateTime(reader["管理者生日"]);
                    chk婚姻狀態.Checked = (bool)reader["管理者婚姻狀態"];
                    txt權限.Text = reader["管理者權限"].ToString();
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
    }
}
