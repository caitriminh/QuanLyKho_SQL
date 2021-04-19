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
    public partial class FrmPhanQuyen : XtraForm
    {
        public FrmPhanQuyen()
        {
            InitializeComponent();
        }

        #region "Function"
        public void GetPhanQuyen()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoPhanQuyen", new { action = "GET_DATA" });
            dgvPhanQuyenNguoiDung.DataSource = dt;
            lbl_tendangnhap.DataBindings.Clear();
            lbl_tendangnhap.DataBindings.Add("text", dt, "tendangnhap");
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void LuuPhanQuyen()
        {
            for (var index = 0; index <= gridView1.RowCount - 1; index++)
            {
                var dr = gridView1.GetDataRow(Convert.ToInt32(index));
                if (ReferenceEquals(dr, null))
                {
                    break;
                }
                if (dr.RowState == DataRowState.Modified)
                {
                    ExecSQL.ExecProcedureNonData("prokhoPhanQuyen", new { action = "UPDATE", xem = Convert.ToBoolean(dr["xem"]), luu = Convert.ToBoolean(dr["luu"]), sua = Convert.ToBoolean(dr["sua"]), xoa = Convert.ToBoolean(dr["xoa"]), inan = Convert.ToBoolean(dr["inan"]), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), id = Convert.ToInt32(dr["id"]) });
                }
            }
            GetPhanQuyen();
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "phanquyen")
            {
                GetPhanQuyen();
            }
        }

        #endregion

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhanQuyen();
        }
        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_tendangnhap.Focus();
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                LuuPhanQuyen();
            }
        }


        private void frm_phanquyen_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetPhanQuyen();
        }


        private void btnXoa2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa các danh sách phân quyền của người dùng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("prokhoPhanQuyen", new { action = "DELETE", tendangnhap = lbl_tendangnhap.Text });
                GetPhanQuyen();
            }
        }

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmSaoChepPhanQuyen frm = new FrmSaoChepPhanQuyen();
            frm.ShowDialog();
        }
    }
}
