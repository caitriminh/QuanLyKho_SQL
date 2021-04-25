namespace QuanLyKho.BieuDo
{
    partial class FrmBieuDoDoanhThu
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
            this.userBieuDo1 = new QuanLyKho.BieuDo.UcBieuDoDoanhThu();
            this.SuspendLayout();
            // 
            // userBieuDo1
            // 
            this.userBieuDo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userBieuDo1.Location = new System.Drawing.Point(0, 0);
            this.userBieuDo1.Margin = new System.Windows.Forms.Padding(2);
            this.userBieuDo1.Name = "userBieuDo1";
            this.userBieuDo1.Size = new System.Drawing.Size(1138, 479);
            this.userBieuDo1.TabIndex = 0;
            // 
            // FrmBieuDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 479);
            this.Controls.Add(this.userBieuDo1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmBieuDo";
            this.Text = "Biểu Đồ Doanh Thu";
            this.Load += new System.EventHandler(this.FrmBanLamViec_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UcBieuDoDoanhThu userBieuDo1;
    }
}