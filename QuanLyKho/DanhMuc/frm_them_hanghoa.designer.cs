namespace QuanLyKho.DanhMuc
{
    partial class frm_them_hanghoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_them_hanghoa));
            this.GroupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txt_dongia = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbo_dvt = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbo_nhomhang = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txt_ghichu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txt_tenhanghoa = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Thoat = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Luu = new DevExpress.XtraEditors.SimpleButton();
            this.label1A = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl1)).BeginInit();
            this.GroupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_dongia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_dvt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_nhomhang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ghichu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tenhanghoa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupControl1
            // 
            this.GroupControl1.Controls.Add(this.txt_dongia);
            this.GroupControl1.Controls.Add(this.labelControl1);
            this.GroupControl1.Controls.Add(this.cbo_dvt);
            this.GroupControl1.Controls.Add(this.cbo_nhomhang);
            this.GroupControl1.Controls.Add(this.txt_ghichu);
            this.GroupControl1.Controls.Add(this.labelControl9);
            this.GroupControl1.Controls.Add(this.labelControl8);
            this.GroupControl1.Controls.Add(this.txt_tenhanghoa);
            this.GroupControl1.Controls.Add(this.labelControl4);
            this.GroupControl1.Controls.Add(this.labelControl3);
            this.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupControl1.Location = new System.Drawing.Point(0, 0);
            this.GroupControl1.Name = "GroupControl1";
            this.GroupControl1.Size = new System.Drawing.Size(447, 150);
            this.GroupControl1.TabIndex = 0;
            this.GroupControl1.Text = "Thông tin";
            // 
            // txt_dongia
            // 
            this.txt_dongia.EditValue = "0";
            this.txt_dongia.Location = new System.Drawing.Point(87, 98);
            this.txt_dongia.Name = "txt_dongia";
            this.txt_dongia.Properties.Mask.EditMask = "n2";
            this.txt_dongia.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_dongia.Size = new System.Drawing.Size(351, 20);
            this.txt_dongia.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 101);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Đơn giá";
            // 
            // cbo_dvt
            // 
            this.cbo_dvt.Location = new System.Drawing.Point(87, 75);
            this.cbo_dvt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_dvt.Name = "cbo_dvt";
            this.cbo_dvt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.cbo_dvt.Properties.NullText = "";
            this.cbo_dvt.Properties.PopupView = this.gridView1;
            this.cbo_dvt.Size = new System.Drawing.Size(351, 20);
            this.cbo_dvt.TabIndex = 8;
            this.cbo_dvt.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cbo_dvt_ButtonClick);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2});
            this.gridView1.DetailHeight = 284;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Đơn vị tính";
            this.gridColumn2.FieldName = "tendvt";
            this.gridColumn2.MinWidth = 17;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 64;
            // 
            // cbo_nhomhang
            // 
            this.cbo_nhomhang.Location = new System.Drawing.Point(87, 28);
            this.cbo_nhomhang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_nhomhang.Name = "cbo_nhomhang";
            this.cbo_nhomhang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.cbo_nhomhang.Properties.NullText = "";
            this.cbo_nhomhang.Properties.PopupView = this.gridLookUpEdit1View;
            this.cbo_nhomhang.Size = new System.Drawing.Size(351, 20);
            this.cbo_nhomhang.TabIndex = 1;
            this.cbo_nhomhang.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cbo_nhomhang_ButtonClick);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridLookUpEdit1View.DetailHeight = 284;
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nhóm hàng";
            this.gridColumn1.FieldName = "nhomhang";
            this.gridColumn1.MinWidth = 17;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 64;
            // 
            // txt_ghichu
            // 
            this.txt_ghichu.Location = new System.Drawing.Point(87, 121);
            this.txt_ghichu.Name = "txt_ghichu";
            this.txt_ghichu.Size = new System.Drawing.Size(351, 20);
            this.txt_ghichu.TabIndex = 7;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(11, 77);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(20, 13);
            this.labelControl9.TabIndex = 4;
            this.labelControl9.Text = "ĐVT";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(11, 124);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(35, 13);
            this.labelControl8.TabIndex = 6;
            this.labelControl8.Text = "Ghi chú";
            // 
            // txt_tenhanghoa
            // 
            this.txt_tenhanghoa.Location = new System.Drawing.Point(88, 51);
            this.txt_tenhanghoa.Name = "txt_tenhanghoa";
            this.txt_tenhanghoa.Properties.Mask.EditMask = "[A-Z].*";
            this.txt_tenhanghoa.Size = new System.Drawing.Size(351, 20);
            this.txt_tenhanghoa.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 54);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(66, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Tên hàng hóa";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 30);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Nhóm hàng";
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Thoat.ImageOptions.Image")));
            this.btn_Thoat.Location = new System.Drawing.Point(375, 157);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(65, 23);
            this.btn_Thoat.TabIndex = 2;
            this.btn_Thoat.Text = "Thoát";
            this.btn_Thoat.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // btn_Luu
            // 
            this.btn_Luu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Luu.ImageOptions.Image")));
            this.btn_Luu.Location = new System.Drawing.Point(304, 157);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Size = new System.Drawing.Size(65, 23);
            this.btn_Luu.TabIndex = 1;
            this.btn_Luu.Text = "&Lưu";
            this.btn_Luu.Click += new System.EventHandler(this.btn_Luu_Click);
            // 
            // label1A
            // 
            this.label1A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label1A.Location = new System.Drawing.Point(169, 139);
            this.label1A.Name = "label1A";
            this.label1A.Size = new System.Drawing.Size(36, 0);
            this.label1A.TabIndex = 8;
            // 
            // frm_them_hanghoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 199);
            this.Controls.Add(this.label1A);
            this.Controls.Add(this.GroupControl1);
            this.Controls.Add(this.btn_Thoat);
            this.Controls.Add(this.btn_Luu);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frm_them_hanghoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hàng Hóa";
            this.Load += new System.EventHandler(this.Frm_them_hanghoa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_them_hanghoa_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GroupControl1)).EndInit();
            this.GroupControl1.ResumeLayout(false);
            this.GroupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_dongia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_dvt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_nhomhang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ghichu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_tenhanghoa.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.GroupControl GroupControl1;
        internal DevExpress.XtraEditors.SimpleButton btn_Thoat;
        internal DevExpress.XtraEditors.SimpleButton btn_Luu;
        internal DevExpress.XtraEditors.TextEdit txt_ghichu;
        internal DevExpress.XtraEditors.LabelControl labelControl8;
        internal DevExpress.XtraEditors.TextEdit txt_tenhanghoa;
        internal DevExpress.XtraEditors.LabelControl labelControl4;
        internal DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.GridLookUpEdit cbo_nhomhang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        internal DevExpress.XtraEditors.LabelControl label1A;
        private DevExpress.XtraEditors.GridLookUpEdit cbo_dvt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        internal DevExpress.XtraEditors.TextEdit txt_dongia;
        internal DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
