using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyKho.HeThong
{
    public partial class frm_nguoidung : XtraForm
    {
        public frm_nguoidung()
        {
            InitializeComponent();
            gridView1.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, dgv_NguoiDung, gridView1); };
            GetPhanQuyen();
        }

        #region "Function"
        private void OnNext(MessageBroker value)
        {
            if (value.task == "nguoidung")
            {
                GetNguoiDung();
            }
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 1 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            btn_Luu.Enabled = dt.sua == true;
            col_reset_password.Visible = dt.sua == true;
            gridView1.OptionsBehavior.ReadOnly = dt.luu != true;
        }

        public async void GetNguoiDung()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("prokhoNguoiDung", new { action = "GET_DATA" });
            dgv_NguoiDung.DataSource = dt;
            lbl_tendangnhap.DataBindings.Clear();
            lbl_tendangnhap.DataBindings.Add("text", dt, "tendangnhap");
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void SaveNguoiDung()
        {
            for (var index = 0; index <= gridView1.RowCount - 1; index++)
            {
                var dr = gridView1.GetDataRow(Convert.ToInt32(index));
                if (dr is null)
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonDataAsync("prokhoNguoiDung", new { action = "UPDATE", tendangnhap = dr["tendangnhap"].ToString(), hoten = dr["hoten"].ToString(), ghichu = dr["ghichu"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper() });
                    //Ghi lại log
                    Data.Data._run_history_log("Đã cập nhật lại thông tin người dùng có tên " + dr["tendangnhap"].ToString().ToUpper() + ".", "Danh mục người dùng");
                }
            }
            GetNguoiDung();
        }
        #endregion

        private void frm_nguoidung_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNguoiDung();
        }

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_them_nguoidung frm = new frm_them_nguoidung();
            frm.ShowDialog();
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = gridView1.FocusedRowHandle;
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa tên đăng nhập " + gridView1.GetRowCellValue(i, "tendangnhap") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonDataAsync("prokhoNguoiDung", new { action = "DELETE", tendangnhap = gridView1.GetRowCellValue(i, "tendangnhap").ToString() });
                //Ghi lại log
                Data.Data._run_history_log("Đã xóa người dùng có tên " + gridView1.GetRowCellValue(i, "tendangnhap") + ".", "Danh mục người dùng");
                GetNguoiDung();
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNguoiDung();
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_tendangnhap.Focus();
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                SaveNguoiDung();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_reset_password))
            {
                DialogResult dgr = XtraMessageBox.Show("Bạn có muốn khôi phục lại mật khẩu mặc định của tài khoản " + gridView1.GetRowCellValue(i, "tendangnhap") + " không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonDataAsync("prokhoNguoiDung", new { action = "RESET_PASSWORD", tendangnhap = gridView1.GetRowCellValue(i, "tendangnhap").ToString() });
                    //Ghi lại log
                    // Data.Data._run_history_log("Đã khôi phục lại mật khẩu người dùng có tên " + gridView1.GetRowCellValue(i, "tendangnhap") + ".", "Danh mục người dùng");
                    XtraMessageBox.Show("Đã khôi phục mật khẩu thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
