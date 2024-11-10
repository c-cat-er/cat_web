namespace PlayFood
{
    partial class FormPersonMang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl會員 = new System.Windows.Forms.TabControl();
            this.tabPage所有管理者 = new System.Windows.Forms.TabPage();
            this.dgv管理者資料列表 = new System.Windows.Forms.DataGridView();
            this.tabPage管理者資料修改 = new System.Windows.Forms.TabPage();
            this.dgv管理者資料修改列表 = new System.Windows.Forms.DataGridView();
            this.tabControl會員.SuspendLayout();
            this.tabPage所有管理者.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv管理者資料列表)).BeginInit();
            this.tabPage管理者資料修改.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv管理者資料修改列表)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl會員
            // 
            this.tabControl會員.Controls.Add(this.tabPage所有管理者);
            this.tabControl會員.Controls.Add(this.tabPage管理者資料修改);
            this.tabControl會員.ItemSize = new System.Drawing.Size(1, 1);
            this.tabControl會員.Location = new System.Drawing.Point(251, 90);
            this.tabControl會員.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl會員.Name = "tabControl會員";
            this.tabControl會員.SelectedIndex = 0;
            this.tabControl會員.Size = new System.Drawing.Size(1043, 724);
            this.tabControl會員.TabIndex = 2;
            // 
            // tabPage所有管理者
            // 
            this.tabPage所有管理者.Controls.Add(this.dgv管理者資料列表);
            this.tabPage所有管理者.Location = new System.Drawing.Point(4, 14);
            this.tabPage所有管理者.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage所有管理者.Name = "tabPage所有管理者";
            this.tabPage所有管理者.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage所有管理者.Size = new System.Drawing.Size(1035, 706);
            this.tabPage所有管理者.TabIndex = 0;
            this.tabPage所有管理者.UseVisualStyleBackColor = true;
            // 
            // dgv管理者資料列表
            // 
            this.dgv管理者資料列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv管理者資料列表.Location = new System.Drawing.Point(5, 6);
            this.dgv管理者資料列表.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv管理者資料列表.Name = "dgv管理者資料列表";
            this.dgv管理者資料列表.RowHeadersWidth = 51;
            this.dgv管理者資料列表.RowTemplate.Height = 27;
            this.dgv管理者資料列表.Size = new System.Drawing.Size(1024, 705);
            this.dgv管理者資料列表.TabIndex = 0;
            this.dgv管理者資料列表.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv管理者資料列表_CellClick);
            // 
            // tabPage管理者資料修改
            // 
            this.tabPage管理者資料修改.Controls.Add(this.dgv管理者資料修改列表);
            this.tabPage管理者資料修改.Location = new System.Drawing.Point(4, 5);
            this.tabPage管理者資料修改.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage管理者資料修改.Name = "tabPage管理者資料修改";
            this.tabPage管理者資料修改.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage管理者資料修改.Size = new System.Drawing.Size(1035, 715);
            this.tabPage管理者資料修改.TabIndex = 1;
            this.tabPage管理者資料修改.UseVisualStyleBackColor = true;
            // 
            // dgv管理者資料修改列表
            // 
            this.dgv管理者資料修改列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv管理者資料修改列表.Location = new System.Drawing.Point(40, 150);
            this.dgv管理者資料修改列表.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv管理者資料修改列表.Name = "dgv管理者資料修改列表";
            this.dgv管理者資料修改列表.RowHeadersWidth = 51;
            this.dgv管理者資料修改列表.RowTemplate.Height = 27;
            this.dgv管理者資料修改列表.Size = new System.Drawing.Size(350, 380);
            this.dgv管理者資料修改列表.TabIndex = 0;
            this.dgv管理者資料修改列表.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv管理者資料修改列表_CellClick);
            // 
            // FormPersonMang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 828);
            this.Controls.Add(this.tabControl會員);
            this.Name = "FormPersonMang";
            this.Text = "FormPersonMang";
            this.Load += new System.EventHandler(this.FormPersonMang_Load);
            this.tabControl會員.ResumeLayout(false);
            this.tabPage所有管理者.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv管理者資料列表)).EndInit();
            this.tabPage管理者資料修改.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv管理者資料修改列表)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl會員;
        private System.Windows.Forms.TabPage tabPage所有管理者;
        private System.Windows.Forms.DataGridView dgv管理者資料列表;
        private System.Windows.Forms.TabPage tabPage管理者資料修改;
        private System.Windows.Forms.DataGridView dgv管理者資料修改列表;
    }
}