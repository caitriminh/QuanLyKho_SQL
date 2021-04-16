using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.DanhMuc;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;


namespace QuanLyKho.DanhMuc
{
    public partial class frm_them_nhomhang : XtraForm
    {
        public frm_them_nhomhang()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btn_Luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_manhom.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào mã nhóm hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_manhom.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_nhomhang.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào tên nhóm hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nhomhang.Focus();
                return;
            }
            //if (Data.Data._check_id("select count(*) from tbl_nhomhang where manhom='" + txt_manhom.Text + "'") == 0)
            //{
            //    XtraMessageBox.Show("Mã nhóm hàng này đã tồn tại.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txt_manhom.Text = "";
            //    txt_manhom.Focus();
            //}

            await ExecSQL.ExecProcedureDataAsync<khoNhomHang>("proKhoNhomHang", new { action = "SAVE", nhomhang = txt_nhomhang.Text, manhom = txt_manhom.Text.ToUpper(), nguoitd = Data.Data._strtendangnhap.ToUpper() });

            //Ghi lại log
            // Data.Data._run_history_log("Đã thêm nhóm hàng là " + txt_nhomhang.Text + ".", "Danh mục nhóm hàng");
            if (Data.Data._int_flag == 1)
            {
                txt_nhomhang.Text = "";
                txt_manhom.Text = "";
                txt_manhom.Focus();

                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "nhomhang"
                };
                msgBroker.Publish();
            }
            else if (Data.Data._int_flag == 2)
            {
                Data.Data._str_manhomhang = txt_manhom.Text;
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "hanghoa"
                };
                msgBroker.Publish();
                Close();
            }

        }

        private void frm_add_nhomhang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Data._bol_start = false;
        }

        private void frm_add_nhomhang_KeyDown(object sender, KeyEventArgs e)
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

        private void txt_manhom_Leave(object sender, EventArgs e)
        {
            txt_manhom.Text = txt_manhom.Text.ToUpper();
        }
    }
}
