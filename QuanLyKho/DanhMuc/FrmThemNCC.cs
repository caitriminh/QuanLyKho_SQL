using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class FrmThemNCC : XtraForm
    {
        public FrmThemNCC()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_mancc.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào mã nhà cung cấp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_mancc.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_ncc.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào tên nhà cung cấp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_ncc.Focus();
                return;
            }
            ExecSQL.ExecProcedureNonData("prokhoNhaCungCap", new { action = "SAVE", ncc = txt_ncc.Text, diachi = txt_diachi.Text, sodt = txt_sodt.Text, sofax = txt_sofax.Text, email = txt_email.Text, masothue = txt_masothue.Text, ghichu = txt_ghichu.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
            //Ghi lại nhật ký
            Data.Data._run_history_log($"Thêm mới thông tin nhà cung cấp ({txt_ncc.Text}).", "Danh Mục Nhà Cung Cấp");
            Data.Data._str_mancc = txt_mancc.Text;

            _clear_text();
            if (Data.Data._int_flag == 1)
            {
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "nhacungcap"
                };
                msgBroker.Publish();
            }
            else if (Data.Data._int_flag == 2)
            {
                //Gửi dữ liệu load form chính
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "nhapkho"
                };
                msgBroker.Publish();
                this.Close();
            }
        }

        public void _clear_text()
        {
            txt_mancc.Text = ExecSQL.ExecProcedureSacalar("prokhoNhaCungCap", new { action = "CREATE_ID" }).ToString();
            txt_ncc.Text = "";
            txt_diachi.Text = "TP. Hồ Chí Minh";
            txt_email.Text = "";
            txt_sodt.Text = "";
            txt_sofax.Text = "";
            txt_masothue.Text = "";
            txt_ghichu.Text = "";
            txt_ncc.Focus();
        }
        private void btn_nhaplai_Click(object sender, EventArgs e)
        {
            _clear_text();
        }

        private void frm_them_ncc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Data._int_flag = 1;
        }

        private void frm_them_ncc_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btn_Luu_Click(sender, e);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void frm_them_ncc_Load(object sender, EventArgs e)
        {
            txt_mancc.Text = ExecSQL.ExecProcedureSacalar("prokhoNhaCungCap", new { action = "CREATE_ID" }).ToString();
        }
    }
}
