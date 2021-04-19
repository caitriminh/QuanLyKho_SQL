using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class FrmThemLoaiChiPhi : XtraForm
    {
        public FrmThemLoaiChiPhi()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (txt_loaichiphi.Text.Length > 0)
            {
                ExecSQL.ExecProcedureNonData("prokhoLoaiChiPhi", new { action = "SAVE", loaichiphi = txt_loaichiphi.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
                //Ghi lại log
                //Data.Data._run_history_log($@"Đã thêm loại chi phí { txt_loaichiphi.Text }.", "Danh mục loại chi phí");
                txt_loaichiphi.Text = "";
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "loaichiphi"
                };
                msgBroker.Publish();
            }
            else
            {
                XtraMessageBox.Show("Bạn phải nhập vào tên loại chi phí.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_loaichiphi.Focus();
            }
        }


        private void frm_them_loaichiphi_KeyDown(object sender, KeyEventArgs e)
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
    }
}
