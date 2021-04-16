using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class frm_loaichiphi : XtraForm
    {
        #region "Function"
        public void GetLoaiChiPhi()
        {
            var x = grvView_LoaiChiPhi.FocusedRowHandle;
            var y = grvView_LoaiChiPhi.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoLoaiChiPhi", new { action = "GET_DATA" });
            grcLoaiChiPhi.DataSource = dt;
            grvView_LoaiChiPhi.FocusedRowHandle = x;
            grvView_LoaiChiPhi.TopRowIndex = y;
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 16 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            grvView_LoaiChiPhi.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
        }

        private void LuuLoaiChiPhi()
        {
            for (var index = 0; index <= grvView_LoaiChiPhi.RowCount - 1; index++)
            {
                var dr = grvView_LoaiChiPhi.GetDataRow(Convert.ToInt32(index));
                if (ReferenceEquals(dr, null))
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã cập nhật lại thông tin loại chi phí " + dr["loaichiphi"] + ".", "Danh mục loại chi phí");
                    ExecSQL.ExecProcedureNonData("prokhoLoaiChiPhi", new { action = "UPDATE", loaichiphi = dr["loaichiphi"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), maloai = Convert.ToInt32(dr["maloai"]) });
                }
            }
            GetLoaiChiPhi();
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "loaichiphi")
            {
                GetLoaiChiPhi();
            }
        }
        #endregion

        public frm_loaichiphi()
        {
            InitializeComponent();
            grvView_LoaiChiPhi.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcLoaiChiPhi, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcLoaiChiPhi, Name);

            grvView_LoaiChiPhi.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcLoaiChiPhi, grvView_LoaiChiPhi); };
            grvView_LoaiChiPhi.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcLoaiChiPhi, Name); };
        }

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_them_loaichiphi frm = new frm_them_loaichiphi();
            frm.ShowDialog();
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = grvView_LoaiChiPhi.FocusedRowHandle;
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa loại chi phí " + grvView_LoaiChiPhi.GetRowCellValue(i, "loaichiphi") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("dbo.prokhoLoaiChiPhi", new { action = "DELETE", maloai = Convert.ToInt32(grvView_LoaiChiPhi.GetRowCellValue(i, "maloai")) });
                if (dt.Rows[0]["status"].ToString() == "NO")
                {
                    XtraMessageBox.Show("Loại chi phí này đã được sử dụng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    GetLoaiChiPhi();
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã xóa loại chi phí " + gridView1.GetRowCellValue(i, "loaichiphi") + ".", "Danh mục loại chi phí");
                }
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetLoaiChiPhi();
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_tendangnhap.Focus();
            var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                LuuLoaiChiPhi();
            }
        }

        private void frm_loaichiphi_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetLoaiChiPhi();
            GetPhanQuyen();
        }
    }
}
