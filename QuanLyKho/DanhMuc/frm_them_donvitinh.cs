using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class Frm_them_donvitinh : XtraForm
    {
        public Frm_them_donvitinh()
        {
            InitializeComponent();
        }

        private void Btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tendvt.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào tên đơn vị tính.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tendvt.Focus();
                return;
            }
            ExecSQL.ExecProcedureNonData("proKhoDonViTinh", new { action = "SAVE", tendvt = txt_tendvt.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
            //Xóa text
            if (Data.Data._int_flag == 1)
            {
                txt_tendvt.Text = "";
                txt_tendvt.Focus();
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "donvitinh"
                };
                msgBroker.Publish();
            }
            else if (Data.Data._int_flag == 2)
            {

                Close();
            }

        }


        private void Frm_them_donvitinh_KeyDown(object sender, KeyEventArgs e)
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
