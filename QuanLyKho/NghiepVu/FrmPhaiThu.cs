using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using SimpleBroker;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
namespace QuanLyKho.NghiepVu
{
    public partial class FrmPhaiThu : XtraForm
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
            var x = grvViewPhaiThu.FocusedRowHandle;
            var y = grvViewPhaiThu.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoPhaiThu", new { action = "GET_DATA" });
            grcPhaiThu.DataSource = dt;
            lbl_maphieu.DataBindings.Clear();
            lbl_maphieu.DataBindings.Add("text", dt, "maphieu");
            grvViewPhaiThu.FocusedRowHandle = x;
            grvViewPhaiThu.TopRowIndex = y;
        }

        public void GetPhaiThuFromDate()
        {
            var x = grvViewPhaiThu.FocusedRowHandle;
            var y = grvViewPhaiThu.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoPhaiThu", new { action = "GET_DATA", tungay = Convert.ToDateTime(date_tungay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(date_denngay.EditValue).ToString("yyyyMMdd") });
            grcPhaiThu.DataSource = dt;
            lbl_maphieu.DataBindings.Clear();
            lbl_maphieu.DataBindings.Add("text", dt, "maphieu");
            grvViewPhaiThu.FocusedRowHandle = x;
            grvViewPhaiThu.TopRowIndex = y;
        }

        public void GetChiTietPhieuXuat()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "GET_DATA_MAPHIEU", maphieu = lbl_maphieu.Text });
            grcChiTietPhieuXuat.DataSource = dt;
        }
        #endregion
        public FrmPhaiThu()
        {
            InitializeComponent();
            grvViewPhaiThu.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhaiThu, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcPhaiThu, Name);

            grvViewPhaiThu.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcPhaiThu, grvViewPhaiThu); };
            grvViewPhaiThu.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhaiThu, Name); };
            grvViewPhaiThu.RowCellClick += GrvViewPhaiThu_RowCellClick;
            grvViewPhaiThu.CustomRowCellEdit += GrvViewPhaiThu_CustomRowCellEdit;
        }

        private void GrvViewPhaiThu_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column == colThanhToan)
                {
                    if (Convert.ToBoolean(grvViewPhaiThu.GetRowCellValue(e.RowHandle, "thanhtoan")) == true)
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
            var i = grvViewPhaiThu.FocusedRowHandle;
            if (e.Column == colThanhToan)
            {
                if (Convert.ToBoolean(grvViewPhaiThu.GetRowCellValue(e.RowHandle, "thanhtoan")) == true)
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn thu hồi xác nhận thanh toán phiếu xuất ({grvViewPhaiThu.GetRowCellValue(i, "maphieu")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "THANHTOAN", maphieu = grvViewPhaiThu.GetRowCellValue(i, "maphieu") });
                        //Ghi log
                        Data.Data._run_history_log($"Đã xác nhận thanh toán phiếu xuất ({grvViewPhaiThu.GetRowCellValue(i, "maphieu")}) của khách hàng ({grvViewPhaiThu.GetRowCellValue(i, "tenkh")}).", "Phải Thu");
                        GetPhaiThu();
                    }
                }
                else
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn xác nhận thanh toán phiếu xuất ({grvViewPhaiThu.GetRowCellValue(i, "maphieu")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "THANHTOAN", maphieu = grvViewPhaiThu.GetRowCellValue(i, "maphieu") });
                        //Ghi log
                        Data.Data._run_history_log($"Đã thu hồi thanh toán phiếu xuất ({grvViewPhaiThu.GetRowCellValue(i, "maphieu")}) của khách hàng ({grvViewPhaiThu.GetRowCellValue(i, "tenkh")}).", "Phải Thu");
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
                grcPhaiThu.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void btn_in_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvViewPhaiThu.GetSelectedRows() where grvViewPhaiThu.IsDataRow(f) select grvViewPhaiThu.GetDataRow(f);
            if (grvViewPhaiThu.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 12;
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
            Data.Data._run_history_log("Xem danh mục Phải Thu.", "Phải Thu");
        }

        private void lbl_maphieu_TextChanged(object sender, EventArgs e)
        {
            GetChiTietPhieuXuat();
        }
    }
}
