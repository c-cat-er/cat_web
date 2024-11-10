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
    public partial class FormLoginMember : Form
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

        Label lbl回應訊息;
        TextBox txt會員登入帳號;
        TextBox txt會員登入密碼;

        public FormLoginMember()
        {
            InitializeComponent();
            Size = new Size(600, 400);
            BackColor = Color.LightSkyBlue;
        }

        private void FormLoginMember_Load(object sender, EventArgs e)
        {
            /*  */
            scsb.DataSource = @".";
            scsb.InitialCatalog = "playfood";
            scsb.IntegratedSecurity = true;
            GlobalVar.strDBConnectionString = scsb.ConnectionString;

            /* 動態生成 */
            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(235, 20);
            lbl名稱.Size = new Size(130, 40);
            lbl名稱.Text = "會員登入";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            Label lbl會員帳號 = new Label();
            lbl會員帳號.Location = new Point(200, 90);
            lbl會員帳號.Size = new Size(80, 30);
            lbl會員帳號.Text = "帳號：";
            lbl會員帳號.Font = new Font("微軟正黑體", 15);
            Controls.Add(lbl會員帳號);

            Label lbl會員密碼 = new Label();
            lbl會員密碼.Location = new Point(200, 127);
            lbl會員密碼.Size = new Size(80, 30);
            lbl會員密碼.Text = "密碼：";
            lbl會員密碼.Font = new Font("微軟正黑體", 15);
            Controls.Add(lbl會員密碼);

            txt會員登入帳號 = new TextBox();
            txt會員登入帳號.Location = new Point(300, 95);
            txt會員登入帳號.Size = new Size(100, 30);
            Controls.Add(txt會員登入帳號);

            txt會員登入密碼 = new TextBox();
            txt會員登入密碼.Location = new Point(300, 130);
            txt會員登入密碼.Size = new Size(100, 30);
            Controls.Add(txt會員登入密碼);

            lbl回應訊息 = new Label();
            lbl回應訊息.Location = new Point(250, 180);
            lbl回應訊息.Size = new Size(200, 60);
            lbl回應訊息.Text = "尚未登入...";
            Controls.Add(lbl回應訊息);

            Button btn會員註冊 = new Button();
            btn會員註冊.Location = new Point(120, 270);
            btn會員註冊.Size = new Size(100, 50);
            btn會員註冊.BackColor = Color.LightBlue;
            btn會員註冊.Text = "會員註冊";
            btn會員註冊.Font = new Font("微軟正黑體", 14);
            btn會員註冊.Click += new EventHandler(btn會員登入_Click);
            Controls.Add(btn會員註冊);

            Button btn會員登入 = new Button();
            btn會員登入.Location = new Point(250, 270);
            btn會員登入.Size = new Size(100, 50);
            btn會員登入.BackColor = Color.LightCyan;
            btn會員登入.Text = "會員登入";
            btn會員登入.Font = new Font("微軟正黑體", 14);
            btn會員登入.Click += new EventHandler(btn會員登入_Click);
            Controls.Add(btn會員登入);

            Button btn會員登出 = new Button();
            btn會員登出.Location = new Point(380, 270);
            btn會員登出.Size = new Size(100, 50);
            btn會員登出.BackColor = Color.LightBlue;
            btn會員登出.Text = "會員登出";
            btn會員登出.Font = new Font("微軟正黑體", 14);
            btn會員登出.Click += new EventHandler(btn會員登入_Click);
            Controls.Add(btn會員登出);

            if (GlobalVar.is會員登入 == false)
            {
                btn會員登出.Visible = false;
            }
        }

        void btn會員登入_Click(object sender, EventArgs e)
        {
            int intID = 0;
            if ((txt會員登入帳號.Text != "") && (txt會員登入密碼.Text != "") && (Int32.TryParse(txt會員登入帳號.Text, out intID)))
            {
                SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
                con.Open();
                string strSQL = "select * from Members where 會員登入帳號 = @SearchAccount and 會員登入密碼 = @SearchPassword";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchAccount", txt會員登入帳號.Text);
                cmd.Parameters.AddWithValue("@SearchPassword", txt會員登入密碼.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    GlobalVar.is會員登入 = true;
                    GlobalVar.會員id = (int)reader["MID"];
                    GlobalVar.會員姓名 = reader["會員姓名"].ToString();
                    GlobalVar.會員電話 = reader["會員電話"].ToString();
                    GlobalVar.會員地址 = reader["會員地址"].ToString();
                    GlobalVar.會員Email = reader["會員Email"].ToString();
                    GlobalVar.會員生日 = reader["會員生日"].ToString();
                    GlobalVar.會員等級 = (int)reader["會員等級"];

                    switch (GlobalVar.會員等級)
                    {
                        case 100:
                            GlobalVar.會員職稱 = "輝耀級";
                            break;
                        case 200:
                            GlobalVar.會員職稱 = "鑽石級";
                            break;
                        case 300:
                            GlobalVar.會員職稱 = "白金級";
                            break;
                        case 400:
                            GlobalVar.會員職稱 = "青銅級";
                            break;
                    }

                    GlobalVar.會員點數 = (int)reader["會員點數"];
                    GlobalVar.會員頭像 = reader["會員頭像"].ToString();

                    GlobalVar.會員資訊 = $"{GlobalVar.會員職稱} 會員：{GlobalVar.會員姓名}, 當前共有 {GlobalVar.會員點數} 點";
                    lbl回應訊息.Text = $"登入成功!\n您好，尊敬的 {GlobalVar.會員職稱} 會員：{GlobalVar.會員姓名}\n歡迎您再次使用本系統～";
                    MessageBox.Show($"登入成功!\n您好，尊敬的 {GlobalVar.會員職稱} 會員：{GlobalVar.會員姓名}\n歡迎您再次使用本系統～");

                    reader.Close();
                    con.Close();
                    Hide();
                }

                if (GlobalVar.is會員登入 == false)
                {
                    lbl回應訊息.Text = "登入失敗, 登入資料錯誤";
                }
                reader.Close();
                con.Close();
            }
            else
            {
                lbl回應訊息.Text = "輸入登入資訊錯誤";
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
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
