using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.HeThong
{
    public partial class FrmThemNguoiDung : DevExpress.XtraEditors.XtraForm
    {
        public FrmThemNguoiDung()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tendanhnhap.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào tên đăng nhập.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tendanhnhap.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_matkhau.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào mật khẩu đăng nhập.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_matkhau.Focus();
                return;
            }
            if (txt_matkhau.Text != txt_matkhau2.Text)
            {
                XtraMessageBox.Show("Mật khẩu nhập lại không đúng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_matkhau2.Focus();
                return;
            }

            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNguoiDung", new { action = "SAVE", tendangnhap = txt_tendanhnhap.Text.ToUpper(), matkhau = txt_matkhau.Text, hoten = txt_hoten.Text, ghichu = txt_ghichu.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
            if (dt.Rows.Count == 0)
            {
                //Ghi lại log
                //Data.Data._run_history_log("Đã thêm người dùng có tên là " + txt_tendanhnhap.Text + ".", "Danh mục người dùng");
                _xoatext();
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "nguoidung"
                };
                msgBroker.Publish();
            }
            else
            {
                XtraMessageBox.Show($"Tên đăng nhập ({txt_tendanhnhap.Text}) đã tồn tại.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void _xoatext()
        {
            txt_tendanhnhap.Text = "";
            txt_hoten.Text = "";
            txt_ghichu.Text = "";
            txt_tendanhnhap.Focus();
        }
        private void btn_nhaplai_Click(object sender, EventArgs e)
        {
            _xoatext();
        }

        private void txt_tendanhnhap_Leave(object sender, EventArgs e)
        {
            txt_tendanhnhap.Text = txt_tendanhnhap.Text.ToUpper();
        }

        private void frm_them_nguoidung_KeyDown(object sender, KeyEventArgs e)
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
    }
}
