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
    public partial class FormLogin : Form
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

        Label lbl回應訊息;
        TextBox txt管理者登入帳號;
        TextBox txt管理者登入密碼;

        public FormLogin()
        {
            InitializeComponent();
            Size = new Size(600, 400);
            //BackColor = Color.LightCyan;  // 淡藍色
            //BackColor = Color.LightCoral;   // 橘紅色
            //BackColor = Color.LightSalmon;  // 橘色
            BackColor = Color.LightGoldenrodYellow; // 金黃色
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            /*  */
            scsb.DataSource = @".";
            scsb.InitialCatalog = "playfood";
            scsb.IntegratedSecurity = true;
            GlobalVar.strDBConnectionString = scsb.ConnectionString;

            /* 動態生成 */
            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(222, 20);
            lbl名稱.Size = new Size(155, 40);
            lbl名稱.Text = "管理者登入";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            Label lbl管理者帳號 = new Label();
            lbl管理者帳號.Location = new Point(200, 90);
            lbl管理者帳號.Size = new Size(80, 30);
            lbl管理者帳號.Text = "帳號：";
            lbl管理者帳號.Font = new Font("微軟正黑體", 15);
            Controls.Add(lbl管理者帳號);

            Label lbl管理者密碼 = new Label();
            lbl管理者密碼.Location = new Point(200, 127);
            lbl管理者密碼.Size = new Size(80, 30);
            lbl管理者密碼.Text = "密碼：";
            lbl管理者密碼.Font = new Font("微軟正黑體", 15);
            Controls.Add(lbl管理者密碼);

            txt管理者登入帳號 = new TextBox();
            txt管理者登入帳號.Location = new Point(300, 95);
            txt管理者登入帳號.Size = new Size(100, 30);
            Controls.Add(txt管理者登入帳號);

            txt管理者登入密碼 = new TextBox();
            txt管理者登入密碼.Location = new Point(300, 130);
            txt管理者登入密碼.Size = new Size(100, 30);
            Controls.Add(txt管理者登入密碼);

            lbl回應訊息 = new Label();
            lbl回應訊息.Location = new Point(250, 180);
            lbl回應訊息.Size = new Size(200, 60);
            lbl回應訊息.Text = "尚未登入...";
            Controls.Add(lbl回應訊息);

            Button btn管理者登入 = new Button();
            btn管理者登入.Location = new Point(250, 270);
            btn管理者登入.Size = new Size(100, 50);
            btn管理者登入.BackColor = Color.LightBlue;
            btn管理者登入.Text = "登入";
            btn管理者登入.Font = new Font("微軟正黑體", 14);
            btn管理者登入.Click += new EventHandler(btn管理者登入_Click);
            Controls.Add(btn管理者登入);
        }

        void btn管理者登入_Click(object sender, EventArgs e)
        {
            int intID = 0;
            if ((txt管理者登入帳號.Text != "") && (txt管理者登入密碼.Text != "") && (Int32.TryParse(txt管理者登入帳號.Text, out intID)))
            {
                SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
                con.Open();
                string strSQL = "select * from Persons where 管理者登入帳號 = @SearchAccount and 管理者登入密碼 = @SearchPassword";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchAccount", txt管理者登入帳號.Text);
                cmd.Parameters.AddWithValue("@SearchPassword", txt管理者登入密碼.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    GlobalVar.is管理者登入 = true;
                    GlobalVar.管理者姓名 = reader["管理者姓名"].ToString();
                    GlobalVar.管理者id = (int)reader["PID"];
                    GlobalVar.管理者權限 = (int)reader["管理者權限"];

                    switch (GlobalVar.管理者權限)
                    {
                        case 10:
                            GlobalVar.管理者職稱 = "董事";
                            break;
                        case 20:
                            GlobalVar.管理者職稱 = "店長";
                            break;
                        case 30:
                            GlobalVar.管理者職稱 = "主管";
                            break;
                        case 40:
                            GlobalVar.管理者職稱 = "員工";
                            break;
                    }

                    GlobalVar.管理者資訊 = $"{GlobalVar.管理者職稱}：{GlobalVar.管理者姓名}";
                    lbl回應訊息.Text = $"登入成功!\n您好，尊敬的 {GlobalVar.管理者姓名} {GlobalVar.管理者職稱}\n歡迎您再次使用本系統～";
                    MessageBox.Show($"登入成功!\n您好，尊敬的 {GlobalVar.管理者姓名} {GlobalVar.管理者職稱}\n歡迎您再次使用本系統～");
                    //FormMain FormMainInstrance = new FormMain();
                    //FormMainInstrance.更新管理者標籤($"{GlobalVar.管理者姓名} {GlobalVar.管理者職稱}");

                    reader.Close();
                    con.Close();
                    Hide();
                }

                if (GlobalVar.is管理者登入 == false)
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
            /* 確保在用戶登入成功之前，不能簡單地通過關閉窗體而結束應用程式 */

            if (GlobalVar.is管理者登入 == true)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
