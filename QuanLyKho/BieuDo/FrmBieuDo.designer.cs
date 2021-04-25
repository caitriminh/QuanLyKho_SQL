namespace QuanLyKho.BieuDo
{
    partial class FrmBieuDo
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
            this.userBieuDo1 = new QuanLyKho.BieuDo.UcBieuDo();
            this.SuspendLayout();
            // 
            // userBanLamViec1
            // 
            this.userBieuDo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userBieuDo1.Location = new System.Drawing.Point(0, 0);
            this.userBieuDo1.Name = "userBieuDo1";
            this.userBieuDo1.Size = new System.Drawing.Size(1328, 589);
            this.userBieuDo1.TabIndex = 0;
            // 
            // FrmBanLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 589);
            this.Controls.Add(this.userBieuDo1);
            this.Name = "FrmBieuDo";
            this.Text = "Bàn Làm Việc";
            this.Load += new System.EventHandler(this.FrmBanLamViec_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UcBieuDo userBieuDo1;
    }
}