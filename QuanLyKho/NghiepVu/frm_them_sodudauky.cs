using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.NghiepVu
{
    public partial class frm_them_sodudauky : XtraForm
    {
        #region "Function"
        public void XoaText()
        {
            cbo_tenhanghoa.EditValue = DBNull.Value;
            txt_soluong.Text = "0";
            txtDonGia.Text = "0";
            lblThanhTien.Text = "0";
            cbo_tenhanghoa.Focus();
        }

        public void GetHangHoa()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoHangHoa", new { action = "GET_DATA_ACTIVE" });
            cbo_tenhanghoa.Properties.DataSource = dt;
            cbo_tenhanghoa.Properties.DisplayMember = "tenhanghoa";
            cbo_tenhanghoa.Properties.ValueMember = "mahanghoa";
        }
        #endregion
        public frm_them_sodudauky()
        {
            InitializeComponent();
            date_ngaythang.EditValue = DateTime.Now.Date;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (cbo_tenhanghoa.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn tên hàng hóa để cập nhật.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbo_tenhanghoa.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_soluong.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào số dư đầu kỳ.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_soluong.Focus();
                return;
            }

            if (Convert.ToDouble(txt_soluong.Text) <= 0) { XtraMessageBox.Show("Số đầu kỳ phải lớn hơn 0.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (Convert.ToDouble(lblThanhTien.Text) <= 0) { XtraMessageBox.Show("Số tiền đầu kỳ phải lớn hơn 0.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            ExecSQL.ExecProcedureNonData("prokhoSoDuDauKy", new { action = "SAVE", ngaythang = Convert.ToDateTime(date_ngaythang.EditValue).ToString("yyyyMM01"), mahanghoa = cbo_tenhanghoa.EditValue.ToString(), slton = Convert.ToDecimal(txt_soluong.Text), tiendau = Convert.ToDecimal(txtDonGia.Text), nguoitd = Data.Data._strtendangnhap.ToUpper() });
            XoaText();
            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "tonkho"
            };
            msgBroker.Publish();
        }

        private void cbo_tenhanghoa_Click(object sender, EventArgs e)
        {
            gridLookUpEdit1View.FocusedRowHandle = GridControl.AutoFilterRowHandle;
            gridLookUpEdit1View.FocusedColumn = gridLookUpEdit1View.Columns["tenhanghoa"];
            gridLookUpEdit1View.ShowEditor();
        }

        private void frm_them_sodudauky_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btn_Luu_Click(sender, e);
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        private void frm_them_sodudauky_Load(object sender, EventArgs e)
        {
            GetHangHoa();
        }

        private void txt_soluong_TextChanged(object sender, EventArgs e)
        {
            double thanhtien = Convert.ToDouble(txtDonGia.Text) * Convert.ToDouble(txt_soluong.Text);
            lblThanhTien.Text = thanhtien.ToString();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            double thanhtien = Convert.ToDouble(txtDonGia.Text) * Convert.ToDouble(txt_soluong.Text);
            lblThanhTien.Text = thanhtien.ToString();
        }
    }
}
