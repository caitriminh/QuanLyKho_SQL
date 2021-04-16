using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyKho.HeThong
{
    public partial class FrmHT_NhatKyHeThong : XtraForm
    {
        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 2 });
            btn_xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            btn_excel.Enabled = dt.inan == true;
        }

        public void GetNhatKyHeThong()
        {
            var x = grvViewNhatKyHeThong.FocusedRowHandle;
            var y = grvViewNhatKyHeThong.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNhatKyHoatDong", new { action = "GET_DATA", tungay = Convert.ToDateTime(txt_tungay.EditValue), denngay = Convert.ToDateTime(txt_denngay.EditValue) });
            grcNhatKyHeThong.DataSource = dt;
            grvViewNhatKyHeThong.FocusedRowHandle = x;
            grvViewNhatKyHeThong.TopRowIndex = y;
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 1;
        }
        #endregion
        public FrmHT_NhatKyHeThong()
        {
            InitializeComponent();
            txt_tungay.EditValue = DateTime.Now.Date;
            txt_denngay.EditValue = DateTime.Now.Date;

            grvViewNhatKyHeThong.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNhatKyHeThong, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcNhatKyHeThong, Name);

            grvViewNhatKyHeThong.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcNhatKyHeThong, grvViewNhatKyHeThong); };
            grvViewNhatKyHeThong.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNhatKyHeThong, Name); };
        }

        private void frm_nhatky_hethong_Load(object sender, EventArgs e)
        {
            GetKy();
            GetPhanQuyen();
            GetNhatKyHeThong();
        }

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhatKyHeThong();
        }

        private void btn_lammoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhatKyHeThong();
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel file *|.xlsx";
            if (grvViewNhatKyHeThong.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Bạn phải chọn chi tiết nội dụng để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.FileName = "NhatKy_HeThong_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var selectedRows = grvViewNhatKyHeThong.GetSelectedRows();
                var joinId = string.Join(",", from r in selectedRows where grvViewNhatKyHeThong.IsDataRow(Convert.ToInt32(r)) select grvViewNhatKyHeThong.GetRowCellValue(Convert.ToInt32(r), "id"));

                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNhatKyHoatDong", new { action = "EXPORT_EXCEL", id = joinId });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (ReferenceEquals(e.Column, col_xoa))
            {
                int i = grvViewNhatKyHeThong.FocusedRowHandle;
                DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa thông tin nhật ký hệ thống này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoNhatKyHoatDong", new { action = "DELETE", id = grvViewNhatKyHeThong.GetRowCellValue(i, "id").ToString() });
                    GetNhatKyHeThong();
                }
            }
        }

        private void btn_xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvViewNhatKyHeThong.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Bạn phải chọn chi tiết nội dụng để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var dgr = XtraMessageBox.Show("Bạn có muốn xóa các chi tiết đã chọn không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var selectedRows = grvViewNhatKyHeThong.GetSelectedRows();
                var joinId = string.Join(",", from r in selectedRows where grvViewNhatKyHeThong.IsDataRow(Convert.ToInt32(r)) select grvViewNhatKyHeThong.GetRowCellValue(Convert.ToInt32(r), "id"));

                ExecSQL.ExecProcedureNonData("prokhoNhatKyHoatDong", new { action = "DELETE", id = joinId });
                GetNhatKyHeThong();

            }
        }

        private void btnXoaTatCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Bạn có muốn xóa tất cả nhật ký hệ thống không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("prokhoNhatKyHoatDong", new { action = "DELETE_ALL" });
                GetNhatKyHeThong();
            }
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            txt_tungay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            txt_denngay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
        }
    }
}
