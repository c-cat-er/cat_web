namespace PlayFood
{
    partial class FormOrder
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
            this.components = new System.ComponentModel.Container();
            this.tabControl訂單 = new System.Windows.Forms.TabControl();
            this.tabPage所有訂單 = new System.Windows.Forms.TabPage();
            this.dgv訂單資料列表 = new System.Windows.Forms.DataGridView();
            this.tabPage訂單資料修改 = new System.Windows.Forms.TabPage();
            this.dgv訂單資料修改列表 = new System.Windows.Forms.DataGridView();
            this.tabPage數據分析 = new System.Windows.Forms.TabPage();
            this.ordersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.playfoodDataSet = new PlayFood.playfoodDataSet();
            this.ordersTableAdapter = new PlayFood.playfoodDataSetTableAdapters.OrdersTableAdapter();
            this.tabControl訂單.SuspendLayout();
            this.tabPage所有訂單.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv訂單資料列表)).BeginInit();
            this.tabPage訂單資料修改.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv訂單資料修改列表)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playfoodDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl訂單
            // 
            this.tabControl訂單.Controls.Add(this.tabPage所有訂單);
            this.tabControl訂單.Controls.Add(this.tabPage訂單資料修改);
            this.tabControl訂單.Controls.Add(this.tabPage數據分析);
            this.tabControl訂單.ItemSize = new System.Drawing.Size(1, 1);
            this.tabControl訂單.Location = new System.Drawing.Point(251, 90);
            this.tabControl訂單.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl訂單.Name = "tabControl訂單";
            this.tabControl訂單.SelectedIndex = 0;
            this.tabControl訂單.Size = new System.Drawing.Size(1043, 724);
            this.tabControl訂單.TabIndex = 1;
            // 
            // tabPage所有訂單
            // 
            this.tabPage所有訂單.Controls.Add(this.dgv訂單資料列表);
            this.tabPage所有訂單.Location = new System.Drawing.Point(4, 14);
            this.tabPage所有訂單.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage所有訂單.Name = "tabPage所有訂單";
            this.tabPage所有訂單.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage所有訂單.Size = new System.Drawing.Size(1035, 706);
            this.tabPage所有訂單.TabIndex = 0;
            this.tabPage所有訂單.UseVisualStyleBackColor = true;
            // 
            // dgv訂單資料列表
            // 
            this.dgv訂單資料列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv訂單資料列表.Location = new System.Drawing.Point(5, 6);
            this.dgv訂單資料列表.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv訂單資料列表.Name = "dgv訂單資料列表";
            this.dgv訂單資料列表.RowHeadersWidth = 51;
            this.dgv訂單資料列表.RowTemplate.Height = 27;
            this.dgv訂單資料列表.Size = new System.Drawing.Size(1024, 705);
            this.dgv訂單資料列表.TabIndex = 0;
            // 
            // tabPage訂單資料修改
            // 
            this.tabPage訂單資料修改.Controls.Add(this.dgv訂單資料修改列表);
            this.tabPage訂單資料修改.Location = new System.Drawing.Point(4, 14);
            this.tabPage訂單資料修改.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage訂單資料修改.Name = "tabPage訂單資料修改";
            this.tabPage訂單資料修改.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage訂單資料修改.Size = new System.Drawing.Size(1035, 706);
            this.tabPage訂單資料修改.TabIndex = 1;
            this.tabPage訂單資料修改.UseVisualStyleBackColor = true;
            // 
            // dgv訂單資料修改列表
            // 
            this.dgv訂單資料修改列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv訂單資料修改列表.Location = new System.Drawing.Point(40, 150);
            this.dgv訂單資料修改列表.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv訂單資料修改列表.Name = "dgv訂單資料修改列表";
            this.dgv訂單資料修改列表.RowHeadersWidth = 51;
            this.dgv訂單資料修改列表.RowTemplate.Height = 27;
            this.dgv訂單資料修改列表.Size = new System.Drawing.Size(350, 380);
            this.dgv訂單資料修改列表.TabIndex = 0;
            this.dgv訂單資料修改列表.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv訂單資料修改列表_CellClick);
            // 
            // tabPage數據分析
            // 
            this.tabPage數據分析.Location = new System.Drawing.Point(4, 5);
            this.tabPage數據分析.Name = "tabPage數據分析";
            this.tabPage數據分析.Size = new System.Drawing.Size(1035, 715);
            this.tabPage數據分析.TabIndex = 2;
            this.tabPage數據分析.UseVisualStyleBackColor = true;
            this.tabPage數據分析.Click += new System.EventHandler(this.tabPage數據分析_Click);
            // 
            // ordersBindingSource
            // 
            this.ordersBindingSource.DataMember = "Orders";
            this.ordersBindingSource.DataSource = this.playfoodDataSet;
            // 
            // playfoodDataSet
            // 
            this.playfoodDataSet.DataSetName = "playfoodDataSet";
            this.playfoodDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ordersTableAdapter
            // 
            this.ordersTableAdapter.ClearBeforeFill = true;
            // 
            // FormOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 828);
            this.Controls.Add(this.tabControl訂單);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormOrder";
            this.Text = "FormOrder";
            this.Load += new System.EventHandler(this.FormOrder_Load);
            this.tabControl訂單.ResumeLayout(false);
            this.tabPage所有訂單.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv訂單資料列表)).EndInit();
            this.tabPage訂單資料修改.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv訂單資料修改列表)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playfoodDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl訂單;
        private System.Windows.Forms.TabPage tabPage所有訂單;
        private System.Windows.Forms.DataGridView dgv訂單資料列表;
        private System.Windows.Forms.TabPage tabPage訂單資料修改;
        private System.Windows.Forms.DataGridView dgv訂單資料修改列表;
        private System.Windows.Forms.TabPage tabPage數據分析;
        private playfoodDataSet playfoodDataSet;
        private System.Windows.Forms.BindingSource ordersBindingSource;
        private playfoodDataSetTableAdapters.OrdersTableAdapter ordersTableAdapter;
    }
}