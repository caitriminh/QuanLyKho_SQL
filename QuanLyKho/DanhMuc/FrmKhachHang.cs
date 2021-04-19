using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class FrmKhachHang : XtraForm
    {
        public FrmKhachHang()
        {
            InitializeComponent();
            grvView_KhachHang.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcKhachHang, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcKhachHang, Name);

            grvView_KhachHang.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcKhachHang, grvView_KhachHang); };
            grvView_KhachHang.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcKhachHang, Name); };
            GetPhanQuyen();
        }

        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 7 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            grvView_KhachHang.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
            btn_excel.Enabled = dt.inan == true;
        }

        public async void GetKhachHang()
        {
            int z = grvView_KhachHang.TopRowIndex;
            int y = grvView_KhachHang.FocusedRowHandle;

            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("prokhoKhachHang", new { action = "GET_DATA" });
            grcKhachHang.DataSource = dt;

            grvView_KhachHang.TopRowIndex = z;
            grvView_KhachHang.FocusedRowHandle = y;
        }

        private void LuuKhachHang()
        {
            for (var index = 0; index <= grvView_KhachHang.RowCount - 1; index++)
            {
                var dr = grvView_KhachHang.GetDataRow(Convert.ToInt32(index));
                if (dr is null)
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonData("prokhoKhachHang", new { action = "UPDATE", makh = dr["makh"].ToString(), tenkh = dr["tenkh"].ToString(), diachi = dr["diachi"].ToString(), sodt = dr["sodt"].ToString(), sofax = dr["sofax"].ToString(), ghichu = dr["ghichu"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper() });
                    //Ghi lại log
                    // Data.Data._run_history_log("Đã cập nhật lại thông tin khách hàng " + dr["tenkh"] + ".", "Danh mục khách hàng");

                }
            }
            GetKhachHang();
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "khachhang")
            {
                GetKhachHang();
            }
        }
        #endregion

        private void Btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._int_flag = 1;
            FrmThemKhachHang frm = new FrmThemKhachHang();
            frm.ShowDialog();
        }

        private void Btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = grvView_KhachHang.FocusedRowHandle;
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa khách hàng " + grvView_KhachHang.GetRowCellValue(i, "tenkh") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoKhachHang", new { action = "DELETE", makh = grvView_KhachHang.GetRowCellValue(i, "makh").ToString() });
                if (dt.Rows[0]["status"].ToString() == "NO")
                {
                    XtraMessageBox.Show("Mã khách hàng " + grvView_KhachHang.GetRowCellValue(i, "tenkh") + " đã được sử dụng, bạn không thể xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã xóa thông tin khách hàng " + gridView1.GetRowCellValue(i, "tenkh") + ".", "Danh mục khách hàng");
                    GetKhachHang();
                }
            }
        }

        private void Btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetKhachHang();
        }

        private void Frm_khachhang_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKhachHang();
        }

        private void Btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_tendangnhap.Focus();
            var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                LuuKhachHang();
            }
        }

        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_KhachHang.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa khách hàng " + grvView_KhachHang.GetRowCellValue(i, "tenkh") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoKhachHang", new { action = "DELETE", makh = grvView_KhachHang.GetRowCellValue(i, "makh").ToString() });
                    if (dt.Rows[0]["status"].ToString() == "NO")
                    {
                        XtraMessageBox.Show("Mã khách hàng " + grvView_KhachHang.GetRowCellValue(i, "tenkh") + " đã được sử dụng, bạn không thể xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //Ghi lại log
                        //Data.Data._run_history_log("Đã xóa thông tin khách hàng " + gridView1.GetRowCellValue(i, "tenkh") + ".", "Danh mục khách hàng");
                        GetKhachHang();
                    }
                }
            }
        }

        private void Btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            if (grvView_KhachHang.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Bạn phải chọn khách hàng để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.FileName = "DanhMuc_KhachHang_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var selectedRows = grvView_KhachHang.GetSelectedRows();
                var joinKhachHang = string.Join(",", from r in selectedRows where grvView_KhachHang.IsDataRow(Convert.ToInt32(r)) select grvView_KhachHang.GetRowCellValue(Convert.ToInt32(r), "makh"));

                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoKhachHang", new { action = "EXPORT_EXCEL", makh = joinKhachHang });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }
    }
}
