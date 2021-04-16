using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyKho.NghiepVu
{
    public partial class frm_dinhmuc_linhkien : XtraForm
    {
        public frm_dinhmuc_linhkien()
        {
            InitializeComponent();
            grvView_DinhMuc.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcDinhMuc, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcDinhMuc, Name);

            grvView_DinhMuc.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcDinhMuc, grvView_DinhMuc); };
            grvView_DinhMuc.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcDinhMuc, Name); };
            GetPhanQuyen();
        }

        #region "Function"
        public async void GetChiTietQuyDoi()
        {
            int z = grvView_DinhMuc.TopRowIndex;
            int y = grvView_DinhMuc.FocusedRowHandle;
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("prokhoQuyDoi", new { action = "GET_DATA" });
            grcDinhMuc.DataSource = dt;
            lbl_mahang.DataBindings.Clear();
            lbl_mahang.DataBindings.Add("text", dt, "mahanghoa");

            grvView_DinhMuc.TopRowIndex = z;
            grvView_DinhMuc.FocusedRowHandle = y;
        }

        private void LuuChiTietQuyDoi()
        {
            for (var index = 0; index <= grvView_DinhMuc.RowCount - 1; index++)
            {
                var dr = grvView_DinhMuc.GetDataRow(Convert.ToInt32(index));
                if (dr is null)
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonData("prokhoQuyDoi", new { action = "UPDATE", id = Convert.ToInt32(dr["id"]), heso = Convert.ToDecimal(dr["heso"]), thamsoquydoi = Convert.ToDecimal(dr["thamsoquydoi"]), ghichu = dr["ghichu"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper() });
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã cập nhật định mức hàng hóa " + dr["tenhanghoa"] + ".", "Định mức hàng hóa");
                }
            }
            GetHangHoa();
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 9 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            grvView_DinhMuc.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
        }

        public void GetHangHoa()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoHangHoa", new { action = "GET_DATA" });
            cboHangHoa.DataSource = dt;
            cboHangHoa.DisplayMember = "tenhanghoa";
            cboHangHoa.ValueMember = "mahanghoa";
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "quydoi")
            {
                GetChiTietQuyDoi();
            }
        }
        #endregion


        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._str_mahang = lbl_mahang.Text;
            frm_them_dinhmuc_linhkien frm = new frm_them_dinhmuc_linhkien();
            frm.ShowDialog();
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = grvView_DinhMuc.FocusedRowHandle;
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa quy đổi hàng hóa đã chọn này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("prokhoQuyDoi", new { action = "DELETE", id = Convert.ToInt32(grvView_DinhMuc.GetRowCellValue(i, "id")) });
                //Ghi lại log
                // Data.Data._run_history_log("Đã xóa định mức hàng hóa đã chọn.", "Định mức hàng hóa");
                GetChiTietQuyDoi();
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetChiTietQuyDoi();
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_mahang.Focus();
            DialogResult dgrResult = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi quy đổi hàng hóa không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult == DialogResult.Yes)
            {
                LuuChiTietQuyDoi();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_DinhMuc.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa quy đổi hàng hóa " + grvView_DinhMuc.GetRowCellValue(i, "tenhanghoa") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoQuyDoi", new { action = "DELETE", id = Convert.ToInt32(grvView_DinhMuc.GetRowCellValue(i, "id")) });
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã xóa quy đổi hàng hóa " + gridView1.GetRowCellValue(i, "tenhanghoa") + ".", "Quy đổi hàng hóa");
                    GetHangHoa();
                }
            }
        }

        private void Frm_dinhmuc_linhkien_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetHangHoa();
            GetChiTietQuyDoi();
        }
    }
}


