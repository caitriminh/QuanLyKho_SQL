using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class FrmThemKhachHang : XtraForm
    {
        public FrmThemKhachHang()
        {
            InitializeComponent();
        }

        private void Btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Luu_Click(object sender, EventArgs e)
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
            Clear_text();
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

        public void Clear_text()
        {
            txt_makh.Text = ExecSQL.ExecProcedureSacalar("prokhoKhachHang", new { action = "CREATE_ID" }).ToString();
            txt_khachhang.Text = "";
            txt_diachi.Text = "";
            txt_sofax.Text = "";
            txt_sodt.Text = "";
            txt_ghichu.Text = "";
            txt_khachhang.Focus();
        }
        private void Btn_nhaplai_Click(object sender, EventArgs e)
        {
            Clear_text();
        }

        private void Frm_add_khachhang_KeyDown(object sender, KeyEventArgs e)
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

        private void Frm_them_khachhang_Load(object sender, EventArgs e)
        {
            txt_makh.Text = ExecSQL.ExecProcedureSacalar("prokhoKhachHang", new { action = "CREATE_ID" }).ToString();
        }

        private void Frm_them_khachhang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Data._int_flag = 1;
        }
    }
}
