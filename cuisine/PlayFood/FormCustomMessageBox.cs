using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace PlayFood
{
    public partial class FormCustomMessageBox : Form
    {
        public FormCustomMessageBox()
        {
            InitializeComponent();
            Size = new Size(800, 600);
        }

        private void FormCustomMessageBox_Load(object sender, EventArgs e)
        {
            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(180, 90);
            lbl名稱.Size = new Size(400, 80);
            lbl名稱.AutoSize = false;
            lbl名稱.Text = "⚠️您尚未註冊 / 登入會員，無法使用折扣、抽獎、消費反饋點數，是否要先註冊 / 登入？";
            lbl名稱.Font = new Font("微軟正黑體", 14);
            Controls.Add(lbl名稱);

            FlowLayoutPanel flowLayoutPanel詢問會員 = new FlowLayoutPanel();
            flowLayoutPanel詢問會員.Location = new Point(180, 200);
            flowLayoutPanel詢問會員.Size = new Size(380, 150);
            Controls.Add(flowLayoutPanel詢問會員);

            List<string> listBtnStr = new List<string>()
            {
                "返回菜單", "我要註冊", "我要登入" , "不要，直接購買"
            };

            List<EventHandler> listEH詢問會員 = new List<EventHandler>()
            {
                new EventHandler(btn返回菜單_Click), new EventHandler(btn我要註冊_Click),
                new EventHandler(btn我要登入_Click), new EventHandler(btn直接購買_Click),
            };

            int i = 0;
            foreach (var item in listBtnStr)
            {
                Button btn = new Button();
                btn.Size = new Size(180, 50);
                btn.BackColor = Color.LightBlue;
                btn.Text = item;
                btn.Font = new Font("微軟正黑體", 14);
                btn.Click += listEH詢問會員[i];
                flowLayoutPanel詢問會員.Controls.Add(btn);
                i++;
            }
        }

        public string Result { get; private set; }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Result = "Register";
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Result = "Login";
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = "Cancel";
            Close();
        }

        void btn返回菜單_Click(object sender, EventArgs e)
        {
            Close();
        }

        void btn我要註冊_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.Show();
            Close();
        }

        void btn我要登入_Click(object sender, EventArgs e)
        {
            FormLoginMember formLoginMember = new FormLoginMember();
            formLoginMember.Show();
            Close();
        }

        void btn直接購買_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
