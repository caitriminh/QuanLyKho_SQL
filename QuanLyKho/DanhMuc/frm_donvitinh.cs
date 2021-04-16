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
    public partial class frm_donvitinh : XtraForm
    {
        public frm_donvitinh()
        {
            InitializeComponent();
            gridView1.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, dgv_donvitinh, gridView1); };
        }

        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 4 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            gridView1.OptionsBehavior.ReadOnly = dt.sua != true;
            btn_Luu.Enabled = dt.sua == true;
        }

        public async void GetDonViTinh()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstDonViTinh = await ExecSQL.ExecProcedureDataAsync<khoDonViTinh>("proKhoDonViTinh", new { action = "GET_DATA" });
            dgv_donvitinh.DataSource = lstDonViTinh;

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "donvitinh")
            {
                GetDonViTinh();
            }
        }

        private async void LuuDonViTinh()
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
                    await ExecSQL.ExecProcedureDataAsync<khoDonViTinh>("proKhoDonViTinh", new { action = "UPDATE", madvt = Convert.ToInt32(dr["madvt"]), tendvt = dr["tendvt"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper() });
                }
            }
        }
        #endregion

        private void Btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._int_flag = 1;
            Frm_them_donvitinh frm = new Frm_them_donvitinh();
            frm.Show();
        }

        private void Btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = gridView1.FocusedRowHandle;
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa đơn vị tính " + gridView1.GetRowCellValue(i, "tendvt") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoDonViTinh", new { action = "DELETE", madvt = Convert.ToInt32(gridView1.GetRowCellValue(i, "madvt")) });
                if (dt.Rows[0]["status"].ToString() == "OK")
                {
                    GetDonViTinh();
                }
                else
                {
                    XtraMessageBox.Show("Mã đơn vị tính này đã được sử dụng. Bạn không thể xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDonViTinh();
        }

        private void Btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_tendangnhap.Focus();
            var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                LuuDonViTinh();
            }
        }

        private void Frm_donvitinh_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetDonViTinh();
            GetPhanQuyen();
        }
    }
}
