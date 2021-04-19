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
    public partial class FrmThemSoDuDauKy : XtraForm
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
        public FrmThemSoDuDauKy()
        {
            InitializeComponent();
            date_ngaythang.EditValue = DateTime.Now.Date;
        }

        private void Btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Luu_Click(object sender, EventArgs e)
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

        private void Cbo_tenhanghoa_Click(object sender, EventArgs e)
        {
            gridLookUpEdit1View.FocusedRowHandle = GridControl.AutoFilterRowHandle;
            gridLookUpEdit1View.FocusedColumn = gridLookUpEdit1View.Columns["tenhanghoa"];
            gridLookUpEdit1View.ShowEditor();
        }

        private void Frm_them_sodudauky_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Btn_Luu_Click(sender, e);
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        private void Frm_them_sodudauky_Load(object sender, EventArgs e)
        {
            GetHangHoa();
        }

        private void Txt_soluong_TextChanged(object sender, EventArgs e)
        {
            double thanhtien = Convert.ToDouble(txtDonGia.Text) * Convert.ToDouble(txt_soluong.Text);
            lblThanhTien.Text = thanhtien.ToString();
        }

        private void TxtDonGia_TextChanged(object sender, EventArgs e)
        {
            double thanhtien = Convert.ToDouble(txtDonGia.Text) * Convert.ToDouble(txt_soluong.Text);
            lblThanhTien.Text = thanhtien.ToString();
        }
    }
}
