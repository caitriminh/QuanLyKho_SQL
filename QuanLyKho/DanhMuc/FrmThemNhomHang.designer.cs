namespace QuanLyKho.DanhMuc
{
    partial class FrmThemNhomHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemNhomHang));
            this.GroupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txt_ghichu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_nhomhang = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_manhom = new DevExpress.XtraEditors.TextEdit();
            this.LabelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Thoat = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Luu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl1)).BeginInit();
            this.GroupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ghichu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_nhomhang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_manhom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupControl1
            // 
            this.GroupControl1.Controls.Add(this.txt_ghichu);
            this.GroupControl1.Controls.Add(this.labelControl2);
            this.GroupControl1.Controls.Add(this.txt_nhomhang);
            this.GroupControl1.Controls.Add(this.labelControl1);
            this.GroupControl1.Controls.Add(this.txt_manhom);
            this.GroupControl1.Controls.Add(this.LabelControl4);
            this.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupControl1.Location = new System.Drawing.Point(0, 0);
            this.GroupControl1.Name = "GroupControl1";
            this.GroupControl1.Size = new System.Drawing.Size(387, 102);
            this.GroupControl1.TabIndex = 0;
            this.GroupControl1.Text = "Thông tin";
            // 
            // txt_ghichu
            // 
            this.txt_ghichu.Location = new System.Drawing.Point(80, 77);
            this.txt_ghichu.Name = "txt_ghichu";
            this.txt_ghichu.Properties.Mask.EditMask = "[A-Z].*";
            this.txt_ghichu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txt_ghichu.Size = new System.Drawing.Size(297, 20);
            this.txt_ghichu.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 79);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Ghi chú";
            // 
            // txt_nhomhang
            // 
            this.txt_nhomhang.Location = new System.Drawing.Point(80, 53);
            this.txt_nhomhang.Name = "txt_nhomhang";
            this.txt_nhomhang.Size = new System.Drawing.Size(297, 20);
            this.txt_nhomhang.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Nhóm hàng";
            // 
            // txt_manhom
            // 
            this.txt_manhom.Location = new System.Drawing.Point(80, 28);
            this.txt_manhom.Name = "txt_manhom";
            this.txt_manhom.Properties.Mask.EditMask = "\\p{Lu}+";
            this.txt_manhom.Properties.MaxLength = 5;
            this.txt_manhom.Size = new System.Drawing.Size(297, 20);
            this.txt_manhom.TabIndex = 1;
            this.txt_manhom.Leave += new System.EventHandler(this.txt_manhom_Leave);
            // 
            // LabelControl4
            // 
            this.LabelControl4.Location = new System.Drawing.Point(10, 30);
            this.LabelControl4.Name = "LabelControl4";
            this.LabelControl4.Size = new System.Drawing.Size(43, 13);
            this.LabelControl4.TabIndex = 0;
            this.LabelControl4.Text = "Mã nhóm";
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Thoat.ImageOptions.Image")));
            this.btn_Thoat.Location = new System.Drawing.Point(312, 109);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(65, 23);
            this.btn_Thoat.TabIndex = 2;
            this.btn_Thoat.Text = "Thoát";
            this.btn_Thoat.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // btn_Luu
            // 
            this.btn_Luu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Luu.ImageOptions.Image")));
            this.btn_Luu.Location = new System.Drawing.Point(241, 109);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Size = new System.Drawing.Size(65, 23);
            this.btn_Luu.TabIndex = 1;
            this.btn_Luu.Text = "&Lưu";
            this.btn_Luu.Click += new System.EventHandler(this.btn_Luu_Click);
            // 
            // frm_them_nhomhang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 142);
            this.Controls.Add(this.GroupControl1);
            this.Controls.Add(this.btn_Thoat);
            this.Controls.Add(this.btn_Luu);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frm_them_nhomhang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhóm Hàng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_add_nhomhang_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_add_nhomhang_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl1)).EndInit();
            this.GroupControl1.ResumeLayout(false);
            this.GroupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ghichu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_nhomhang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_manhom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.GroupControl GroupControl1;
        internal DevExpress.XtraEditors.TextEdit txt_manhom;
        internal DevExpress.XtraEditors.LabelControl LabelControl4;
        internal DevExpress.XtraEditors.SimpleButton btn_Thoat;
        internal DevExpress.XtraEditors.SimpleButton btn_Luu;
        internal DevExpress.XtraEditors.TextEdit txt_nhomhang;
        internal DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.TextEdit txt_ghichu;
        internal DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
