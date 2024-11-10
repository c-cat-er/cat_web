namespace PlayFood
{
    partial class FormFood
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
            this.imageList熱食商品圖檔 = new System.Windows.Forms.ImageList(this.components);
            this.imageList冷食商品圖檔 = new System.Windows.Forms.ImageList(this.components);
            this.imageList點心商品圖檔 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList熱食商品圖檔
            // 
            this.imageList熱食商品圖檔.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this.imageList熱食商品圖檔.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList熱食商品圖檔.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList冷食商品圖檔
            // 
            this.imageList冷食商品圖檔.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList冷食商品圖檔.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList冷食商品圖檔.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList點心商品圖檔
            // 
            this.imageList點心商品圖檔.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList點心商品圖檔.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList點心商品圖檔.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 694);
            this.Name = "FormFood";
            this.Text = "FormFood";
            this.Load += new System.EventHandler(this.FormFood_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList熱食商品圖檔;
        private System.Windows.Forms.ImageList imageList冷食商品圖檔;
        private System.Windows.Forms.ImageList imageList點心商品圖檔;
    }
}