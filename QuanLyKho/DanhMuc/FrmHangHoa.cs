using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using SimpleBroker;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class FrmHangHoa : XtraForm
    {
        string strMaHangHoa = "";
        public FrmHangHoa()
        {
            InitializeComponent();
            grvView_HangHoa.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcHangHoa, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcHangHoa, Name);

            grvView_BaoGia.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcBaoGia, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcBaoGia, Name);

            grvView_HangHoa.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcHangHoa, grvView_HangHoa); };
            grvView_HangHoa.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcHangHoa, Name); };

            grvView_BaoGia.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcBaoGia, grvView_BaoGia); };
            grvView_BaoGia.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcBaoGia, Name); };

            grvView_HangHoa.FocusedRowChanged += GrvView_HangHoa_FocusedRowChanged;
            grvView_HangHoa.RowCellClick += GrvView_HangHoa_RowCellClick;
            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
            GetPhanQuyen();
        }

        private void GrvView_HangHoa_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_HangHoa.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                DialogResult dgr = XtraMessageBox.Show($@"Bạn có muốn xóa hàng hóa { grvView_HangHoa.GetRowCellValue(i, "tenhanghoa") } này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoHangHoa", new { action = "DELETE", mahanghoa = strMaHangHoa });
                    if (dt.Rows[0]["status"].ToString() == "NO")
                    {
                        XtraMessageBox.Show("Mã hàng hóa này đã được sử dụng. Bạn không thể xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //Ghi lại log
                        Data.Data._run_history_log($"Đã xóa hàng hóa ({grvView_HangHoa.GetRowCellValue(i, "mathamchieu")} - {grvView_HangHoa.GetRowCellValue(i, "tenhanghoa")})", "Danh Mục Hàng Hóa");
                        GetHangHoa();
                    }
                }
            }
            else if (ReferenceEquals(e.Column, col_copy))
            {
                ExecSQL.ExecProcedureNonDataAsync("proKhoHangHoa", new { action = "COPY", mahanghoa = strMaHangHoa, nguoitd = Data.Data._strtendangnhap.ToUpper() });
                //Ghi lại log
                Data.Data._run_history_log($"Đã sao chép thông tin hàng hóa ({grvView_HangHoa.GetRowCellValue(i, "mathamchieu")} - {grvView_HangHoa.GetRowCellValue(i, "tenhanghoa")})", "Danh Mục Hàng Hóa");
                GetHangHoa();
            }
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tabDanhMucHangHoa)
            {
                GetPhanQuyen();
                GetHangHoa();
                btnDanhSachHangHoa.Enabled = true;
                btnBangBaoGia.Enabled = false;
                var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 8 });
                btn_Luu.Enabled = dt.sua == true;
                btn_Xoa.Enabled = dt.xoa == true;
                btn_Them.Enabled = dt.luu == true;
                btn_excel.Enabled = dt.inan == true;
            }
            else
            {
                GetBaoGia();
                btn_Luu.Enabled = false;
                btn_Xoa.Enabled = false;
                btn_Them.Enabled = false;
                btn_excel.Enabled = false;
                btnDanhSachHangHoa.Enabled = false;
                btnBangBaoGia.Enabled = true;
            }
        }

        private void GrvView_HangHoa_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = grvView_HangHoa.FocusedRowHandle;
            if (grvView_HangHoa.GetRowCellValue(i, "mahanghoa") == null) { return; }
            strMaHangHoa = grvView_HangHoa.GetRowCellValue(i, "mahanghoa").ToString();
        }

        #region "Function"
        public async void GetHangHoa()
        {
            int z = grvView_HangHoa.TopRowIndex;
            int y = grvView_HangHoa.FocusedRowHandle;

            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoHangHoa", new { action = "GET_DATA" });
            grcHangHoa.DataSource = dt;

            grvView_HangHoa.TopRowIndex = z;
            grvView_HangHoa.FocusedRowHandle = y;
        }

        public async void GetBaoGia()
        {
            int z = grvView_BaoGia.TopRowIndex;
            int y = grvView_BaoGia.FocusedRowHandle;

            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoHangHoa", new { action = "GET_DATA_BAOGIA", baogia = true });
            grcBaoGia.DataSource = dt;

            grvView_BaoGia.TopRowIndex = z;
            grvView_BaoGia.FocusedRowHandle = y;
        }

        private void LuuHangHoa()
        {
            for (var index = 0; index <= grvView_HangHoa.RowCount - 1; index++)
            {
                var dr = grvView_HangHoa.GetDataRow(Convert.ToInt32(index));
                if (dr is null)
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonDataAsync("proKhoHangHoa", new { action = "UPDATE", mahanghoa = dr["mahanghoa"].ToString(), mathamchieu = dr["mathamchieu"].ToString(), manhom = dr["manhom"].ToString(), tenhanghoa = dr["tenhanghoa"].ToString(), madvt = Convert.ToInt32(dr["madvt"]), ghichu = dr["ghichu"].ToString(), sudung = Convert.ToBoolean(dr["sudung"]), baogia = Convert.ToBoolean(dr["baogia"]), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), dongia = Convert.ToDecimal(dr["dongia"]) });
                    ////Ghi lại log
                    Data.Data._run_history_log($"Đã thay đổi thông tin hàng hóa ({dr["mathamchieu"]} - {dr["tenhanghoa"]})", "Danh Mục Hàng Hóa");
                }
            }
            GetHangHoa();
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 8 });
            btn_Them.Enabled = dt.luu == true;
            col_copy.Visible = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            grvView_HangHoa.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
            btn_excel.Enabled = dt.inan == true;
            btnInOption.Enabled = dt.inan == true;
        }

        public async void GetDonViTinh()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoDonViTinh", new { action = "GET_DATA" });
            cbo_dvt.DataSource = dt;
            cbo_dvt.DisplayMember = "tendvt";
            cbo_dvt.ValueMember = "madvt";
        }

        public async void GetNhomHang()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoNhomHang", new { action = "GET_DATA" });
            cbo_nhomhang.DataSource = dt;
            cbo_nhomhang.DisplayMember = "nhomhang";
            cbo_nhomhang.ValueMember = "manhom";
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "hanghoa")
            {
                GetData();
            }
        }

        public void GetData()
        {
            if (xtraTabControl1.SelectedTabPage == tabDanhMucHangHoa)
            {
                GetHangHoa();
            }
            else
            {
                GetBaoGia();
            }
        }
        #endregion

        private void Btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_them_hanghoa frm = new frm_them_hanghoa();
            frm.ShowDialog();
        }

        private void Btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = grvView_HangHoa.FocusedRowHandle;
            if (i < 0) { return; }
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa hàng hóa đã chọn này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoHangHoa", new { action = "DELETE", mahanghoa = strMaHangHoa });
                if (dt.Rows[0]["status"].ToString() == "NO")
                {
                    XtraMessageBox.Show("Mã hàng hóa này đã được sử dụng. Bạn không thể xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //Ghi lại log
                    Data.Data._run_history_log($"Đã xóa hàng hóa ({grvView_HangHoa.GetRowCellValue(i, "mathamchieu")} - {grvView_HangHoa.GetRowCellValue(i, "tenhanghoa")})", "Danh Mục Hàng Hóa");
                    GetHangHoa();
                }
            }
        }

        private void Btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }

        private void Btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            labelControl1.Focus();
            DialogResult dgrResult = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi danh mục hàng hóa không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult == DialogResult.Yes)
            {
                LuuHangHoa();
            }
        }

        private void Btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            if (grvView_HangHoa.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Bạn phải chọn danh mục hàng hóa để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.FileName = "DanhMuc_HangHoa_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var selectedRows = grvView_HangHoa.GetSelectedRows();
                var joinHangHoa = string.Join(",", from r in selectedRows where grvView_HangHoa.IsDataRow(Convert.ToInt32(r)) select grvView_HangHoa.GetRowCellValue(Convert.ToInt32(r), "mahanghoa"));

                var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoHangHoa", new { action = "EXPORT_EXCEL", mahanghoa = joinHangHoa });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void Frm_hanghoa_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetDonViTinh();
            GetNhomHang();
            GetHangHoa();
            //Ghi log
            Data.Data._run_history_log("Xem danh mục Hàng Hóa.", "Danh Mục Hàng Hóa");
        }

        private void Btn_in_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_HangHoa.GetSelectedRows() where grvView_HangHoa.IsDataRow(f) select grvView_HangHoa.GetDataRow(f);
            if (grvView_HangHoa.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 1;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Vui lòng chọn các mã hàng để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBangBaoGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_BaoGia.GetSelectedRows() where grvView_BaoGia.IsDataRow(f) select grvView_BaoGia.GetDataRow(f);
            if (grvView_BaoGia.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 11;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Vui lòng chọn các mã hàng để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
