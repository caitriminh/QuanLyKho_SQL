namespace QuanLyKho.BieuDo
{
    partial class UcBieuDo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions1 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcBieuDo));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions2 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.nhomhang = new DevExpress.XtraBars.Docking2010.Views.Widget.Document(this.components);
            this.giatri = new DevExpress.XtraBars.Docking2010.Views.Widget.Document(this.components);
            this.ss_doanhthu = new DevExpress.XtraBars.Docking2010.Views.Widget.Document(this.components);
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.widgetView1 = new DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView(this.components);
            this.columnDefinition1 = new DevExpress.XtraBars.Docking2010.Views.Widget.ColumnDefinition();
            this.columnDefinition2 = new DevExpress.XtraBars.Docking2010.Views.Widget.ColumnDefinition();
            this.columnDefinition3 = new DevExpress.XtraBars.Docking2010.Views.Widget.ColumnDefinition();
            this.rowDefinition1 = new DevExpress.XtraBars.Docking2010.Views.Widget.RowDefinition();
            this.rowDefinition2 = new DevExpress.XtraBars.Docking2010.Views.Widget.RowDefinition();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.mnu_doanhthu_theothang = new DevExpress.XtraBars.BarButtonItem();
            this.mnu_doanhthu_hanghoa = new DevExpress.XtraBars.BarButtonItem();
            this.mnu_doanhthu_theonam = new DevExpress.XtraBars.BarButtonItem();
            this.mnu_doanhthu_nhomhang = new DevExpress.XtraBars.BarButtonItem();
            this.mnu_quocgia = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.nhomhang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.giatri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ss_doanhthu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widgetView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnDefinition1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnDefinition2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnDefinition3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowDefinition1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowDefinition2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // nhomhang
            // 
            this.nhomhang.Caption = "Nhóm Hàng";
            this.nhomhang.ControlName = "nhomhang";
            this.nhomhang.ControlTypeName = "QuanLyKho.BieuDo.UcDoanhThuNhomHang";
            customHeaderButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("customHeaderButtonImageOptions1.Image")));
            this.nhomhang.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("", true, customHeaderButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, serializableAppearanceObject1, null, -1)});
            this.nhomhang.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.loaitruyen_CustomButtonClick);
            // 
            // giatri
            // 
            this.giatri.Caption = "Giá Trị";
            this.giatri.ControlName = "giatri";
            this.giatri.ControlTypeName = "QuanLyKho.BieuDo.UcDoanhThuNhomHang";
            customHeaderButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("customHeaderButtonImageOptions2.Image")));
            this.giatri.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("", true, customHeaderButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, serializableAppearanceObject2, null, -1)});
            this.giatri.RowIndex = 1;
            this.giatri.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.giatri_CustomButtonClick);
            // 
            // ss_doanhthu
            // 
            this.ss_doanhthu.Caption = "Doanh Thu";
            this.ss_doanhthu.ColumnIndex = 1;
            this.ss_doanhthu.ColumnSpan = 2;
            this.ss_doanhthu.ControlName = "ss_doanhthu";
            this.ss_doanhthu.ControlTypeName = "QuanLyKho.BieuDo.UcSSDoanhThu";
            this.ss_doanhthu.Height = 674;
            this.ss_doanhthu.RowSpan = 2;
            this.ss_doanhthu.Width = 652;
            this.ss_doanhthu.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.tuatruyen_CustomButtonClick);
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.View = this.widgetView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.widgetView1});
            // 
            // widgetView1
            // 
            this.widgetView1.Columns.AddRange(new DevExpress.XtraBars.Docking2010.Views.Widget.ColumnDefinition[] {
            this.columnDefinition1,
            this.columnDefinition2,
            this.columnDefinition3});
            this.widgetView1.Documents.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseDocument[] {
            this.nhomhang,
            this.giatri,
            this.ss_doanhthu});
            this.widgetView1.LayoutMode = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.TableLayout;
            this.widgetView1.RootContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.widgetView1.Rows.AddRange(new DevExpress.XtraBars.Docking2010.Views.Widget.RowDefinition[] {
            this.rowDefinition1,
            this.rowDefinition2});
            this.widgetView1.QueryControl += new DevExpress.XtraBars.Docking2010.Views.QueryControlEventHandler(this.widgetView1_QueryControl);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mnu_doanhthu_theothang),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnu_doanhthu_theonam),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnu_doanhthu_hanghoa),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnu_doanhthu_nhomhang)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // mnu_doanhthu_theothang
            // 
            this.mnu_doanhthu_theothang.Caption = "Doanh thu theo tháng";
            this.mnu_doanhthu_theothang.Id = 3;
            this.mnu_doanhthu_theothang.Name = "mnu_doanhthu_theothang";
            this.mnu_doanhthu_theothang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnu_loaitruyen_ItemClick);
            // 
            // mnu_doanhthu_hanghoa
            // 
            this.mnu_doanhthu_hanghoa.Caption = "Doanh thu hàng hóa";
            this.mnu_doanhthu_hanghoa.Id = 6;
            this.mnu_doanhthu_hanghoa.Name = "mnu_doanhthu_hanghoa";
            this.mnu_doanhthu_hanghoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnu_tuatruyen_ItemClick);
            // 
            // mnu_doanhthu_theonam
            // 
            this.mnu_doanhthu_theonam.Caption = "Doanh thu theo năm";
            this.mnu_doanhthu_theonam.Id = 5;
            this.mnu_doanhthu_theonam.Name = "mnu_doanhthu_theonam";
            this.mnu_doanhthu_theonam.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnu_giatri_ItemClick);
            // 
            // mnu_doanhthu_nhomhang
            // 
            this.mnu_doanhthu_nhomhang.Caption = "Doanh thu theo nhóm hàng";
            this.mnu_doanhthu_nhomhang.Id = 7;
            this.mnu_doanhthu_nhomhang.Name = "mnu_doanhthu_nhomhang";
            this.mnu_doanhthu_nhomhang.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnu_nhaxuatban_ItemClick);
            // 
            // mnu_quocgia
            // 
            this.mnu_quocgia.Caption = "Quốc gia";
            this.mnu_quocgia.Id = 8;
            this.mnu_quocgia.Name = "mnu_quocgia";
            this.mnu_quocgia.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnu_quocgia_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.mnu_doanhthu_theothang,
            this.mnu_doanhthu_theonam,
            this.mnu_doanhthu_hanghoa,
            this.mnu_doanhthu_nhomhang,
            this.mnu_quocgia});
            this.barManager1.MaxItemId = 9;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(764, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 474);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(764, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 474);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(764, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 474);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Custom 2";
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 2";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.Text = "Custom 2";
            // 
            // bar3
            // 
            this.bar3.BarName = "Custom 3";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 1;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.Text = "Custom 3";
            // 
            // UcBieuDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UcBieuDo";
            this.Size = new System.Drawing.Size(764, 474);
            ((System.ComponentModel.ISupportInitialize)(this.nhomhang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.giatri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ss_doanhthu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widgetView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnDefinition1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnDefinition2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnDefinition3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowDefinition1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowDefinition2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView widgetView1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.Document nhomhang;
        private DevExpress.XtraBars.Docking2010.Views.Widget.Document giatri;
        private DevExpress.XtraBars.Docking2010.Views.Widget.Document ss_doanhthu;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem mnu_doanhthu_theothang;
        private DevExpress.XtraBars.BarButtonItem mnu_doanhthu_theonam;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem mnu_doanhthu_hanghoa;
        private DevExpress.XtraBars.Docking2010.Views.Widget.ColumnDefinition columnDefinition1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.ColumnDefinition columnDefinition2;
        private DevExpress.XtraBars.Docking2010.Views.Widget.ColumnDefinition columnDefinition3;
        private DevExpress.XtraBars.Docking2010.Views.Widget.RowDefinition rowDefinition1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.RowDefinition rowDefinition2;
        private DevExpress.XtraBars.BarButtonItem mnu_doanhthu_nhomhang;
        private DevExpress.XtraBars.BarButtonItem mnu_quocgia;
    }
}
