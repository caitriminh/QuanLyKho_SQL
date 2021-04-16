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
    public partial class FrmThemNhapPhaiThu : XtraForm
    {
        #region "Function"
        public void XoaText()
        {
            cboKhachHang.EditValue = DBNull.Value;
            txt_sotiendau.Text = "0";
            txt_ghichu.Text = "";
            cboKhachHang.Focus();
        }

        public void GetNCC()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoKhachHang", new { action = "GET_DATA" });
            cboKhachHang.Properties.DataSource = dt;
            cboKhachHang.Properties.DisplayMember = "tenkh";
            cboKhachHang.Properties.ValueMember = "makh";
        }
        #endregion
        public FrmThemNhapPhaiThu()
        {
            InitializeComponent();
            dte_ngaynhap.EditValue = DateTime.Now.Date;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn tên nhà cung cấp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_sotiendau.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào số dư đầu kỳ.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sotiendau.Focus();
                return;
            }

            if (Convert.ToDouble(txt_sotiendau.Text) <= 0)
            {
                XtraMessageBox.Show("Số đầu kỳ phải lớn hơn 0.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            ExecSQL.ExecProcedureNonData("prokhoNhapXuatPhaiThu", new { action = "SAVE", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd"), makh = cboKhachHang.EditValue.ToString(), loaiphieu = "NPT", sotien = Convert.ToDecimal(txt_sotiendau.Text), ghichu = txt_ghichu.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
            XoaText();
            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "nhapphaithu"
            };
            msgBroker.Publish();
        }

        private void cboNCC_Click(object sender, EventArgs e)
        {
            gridLookUpEdit1View.FocusedRowHandle = GridControl.AutoFilterRowHandle;
            gridLookUpEdit1View.FocusedColumn = gridLookUpEdit1View.Columns["tenkh"];
            gridLookUpEdit1View.ShowEditor();
        }

        private void FrmThemNhapTra_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmThemNhapTra_Load(object sender, EventArgs e)
        {
            GetNCC();
        }
    }
}
