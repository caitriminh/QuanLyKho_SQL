﻿namespace QuanLyKho.BieuDo
{
    partial class FrmBieuDoLoiNhuan
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
            this.ucBieuDoLoiNhuan1 = new QuanLyKho.BieuDo.UcBieuDoLoiNhuan();
            this.SuspendLayout();
            // 
            // ucBieuDoLoiNhuan1
            // 
            this.ucBieuDoLoiNhuan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBieuDoLoiNhuan1.Location = new System.Drawing.Point(0, 0);
            this.ucBieuDoLoiNhuan1.Margin = new System.Windows.Forms.Padding(2);
            this.ucBieuDoLoiNhuan1.Name = "ucBieuDoLoiNhuan1";
            this.ucBieuDoLoiNhuan1.Size = new System.Drawing.Size(1138, 479);
            this.ucBieuDoLoiNhuan1.TabIndex = 0;
            // 
            // FrmBieuDoLoiNhuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 479);
            this.Controls.Add(this.ucBieuDoLoiNhuan1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmBieuDoLoiNhuan";
            this.Text = "Biểu Đồ Lợi Nhuận";
            this.ResumeLayout(false);

        }

        #endregion

        private UcBieuDoChiPhi userBieuDoChiPhi1;
        private UcBieuDoLoiNhuan ucBieuDoLoiNhuan1;
    }
}