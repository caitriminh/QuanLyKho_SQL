namespace QuanLyKho.DanhMuc
{
    partial class FrmLoaiChiPhi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoaiChiPhi));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btn_Them = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Luu = new DevExpress.XtraBars.BarButtonItem();
            this.btn_Xoa = new DevExpress.XtraBars.BarButtonItem();
            this.btn_NapLai = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.lbl_tendangnhap = new DevExpress.XtraEditors.LabelControl();
            this.label1A = new DevExpress.XtraEditors.LabelControl();
            this.grcLoaiChiPhi = new DevExpress.XtraGrid.GridControl();
            this.grvView_LoaiChiPhi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcLoaiChiPhi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvView_LoaiChiPhi)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btn_Them,
            this.btn_Luu,
            this.btn_Xoa,
            this.btn_NapLai});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btn_Them, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btn_Luu, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btn_Xoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btn_NapLai, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btn_Them
            // 
            this.btn_Them.Caption = "&Thêm";
            this.btn_Them.Id = 0;
            this.btn_Them.ImageOptions.ImageIndex = 0;
            this.btn_Them.Name = "btn_Them";
            this.btn_Them.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Them_ItemClick);
            // 
            // btn_Luu
            // 
            this.btn_Luu.Caption = "Lưu";
            this.btn_Luu.Id = 1;
            this.btn_Luu.ImageOptions.ImageIndex = 3;
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Luu_ItemClick);
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.Caption = "Xóa";
            this.btn_Xoa.Id = 2;
            this.btn_Xoa.ImageOptions.ImageIndex = 1;
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_Xoa_ItemClick);
            // 
            // btn_NapLai
            // 
            this.btn_NapLai.Caption = "Nạp lại";
            this.btn_NapLai.Id = 3;
            this.btn_NapLai.ImageOptions.ImageIndex = 2;
            this.btn_NapLai.Name = "btn_NapLai";
            this.btn_NapLai.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_NapLai_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(718, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 353);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(718, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 329);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(718, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 329);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "additem_16x16.png");
            this.imageCollection1.Images.SetKeyName(1, "close_16x16.png");
            this.imageCollection1.Images.SetKeyName(2, "refresh_16x16.png");
            this.imageCollection1.Images.SetKeyName(3, "saveas_16x16.png");
            this.imageCollection1.Images.SetKeyName(4, "exporttoxlsx_16x16.png");
            // 
            // lbl_tendangnhap
            // 
            this.lbl_tendangnhap.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_tendangnhap.Location = new System.Drawing.Point(357, 150);
            this.lbl_tendangnhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbl_tendangnhap.Name = "lbl_tendangnhap";
            this.lbl_tendangnhap.Size = new System.Drawing.Size(64, 0);
            this.lbl_tendangnhap.TabIndex = 10;
            // 
            // label1A
            // 
            this.label1A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label1A.Location = new System.Drawing.Point(357, 162);
            this.label1A.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label1A.Name = "label1A";
            this.label1A.Size = new System.Drawing.Size(64, 0);
            this.label1A.TabIndex = 11;
            // 
            // grcLoaiChiPhi
            // 
            this.grcLoaiChiPhi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcLoaiChiPhi.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grcLoaiChiPhi.Location = new System.Drawing.Point(0, 24);
            this.grcLoaiChiPhi.MainView = this.grvView_LoaiChiPhi;
            this.grcLoaiChiPhi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grcLoaiChiPhi.MenuManager = this.barManager1;
            this.grcLoaiChiPhi.Name = "grcLoaiChiPhi";
            this.grcLoaiChiPhi.Size = new System.Drawing.Size(718, 329);
            this.grcLoaiChiPhi.TabIndex = 16;
            this.grcLoaiChiPhi.UseEmbeddedNavigator = true;
            this.grcLoaiChiPhi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvView_LoaiChiPhi});
            // 
            // grvView_LoaiChiPhi
            // 
            this.grvView_LoaiChiPhi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.grvView_LoaiChiPhi.DetailHeight = 284;
            this.grvView_LoaiChiPhi.GridControl = this.grcLoaiChiPhi;
            this.grvView_LoaiChiPhi.Name = "grvView_LoaiChiPhi";
            this.grvView_LoaiChiPhi.OptionsView.ColumnAutoWidth = false;
            this.grvView_LoaiChiPhi.OptionsView.ShowAutoFilterRow = true;
            this.grvView_LoaiChiPhi.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "maloai";
            this.gridColumn1.MinWidth = 17;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 64;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Loại chi phí";
            this.gridColumn2.FieldName = "loaichiphi";
            this.gridColumn2.MinWidth = 17;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 171;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Người tạo";
            this.gridColumn3.FieldName = "nguoitd";
            this.gridColumn3.MinWidth = 17;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 94;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngày tạo";
            this.gridColumn4.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm:ss}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "thoigian";
            this.gridColumn4.MinWidth = 17;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 129;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Ngày sửa";
            this.gridColumn5.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm:ss}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "thoigian2";
            this.gridColumn5.MinWidth = 21;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 129;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Người sửa";
            this.gridColumn6.FieldName = "nguoitd2";
            this.gridColumn6.MinWidth = 21;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 94;
            // 
            // frm_loaichiphi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 353);
            this.Controls.Add(this.grcLoaiChiPhi);
            this.Controls.Add(this.label1A);
            this.Controls.Add(this.lbl_tendangnhap);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_loaichiphi";
            this.Text = "Loại Chi Phí";
            this.Load += new System.EventHandler(this.frm_loaichiphi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcLoaiChiPhi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvView_LoaiChiPhi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btn_Them;
        private DevExpress.XtraBars.BarButtonItem btn_Luu;
        private DevExpress.XtraBars.BarButtonItem btn_Xoa;
        private DevExpress.XtraBars.BarButtonItem btn_NapLai;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl label1A;
        private DevExpress.XtraEditors.LabelControl lbl_tendangnhap;
        private DevExpress.XtraGrid.GridControl grcLoaiChiPhi;
        private DevExpress.XtraGrid.Views.Grid.GridView grvView_LoaiChiPhi;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}
