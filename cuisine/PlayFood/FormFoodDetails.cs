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
    public partial class FormFoodDetails : Form
    {
        TextBox txt商品ID;
        TextBox txt商品名稱;
        TextBox txt商品價格;
        TextBox txt商品描述;
        PictureBox pictureBox商品圖檔;

        public int selectHID = 0;

        string image_修改後的檔名 = "";
        bool is修改圖檔 = false;

        public FormFoodDetails()
        {
            InitializeComponent();
            Size = new Size(800, 600);
        }

        private void FormFoodDetails_Load(object sender, EventArgs e)
        {
            if (GlobalVar.is管理者登入 == false)
            {
                MessageBox.Show("請先登入");
                Close();
            }

            /* 左邊 */
            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(310, 20);
            lbl名稱.Size = new Size(180, 40);
            lbl名稱.Text = "商品詳細資料";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            Label lblID = new Label();
            lblID.Location = new Point(50, 90);
            lblID.Size = new Size(90, 25);
            lblID.Text = "商品ID";
            lblID.Font = new Font("微軟正黑體", 14);
            Controls.Add(lblID);

            Label lbl商品名稱 = new Label();
            lbl商品名稱.Location = new Point(50, 125);
            lbl商品名稱.Size = new Size(90, 25);
            lbl商品名稱.Text = "商品名稱";
            lbl商品名稱.Font = new Font("微軟正黑體", 14);
            Controls.Add(lbl商品名稱);

            Label lbl商品價格 = new Label();
            lbl商品價格.Location = new Point(50, 160);
            lbl商品價格.Size = new Size(90, 25);
            lbl商品價格.Text = "商品價格";
            lbl商品價格.Font = new Font("微軟正黑體", 14);
            Controls.Add(lbl商品價格);

            Label lbl商品描述 = new Label();
            lbl商品描述.Location = new Point(50, 200);
            lbl商品描述.Size = new Size(250, 25);
            lbl商品描述.Text = "商品描述(限輸入100字)";
            lbl商品描述.Font = new Font("微軟正黑體", 14);
            Controls.Add(lbl商品描述);

            txt商品ID = new TextBox();
            txt商品ID.Location = new Point(150, 92);
            txt商品ID.Size = new Size(120, 30);
            txt商品ID.Text = selectHID.ToString();
            Controls.Add(txt商品ID);

            txt商品名稱 = new TextBox();
            txt商品名稱.Location = new Point(150, 128);
            txt商品名稱.Size = new Size(120, 30);
            Controls.Add(txt商品名稱);

            txt商品價格 = new TextBox();
            txt商品價格.Location = new Point(150, 162);
            txt商品價格.Size = new Size(120, 30);
            Controls.Add(txt商品價格);

            txt商品描述 = new TextBox();
            txt商品描述.Location = new Point(50, 230);
            txt商品描述.Size = new Size(220, 150);
            txt商品描述.Multiline = true;
            txt商品描述.BackColor = Color.LightGray;
            Controls.Add(txt商品描述);

            GroupBox groupBox商品修改 = new GroupBox();
            groupBox商品修改.Location = new Point(50, 410);
            groupBox商品修改.Size = new Size(300, 100);
            groupBox商品修改.Text = "商品修改";
            groupBox商品修改.Font = new Font("微軟正黑體", 14);
            Controls.Add(groupBox商品修改);

            Button btn選取商品照片 = new Button();
            btn選取商品照片.Location = new Point(30, 35);
            btn選取商品照片.Size = new Size(140, 40);
            btn選取商品照片.BackColor = Color.LightYellow;
            btn選取商品照片.Text = "選取商品照片";
            btn選取商品照片.Font = new Font("微軟正黑體", 14);
            btn選取商品照片.Click += new EventHandler(btn選取商品照片1_Click);
            groupBox商品修改.Controls.Add(btn選取商品照片);

            Button btn儲存修改 = new Button();
            btn儲存修改.Location = new Point(180, 35);
            btn儲存修改.Size = new Size(100, 40);
            btn儲存修改.BackColor = Color.LightYellow;
            btn儲存修改.Text = "儲存修改";
            btn儲存修改.Font = new Font("微軟正黑體", 14);
            btn儲存修改.Click += new EventHandler(btn儲存修改_Click);
            groupBox商品修改.Controls.Add(btn儲存修改);


            /* 右邊 */

            Label lbl商品圖檔 = new Label();
            lbl商品圖檔.Location = new Point(380, 90);
            lbl商品圖檔.Size = new Size(120, 30);
            lbl商品圖檔.Text = "'商品圖檔";
            lbl商品圖檔.Font = new Font("微軟正黑體", 14);
            Controls.Add(lbl商品圖檔);

            pictureBox商品圖檔 = new PictureBox();
            pictureBox商品圖檔.Location = new Point(380, 120);
            pictureBox商品圖檔.Size = new Size(220, 220);
            pictureBox商品圖檔.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox商品圖檔.BackColor = Color.LightSalmon;
            Controls.Add(pictureBox商品圖檔);
            顯示商品詳細資訊();

            GroupBox groupBox商品新增 = new GroupBox();
            groupBox商品新增.Location = new Point(380, 355);
            groupBox商品新增.Size = new Size(350, 155);
            groupBox商品新增.Text = "商品新增";
            groupBox商品新增.Font = new Font("微軟正黑體", 14);
            Controls.Add(groupBox商品新增);

            Button btn清空欄位 = new Button();
            btn清空欄位.Location = new Point(30, 40);
            btn清空欄位.Size = new Size(120, 40);
            btn清空欄位.BackColor = Color.LightYellow;
            btn清空欄位.Text = "清空欄位";
            btn清空欄位.Font = new Font("微軟正黑體", 14);
            btn清空欄位.Click += new EventHandler(btn清空欄位_Click);
            groupBox商品新增.Controls.Add(btn清空欄位);

            Button btn選取商品照片2 = new Button();
            btn選取商品照片2.Location = new Point(170, 40);
            btn選取商品照片2.Size = new Size(140, 40);
            btn選取商品照片2.BackColor = Color.LightYellow;
            btn選取商品照片2.Text = "選取商品照片";
            btn選取商品照片2.Font = new Font("微軟正黑體", 14);
            btn選取商品照片2.Click += new EventHandler(btn選取商品照片2_Click);
            groupBox商品新增.Controls.Add(btn選取商品照片2);

            Button btn新增商品 = new Button();
            btn新增商品.Location = new Point(30, 90);
            btn新增商品.Size = new Size(120, 40);
            btn新增商品.BackColor = Color.LightYellow;
            btn新增商品.Text = "新增商品";
            btn新增商品.Font = new Font("微軟正黑體", 14);
            btn新增商品.Click += new EventHandler(btn新增商品_Click);
            groupBox商品新增.Controls.Add(btn新增商品);

            Button btn返回商品管理 = new Button();
            btn返回商品管理.Location = new Point(170, 90);
            btn返回商品管理.Size = new Size(140, 40);
            btn返回商品管理.BackColor = Color.LightYellow;
            btn返回商品管理.Text = "返回商品管理";
            btn返回商品管理.Font = new Font("微軟正黑體", 14);
            btn返回商品管理.Click += new EventHandler(btn返回商品管理_Click);
            groupBox商品新增.Controls.Add(btn返回商品管理);

            /*
            if (selectHID == 0)
            { //新增商品
                groupBox商品新增.Visible = true;
                groupBox商品修改.Visible = false;
            }
            else
            { //修改商品
                groupBox商品新增.Visible = false;
                groupBox商品修改.Visible = true;
            }*/
        }

        void 顯示商品詳細資訊()
        {
            if (selectHID > 0)
            {
                SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
                con.Open();
                string strSQL = "select * from HotFood where HID = @SearcHID;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearcHID", selectHID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    txt商品ID.Text = reader["HID"].ToString();
                    txt商品名稱.Text = reader["Hname"].ToString();
                    txt商品價格.Text = reader["Hprice"].ToString();
                    txt商品描述.Text = reader["Hdesc"].ToString();
                    image_修改後的檔名 = reader["Himg"].ToString();
                    string 完整圖檔路徑 = GlobalVar.image_dir熱食 + @"\" + image_修改後的檔名;
                    pictureBox商品圖檔.Image = Image.FromFile(完整圖檔路徑);
                }
                reader.Close();
                con.Close();
            }
        }

        void 顯示資料筆數()
        {
            /* 使用 using 關鍵字可以確保在 using 塊結束時自動關閉連線 */
            /*
            using (SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString))
            {
                con.Open();

                // 使用 COUNT 聚合函數來計算總筆數
                string strSQL = "SELECT COUNT(*) FROM Persons;";
                SqlCommand cmd = new SqlCommand(strSQL, con);

                int 總資料筆數 = (int)cmd.ExecuteScalar();
                int 目前第幾筆 = dgv商品資料修改列表.CurrentRow?.Index + 1 ?? 0;

                lbl筆數.Text = $"第{目前第幾筆}筆/共{總資料筆數}筆";
            } // 在這裡自動關閉連線*/
        }

        void 選取商品照片()
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "檔案類型(*.jpg, *.jpeg, *.png)|*.jpeg;*.jpg;*.png";

            DialogResult R = f.ShowDialog();

            if (R == DialogResult.OK)
            {
                pictureBox商品圖檔.Image = Image.FromFile(f.FileName);//f.FileName完整檔案路徑
                string 圖片副檔名 = System.IO.Path.GetExtension(f.SafeFileName).ToLower();
                Random myRnd = new Random();
                image_修改後的檔名 = DateTime.Now.ToString("yyMMddHHmmss") + myRnd.Next(1000, 10000).ToString() + 圖片副檔名;
                is修改圖檔 = true;
            }
        }

        void btn選取商品照片1_Click(object sender, EventArgs e)
        {
            選取商品照片();
        }

        void btn儲存修改_Click(object sender, EventArgs e)
        {
            if ((txt商品名稱.Text != "") && (txt商品價格.Text != "") && (txt商品描述.Text != "") && (pictureBox商品圖檔.Image != null))
            {
                SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
                con.Open();
                string strSQL = "update HotFood set Hname=@NewName, Hprice=@NewPrice, Hdesc=@NewDesc, Himg=@NewImage" +
                    "where HID = @SearcHID;";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearcHID", selectHID);
                cmd.Parameters.AddWithValue("@NewName", txt商品名稱.Text);
                int intPrice = 0;
                Int32.TryParse(txt商品價格.Text, out intPrice);
                cmd.Parameters.AddWithValue("@NewPrice", intPrice);
                cmd.Parameters.AddWithValue("@NewDesc", txt商品描述.Text);
                cmd.Parameters.AddWithValue("@NewImage", image_修改後的檔名);

                int rows = cmd.ExecuteNonQuery();
                con.Close();

                if (is修改圖檔 == true)
                {
                    string 完整圖檔路徑 = GlobalVar.image_dir熱食 + @"\" + image_修改後的檔名;
                    pictureBox商品圖檔.Image.Save(完整圖檔路徑);
                    is修改圖檔 = false;
                }

                MessageBox.Show($"資料異動成功, 影響{rows}筆資料");
            }
            else
            {
                MessageBox.Show("所有欄位必填");
            }
        }

        void btn選取商品照片2_Click(object sender, EventArgs e)
        {
            選取商品照片();
        }

        private void btn清空欄位_Click(object sender, EventArgs e)
        {
            txt商品ID.Clear();
            txt商品名稱.Clear();
            txt商品價格.Clear();
            txt商品描述.Clear();
            pictureBox商品圖檔.Image = null;
        }

        private void btn新增商品_Click(object sender, EventArgs e)
        {
            if ((txt商品名稱.Text != "") && (txt商品價格.Text != "") && (txt商品描述.Text != "") && (pictureBox商品圖檔.Image != null))
            {
                SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
                con.Open();
                string strSQL = "insert into HotFood values(@NewName, @NewPrice, @NewDesc, @NewImage);";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@NewName", txt商品名稱.Text);
                int intPrice = 0;
                Int32.TryParse(txt商品價格.Text, out intPrice);
                cmd.Parameters.AddWithValue("@NewPrice", intPrice);
                cmd.Parameters.AddWithValue("@NewDesc", txt商品描述.Text);
                cmd.Parameters.AddWithValue("@NewImage", image_修改後的檔名);

                int rows = cmd.ExecuteNonQuery();
                con.Close();

                if (is修改圖檔 == true)
                {
                    string 完整圖檔路徑 = GlobalVar.image_dir熱食 + @"\" + image_修改後的檔名;
                    pictureBox商品圖檔.Image.Save(完整圖檔路徑);
                    is修改圖檔 = false;
                }

                MessageBox.Show($"資料異動成功, 影響{rows}筆資料");
            }
            else
            {
                MessageBox.Show("所有欄位必填");
            }
        }

        void btn返回商品管理_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
