using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using QuanLyKho.Data;
using QuanLyKho.Entities.DanhMuc;
using QuanLyKho.Entities.NghiepVu;
using QuanLyKho.Extension;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.NghiepVu
{
    public partial class FrmThemDinhMucLinhKien : XtraForm
    {
        public FrmThemDinhMucLinhKien()
        {
            InitializeComponent();
            grvViewQuyDoi.RowCellClick += GrvViewQuyDoi_RowCellClick;
            grvViewQuyDoi.ValidatingEditor += GrvViewQuyDoi_ValidatingEditor;
        }

        private void GrvViewQuyDoi_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            int i = view.FocusedRowHandle;
            if (view.FocusedColumn.FieldName == "mahanghoa_qd")
            {
                var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoHangHoa>("proKhoHangHoa", new { action = "GET_DATA_MAHANG", mahanghoa = e.Value.ToString() });
                if (dt == null) { return; }
                view.SetRowCellValue(i, "tenhanghoa1", dt.tenhanghoa);
                view.SetRowCellValue(i, "tendvt1", dt.tendvt);

                var dt2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoQuyDoi", new { action = "SAVE", mahanghoa = cbo_hanghoa2.EditValue.ToString(), mahanghoa_qd = e.Value.ToString(), heso = 0, thamsoquydoi = Convert.ToDecimal(txtThamSoQuyDoi.Text), ghichu = txt_ghichu.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
                if (dt2.Rows.Count > 0)
                {
                    //XtraMessageBox.Show("Mã hàng này đã tồn tại trong danh mục quy đổi.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GetChiTietQuyDoi();
                    return;
                }
                ////Ghi lại log
                //Data.Data._run_history_log("Đã thêm mới quy đổi hàng hóa " + cbo_hanghoa2.Text, "Quy đổi hàng hóa");
            }
            else if (view.FocusedColumn.FieldName == "heso")
            {
                if (e.Value == null) { return; }
                ExecSQL.ExecProcedureNonData("prokhoQuyDoi", new { action = "UPDATE_HESO", mahanghoa = cbo_hanghoa2.EditValue.ToString(), mahanghoa_qd = view.GetRowCellValue(i, "mahanghoa_qd").ToString(), heso = Convert.ToDecimal(e.Value) });
            }
            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "quydoi"
            };
            msgBroker.Publish();
        }

        private void GrvViewQuyDoi_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = grvViewQuyDoi.FocusedRowHandle;
            if (ReferenceEquals(e.Column, colXoa))
            {
                var dgr = XtraMessageBox.Show($@"Bạn có muốn xóa mã hàng này {grvViewQuyDoi.GetRowCellValue(i, "tenhanghoa")} không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr != DialogResult.Yes) { return; }
                ExecSQL.ExecProcedureNonData("prokhoQuyDoi", new { action = "DELETE", id = Convert.ToInt32(grvViewQuyDoi.GetRowCellValue(i, "id")) });
                grvViewQuyDoi.DeleteRow(i);
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "quydoi"
                };
                msgBroker.Publish();
            }
        }

        #region "Function"
        public async void GetHangHoa()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoHangHoa", new { action = "GET_DATA" });
            cbo_hanghoa2.Properties.DataSource = dt;
            cbo_hanghoa2.Properties.DisplayMember = "tenhanghoa";
            cbo_hanghoa2.Properties.ValueMember = "mahanghoa";
            cbo_hanghoa2.EditValue = Data.Data._str_mahang;
            var dt2 = ExecSQL.ExecProcedureDataFistOrDefault<khoQuyDoi>("prokhoQuyDoi", new { action = "GET_DATA_MAHANG", mahanghoa = Data.Data._str_mahang });
            if (dt2 == null) { return; }
            txtThamSoQuyDoi.Text = dt2.thamsoquydoi.ToString();
        }

        public async void GetHangHoa2()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoHangHoa", new { action = "GET_DATA", mahanghoa = cbo_hanghoa2.EditValue.ToString() });
            cbo_hanghoa.DataSource = dt;
            cbo_hanghoa.DisplayMember = "mathamchieu";
            cbo_hanghoa.ValueMember = "mahanghoa";
        }

        public async void GetChiTietQuyDoi()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("prokhoQuyDoi", new { action = "GET_DATA_MAHANG", mahanghoa = cbo_hanghoa2.EditValue.ToString() });
            dt.Columns["id"].AllowDBNull = true;
            grcQuyDoi.DataSource = dt;
        }
        #endregion

        private void Frm_them_quydoi_Load(object sender, EventArgs e)
        {
            GetHangHoa();
        }

        private void Cbo_hanghoa2_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbo_hanghoa2.Text)) { return; }
            GetHangHoa2();
            GetChiTietQuyDoi();
        }

        private void Btn_lammoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvViewQuyDoi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            GetChiTietQuyDoi();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvViewQuyDoi.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
        }
    }
}
