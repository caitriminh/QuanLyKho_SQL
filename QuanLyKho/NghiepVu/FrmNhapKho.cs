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
    public partial class FrmNhapkho : XtraForm
    {
        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 10 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            grvView_PhieuNhap.OptionsBehavior.ReadOnly = dt.sua != true;
            grvView_ChiTietNhapKho.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
            btn_in.Enabled = dt.inan == true;
            btn_excel.Enabled = dt.inan == true;
        }

        public async void GetPhieuNhap()
        {
            var x = grvView_PhieuNhap.FocusedRowHandle;
            var y = grvView_PhieuNhap.TopRowIndex;
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoPhieuNhap", new { action = "GET_DATA_DATE", tungay = Convert.ToDateTime(date_tungay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(date_denngay.EditValue).ToString("yyyyMMdd") });
            grcPhieuNhap.DataSource = dt;
            lbl_maphieu.DataBindings.Clear();
            lbl_maphieu.DataBindings.Add("text", dt, "maphieu");
            grvView_PhieuNhap.FocusedRowHandle = x;
            grvView_PhieuNhap.TopRowIndex = y;
        }

        public void GetKy()
        {
            cbo_Ky2.DataSource = DateTime.Now.GetDateOfWeek();
            cbo_Ky2.DisplayMember = "name";
            cbo_Ky2.ValueMember = "id";
            cbo_Ky.EditValue = 4;
        }

        public void GetChiTietPhieuNhap()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "GET_DATA_MAPHIEU", maphieu = lbl_maphieu.Text });
            grcChiTietNhapKho.DataSource = dt;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "phieunhap")
            {
                GetPhieuNhap();
            }
        }

        private void LuuPhieuNhap()
        {
            for (var index = 0; index <= grvView_PhieuNhap.RowCount - 1; index++)
            {
                var dr = grvView_PhieuNhap.GetDataRow(Convert.ToInt32(index));
                if (dr is null)
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "UPDATE", ngaynhap = Convert.ToDateTime(dr["ngaynhap"]).ToString("yyyyMMdd"), maphieu = dr["maphieu"].ToString(), diengiai = dr["diengiai"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper() });
                    //Ghi lại log
                    Data.Data._run_history_log($"Đã thay đổi thông tin phiếu nhập kho ({lbl_maphieu.Text}) của nhà cung cấp ({dr["ncc"]}).", "Phiếu Nhập");
                }
            }
        }

        #endregion

        public FrmNhapkho()
        {
            InitializeComponent();

            grvView_PhieuNhap.CustomDrawRowIndicator += (ss, ee) => { GridViewHelper.GridView_CustomDrawRowIndicator(ss, ee, grcPhieuNhap, grvView_PhieuNhap); };
            grvView_PhieuNhap.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcPhieuNhap, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcPhieuNhap, Name);

            grvView_ChiTietNhapKho.CustomDrawRowIndicator += (ss, ee) => { GridViewHelper.GridView_CustomDrawRowIndicator(ss, ee, grcChiTietNhapKho, grvView_ChiTietNhapKho); };
            grvView_ChiTietNhapKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcChiTietNhapKho, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcChiTietNhapKho, Name);

            grvView_PhieuNhap.CustomRowCellEdit += GrvView_PhieuNhap_CustomRowCellEdit;

            date_tungay.EditValue = DateTime.Now.Date;
            date_denngay.EditValue = DateTime.Now.Date;
            GetPhanQuyen();
        }

        private void GrvView_PhieuNhap_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column == colThanhToan)
                {
                    if (grvView_PhieuNhap.GetRowCellValue(e.RowHandle, "tinhtrang").ToString() == "Đã thanh toán")
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

        private void Btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._int_flag = 1;
            FrmThemNhapkho frm = new FrmThemNhapkho();
            frm.Show();
        }

        private void Btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Bạn có muốn xóa phiếu nhập kho " + lbl_maphieu.Text + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                if (VB.Left(lbl_maphieu.Text, 2) == "XT")
                {
                    ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                }
                //Ghi lại log
                // Data.Data._run_history_log("Đã xóa phiếu nhập kho " + lbl_maphieu.Text + ".", "Phiếu nhập kho");
                GetPhieuNhap();
            }
        }

        private void Btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhieuNhap();
        }

        private void Btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_maphieu.Focus();
            DialogResult dgrResult = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi phiếu nhập kho không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult == DialogResult.Yes)
            {
                LuuPhieuNhap();
            }
        }

        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_PhieuNhap.FocusedRowHandle;
            if (e.Column == col_xoa)
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn mã phiếu nhập kho " + lbl_maphieu.Text + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                    if (VB.Left(lbl_maphieu.Text, 2) == "XT")
                    {
                        ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "DELETE", maphieu = lbl_maphieu.Text });
                    }
                    //Ghi lại log
                    Data.Data._run_history_log($"Đã xóa phiếu nhập kho ({lbl_maphieu.Text}) của nhà cung cấp ({grvView_PhieuNhap.GetRowCellValue(i, "ncc")}).", "Phiếu Nhập");
                    GetPhieuNhap();
                }
            }
            else if (e.Column == colThanhToan)
            {
                if (grvView_PhieuNhap.GetRowCellValue(i, "tinhtrang").ToString() == "Đã thanh toán")
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn thu hồi xác nhận thanh toán phiếu nhập ({lbl_maphieu.Text}) của nhà cung cấp ({grvView_PhieuNhap.GetRowCellValue(i, "ncc")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "THANHTOAN", maphieu = lbl_maphieu.Text });
                        //Ghi log
                        Data.Data._run_history_log($"Thu hồi thanh toán phiếu nhập ({grvView_PhieuNhap.GetRowCellValue(i, "maphieu")}) của nhà cung cấp ({grvView_PhieuNhap.GetRowCellValue(i, "ncc")}).", "Phiếu Nhập");
                        GetPhieuNhap();
                    }
                }
                else
                {
                    var dgr = XtraMessageBox.Show($"Bạn có muốn xác nhận thanh toán phiếu nhập ({lbl_maphieu.Text}) của nhà cung cấp ({grvView_PhieuNhap.GetRowCellValue(i, "ncc")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr == DialogResult.Yes)
                    {
                        ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "THANHTOAN", maphieu = lbl_maphieu.Text });
                        //Ghi log
                        Data.Data._run_history_log($"Xác nhận thanh toán phiếu nhập ({grvView_PhieuNhap.GetRowCellValue(i, "maphieu")}) của nhà cung cấp ({grvView_PhieuNhap.GetRowCellValue(i, "ncc")}).", "Phiếu Nhập");
                        GetPhieuNhap();
                    }
                }
            }
        }

        private void Btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuNhapKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "EXPORT_EXCEL", maphieu = lbl_maphieu.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void Frm_hanghoa_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
            GetPhieuNhap();
            //Ghi log
            Data.Data._run_history_log("Xem danh mục Phiếu Nhập.", "Phiếu Nhập");

            FrmThemNhapkho frm = new FrmThemNhapkho();
            frm.Show();
        }

        private void Lbl_maphieu_TextChanged(object sender, EventArgs e)
        {
            GetChiTietPhieuNhap();
        }

        private void Btn_sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (VB.Left(lbl_maphieu.Text, 2) == "XT")
            {
                XtraMessageBox.Show("Đây là phiếu xuất thẳng bạn không thể chỉnh sửa ở mục này.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Data.Data._int_flag = 1;
            Data.Data._strmaphieu = lbl_maphieu.Text;
            Data.Data._edit = true;
            FrmThemNhapkho frm = new FrmThemNhapkho();
            frm.ShowDialog();
        }

        private void Btn_in_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void Btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhieuNhap();
        }

        private void Cbo_Ky_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cbo_Ky2.GetRowByKeyValue(cbo_Ky.EditValue);
            date_tungay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            date_denngay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
        }


    }
}
