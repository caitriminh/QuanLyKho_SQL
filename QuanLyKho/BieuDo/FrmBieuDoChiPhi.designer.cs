namespace QuanLyKho.BieuDo
{
    partial class FrmBieuDoChiPhi
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
            this.ucBieuDoChiPhi1 = new QuanLyKho.BieuDo.UcBieuDoChiPhi();
            this.SuspendLayout();
            // 
            // ucBieuDoChiPhi1
            // 
            this.ucBieuDoChiPhi1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBieuDoChiPhi1.Location = new System.Drawing.Point(0, 0);
            this.ucBieuDoChiPhi1.Margin = new System.Windows.Forms.Padding(2);
            this.ucBieuDoChiPhi1.Name = "ucBieuDoChiPhi1";
            this.ucBieuDoChiPhi1.Size = new System.Drawing.Size(1138, 479);
            this.ucBieuDoChiPhi1.TabIndex = 0;
            // 
            // FrmBieuDoChiPhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 479);
            this.Controls.Add(this.ucBieuDoChiPhi1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmBieuDoChiPhi";
            this.Text = "Biểu Đồ Chi Phí";
            this.ResumeLayout(false);

        }

        #endregion

        //private UcBieuDoChiPhi userBieuDoChiPhi1;
        private UcBieuDoChiPhi ucBieuDoChiPhi1;
    }
}