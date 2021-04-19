using DevExpress.XtraEditors;
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
    public partial class FrmTonkho : XtraForm
    {
        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 12 });
            barSubItem1.Enabled = dt.luu == true;
            col_soducuoiky.Visible = dt.xoa == true;
            grvView_TonKho.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_chuyen_sodu_dauky.Enabled = dt.sua == true;
            barSubItem1.Enabled = dt.inan == true;
            barSubItem3.Enabled = dt.inan == true;
        }


        public void GetSoDuDauKy()
        {
            var x = grvView_SoDuDauKy.FocusedRowHandle;
            var y = grvView_SoDuDauKy.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoSoDuDauKy", new { action = "GET_DATA_DATE", ngaythang = Convert.ToDateTime(date_thangnam.EditValue).ToString("yyyyMMdd") });
            grcSoDuDauKy.DataSource = dt;
            grvView_SoDuDauKy.FocusedRowHandle = x;
            grvView_SoDuDauKy.TopRowIndex = y;
        }

        public void GetTonKho()
        {
            ExecSQL.ExecProcedureNonData("prokhoTonKho", new { action = "SAVE", ngaythang = Convert.ToDateTime(date_thangnam.EditValue).ToString("yyyyMM01") });
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoTonKho", new { action = "GET_DATA", option = 1, ngaythang = Convert.ToDateTime(date_thangnam.EditValue).ToString("yyyyMM01") });
            grcTonKho.DataSource = dt;
            lbl_mahanghoa.DataBindings.Clear();
            lbl_mahanghoa.DataBindings.Add("text", dt, "mahanghoa");
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "tonkho")
            {
                if (xtraTabControl2.SelectedTabPage == tabTonKho)
                {
                    GetTonKho();
                }
                else
                {
                    GetSoDuDauKy();
                }
            }
        }

        private void LuuSoDuDauKy()
        {
            for (var index = 0; index <= grvView_SoDuDauKy.RowCount - 1; index++)
            {
                var dr = grvView_SoDuDauKy.GetDataRow(Convert.ToInt32(index));
                if (ReferenceEquals(dr, null))
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonData("prokhoSoDuDauKy", new { action = "UPDATE", slton = Convert.ToDecimal(dr["slton"]), tiendau = Convert.ToDecimal(dr["tiendau"]), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), id = Convert.ToInt32(dr["id"]) });
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã cập nhật lại số dư đầu kỳ của mà hàng " + dr["mahanghoa"] + ".", "Danh mục tồn kho");
                }
            }
        }
        #endregion

        public FrmTonkho()
        {
            InitializeComponent();
            grvView_TonKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcTonKho, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcTonKho, Name);

            grvView_SoDuDauKy.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcSoDuDauKy, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcSoDuDauKy, Name);

            grvView_TonKho.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcTonKho, grvView_TonKho); };
            grvView_TonKho.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcTonKho, Name); };

            grvView_SoDuDauKy.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcSoDuDauKy, grvView_SoDuDauKy); };
            grvView_SoDuDauKy.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcSoDuDauKy, Name); };

            date_thangnam.EditValue = DateTime.Now.Date;
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl2.SelectedTabPage == tabTonKho)
            {
                GetTonKho();
            }
            else if (xtraTabControl2.SelectedTabPage == tabSoDuDauKy)
            {
                GetSoDuDauKy();
            }
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = @"Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BaoCaoTonKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                grvView_TonKho.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl2.SelectedTabPage == tabTonKho)
            {
                GetTonKho();
            }
            else if (xtraTabControl2.SelectedTabPage == tabSoDuDauKy)
            {
                GetSoDuDauKy();
            }
        }

        private void btn_xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl2.SelectedTabPage == tabTonKho)
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa toàn bộ số dư của danh mục hàng hóa này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoTonKho", new { action = "DELETE", ngaythang = Convert.ToDateTime(date_thangnam.EditValue).ToString("yyyyMM01") });
                    //Ghi lại log
                    Data.Data._run_history_log("Đã xóa danh mục tồn kho tháng (" + date_thangnam.EditValue + ").", "Tồn Kho");
                    GetTonKho();
                }
            }
            else if (xtraTabControl2.SelectedTabPage == tabSoDuDauKy)
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa toàn bộ số dư đầu kỳ của danh mục hàng hóa này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoSoDuDauKy", new { action = "DELETE_DATE", ngaythang = Convert.ToDateTime(date_thangnam.EditValue).ToString("yyyyMM01") });
                    //Ghi lại log
                    Data.Data._run_history_log("Đã xóa danh mục số dư đầu kỳ tháng (" + date_thangnam.EditValue + ").", "Tồn Kho");
                    GetSoDuDauKy();
                }
            }
        }

        private void frm_tonkho_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetTonKho();
            GetPhanQuyen();
            //Ghi log
            Data.Data._run_history_log("Xem danh mục tồn kho.", "Tồn Kho");
        }

        private void btn_capnhat_sodu_dauky_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_capnhat_sodu_dauky frm = new frm_capnhat_sodu_dauky();
            frm.ShowDialog();
        }

        private void btn_rpt_baocao_tongkho_soluong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_TonKho.GetSelectedRows() where grvView_TonKho.IsDataRow(f) select grvView_TonKho.GetDataRow(f);
            if (grvView_TonKho.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 8;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn các mặt hàng trong danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_rpt_baocao_tonkho_giatri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_TonKho.GetSelectedRows() where grvView_TonKho.IsDataRow(f) select grvView_TonKho.GetDataRow(f);
            if (grvView_TonKho.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 5;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn các mặt hàng trong danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_capnhat_sodudauky_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_them_sodudauky frm = new frm_them_sodudauky();
            frm.ShowDialog();
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 12 });
            if (xtraTabControl2.SelectedTabPage == tabTonKho)
            {
                btnThem.Enabled = false;
                btn_excel.Enabled = false;
                btnExcelSoDuDauKy.Enabled = false;
                btnLuu.Enabled = false;
                btn_xoa.Enabled = dt.xoa == true;
                btn_chuyen_sodu_dauky.Enabled = dt.sua == true;
                barSubItem1.Enabled = dt.inan == true;
            }
            else if (xtraTabControl2.SelectedTabPage == tabSoDuDauKy)
            {
                GetSoDuDauKy();
                btn_chuyen_sodu_dauky.Enabled = false;
                barSubItem1.Enabled = false;
                btn_excel.Enabled = false;
                btn_xoa.Enabled = dt.xoa == true;
                btnExcelSoDuDauKy.Enabled = dt.inan == true;
                btnLuu.Enabled = dt.sua == true;
                btnThem.Enabled = dt.luu == true;
            }
        }

        private void gridView6_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_SoDuDauKy.FocusedRowHandle;
            if (ReferenceEquals(e.Column, colXoa))
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa số dư đầu kỳ của mặt hàng " + grvView_SoDuDauKy.GetRowCellValue(i, "tenhanghoa") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoSoDuDauKy", new { action = "DELETE", id = Convert.ToInt32(grvView_SoDuDauKy.GetRowCellValue(i, "id")) });
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã xóa số dư đầu kỳ của mã hàng " + gridView6.GetRowCellValue(i, "mahanghoa") + " trong tháng " + gridView6.GetRowCellValue(i, "ngaythang") + ".", "Danh mục tồn kho");
                    GetSoDuDauKy();
                }
            }
        }

        private void btnExcelSoDuDauKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "SuDuDauKy_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                grvView_SoDuDauKy.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_mahanghoa.Focus();
            var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                LuuSoDuDauKy();
            }
        }

    }
}
