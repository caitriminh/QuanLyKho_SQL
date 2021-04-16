using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace QuanLyKho.NghiepVu
{
    public partial class frm_xulyamkho : XtraForm
    {
        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 12 });
            barSubItem1.Enabled = dt.luu == true;
            col_soducuoiky.Visible = dt.xoa == true;
            grvViewTonAm.OptionsBehavior.ReadOnly = dt.sua != true;
            barSubItem1.Enabled = dt.inan == true;
            barSubItem3.Enabled = dt.inan == true;
        }

        public void GetChiTietPhieuXuat()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "GET_DATA_DATE_MAHANG", ngayxuat = DateTime.Now.ToString("yyyyMMdd"), mahanghoa = lbl_mahanghoa.Text });
            grcPhieuXuat.DataSource = dt;
        }

        public void GetChiTietPhieuNhap()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "GET_DATA_DATE_MAHANG", ngaynhap = DateTime.Now.ToString("yyyyMMdd"), mahanghoa = lbl_mahanghoa.Text });
            grcNhapKho.DataSource = dt;
        }

        public void GetTonKho()
        {
            ExecSQL.ExecProcedureNonData("prokhoTonKho", new { action = "SAVE", ngaythang = DateTime.Now.ToString("yyyyMM01") });
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoTonKho", new { action = "GET_DATA", option = 3, ngaythang = DateTime.Now.ToString("yyyyMM01") });
            grcTonAm.DataSource = dt;
            lbl_mahanghoa.DataBindings.Clear();
            lbl_mahanghoa.DataBindings.Add("text", dt, "mahanghoa");
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "tonkho" || value.task == "phieunhap")
            {
                GetTonKho();
            }
        }


        #endregion

        public frm_xulyamkho()
        {
            InitializeComponent();
            grvViewTonAm.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcTonAm, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcTonAm, Name);

            grvViewTonAm.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcTonAm, grvViewTonAm); };
            grvViewTonAm.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcTonAm, Name); };

            grvViewNhapKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNhapKho, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcNhapKho, Name);

            grvViewNhapKho.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcNhapKho, grvViewNhapKho); };
            grvViewNhapKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNhapKho, Name); };

            grvViewPhieuXuat.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhieuXuat, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcPhieuXuat, Name);

            grvViewPhieuXuat.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcPhieuXuat, grvViewPhieuXuat); };
            grvViewPhieuXuat.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhieuXuat, Name); };
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTonKho();
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = @"Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BaoCaoTonKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                grvViewTonAm.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void frm_tonkho_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetTonKho();
            GetPhanQuyen();
        }

        private void lbl_mahanghoa_TextChanged(object sender, EventArgs e)
        {
            GetChiTietPhieuNhap();
            GetChiTietPhieuXuat();
        }


        private void btn_capnhat_sodudauky_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmThemNhapKho_XuLyAmKho frm = new FrmThemNhapKho_XuLyAmKho();
            frm.Show();
        }


    }
}
