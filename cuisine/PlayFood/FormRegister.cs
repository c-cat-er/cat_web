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
    public partial class FormRegister : Form
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

        Label lbl回應訊息;
        TextBox txt會員登入帳號;
        TextBox txt會員登入密碼;

        public FormRegister()
        {
            InitializeComponent();
            Size = new Size(600, 400);
        }

        private void FormRegister_Load(object sender, EventArgs e)
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
            lbl名稱.Text = "會員註冊";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            Label lbl會員姓名 = new Label();
            lbl會員姓名.Location = new Point(200, 90);
            lbl會員姓名.Size = new Size(80, 30);
            lbl會員姓名.Text = "姓名：";
            lbl會員姓名.Font = new Font("微軟正黑體", 15);
            Controls.Add(lbl會員姓名);

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
            Controls.Add(lbl回應訊息);

            Button btn會員註冊 = new Button();
            btn會員註冊.Location = new Point(250, 270);
            btn會員註冊.Size = new Size(100, 50);
            btn會員註冊.BackColor = Color.LightBlue;
            btn會員註冊.Text = "會員註冊";
            btn會員註冊.Font = new Font("微軟正黑體", 14);
            btn會員註冊.Click += new EventHandler(btn會員註冊_Click);
            Controls.Add(btn會員註冊);

            Button btn會員登入 = new Button();
            btn會員登入.Location = new Point(380, 270);
            btn會員登入.Size = new Size(100, 50);
            btn會員登入.BackColor = Color.LightSalmon;
            btn會員登入.Text = "我要登入";
            btn會員登入.Font = new Font("微軟正黑體", 14);
            btn會員登入.Click += new EventHandler(btn會員登入_Click);
            Controls.Add(btn會員登入);
        }

        void btn會員註冊_Click(object sender, EventArgs e)
        {/*
            int intID = 0;
            if ((txt會員登入帳號.Text != "") && (txt會員登入密碼.Text != "") && (Int32.TryParse(txt會員登入帳號.Text, out intID)))
            {
                SqlConnection con = new SqlConnection(GlobalVar.strDBConnectionString);
                con.Open();

                string strSQL = "insert into Persons (管理者姓名, 管理者電話, 管理者地址, 管理者Email, 管理者生日, 管理者婚姻狀態, 管理者點數, 管理者權限)" +
                    "values(@NewName, @NewPhone, @NewAddress, @NewEmail, @NewBirth, @NewMarriage, @NewPoints, @NewAuthority);";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@NewName", txt姓名.Text);
                cmd.Parameters.AddWithValue("@NewPhone", txt電話.Text);
                cmd.Parameters.AddWithValue("@NewAddress", txt地址.Text);
                cmd.Parameters.AddWithValue("@NewEmail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@NewBirth", dtp生日.Value);
                cmd.Parameters.AddWithValue("@NewMarriage", chk婚姻狀態.Checked);
                int intPoints = 0;
                Int32.TryParse(txt點數.Text, out intPoints);      // k-p
                cmd.Parameters.AddWithValue("@NewPoints", intPoints);
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
            }*/
        }

        void btn會員登入_Click(object sender, EventArgs e)
        {
            GlobalVar.is會員登入 = true;
            Close();
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
