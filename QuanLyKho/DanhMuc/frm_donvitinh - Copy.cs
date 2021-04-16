using DevExpress.XtraEditors;
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
        }

        public void LoadPhanQuyen()
        {
            if (Data.Data._check_id($@"select count(*) from tbl_phanquyen where tendangnhap='{Data.Data._strtendangnhap.ToUpper()}' and mamenu='4' and luu='True'") == 1)
            {
                btn_Them.Enabled = true;
            }
            else
            {
                btn_Them.Enabled = false;
            }

            if (Data.Data._check_id($@"select count(*) from tbl_phanquyen where tendangnhap='{Data.Data._strtendangnhap.ToUpper()}' and mamenu='4' and xoa='True'") == 1)
            {
                btn_Xoa.Enabled = true;
            }
            else
            {
                btn_Xoa.Enabled = false;
            }

            if (Data.Data._check_id($@"select count(*) from tbl_phanquyen where tendangnhap='{Data.Data._strtendangnhap.ToUpper()}' and mamenu='4' and sua='True'") == 1)
            {
                gridView1.OptionsBehavior.ReadOnly = false;
                btn_Luu.Enabled = true;
            }
            else
            {
                gridView1.OptionsBehavior.ReadOnly = true;
                btn_Luu.Enabled = false;
            }
        }

        public void LoadDonViTinh()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataSet ds = new DataSet();
            ds = Data.Data._load_data("select * from tbl_donvitinh where madvt not in ('0') order by tendvt");
            dgv_donvitinh.DataSource = ds.Tables[0];
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }


        private void label1A_TextChanged(object sender, EventArgs e)
        {
            LoadDonViTinh();
        }

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm2 == null || frm2.IsDisposed)
                frm2 = new frm_them_donvitinh();
            frm2.Show();

            PassIDform1 sendIDObjectfrm1 = new PassIDform1(frm2.funObjectB);
            sendIDObjectfrm1(this);
        }

        frm_them_donvitinh frm2 = new frm_them_donvitinh();
        public delegate void PassIDform1(frm_donvitinh frm1_copy);
        public void funDataA(string txtform2)
        {
            label1A.Text = txtform2;
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = gridView1.FocusedRowHandle;
            if (Data.Data._check_id("select count(*) from tbl_hanghoa where madvt='" + gridView1.GetRowCellValue(i, "madvt") + "'") > 0)
            {
                XtraMessageBox.Show("Đơn vị tính " + gridView1.GetRowCellValue(i, "tendvt") + " đã được sử dụng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            DialogResult dgr = XtraMessageBox.Show("Bạn có muốn xóa đơn vị tính " + gridView1.GetRowCellValue(i, "tendvt") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                var row = gridView1.GetFocusedDataRow();
                Data.Data._run_cmd("delete from tbl_donvitinh where madvt='" + gridView1.GetRowCellValue(i, "madvt") + "'");

                //Ghi lại log
                Data.Data._run_history_log("Đã xóa đơn vị tính " + gridView1.GetRowCellValue(i, "tendvt") + ".", "Danh mục đơn vị tính");
                row.Table.Rows.Remove(row);
            }
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadDonViTinh();
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lbl_tendangnhap.Focus();
            var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại những thay đổi không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                LuuDonViTinh();
            }
        }

        private void LuuDonViTinh()
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
                    Data.Data._run_cmd("update tbl_donvitinh set tendvt='" + dr["tendvt"] + "', thoigian2='" + DateTime.Now.ToString() + "', nguoitd2='" + Data.Data._strtendangnhap.ToUpper() + "' where madvt='" + dr["madvt"] + "'");
                    //Ghi lại log
                    Data.Data._run_history_log("Đã cập nhật lại thông tin đơn vị tính " + dr["tendvt"] + ".", "Danh mục đơn vị tính");
                }
            }
        }

        private void frm_donvitinh_Load(object sender, EventArgs e)
        {
            LoadDonViTinh();
            LoadPhanQuyen();
        }
    }
}
