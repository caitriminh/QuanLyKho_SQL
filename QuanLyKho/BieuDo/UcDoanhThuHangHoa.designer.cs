namespace QuanLyKho.BieuDo
{
    partial class UcDoanhThuHangHoa
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
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grcNhomHang = new DevExpress.XtraGrid.GridControl();
            this.grvViewNhomHang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcNhomHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvViewNhomHang)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(708, 531);
            this.panelControl.TabIndex = 2;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grcNhomHang);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(885, 531);
            this.splitContainerControl1.SplitterPosition = 167;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // grcNhomHang
            // 
            this.grcNhomHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcNhomHang.Location = new System.Drawing.Point(0, 0);
            this.grcNhomHang.MainView = this.grvViewNhomHang;
            this.grcNhomHang.Name = "grcNhomHang";
            this.grcNhomHang.Size = new System.Drawing.Size(167, 531);
            this.grcNhomHang.TabIndex = 1;
            this.grcNhomHang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvViewNhomHang});
            // 
            // grvViewNhomHang
            // 
            this.grvViewNhomHang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.grvViewNhomHang.GridControl = this.grcNhomHang;
            this.grvViewNhomHang.Name = "grvViewNhomHang";
            this.grvViewNhomHang.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nhóm hàng";
            this.gridColumn1.FieldName = "nhomhang";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // UcDoanhThuHangHoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcDoanhThuHangHoa";
            this.Size = new System.Drawing.Size(885, 531);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcNhomHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvViewNhomHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grcNhomHang;
        private DevExpress.XtraGrid.Views.Grid.GridView grvViewNhomHang;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
