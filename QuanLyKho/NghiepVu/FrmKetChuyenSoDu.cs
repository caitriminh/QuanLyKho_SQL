using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;
namespace QuanLyKho.NghiepVu
{
    public partial class frm_capnhat_sodu_dauky : XtraForm
    {
        public frm_capnhat_sodu_dauky()
        {
            InitializeComponent();
            date_thangnam.EditValue = DateTime.Now.Date.ToString("MM/yyyy");
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            var dgr = XtraMessageBox.Show($"Bạn có muốn kết chuyển số dư sang tháng sau không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            ExecSQL.ExecProcedureNonData("prokhoTonKho", new { action = "GET_DATA", option = 2, ngaythang = Convert.ToDateTime(date_thangnam.EditValue).ToString("yyyyMM01") });
            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "tonkho"
            };
            msgBroker.Publish();

            XtraMessageBox.Show("Đã chuyển số dư thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

    }

}

