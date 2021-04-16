using DevExpress.XtraEditors;
using QuanLyKho.Data;
using System;
using System.Windows.Forms;

namespace QuanLyKho.HeThong
{
    public partial class frm_doimatkhau : DevExpress.XtraEditors.XtraForm
    {
        public frm_doimatkhau()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tendangnhap.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào tên đăng nhập.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tendangnhap.Focus();
                return;
            }
            if (txt_matkhaumoi.Text == txt_nhaplai_matkhaumoi.Text)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNguoiDung", new { action = "CHANGE_PASSWORD", tendangnhap = txt_tendangnhap.Text.ToLower(), matkhau = txt_nhaplai_matkhaumoi.Text, matkhaucu = txt_matkhau.Text });
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Bạn đã đổi mật khẩu thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show(dt.Rows[0]["status"].ToString(), "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_matkhaumoi.Text = "";
                    txt_nhaplai_matkhaumoi.Text = "";
                    txt_matkhau.Text = "";
                    txt_tendangnhap.Text = "";
                    txt_tendangnhap.Focus();
                }
                //Data.Data._run_cmd("update tbl_nguoidung set matkhau='" + Data.Data.Md5(txt_matkhaumoi.Text) + "' where tendangnhap='" + txt_tendangnhap.Text + "'");
                //Ghi lại log
                //Data.Data._run_history_log("Đổi mật khẩu.", "Đổi mật khẩu");

            }
            else
            {
                XtraMessageBox.Show("Mật khẩu bạn nhập vào không khớp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_matkhaumoi.Text = "";
                txt_nhaplai_matkhaumoi.Text = "";
                txt_matkhaumoi.Focus();
            }
        }

        private void frm_doimatkhau_Load(object sender, EventArgs e)
        {
            txt_tendangnhap.Text = Data.Data._strtendangnhap.ToLower();
        }
    }
}
