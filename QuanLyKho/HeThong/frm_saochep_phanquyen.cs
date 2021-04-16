using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using QuanLyKho.Data;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.HeThong
{
    public partial class frm_saochep_phanquyen : XtraForm
    {
        public frm_saochep_phanquyen()
        {
            InitializeComponent();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void frm_them_phanquyen_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnSaoChep_Click(sender, e);
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        public async void GetTuNguoiDung()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("prokhoNguoiDung", new { action = "GET_DATA_ALL" });
            cboTuNguoiDung.Properties.DataSource = dt;
            cboTuNguoiDung.Properties.DisplayMember = "tendangnhap";
            cboTuNguoiDung.Properties.ValueMember = "tendangnhap";
        }

        private void frm_them_phanquyen_Load(object sender, EventArgs e)
        {
            GetTuNguoiDung();
        }

        private void cboNguoiDung_Click(object sender, EventArgs e)
        {
            gridLookUpEdit1View.FocusedRowHandle = GridControl.AutoFilterRowHandle;
            gridLookUpEdit1View.FocusedColumn = gridLookUpEdit1View.Columns["tendangnhap"];
            gridLookUpEdit1View.ShowEditor();
        }

        private void btnSaoChep_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboTuNguoiDung.Text))
            {
                XtraMessageBox.Show("Bạn phải chọn tên người dùng cần sao chép.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTuNguoiDung.Focus();
                return;
            }

            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoPhanQuyen", new { action = "COPY", tendangnhap = cboTuNguoiDung.EditValue.ToString(), nguoitd = Data.Data._strtendangnhap.ToUpper() });
            if (dt.Rows.Count > 0)
            {
                XtraMessageBox.Show("Tên người dùng " + cboTuNguoiDung.Text + " đã tồn tại.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTuNguoiDung.Focus();
                return;
            }
            XtraMessageBox.Show("Đã sao chép các chức năng phân quyền cho người dùng " + cboTuNguoiDung.Text + " thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "phanquyen"
            };
            msgBroker.Publish();

        }

    }
}
