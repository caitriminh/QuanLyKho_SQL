using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.DanhMuc;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class FrmNhomHang : XtraForm
    {
        string strMaNhom = "";
        public FrmNhomHang()
        {
            InitializeComponent();
            grvView_NhomHang.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNhomHang, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcNhomHang, Name);

            grvView_NhomHang.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcNhomHang, grvView_NhomHang); };
            grvView_NhomHang.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcNhomHang, Name); };
            grvView_NhomHang.FocusedRowChanged += GrvView_NhomHang_FocusedRowChanged;
        }

        private void GrvView_NhomHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = grvView_NhomHang.FocusedRowHandle;
            if (grvView_NhomHang.GetRowCellValue(i, "manhom") == null) { return; }
            strMaNhom = grvView_NhomHang.GetRowCellValue(i, "manhom").ToString();
        }

        #region "Function"
        private void OnNext(MessageBroker value)
        {
            if (value.task == "nhomhang")
            {
                GetNhomHang();
            }
        }

        public async void GetNhomHang()
        {
            var x = grvView_NhomHang.FocusedRowHandle;
            var y = grvView_NhomHang.TopRowIndex;
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoNhomHang", new { action = "GET_DATA" });
            grcNhomHang.DataSource = dt;
            grvView_NhomHang.FocusedRowHandle = x;
            grvView_NhomHang.TopRowIndex = y;
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 5 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            col_xoa.Visible = dt.xoa == true;
            grvView_NhomHang.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
        }

        private async void LuuNhomHang()
        {
            for (var index = 0; index <= grvView_NhomHang.RowCount - 1; index++)
            {
                var dr = grvView_NhomHang.GetDataRow(Convert.ToInt32(index));
                if (dr is null)
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    await ExecSQL.ExecProcedureDataAsync<khoNhomHang>("proKhoNhomHang", new { action = "UPDATE", nhomhang = dr["nhomhang"].ToString(), sudung = Convert.ToBoolean(dr["sudung"]), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), manhom = dr["manhom"].ToString() });
                }
            }
            GetNhomHang();
        }

        #endregion

        private void Btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._int_flag = 1;
            FrmThemNhomHang frm = new FrmThemNhomHang();
            frm.ShowDialog();
        }

        private void Btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show($"Bạn có muốn xóa các nhóm hàng ({strMaNhom}) này không", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoNhomHang", new { action = "DELETE", manhom = strMaNhom });
                if (dt.Rows[0]["status"].ToString() == "NO")
                {
                    XtraMessageBox.Show("Mã nhóm hàng này đã được sử dụng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    GetNhomHang();
                }
            }
        }

        private void Btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhomHang();
        }

        private void Frm_nhomhang_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhomHang();
            GetPhanQuyen();
        }

        private void Btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_manhom.Focus();
            var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                LuuNhomHang();
            }
        }

        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = grvView_NhomHang.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa nhóm hàng " + grvView_NhomHang.GetRowCellValue(i, "nhomhang") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoNhomHang", new { action = "DELETE", manhom = strMaNhom });
                    if (dt.Rows[0]["status"].ToString() == "NO")
                    {
                        XtraMessageBox.Show("Mã nhóm hàng này đã được sử dụng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        GetNhomHang();
                    }
                }
            }
        }

    }
}
