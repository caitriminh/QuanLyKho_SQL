namespace QuanLyKho.DanhMuc
{
    partial class FrmNhomHang
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNhomHang));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
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
            this.lbl_manhom = new DevExpress.XtraEditors.LabelControl();
            this.label1A = new DevExpress.XtraEditors.LabelControl();
            this.grcNhomHang = new DevExpress.XtraGrid.GridControl();
            this.grvView_NhomHang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_xoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this._btn_xoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcNhomHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvView_NhomHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._btn_xoa)).BeginInit();
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
            this.barManager1.MaxItemId = 6;
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
            this.btn_Them.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_Them_ItemClick);
            // 
            // btn_Luu
            // 
            this.btn_Luu.Caption = "Lưu";
            this.btn_Luu.Id = 1;
            this.btn_Luu.ImageOptions.ImageIndex = 3;
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_Luu_ItemClick);
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.Caption = "Xóa";
            this.btn_Xoa.Id = 2;
            this.btn_Xoa.ImageOptions.ImageIndex = 1;
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_Xoa_ItemClick);
            // 
            // btn_NapLai
            // 
            this.btn_NapLai.Caption = "Nạp lại";
            this.btn_NapLai.Id = 3;
            this.btn_NapLai.ImageOptions.ImageIndex = 2;
            this.btn_NapLai.Name = "btn_NapLai";
            this.btn_NapLai.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_NapLai_ItemClick);
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
            // lbl_manhom
            // 
            this.lbl_manhom.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_manhom.Location = new System.Drawing.Point(357, 150);
            this.lbl_manhom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbl_manhom.Name = "lbl_manhom";
            this.lbl_manhom.Size = new System.Drawing.Size(64, 0);
            this.lbl_manhom.TabIndex = 10;
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
            // grcNhomHang
            // 
            this.grcNhomHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcNhomHang.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grcNhomHang.Location = new System.Drawing.Point(0, 24);
            this.grcNhomHang.MainView = this.grvView_NhomHang;
            this.grcNhomHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grcNhomHang.MenuManager = this.barManager1;
            this.grcNhomHang.Name = "grcNhomHang";
            this.grcNhomHang.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._btn_xoa});
            this.grcNhomHang.Size = new System.Drawing.Size(718, 329);
            this.grcNhomHang.TabIndex = 16;
            this.grcNhomHang.UseEmbeddedNavigator = true;
            this.grcNhomHang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvView_NhomHang});
            // 
            // grvView_NhomHang
            // 
            this.grvView_NhomHang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn7,
            this.col_xoa,
            this.gridColumn8});
            this.grvView_NhomHang.DetailHeight = 284;
            this.grvView_NhomHang.GridControl = this.grcNhomHang;
            this.grvView_NhomHang.Name = "grvView_NhomHang";
            this.grvView_NhomHang.OptionsView.ColumnAutoWidth = false;
            this.grvView_NhomHang.OptionsView.ShowAutoFilterRow = true;
            this.grvView_NhomHang.OptionsView.ShowGroupPanel = false;
            this.grvView_NhomHang.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.GridView1_RowCellClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn1.Caption = "Mã nhóm";
            this.gridColumn1.FieldName = "manhom";
            this.gridColumn1.MinWidth = 17;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 86;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Nhóm hàng";
            this.gridColumn2.FieldName = "nhomhang";
            this.gridColumn2.MinWidth = 17;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
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
            this.gridColumn3.VisibleIndex = 4;
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
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 129;
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
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 94;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ngày sửa";
            this.gridColumn7.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm:ss}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn7.FieldName = "thoigian2";
            this.gridColumn7.MinWidth = 21;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 129;
            // 
            // col_xoa
            // 
            this.col_xoa.AppearanceHeader.Options.UseTextOptions = true;
            this.col_xoa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_xoa.Caption = "Xóa";
            this.col_xoa.ColumnEdit = this._btn_xoa;
            this.col_xoa.MinWidth = 21;
            this.col_xoa.Name = "col_xoa";
            this.col_xoa.OptionsColumn.AllowEdit = false;
            this.col_xoa.OptionsColumn.FixedWidth = true;
            this.col_xoa.Visible = true;
            this.col_xoa.VisibleIndex = 0;
            this.col_xoa.Width = 30;
            // 
            // _btn_xoa
            // 
            this._btn_xoa.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this._btn_xoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this._btn_xoa.Name = "_btn_xoa";
            this._btn_xoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Sử dụng";
            this.gridColumn8.FieldName = "sudung";
            this.gridColumn8.MinWidth = 21;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 64;
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            // 
            // frm_nhomhang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 353);
            this.Controls.Add(this.grcNhomHang);
            this.Controls.Add(this.label1A);
            this.Controls.Add(this.lbl_manhom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_nhomhang";
            this.Text = "Nhóm Hàng";
            this.Load += new System.EventHandler(this.Frm_nhomhang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcNhomHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvView_NhomHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._btn_xoa)).EndInit();
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
        private DevExpress.XtraEditors.LabelControl lbl_manhom;
        private DevExpress.XtraGrid.GridControl grcNhomHang;
        private DevExpress.XtraGrid.Views.Grid.GridView grvView_NhomHang;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn col_xoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit _btn_xoa;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
    }
}
