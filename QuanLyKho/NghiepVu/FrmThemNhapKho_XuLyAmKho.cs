using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using QuanLyKho.DanhMuc;
using QuanLyKho.Data;
using QuanLyKho.Entities.DanhMuc;
using QuanLyKho.Entities.NghiepVu;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyKho.NghiepVu
{
    public partial class FrmThemNhapKho_XuLyAmKho : XtraForm
    {

        public bool print = false;
        #region "Function"
        public void GetNhaCungCap()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNhaCungCap", new { action = "GET_DATA" });
            cbo_ncc.Properties.DataSource = dt;
            cbo_ncc.Properties.DisplayMember = "ncc";
            cbo_ncc.Properties.ValueMember = "mancc";
            cbo_ncc.EditValue = Data.Data._str_mancc;
        }

        public void GetHangHoa()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoHangHoa", new { action = "GET_DATA" });
            cbo_hanghoa.DataSource = dt;
            cbo_hanghoa.DisplayMember = "mathamchieu";
            cbo_hanghoa.ValueMember = "mathamchieu";
        }

        public void GetChiTietPhieuNhap()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "GET_DATA_MAPHIEU", maphieu = txt_maphieu.Text });
            dt.Columns["maphieu"].AllowDBNull = true;
            dgv_phieunhapkho.DataSource = dt;
            if (dt.Rows.Count == 0) { return; }
            txt_maphieu.Text = dt.Rows[0]["maphieu"].ToString();
            dte_ngaynhap.EditValue = Convert.ToDateTime(dt.Rows[0]["ngaynhap"]);
            cbo_ncc.EditValue = dt.Rows[0]["mancc"];
            txt_diengiai.Text = dt.Rows[0]["diengiai"].ToString();
        }

        public void XoaText()
        {
            txt_diengiai.Text = "";
            cbo_ncc.EditValue = DBNull.Value;
            dte_ngaynhap.EditValue = DateTime.Now.Date;
            txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
        }

        public void LuuPhieuNhapKho2()
        {
            if (string.IsNullOrEmpty(txt_maphieu.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào mã phiếu để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbo_ncc.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn nhà cung cấp để nhập kho.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var i = Convert.ToInt32(ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CHECK_ID", maphieu = txt_maphieu.Text }));
            if (i > 0)
            {
                //Tạo lại mã mới
                txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
            }
            ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "SAVE", maphieu = txt_maphieu.Text, mancc = cbo_ncc.EditValue.ToString(), ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd"), diengiai = txt_diengiai.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });


            LuuChiTietPhieuNhapKho();
            //Xóa text
            txt_diengiai.Text = "";
            cbo_ncc.EditValue = DBNull.Value;


            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "phieunhap"
            };
            msgBroker.Publish();
        }

        #endregion
        public FrmThemNhapKho_XuLyAmKho()
        {
            InitializeComponent();
            dte_ngaynhap.EditValue = DateTime.Now.Date;
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (sender is GridView view)
            {
                int i = view.FocusedRowHandle;
                if (view.FocusedColumn.FieldName == "mathamchieu")
                {
                    if (string.IsNullOrEmpty(cbo_ncc.Text)) { e.Valid = false; e.ErrorText = "Bạn phải chọn nhà cung cấp để thực hiện."; }
                    if (string.IsNullOrEmpty(txt_maphieu.Text)) { e.Valid = false; e.ErrorText = "Bạn phải nhập vào mã phiếu nhập kho."; }
                    var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoHangHoa>("proKhoHangHoa", new { action = "GET_DATA_MAHANG", mathamchieu = e.Value.ToString() });
                    if (dt == null)
                    {
                        e.ErrorText = $"Mã hàng hóa({ e.Value}) không tồn tại.";
                        e.Valid = false;
                    }
                    view.SetRowCellValue(i, "tenhanghoa", dt.tenhanghoa);
                    view.SetRowCellValue(i, "tendvt", dt.tendvt);
                    view.SetRowCellValue(i, "mahanghoa", dt.mahanghoa);
                    view.SetRowCellValue(i, "soluong", 0);
                    view.SetRowCellValue(i, "soducuoiky", dt.soducuoiky);
                    view.SetRowCellValue(i, "dongia", dt.dongia);
                    view.SetRowCellValue(i, "thoigian", DateTime.Now);
                    view.SetRowCellValue(i, "nguoitd", Data.Data._strtendangnhap.ToUpper());
                }
                else if (view.FocusedColumn.FieldName == "soluong")
                {
                    if (Data.Data.IsNumber(e.Value.ToString()) == false)
                    {
                        e.Valid = false;
                        e.ErrorText = "Dữ liệu bạn nhập vào không đúng.";
                    }
                    else
                    {
                        var s = Convert.ToDouble(e.Value) * Convert.ToDouble(view.GetRowCellValue(i, "dongia"));
                        view.SetRowCellValue(i, "thanhtien", s);
                    }
                }
                else if (view.FocusedColumn.FieldName == "dongia")
                {
                    if (Data.Data.IsNumber(e.Value.ToString()) == false)
                    {
                        e.Valid = false;
                        e.ErrorText = "Dữ liệu bạn nhập vào không đúng.";
                    }
                    else
                    {
                        view.SetRowCellValue(i, "thanhtien", Convert.ToDouble(e.Value) * Convert.ToDouble(view.GetRowCellValue(i, "soluong")));
                    }
                }
            }
        }

        private void Btn_lammoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetChiTietPhieuNhap();
        }

        private void Btn_them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvView_PhieuNhap.RowCount > 0)
            {
                LuuPhieuNhapKho2();
            }
            txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
            Data.Data._edit = false;
            txt_maphieu.Properties.ReadOnly = false;
            dte_ngaynhap.Properties.ReadOnly = false;
            XoaText();
        }

        private void Txt_maphieu_TextChanged(object sender, EventArgs e)
        {
            GetChiTietPhieuNhap();
        }

        private void Btn_xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Bạn có muốn xóa phiếu nhập kho " + txt_maphieu.Text + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "DELETE", maphieu = txt_maphieu.Text });
                GetChiTietPhieuNhap();
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "phieunhap"
                };
                msgBroker.Publish();
            }
        }

        private void btn_luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_maphieu.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào mã phiếu để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbo_ncc.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn nhà cung cấp để nhập kho.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Data.Data._edit == false) //Chỉnh sửa phiếu không tạo
            {
                var i = Convert.ToInt32(ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CHECK_ID", maphieu = txt_maphieu.Text }));
                if (i > 0)
                {
                    //Tạo lại mã mới
                    txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
                }
            }
            if (grvView_PhieuNhap.RowCount == 0)
            {
                XtraMessageBox.Show("Bạn vui lòng nhập các mã hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "SAVE", maphieu = txt_maphieu.Text, mancc = cbo_ncc.EditValue.ToString(), ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd"), diengiai = txt_diengiai.Text, nguoitd = Data.Data._strtendangnhap.ToUpper(), thanhtoan = true });

            LuuChiTietPhieuNhapKho();
            GetChiTietPhieuNhap();
            XtraMessageBox.Show("Đã cập nhật thành công phiếu nhập.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Data.Data._edit = true;
            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "phieunhap"
            };
            msgBroker.Publish();
        }

        public void LuuChiTietPhieuNhapKho()
        {
            cbo_ncc.Focus();
            if (grvView_PhieuNhap.RowCount <= 0)
            {
                XtraMessageBox.Show("Vui lòng nhập vào vật tư nhập kho", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgv_phieunhapkho.Focus();
            }
            else
            {
                for (var i = 0; i <= grvView_PhieuNhap.RowCount; i++)
                {
                    var dr = grvView_PhieuNhap.GetDataRow(Convert.ToInt32(i));
                    if (dr is null)
                    {
                        break;
                    }
                    switch (dr.RowState)
                    {
                        case DataRowState.Added:

                            ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuNhap", new { action = "SAVE", maphieu = txt_maphieu.Text, mahanghoa = dr["mahanghoa"].ToString(), soluong = Convert.ToDouble(dr["soluong"]), dongia = Convert.ToDouble(dr["dongia"]), ghichu = dr["ghichu"].ToString(), nguoitd = Data.Data._strtendangnhap.ToUpper() });
                            break;
                        case DataRowState.Modified:
                            ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuNhap", new { action = "UPDATE", soluong = Convert.ToDouble(dr["soluong"]), dongia = Convert.ToDouble(dr["dongia"]), ghichu = dr["ghichu"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), maphieu = txt_maphieu.Text, mahanghoa = dr["mahanghoa"].ToString() });
                            break;
                    }
                }
            }
        }

        private void GridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = grvView_PhieuNhap.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa mã hàng " + grvView_PhieuNhap.GetRowCellValue(i, "mahanghoa") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuNhap", new { action = "DELETE", maphieu = txt_maphieu.Text, mahanghoa = grvView_PhieuNhap.GetRowCellValue(i, "mahanghoa").ToString() });
                    GetChiTietPhieuNhap();
                }
            }
        }

        private void Btn_in_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvView_PhieuNhap.RowCount > 0)
            {
                LuuPhieuNhapKho2();
            }
            Data.Data._dtreport = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "GET_DATA_MAPHIEU", maphieu = txt_maphieu.Text });
            if (Data.Data._dtreport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            rptPhieuNhap report = new rptPhieuNhap();
            /// In trực tiếp
#if (!DEBUG)
            report.BindData();
            ReportPrintTool rpt = new ReportPrintTool(report, false);
            rpt.Print();
#endif


#if (DEBUG)
            Data.Data._report = 2;
            FrmHT_Report frm = new FrmHT_Report();
            frm.Show();
#endif
            //Tạo mã mới
            txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
            Data.Data._edit = false;
        }

        private void Btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = @"Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuNhapKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuNhap", new { action = "EXPORT_EXCEL", maphieu = txt_maphieu.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void frm_them_nhapkho_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Data.Data._edit == false) //Chế độ edit không cần lưu
            {
                if (grvView_PhieuNhap.RowCount > 0)
                {
                    LuuPhieuNhapKho2();
                }
            }
            Data.Data._edit = false;
        }

        private void frm_them_nhapkho_Load(object sender, EventArgs e)
        {
            GetNhaCungCap();
            GetHangHoa();
            if (Data.Data._edit == false)
            {
                txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
                XuLyAmKho();
                Data.Data._edit = true;
            }
            else
            {
                var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhieuNhap>("proKhoPhieuNhap", new { action = "GET_DATA_MAPHIEU", maphieu = Data.Data._strmaphieu });
                if (dt == null) { return; }
                txt_maphieu.Text = Data.Data._strmaphieu;
                txt_diengiai.Text = dt.diengiai;
                cbo_ncc.EditValue = dt.mancc;
                dte_ngaynhap.EditValue = dt.ngaynhap;
                cbo_ncc.Focus();
                txt_maphieu.Properties.ReadOnly = true;
                dte_ngaynhap.Properties.ReadOnly = true;
            }
        }

        public void XuLyAmKho()
        {
            ExecSQL.ExecProcedureNonData("prokhoTonKho", new { action = "GET_DATA", option = 4, nguoitd = Data.Data._strtendangnhap.ToUpper() });
            GetChiTietPhieuNhap();
        }

        private void cbo_ncc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Data.Data._int_flag = 2;
                frm_them_ncc frm = new frm_them_ncc();
                frm.ShowDialog();
            }
        }

        private void cbo_ncc_Click(object sender, EventArgs e)
        {
            gridLookUpEdit1View.FocusedRowHandle = GridControl.AutoFilterRowHandle;
            gridLookUpEdit1View.FocusedColumn = gridLookUpEdit1View.Columns["ncc"];
            gridLookUpEdit1View.ShowEditor();
        }

        private void date_ngaynhap_TextChanged(object sender, EventArgs e)
        {
            if (Data.Data._edit == false)
            {
                txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
            }
        }

        private void cbo_ncc_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    grvView_PhieuNhap.Focus();
                    grvView_PhieuNhap.FocusedRowHandle = GridControl.NewItemRowHandle;
                    grvView_PhieuNhap.FocusedColumn = grvView_PhieuNhap.VisibleColumns[1];
                    break;
            }
        }
    }
}
