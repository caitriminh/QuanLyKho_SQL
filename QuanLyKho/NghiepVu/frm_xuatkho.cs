using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;
using VB = Microsoft.VisualBasic.Strings;

namespace QuanLyKho.NghiepVu
{
    public partial class frm_xuatkho : XtraForm
    {
        #region "Function"
        public void GetChiTietPhieuXuat()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "GET_DATA_MAPHIEU", maphieu = lbl_maphieu.Text });
            grcChiTietPhieuXuat.DataSource = dt;
        }

        public void GetPhieuXuat()
        {
            var x = grvView_PhieuXuat.FocusedRowHandle;
            var y = grvView_PhieuXuat.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoPhieuXuat", new { action = "GET_DATA_DATE", tungay = Convert.ToDateTime(date_tungay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(date_denngay.EditValue).ToString("yyyyMMdd") });
            grcPhieuXuat.DataSource = dt;
            lbl_maphieu.DataBindings.Clear();
            lbl_maphieu.DataBindings.Add("text", dt, "maphieu");
            grvView_PhieuXuat.FocusedRowHandle = x;
            grvView_PhieuXuat.TopRowIndex = y;
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 11 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            grvView_PhieuXuat.OptionsBehavior.ReadOnly = dt.sua != true;
            grvView_ChiTietPhieuXuat.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
            btn_in.Enabled = dt.inan == true;
            btn_excel.Enabled = dt.inan == true;
        }

        public void GetKy()
        {
            cbo_Ky2.DataSource = DateTime.Now.GetDateOfWeek();
            cbo_Ky2.DisplayMember = "name";
            cbo_Ky2.ValueMember = "id";
            cbo_Ky.EditValue = 4;
        }

        private void LuuPhieuXuat()
        {
            for (var index = 0; index <= grvView_PhieuXuat.RowCount - 1; index++)
            {
                var dr = grvView_PhieuXuat.GetDataRow(Convert.ToInt32(index));
                if (dr is null)
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "UPDATE", ngayxuat = Convert.ToDateTime(dr["ngayxuat"]).ToString("yyyyMMdd"), diengiai = dr["diengiai"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), maphieu = dr["maphieu"].ToString() });
                    //Ghi lại log
                    Data.Data._run_history_log($"Đã cập nhật thông tin mã phiếu ({dr["maphieu"]}) của khách hàng ({dr["tenkh"]}).", "Phiếu Xuất");
                }
            }
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "phieuxuat")
            {
                GetPhieuXuat();
            }
        }

        #endregion
        public frm_xuatkho()
        {
            InitializeComponent();
            grvView_PhieuXuat.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhieuXuat, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcPhieuXuat, Name);

            grvView_ChiTietPhieuXuat.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcChiTietPhieuXuat, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcChiTietPhieuXuat, Name);

            grvView_PhieuXuat.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcPhieuXuat, grvView_PhieuXuat); };
            grvView_PhieuXuat.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhieuXuat, Name); };

            grvView_ChiTietPhieuXuat.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcChiTietPhieuXuat, grvView_ChiTietPhieuXuat); };
            grvView_ChiTietPhieuXuat.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcChiTietPhieuXuat, Name); };
            grvView_PhieuXuat.CustomRowCellEdit += GrvView_PhieuXuat_CustomRowCellEdit;

            date_tungay.EditValue = DateTime.Now.Date;
            date_denngay.EditValue = DateTime.Now.Date;
        }

        private void GrvView_PhieuXuat_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column == colThanhToan)
                {
                    if (grvView_PhieuXuat.GetRowCellValue(e.RowHandle, "tinhtrang").ToString() == "Đã thanh toán")
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

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._int_flag = 1;
            frm_them_xuatkho frm = new frm_them_xuatkho();
            frm.Show();
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = grvView_PhieuXuat.FocusedRowHandle;
            if (i < 0) { return; }
            var dgr = XtraMessageBox.Show($"Bạn có muốn xóa phiếu xuất kho ({lbl_maphieu.Text}) của khách hàng ({grvView_PhieuXuat.GetRowCellValue(i, "tenkh")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                if (VB.Left(lbl_maphieu.Text, 2) == "XT")
                {
                    ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                }
                //Ghi lại log
                Data.Data._run_history_log($"Đã xóa mã phiếu ({grvView_PhieuXuat.GetRowCellValue(i, "maphieu")}) của khách hàng ({grvView_PhieuXuat.GetRowCellValue(i, "tenkh")}).", "Phiếu Xuất");
                GetPhieuXuat();
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhieuXuat();
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_maphieu.Focus();
            DialogResult dgrResult = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi phiếu xuất kho không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult == DialogResult.Yes)
            {
                LuuPhieuXuat();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_PhieuXuat.FocusedRowHandle;
            if (e.Column == col_xoa)
            {
                DialogResult dgr = XtraMessageBox.Show("Bạn có muốn mã phiếu xuất kho " + lbl_maphieu.Text + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                    if (VB.Left(lbl_maphieu.Text, 2) == "XT")
                    {
                        ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                    }
                    //Ghi lại log
                    Data.Data._run_history_log($"Đã xóa mã phiếu ({grvView_PhieuXuat.GetRowCellValue(i, "maphieu")}) của khách hàng ({grvView_PhieuXuat.GetRowCellValue(i, "tenkh")}).", "Phiếu Xuất");
                    GetPhieuXuat();
                }
            }
            else if (e.Column == colThanhToan)
            {
                if (grvView_PhieuXuat.GetRowCellValue(i, "tinhtrang").ToString() == "Đã thanh toán")
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn thu hồi xác nhận thanh toán phiếu nhập ({lbl_maphieu.Text}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "THANHTOAN", maphieu = lbl_maphieu.Text });
                        //Ghi log
                        Data.Data._run_history_log($"Thu hồi thanh toán mã phiếu ({grvView_PhieuXuat.GetRowCellValue(i, "maphieu")}) của khách hàng ({grvView_PhieuXuat.GetRowCellValue(i, "tenkh")}).", "Phiếu Xuất");
                        GetPhieuXuat();
                    }
                }
                else
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn xác nhận thanh toán phiếu nhập ({lbl_maphieu.Text}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "THANHTOAN", maphieu = lbl_maphieu.Text });
                        //Ghi log
                        Data.Data._run_history_log($"Xác nhận thanh toán mã phiếu ({grvView_PhieuXuat.GetRowCellValue(i, "maphieu")}) của khách hàng ({grvView_PhieuXuat.GetRowCellValue(i, "tenkh")}).", "Phiếu Xuất");
                        GetPhieuXuat();
                    }
                }
            }
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = @"Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuXuatKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "EXPORT_EXCEL", maphieu = lbl_maphieu.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void frm_hanghoa_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
            GetPhieuXuat();
            GetPhanQuyen();
            //Ghi log
            Data.Data._run_history_log("Xem danh mục Phiếu Xuất.", "Phiếu Xuất");
        }

        private void lbl_maphieu_TextChanged(object sender, EventArgs e)
        {
            GetChiTietPhieuXuat();
        }

        private void btn_sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._strmaphieu = lbl_maphieu.Text;
            Data.Data._edit = true;
            Data.Data._int_flag = 1;
            frm_them_xuatkho frm = new frm_them_xuatkho();
            frm.Show();
        }

        private void btn_in_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhieuXuat();
        }

        private void cbo_Ky_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cbo_Ky2.GetRowByKeyValue(cbo_Ky.EditValue);
            date_tungay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            date_denngay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
        }
    }
}
