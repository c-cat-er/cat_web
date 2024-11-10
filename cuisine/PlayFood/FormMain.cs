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
    public partial class FormMain : Form
    {
        Label lbl管理者資訊;
        Label lbl當日日期;
        Button btn管理者登入;
        Button btn登出;

        public FormMain()
        {
            InitializeComponent();
            Size = new Size(800, 600);
            BackColor = Color.LightCyan;

            // 添加Activated事件处理程序
            Activated += FormMain_Activated;

            // 初始化 Timer
            timer現在時間 = new Timer();
            timer現在時間.Interval = 1000; // 每小時觸發一次
            timer現在時間.Tick += timer現在時間_Tick;
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            // 在窗体激活时执行的操作
            if (GlobalVar.is管理者登入)
            {
                btn管理者登入.Visible = false;
                btn登出.Visible = true;
                lbl管理者資訊.Text = GlobalVar.管理者資訊;
            }
            else
            {
                btn管理者登入.Visible = true;
                btn登出.Visible = false;
                lbl管理者資訊.Text = "尚未登入...";
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (GlobalVar.is管理者登入 == false)
            {
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();
            }

            Label lbl名稱 = new Label();
            lbl名稱.Location = new Point(335, 20);
            lbl名稱.AutoSize = false;
            lbl名稱.Size = new Size(130, 40);
            lbl名稱.Text = "管理中心";
            lbl名稱.Font = new Font("微軟正黑體", 20);
            Controls.Add(lbl名稱);

            lbl管理者資訊 = new Label();
            lbl管理者資訊.Location = new Point(150, 60);
            lbl管理者資訊.AutoSize = false;
            lbl管理者資訊.Size = new Size(160, 30);
            lbl管理者資訊.Text = $"使用者:{GlobalVar.管理者姓名} 權限:{GlobalVar.管理者權限}";
            lbl管理者資訊.Font = new Font("微軟正黑體", 12);
            Controls.Add(lbl管理者資訊);

            lbl當日日期 = new Label();
            lbl當日日期.Location = new Point(450, 60);
            lbl當日日期.Size = new Size(300, 30);
            lbl當日日期.Font = new Font("微軟正黑體", 12);
            Controls.Add(lbl當日日期);

            timer現在時間.Start();

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Location = new Point(140, 90);
            flowLayoutPanel.Size = new Size(500, 320);
            Controls.Add(flowLayoutPanel);

            btn管理者登入 = new Button();
            btn管理者登入.Size = new Size(160, 100);
            btn管理者登入.BackColor = Color.LightBlue;
            btn管理者登入.Text = "登入";
            btn管理者登入.Font = new Font("微軟正黑體", 14);
            btn管理者登入.Click += new EventHandler(btn登入_Click);
            flowLayoutPanel.Controls.Add(btn管理者登入);

            List<string> listBtnStr = new List<string>()
            { "菜單", "購物車", "付款", "員工管理系統", "會員管理系統", "商品管理", "訂單管理", "系統管理" };

            List<EventHandler> listBtnEH = new List<EventHandler>()
            {
                new EventHandler(btn菜單_Click),
                new EventHandler(btn購物車_Click), new EventHandler(btn付款_Click),
                new EventHandler(btn員工管理系統_Click), new EventHandler(btn會員管理系統_Click),
                new EventHandler(btn商品管理_Click), new EventHandler(btn訂單管理_Click),
                new EventHandler(btn系統管理_Click),
            };

            Color[] backgroundColors = new Color[]
            {
                Color.LightGoldenrodYellow, Color.LightSalmon, Color.LightPink, Color.LightSeaGreen,
                Color.LightSteelBlue, Color.LightCoral, Color.LightGreen, Color.LightGray
            };

            int i = 0;
            foreach (string str in listBtnStr)
            {
                Button btn = new Button();
                btn.Size = new Size(160, 100);
                btn.Text = str;
                btn.Font = new Font("微軟正黑體", 14);
                btn.Click += listBtnEH[i];
                btn.BackColor = backgroundColors[i];
                flowLayoutPanel.Controls.Add(btn);
                i++;
            }

            btn登出 = new Button();
            btn登出.Location = new Point(500, 440);
            btn登出.Size = new Size(100, 60);
            btn登出.BackColor = Color.LightGray;
            btn登出.Text = "登出";
            btn登出.Font = new Font("微軟正黑體", 14);
            btn登出.Visible = false;
            btn登出.Click += new EventHandler(btn登出_Click);
            Controls.Add(btn登出);
        }

        public void 更新管理者標籤(string str)
        {
            //lbl管理者登入.Text = str;
        }

        void btn登入_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.ShowDialog();
        }

        void btn菜單_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            Hide();
        }

        void btn購物車_Click(object sender, EventArgs e)
        {
            FormShoppingCart shoppingCart = new FormShoppingCart();
            shoppingCart.Show();
            Hide();
        }

        void btn付款_Click(object sender, EventArgs e)
        {
            FormPay formPay = new FormPay();
            formPay.Show();
            Hide();
        }

        void btn員工管理系統_Click(object sender, EventArgs e)
        {
            FormPersonMang formPersonMang = new FormPersonMang();
            formPersonMang.Show();
            Hide();
        }

        void btn會員管理系統_Click(object sender, EventArgs e)
        {
            FormPersonMang formPersonMang = new FormPersonMang();
            formPersonMang.Show();
            Hide();
        }

        void btn商品管理_Click(object sender, EventArgs e)
        {
            FormFood formFood = new FormFood();
            formFood.Show();
            Hide();
        }

        void btn訂單管理_Click(object sender, EventArgs e)
        {
            FormOrder formOrder = new FormOrder();
            formOrder.Show();
            Hide();
        }

        void btn系統管理_Click(object sender, EventArgs e)
        {

        }

        void btn登出_Click(object sender, EventArgs e)
        {
            GlobalVar.is管理者登入 = false;
            GlobalVar.管理者資訊 = "";
            GlobalVar.管理者姓名 = "";
            GlobalVar.管理者id = 0;
            GlobalVar.管理者權限 = 0;
            GlobalVar.管理者職稱 = "";
            lbl管理者資訊.Text = "尚未登入...";
            btn管理者登入.Visible = true;
            btn登出.Visible = false;
        }

        private void timer現在時間_Tick(object sender, EventArgs e)
        {
            try
            {
                // 定時觸發的事件
                DateTime currentDateTime = DateTime.Now;
                lbl當日日期.Text = "現在時間：" + currentDateTime.ToString();
                lbl當日日期.ForeColor = Color.MediumOrchid;
            }
            catch (Exception ex)
            {
                lbl當日日期.Text = "時間顯示錯誤";
            }
        }
    }
}
