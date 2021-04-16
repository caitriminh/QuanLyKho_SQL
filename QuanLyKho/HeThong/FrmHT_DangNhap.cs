using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using QuanLyKho.Entities.HeThong;

namespace QuanLyKho.HeThong
{
    public partial class FrmHT_DangNhap : XtraForm
    {
        public string IsLogin { set; get; }
        public string Username { set; get; }
        public FrmHT_DangNhap()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsLogin = "OK";
            Application.Exit();
        }

        /// <summary>
        /// Create by Tri Minh, Date: 07/11/2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLoginVinaGiay_Load(object sender, EventArgs e)
        {
            txt_tendangnhap.Text = ConfigAppSetting.GetSetting("UserName");
            txt_matkhau.Text = ConfigAppSetting.GetSetting("Password");
            chkGhiNho.Checked = ConfigAppSetting.GetSetting("GhiNho") == "True" ? true : false;
            btnDangNhap_Click(sender, e);
        }

        /// <summary>
        /// Create by Tri Minh, Date: 07/11/2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLoginVinaGiay_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnDangNhap_Click(sender, e);
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoNguoiDung>("prokhoNguoiDung", new { action = "LOGIN", tendangnhap = txt_tendangnhap.Text.ToUpper(), matkhau = txt_matkhau.Text });
            if (dt.status == "OK")
            {
                Data.Data._strtendangnhap = txt_tendangnhap.Text;
                Data.Data._run_history_log("Đã đăng nhập vào hệ thống.", "Đăng nhập");
                //Gửi dữ liệu load form chính

                IsLogin = "OK";
                Username = txt_tendangnhap.Text.Trim();
                DialogResult = DialogResult.OK;
                if (chkGhiNho.Checked)
                {
                    ConfigAppSetting.SetSetting("UserName", txt_tendangnhap.Text);
                    ConfigAppSetting.SetSetting("Password", txt_matkhau.Text);
                    ConfigAppSetting.SetSetting("GhiNho", "True");
                }
                Close();
            }
            else
            {
                txt_tendangnhap.Text = "";
                txt_matkhau.Text = "";
                txt_tendangnhap.Focus();
                XtraMessageBox.Show("Đăng nhập không thành công.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Create by Tri Minh, Date: 07/11/2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLoginVinaGiay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLogin != "OK")
                e.Cancel = true;
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}