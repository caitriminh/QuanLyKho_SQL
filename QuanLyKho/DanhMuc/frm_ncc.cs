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
    public partial class frm_ncc : XtraForm
    {
        public frm_ncc()
        {
            InitializeComponent();
            grvView_NCC.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNCC, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcNCC, Name);

            grvView_NCC.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcNCC, grvView_NCC); };
            grvView_NCC.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNCC, Name); };
            GetPhanQuyen();
        }

        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 6 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            grvView_NCC.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
            btn_excel.Enabled = dt.inan == true;
        }
        public async void GetNhaCungCap()
        {
            int z = grvView_NCC.TopRowIndex;
            int y = grvView_NCC.FocusedRowHandle;

            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("prokhoNhaCungCap", new { action = "GET_DATA" });
            grcNCC.DataSource = dt;

            grvView_NCC.TopRowIndex = z;
            grvView_NCC.FocusedRowHandle = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "nhacungcap")
            {
                GetNhaCungCap();
            }
        }

        private void LuuNhaCungCap()
        {
            for (var index = 0; index <= grvView_NCC.RowCount - 1; index++)
            {
                var dr = grvView_NCC.GetDataRow(Convert.ToInt32(index));
                if (ReferenceEquals(dr, null))
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonData("prokhoNhaCungCap", new { action = "UPDATE", mancc = dr["mancc"].ToString(), ncc = dr["ncc"].ToString(), diachi = dr["diachi"].ToString(), sodt = dr["sodt"].ToString(), sofax = dr["sofax"].ToString(), email = dr["email"].ToString(), masothue = dr["masothue"].ToString(), ghichu = dr["ghichu"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper() });
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã cập nhật thông tin nhà cung cấp " + dr["ncc"] + ".", "Danh mục nhà cung cấp");
                }
            }
            GetNhaCungCap();
        }
        #endregion

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._int_flag = 1;
            frm_them_ncc frm = new frm_them_ncc();
            frm.ShowDialog();
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = grvView_NCC.FocusedRowHandle;
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa nhà cung cấp (" + grvView_NCC.GetRowCellValue(i, "ncc") + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNhaCungCap", new { action = "DELETE", mancc = grvView_NCC.GetRowCellValue(i, "mancc").ToString() });
                if (dt.Rows[0]["status"].ToString() == "NO")
                {
                    XtraMessageBox.Show("Nhà cung cấp " + grvView_NCC.GetRowCellValue(i, "ncc") + " đã được sử dụng.", "Xác Nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã xóa thông tin nhà cung cấp " + gridView1.GetRowCellValue(i, "ncc") + ".", "Danh mục nhà cung cấp");
                    GetNhaCungCap();
                }
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhaCungCap();
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_tendangnhap.Focus();
            DialogResult dgrResult = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi danh mục nhà cung cấp không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult == DialogResult.Yes)
            {
                LuuNhaCungCap();
            }
        }

        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_NCC.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                DialogResult dgrResult = XtraMessageBox.Show($"Bạn có muốn xóa nhà cung cấp ({grvView_NCC.GetRowCellValue(i, "ncc")}) không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult != DialogResult.Yes) { return; }
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNhaCungCap", new { action = "DELETE", mancc = grvView_NCC.GetRowCellValue(i, "mancc").ToString() });
                if (dt.Rows[0]["status"].ToString() == "NO")
                {
                    XtraMessageBox.Show("Nhà cung cấp " + grvView_NCC.GetRowCellValue(i, "ncc") + " đã được sử dụng.", "Xác Nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã xóa thông tin nhà cung cấp " + gridView1.GetRowCellValue(i, "ncc") + ".", "Danh mục nhà cung cấp");
                    GetNhaCungCap();
                }
            }
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            if (grvView_NCC.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Bạn phải chọn nhà cung cấp để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.FileName = "DanhMuc_NCC_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var selectedRows = grvView_NCC.GetSelectedRows();
                var joinNCC = string.Join(",", from r in selectedRows where grvView_NCC.IsDataRow(Convert.ToInt32(r)) select grvView_NCC.GetRowCellValue(Convert.ToInt32(r), "mancc"));

                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNhaCungCap", new { action = "EXPORT_EXCEL", mancc = joinNCC });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void Frm_ncc_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhaCungCap();
        }


    }
}
