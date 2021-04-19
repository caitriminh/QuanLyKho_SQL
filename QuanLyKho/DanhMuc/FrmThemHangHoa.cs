using DevExpress.XtraEditors;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.DanhMuc
{
    public partial class FrmThemHangHoa : XtraForm
    {
        #region "Function"
        public async void GetDonViTinh()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoDonViTinh", new { action = "GET_DATA" });
            cbo_dvt.Properties.DataSource = dt;
            cbo_dvt.Properties.DisplayMember = "tendvt";
            cbo_dvt.Properties.ValueMember = "madvt";
            cbo_dvt.EditValue = Data.Data._str_madvt;
        }

        public async void GetNhomHang()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoNhomHang", new { action = "GET_DATA" });
            cbo_nhomhang.Properties.DataSource = dt;
            cbo_nhomhang.Properties.DisplayMember = "nhomhang";
            cbo_nhomhang.Properties.ValueMember = "manhom";
            cbo_nhomhang.EditValue = Data.Data._str_manhomhang;
        }

        public void Xoatext()
        {
            txt_tenhanghoa.Text = "";
            txt_ghichu.Text = "";
            txt_dongia.Text = "0";
            cbo_nhomhang.Focus();
        }
        #endregion
        public FrmThemHangHoa()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (cbo_nhomhang.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn nhóm phụ tùng để cập nhật.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbo_nhomhang.Focus();
                return;
            }
            if (cbo_dvt.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn đơn vị tính để cập nhật.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbo_dvt.Focus();
                return;
            }

            ExecSQL.ExecProcedureNonDataAsync("proKhoHangHoa", new { action = "SAVE", manhom = cbo_nhomhang.EditValue.ToString(), tenhanghoa = txt_tenhanghoa.Text, madvt = Convert.ToInt32(cbo_dvt.EditValue), ghichu = txt_ghichu.Text, nguoitd = Data.Data._strtendangnhap.ToUpper(), dongia = txt_dongia.Text == "" ? 0 : Convert.ToDecimal(txt_dongia.Text) });
            Xoatext();

            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "hanghoa"
            };
            msgBroker.Publish();
        }

        private void Frm_them_hanghoa_Load(object sender, EventArgs e)
        {
            GetDonViTinh();
            GetNhomHang();
        }

        private void frm_them_hanghoa_KeyDown(object sender, KeyEventArgs e)
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

        private void cbo_nhomhang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Data.Data._int_flag = 2;
                frm_them_nhomhang frm = new frm_them_nhomhang();
                frm.ShowDialog();
            }
        }

        private void cbo_dvt_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Data.Data._int_flag = 2;
                Frm_them_donvitinh frm = new Frm_them_donvitinh();
                frm.ShowDialog();
            }
        }
    }
}
