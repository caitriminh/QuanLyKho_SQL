using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
namespace QuanLyKho.NghiepVu
{
    public partial class FrmPhaiTra : XtraForm
    {
        private int intOption = 1;
        #region "Function"

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 4;
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 19 });
            btn_excel.Enabled = dt.inan == true;
            btn_in.Enabled = dt.inan == true;
        }

        public void GetPhaiThu()
        {
            var x = grvViewPhaiTra.FocusedRowHandle;
            var y = grvViewPhaiTra.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoPhaiTra", new { action = "GET_DATA" });
            grcPhaiTra.DataSource = dt;
            lbl_maphieu.DataBindings.Clear();
            lbl_maphieu.DataBindings.Add("text", dt, "maphieu");
            grvViewPhaiTra.FocusedRowHandle = x;
            grvViewPhaiTra.TopRowIndex = y;

        }

        public void GetPhaiThuFromDate()
        {
            var x = grvViewPhaiTra.FocusedRowHandle;
            var y = grvViewPhaiTra.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoPhaiTra", new { action = "GET_DATA", tungay = Convert.ToDateTime(date_tungay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(date_denngay.EditValue).ToString("yyyyMMdd") });
            grcPhaiTra.DataSource = dt;
            lbl_maphieu.DataBindings.Clear();
            lbl_maphieu.DataBindings.Add("text", dt, "maphieu");
            grvViewPhaiTra.FocusedRowHandle = x;
            grvViewPhaiTra.TopRowIndex = y;
        }

        public void GetChiTietPhieuNhap()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "GET_DATA_MAPHIEU", maphieu = lbl_maphieu.Text });
            grcChiTietNhapKho.DataSource = dt;
        }

        #endregion
        public FrmPhaiTra()
        {
            InitializeComponent();
            grvViewPhaiTra.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhaiTra, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcPhaiTra, Name);

            grvViewPhaiTra.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcPhaiTra, grvViewPhaiTra); };
            grvViewPhaiTra.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhaiTra, Name); };
            grvViewPhaiTra.RowCellClick += GrvViewPhaiThu_RowCellClick;
            grvViewPhaiTra.CustomRowCellEdit += GrvViewPhaiThu_CustomRowCellEdit;
        }

        private void GrvViewPhaiThu_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column == colThanhToan)
                {
                    if (Convert.ToBoolean(grvViewPhaiTra.GetRowCellValue(e.RowHandle, "thanhtoan")) == true)
                    {
                        e.RepositoryItem = _btn_thuhoi;
                    }
                    else
                    {
                        e.RepositoryItem = _btn_xacnhan;
                    }
                }
            }
        }

        private void GrvViewPhaiThu_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = grvViewPhaiTra.FocusedRowHandle;
            if (e.Column == colThanhToan)
            {
                if (Convert.ToBoolean(grvViewPhaiTra.GetRowCellValue(e.RowHandle, "thanhtoan")) == true)
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn thu hồi xác nhận thanh toán phiếu nhập ({grvViewPhaiTra.GetRowCellValue(i, "maphieu")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("prokhoPhieuNhap", new { action = "THANHTOAN", maphieu = grvViewPhaiTra.GetRowCellValue(i, "maphieu") });
                        //Ghi log
                        Data.Data._run_history_log($"Đã xác nhận thanh toán phiếu nhập ({grvViewPhaiTra.GetRowCellValue(i, "maphieu")}) của nhà cung cấp ({grvViewPhaiTra.GetRowCellValue(i, "ncc")}).", "Phải Trả");
                        GetPhaiThu();
                    }
                }
                else
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn xác nhận thanh toán phiếu nhập ({grvViewPhaiTra.GetRowCellValue(i, "maphieu")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("prokhoPhieuNhap", new { action = "THANHTOAN", maphieu = grvViewPhaiTra.GetRowCellValue(i, "maphieu") });
                        //Ghi log
                        Data.Data._run_history_log($"Đã thu hồi thanh toán phiếu nhập ({grvViewPhaiTra.GetRowCellValue(i, "maphieu")}) của nhà cung cấp ({grvViewPhaiTra.GetRowCellValue(i, "ncc")}).", "Phải Trả");
                        GetPhaiThu();
                    }
                }
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intOption == 1)
            {
                GetPhaiThu();
            }
            else
            {
                GetPhaiThuFromDate();
            }
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhaiThu_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                grcPhaiTra.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void btn_in_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvViewPhaiTra.GetSelectedRows() where grvViewPhaiTra.IsDataRow(f) select grvViewPhaiTra.GetDataRow(f);
            if (grvViewPhaiTra.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 13;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            intOption = 2;
            GetPhaiThuFromDate();

        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            date_tungay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            date_denngay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
        }


        private void FrmPhaiThu_Load(object sender, EventArgs e)
        {
            GetKy();
            GetPhanQuyen();
            GetPhaiThu();
            //Ghi log
            Data.Data._run_history_log("Xem danh mục Phải Trả.", "Phải Trả");
        }

        private void lbl_maphieu_TextChanged(object sender, EventArgs e)
        {
            GetChiTietPhieuNhap();
        }
    }
}
