using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System;
using System.Windows.Forms;

namespace QuanLyKho.HeThong
{
    public partial class frm_thongtin : XtraForm
    {
        public frm_thongtin()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            ExecSQL.ExecProcedureNonData("proKhoThongTin", new { action = "SAVE", tencty = txt_tencongty.Text, tencty2 = txt_tencty2.Text, diachi = txt_diachi.Text, sodt = txt_sodt.Text, sofax = txt_sofax.Text, website = txt_website.Text, email = txt_email.Text, masothue = txt_masothue.Text, nguoidaidien = txt_nguoidaidien.Text });
            XtraMessageBox.Show("Đã cập nhật thành công thông tin của công ty.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frm_thongtin_Load(object sender, EventArgs e)
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoThongTin>("prokhoThongTin", new { action = "GET_DATA" });
            if (dt == null) { return; }
            txt_tencongty.Text = dt.tencty;
            txt_tencty2.Text = dt.tencty2;
            txt_diachi.Text = dt.diachi;
            txt_sodt.Text = dt.sodt;
            txt_sofax.Text = dt.sofax;
            txt_website.Text = dt.website;
            txt_email.Text = dt.email;
            txt_masothue.Text = dt.masothue;
            txt_nguoidaidien.Text = dt.nguoidaidien;
        }
    }
}
