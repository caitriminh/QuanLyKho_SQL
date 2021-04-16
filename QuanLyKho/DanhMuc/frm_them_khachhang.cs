using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class frm_them_khachhang : XtraForm
    {
        public frm_them_khachhang()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_makh.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào mã khách hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_makh.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_khachhang.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào tên khách hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_khachhang.Focus();
                return;
            }

            ExecSQL.ExecProcedureNonData("prokhoKhachHang", new { action = "SAVE", tenkh = txt_khachhang.Text, diachi = txt_diachi.Text, sodt = txt_sodt.Text, sofax = txt_sofax.Text, ghichu = txt_ghichu.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });

            Data.Data._str_makh = txt_makh.Text;
            _clear_text();
            if (Data.Data._int_flag == 1)
            {
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "khachhang"
                };
                msgBroker.Publish();
            }
            else if (Data.Data._int_flag == 2)
            {
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "xuatkho"
                };
                msgBroker.Publish();
                this.Close();
            }
            else if (Data.Data._int_flag == 3)
            {
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "nhapxuatthang"
                };
                msgBroker.Publish();
                this.Close();
            }
        }

        public void _clear_text()
        {
            txt_makh.Text = ExecSQL.ExecProcedureSacalar("prokhoKhachHang", new { action = "CREATE_ID" }).ToString();
            txt_khachhang.Text = "";
            txt_diachi.Text = "";
            txt_sofax.Text = "";
            txt_sodt.Text = "";
            txt_ghichu.Text = "";
            txt_khachhang.Focus();
        }
        private void btn_nhaplai_Click(object sender, EventArgs e)
        {
            _clear_text();
        }

        private void frm_add_khachhang_KeyDown(object sender, KeyEventArgs e)
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

        private void frm_them_khachhang_Load(object sender, EventArgs e)
        {
            txt_makh.Text = ExecSQL.ExecProcedureSacalar("prokhoKhachHang", new { action = "CREATE_ID" }).ToString();
        }

        private void frm_them_khachhang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Data._int_flag = 1;
        }
    }
}
