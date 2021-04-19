using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyKho.NghiepVu
{
    public partial class FrmNhapXuatKho : XtraForm
    {
        #region "Function"
        public void GetKy()
        {
            cbo_Ky2.DataSource = DateTime.Now.GetDateOfWeek();
            cbo_Ky2.DisplayMember = "name";
            cbo_Ky2.ValueMember = "id";
            cbo_Ky.EditValue = 1;
        }
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 14 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            grvView_ChiTietXuatKho.OptionsBehavior.ReadOnly = dt.sua != true;
            grvView_ChiTietNhapKho.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
            barSubItem1.Enabled = dt.inan == true;
            btn_excel.Enabled = dt.inan == true;
        }

        public void GetChiTietPhieuXuat()
        {
            var x = grvView_ChiTietXuatKho.FocusedRowHandle;
            var y = grvView_ChiTietXuatKho.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "GET_DATA_FROM_DATE", tungay = Convert.ToDateTime(date_tungay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(date_denngay.EditValue).ToString("yyyyMMdd") });
            grcChiTietXuatKho.DataSource = dt;
            lbl_maphieu2.DataBindings.Clear();
            lbl_maphieu2.DataBindings.Add("text", dt, "maphieu");
            grvView_ChiTietXuatKho.FocusedRowHandle = x;
            grvView_ChiTietXuatKho.TopRowIndex = y;
        }

        public void GetChiTietPhieuNhap()
        {
            var x = grvView_ChiTietNhapKho.FocusedRowHandle;
            var y = grvView_ChiTietNhapKho.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "GET_DATA_FROM_DATE", tungay = Convert.ToDateTime(date_tungay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(date_denngay.EditValue).ToString("yyyyMMdd") });
            grcChiTietNhapKho.DataSource = dt;
            lbl_maphieu.DataBindings.Clear();
            lbl_maphieu.DataBindings.Add("text", dt, "maphieu");
            grvView_ChiTietNhapKho.FocusedRowHandle = x;
            grvView_ChiTietNhapKho.TopRowIndex = y;
        }
        #endregion
        public FrmNhapXuatKho()
        {
            InitializeComponent();
            grvView_ChiTietNhapKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcChiTietNhapKho, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcChiTietNhapKho, Name);

            grvView_ChiTietNhapKho.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcChiTietNhapKho, grvView_ChiTietNhapKho); };
            grvView_ChiTietNhapKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcChiTietNhapKho, Name); };

            grvView_ChiTietXuatKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcChiTietXuatKho, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcChiTietXuatKho, Name);

            grvView_ChiTietXuatKho.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcChiTietXuatKho, grvView_ChiTietXuatKho); };
            grvView_ChiTietXuatKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcChiTietXuatKho, Name); };

            date_tungay.EditValue = DateTime.Now.Date;
            date_denngay.EditValue = DateTime.Now.Date;
        }

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._int_flag = 2;
            if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                FrmThemNhapkho frm = new FrmThemNhapkho();
                frm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPage == tabPhieuXuat)
            {
                FrmThemXuatKho frm = new FrmThemXuatKho();
                frm.ShowDialog();
            }
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tabPhieuXuat)
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa phiếu xuất kho " + lbl_maphieu2.Text + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "DELETE", maphieu = lbl_maphieu2.Text });
                    //Ghi lại log
                    Data.Data._run_history_log("Đã xóa phiếu xuất kho " + lbl_maphieu2.Text + ".", "Phiếu xuất kho");
                    GetChiTietPhieuXuat();
                }
            }
            else if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa phiếu nhập kho " + lbl_maphieu.Text + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                    //Ghi lại log
                    Data.Data._run_history_log("Đã xóa phiếu nhập kho " + lbl_maphieu.Text + ".", "Phiếu nhập kho");
                    GetChiTietPhieuNhap();
                }
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tabPhieuXuat)
            {
                GetChiTietPhieuXuat();
            }
            else if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                GetChiTietPhieuNhap();
            }
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                if (grvView_ChiTietNhapKho.SelectedRowsCount <= 0)
                {
                    XtraMessageBox.Show("Bạn phải chọn các mã phiếu để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                xtraSaveFileDialog1.FileName = "PhieuNhapKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
                if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var selectedRows = grvView_ChiTietNhapKho.GetSelectedRows();
                    var joinMaPhieu = string.Join(",", from r in selectedRows where grvView_ChiTietNhapKho.IsDataRow(Convert.ToInt32(r)) select grvView_ChiTietNhapKho.GetRowCellValue(Convert.ToInt32(r), "maphieu"));

                    var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "EXPORT_EXCEL", maphieu = joinMaPhieu });
                    ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
                }
            }
            else if (xtraTabControl1.SelectedTabPage == tabPhieuXuat)
            {
                if (grvView_ChiTietXuatKho.SelectedRowsCount <= 0)
                {
                    XtraMessageBox.Show("Bạn phải chọn các mã phiếu để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                xtraSaveFileDialog1.FileName = "PhieuXuatKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
                if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var selectedRows = grvView_ChiTietXuatKho.GetSelectedRows();
                    var joinMaPhieu = string.Join(",", from r in selectedRows where grvView_ChiTietXuatKho.IsDataRow(Convert.ToInt32(r)) select grvView_ChiTietXuatKho.GetRowCellValue(Convert.ToInt32(r), "maphieu"));

                    var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "EXPORT_EXCEL", maphieu = joinMaPhieu });
                    ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
                }
            }
        }

        private void btn_sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._edit = true;
            Data.Data._int_flag = 2;
            if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                Data.Data._strmaphieu = lbl_maphieu.Text;
                FrmThemNhapkho frm = new FrmThemNhapkho();
                frm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPage == tabPhieuXuat)
            {
                Data.Data._strmaphieu = lbl_maphieu2.Text;
                FrmThemXuatKho frm = new FrmThemXuatKho();
                frm.ShowDialog();
            }
        }

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tabPhieuXuat)
            {
                GetChiTietPhieuXuat();
            }
            else if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                GetChiTietPhieuNhap();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 14 });
            if (xtraTabControl1.SelectedTabPage == tabPhieuXuat)
            {
                btn_rpt_phieunhapkho.Enabled = false;
                btn_rpt_baocao_chitiet_nhapkho.Enabled = false;
                btn_rpt_phieuxuatkho.Enabled = dt.inan == true;
                btn_rpt_baocao_chitiet_phieuxuat.Enabled = dt.inan == true;
                GetChiTietPhieuXuat();
            }
            else if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                GetChiTietPhieuNhap();
                btn_rpt_phieuxuatkho.Enabled = false;
                btn_rpt_baocao_chitiet_phieuxuat.Enabled = false;
                btn_rpt_phieunhapkho.Enabled = dt.inan == true;
                btn_rpt_baocao_chitiet_nhapkho.Enabled = dt.inan == true;
            }
        }

        private void btn_phieuxuatkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._dtreport = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "GET_DATA_MAPHIEU", maphieu = lbl_maphieu.Text });
            if (Data.Data._dtreport.Rows.Count > 0)
            {
                Data.Data._report = 3;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_rpt_baocao_chitiet_phieuxuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_ChiTietXuatKho.GetSelectedRows() where grvView_ChiTietXuatKho.IsDataRow(f) select grvView_ChiTietXuatKho.GetDataRow(f);
            if (grvView_ChiTietXuatKho.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 6;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn các mặt hàng trong danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_rpt_phieunhapkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._dtreport = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "GET_DATA_MAPHIEU", maphieu = lbl_maphieu.Text });
            if (Data.Data._dtreport.Rows.Count > 0)
            {
                Data.Data._report = 2;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_rpt_baocao_chitiet_nhapkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_ChiTietNhapKho.GetSelectedRows() where grvView_ChiTietNhapKho.IsDataRow(f) select grvView_ChiTietNhapKho.GetDataRow(f);
            if (grvView_ChiTietNhapKho.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 7;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn các mặt hàng trong danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frm_nhapxuatkho_Load(object sender, EventArgs e)
        {
            GetKy();
            GetChiTietPhieuNhap();
            GetPhanQuyen();
        }

        private void cbo_Ky_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cbo_Ky2.GetRowByKeyValue(cbo_Ky.EditValue);
            date_tungay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            date_denngay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
        }
    }
}
