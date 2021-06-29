using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
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
    public partial class FrmThemNhapkho : XtraForm
    {
        private string _strmaphieuxuat = "";
        public bool print = false;
        #region "Function"
        public void GetNhaCungCap()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoNhaCungCap", new { action = "GET_DATA" });
            cbo_ncc.Properties.DataSource = dt;
            cbo_ncc.Properties.DisplayMember = "ncc";
            cbo_ncc.Properties.ValueMember = "mancc";
            cbo_ncc.EditValue = "NCC000";
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
            grcPhieuNhap.DataSource = dt;
        }

        public void XoaText()
        {
            txt_diengiai.Text = "";
            cbo_ncc.EditValue = DBNull.Value;
            dte_ngaynhap.EditValue = DateTime.Now.Date;
            txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
        }

        public void LuuPhieuXuatKho()
        {
            _strmaphieuxuat = ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CREATE_ID", ngayxuat = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
            ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "SAVE", maphieu = _strmaphieuxuat, ngayxuat = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd"), makh = "KH0000", diengiai = "Xuất kho sau khi chuyển đổi", nguoitd = Data.Data._strtendangnhap.ToUpper() });
            //Ghi lại nhật ký
            Data.Data._run_history_log($"Thêm mới phiếu xuất quy đổi ({txt_maphieu.Text}) của nhà cung cấp ({cbo_ncc.Text})", "Phiếu Xuất");

            ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuXuat", new { action = "SAVE", maphieu = _strmaphieuxuat, mahanghoa = cbo_mathang2.EditValue.ToString(), soluong = Convert.ToDecimal(txt_soluong.Text), dongia = 0, ghichu = "", nguoitd = Data.Data._strtendangnhap.ToUpper() });

            //Ghi lại nhật ký
            Data.Data._run_history_log($"Thêm mới chi tiết phiếu xuất quy đổi ({txt_maphieu.Text}) của mã hàng ({cbo_mathang2.EditValue}: {cbo_mathang2.Text}), SL: {txt_soluong.Text}", "Phiếu Xuất");
        }

        public void GetMaHangQuyDoi()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoQuyDoi", new { action = "GET_DATA2", mahanghoa = "" });
            cbo_mathang2.Properties.DataSource = dt;
            cbo_mathang2.Properties.DisplayMember = "tenhanghoa";
            cbo_mathang2.Properties.ValueMember = "mahanghoa";
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
            if (Data.Data._edit == false)
            {
                var i = Convert.ToInt32(ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CHECK_ID", maphieu = txt_maphieu.Text }));
                if (i > 0)
                {
                    txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
                }
            }
            ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "SAVE", maphieu = txt_maphieu.Text, mancc = cbo_ncc.EditValue.ToString(), ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd"), diengiai = txt_diengiai.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
            //Ghi lại nhật ký
            Data.Data._run_history_log($"Thêm mới phiếu nhập ({txt_maphieu.Text}) của nhà cung cấp ({cbo_ncc.Text})", "Phiếu Nhập");
            if (xtraTabControl1.SelectedTabPage == tabQuyDoi)
            {
                CapNhatQuyDoi();
                //Tạo phiếu xuất kho sau khi chuyển đổi
                LuuPhieuXuatKho();
                txt_soluong.Text = "0";
                cbo_mathang2.EditValue = DBNull.Value;
                xtraTabControl1.SelectedTabPage = tabPhieuNhap;
                GetChiTietPhieuNhap();
            }
            else
            {
                LuuChiTietPhieuNhapKho();
                //Xóa text
                txt_diengiai.Text = "";
                cbo_ncc.EditValue = DBNull.Value;
            }

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
            for (var i = 0; i <= grvView_NhapKho.RowCount; i++)
            {
                var dr = grvView_NhapKho.GetDataRow(Convert.ToInt32(i));
                if (dr is null)
                {
                    break;
                }
                switch (dr.RowState)
                {
                    case DataRowState.Added:
                        ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuNhap", new { action = "SAVE", maphieu = txt_maphieu.Text, mahanghoa = dr["mahanghoa"].ToString(), soluong = Convert.ToDouble(dr["soluong"]), dongia = Convert.ToDouble(dr["dongia"]), ghichu = dr["ghichu"].ToString(), nguoitd = Data.Data._strtendangnhap.ToUpper() });

                        //Ghi lại nhật ký
                        Data.Data._run_history_log($"Thêm mới chi tiết phiếu nhập ({txt_maphieu.Text}) có tên hàng ({dr["mahanghoa"]}: {dr["tenhanghoa"]}, SL: {dr["soluong"]})", "Phiếu Nhập");
                        break;
                    case DataRowState.Modified:
                        ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuNhap", new { action = "UPDATE", soluong = Convert.ToDouble(dr["soluong"]), dongia = Convert.ToDouble(dr["dongia"]), ghichu = dr["ghichu"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), maphieu = txt_maphieu.Text, mahanghoa = dr["mahanghoa"].ToString() });

                        //Ghi lại nhật ký
                        Data.Data._run_history_log($"Thêm thay đổi chi tiết phiếu nhập ({txt_maphieu.Text}) có tên hàng ({dr["mahanghoa"]}: {dr["tenhanghoa"]}, SL: {dr["soluong"]})", "Phiếu Nhập");
                        break;
                }
            }
        }

        public void CapNhatQuyDoi()
        {
            if (cbo_ncc.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn nhà cung cấp để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                xtraTabControl1.SelectedTabPage = tabPhieuNhap;
                cbo_ncc.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_soluong.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào số lượng cần quy đổi.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_soluong.Focus();
                return;
            }
            ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuNhap", new { action = "SAVE_QUYDOI", maphieu = txt_maphieu.Text, mahanghoa = cbo_mathang2.EditValue.ToString(), nguoitd = Data.Data._strtendangnhap.ToUpper(), soluong = Convert.ToDecimal(txt_soluong.Text), ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") });
        }

        #endregion
        public FrmThemNhapkho()
        {
            InitializeComponent();
            dte_ngaynhap.EditValue = DateTime.Now.Date;
            Shown += FrmThemNhapkho_Shown;
        }

        private void FrmThemNhapkho_Shown(object sender, EventArgs e)
        {
            cbo_ncc.Focus();
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
            if (grvView_NhapKho.RowCount > 0)
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
                //Ghi lại log
                Data.Data._run_history_log($"Đã xóa phiếu nhập kho ({txt_maphieu.Text})", "Phiếu Nhập");
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
            if (xtraTabControl1.SelectedTabPage == tabQuyDoi)
            {
                ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "SAVE", maphieu = txt_maphieu.Text, mancc = cbo_ncc.EditValue.ToString(), ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd"), diengiai = txt_diengiai.Text, nguoitd = Data.Data._strtendangnhap.ToUpper(), thanhtoan = Convert.ToBoolean(chkThanhToan.Checked) });
                //Ghi lại nhật ký
                Data.Data._run_history_log($"Thêm mới phiếu nhập ({txt_maphieu.Text}) của nhà cung cấp ({cbo_ncc.Text})", "Phiếu Nhập");

                CapNhatQuyDoi();
                //Tạo phiếu xuất kho sau khi chuyển đổi

                txt_soluong.Text = "0";
                lblSLTon.Text = "0";
                cbo_mathang2.EditValue = DBNull.Value;
                xtraTabControl1.SelectedTabPage = tabPhieuNhap;
                //Load chi tiết
                GetChiTietPhieuNhap();
                XtraMessageBox.Show("Đã cập nhật thành công phiếu nhập và phiếu xuất kho " + _strmaphieuxuat + ".", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Tạo mã phiếu
            }
            else
            {
                if (grvView_NhapKho.RowCount == 0)
                {
                    XtraMessageBox.Show("Bạn vui lòng nhập vào chi tiết mã hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "SAVE", maphieu = txt_maphieu.Text, mancc = cbo_ncc.EditValue.ToString(), ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd"), diengiai = txt_diengiai.Text, nguoitd = Data.Data._strtendangnhap.ToUpper(), thanhtoan = Convert.ToBoolean(chkThanhToan.Checked) });
                //Ghi lại nhật ký
                Data.Data._run_history_log($"Thêm mới phiếu nhập ({txt_maphieu.Text}) của nhà cung cấp ({cbo_ncc.Text})", "Phiếu Nhập");

                LuuChiTietPhieuNhapKho();
                Data.Data._edit = true;
                XtraMessageBox.Show("Đã cập nhật thành công phiếu nhập.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "phieunhap"
            };
            msgBroker.Publish();
        }

        private void GridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = grvView_NhapKho.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa mã hàng " + grvView_NhapKho.GetRowCellValue(i, "mahanghoa") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {

                    ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuNhap", new { action = "DELETE", maphieu = txt_maphieu.Text, mahanghoa = grvView_NhapKho.GetRowCellValue(i, "mahanghoa").ToString() });
                    grvView_NhapKho.DeleteRow(i);
                }
            }
        }

        private void Btn_in_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvView_NhapKho.RowCount == 0)
            {
                XtraMessageBox.Show("Không có dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LuuPhieuNhapKho2();
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
            txt_maphieu.Properties.ReadOnly = false;
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
            var i = Convert.ToInt32(ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CHECK_ID", maphieu = txt_maphieu.Text }));
            if (i == 0)
            {
                if (grvView_NhapKho.RowCount > 0)
                {
                    var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr != DialogResult.Yes) { return; }
                    LuuPhieuNhapKho2();
                }
            }
            Data.Data._edit = false;
        }

        private void frm_them_nhapkho_Load(object sender, EventArgs e)
        {
            GetNhaCungCap();
            GetHangHoa();
            GetMaHangQuyDoi();
            cbo_ncc.Focus();
            if (Data.Data._edit == false)
            {
                txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("proKhoPhieuNhap", new { action = "CREATE_ID", ngaynhap = Convert.ToDateTime(dte_ngaynhap.EditValue).ToString("yyyyMMdd") }).ToString();
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

        private void cbo_ncc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Data.Data._int_flag = 2;
                FrmThemNCC frm = new FrmThemNCC();
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

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tabPhieuNhap)
            {
                grvView_NhapKho.OptionsBehavior.ReadOnly = false;
            }
            else if (xtraTabControl1.SelectedTabPage == tabQuyDoi)
            {
                grvView_NhapKho.OptionsBehavior.ReadOnly = true;
            }
        }

        private void cbo_mathang2_EditValueChanged(object sender, EventArgs e)
        {
            if (cbo_mathang2.EditValue == null) { return; }
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoQuyDoi>("prokhoQuyDoi", new { action = "GET_DATA2", mahanghoa = cbo_mathang2.EditValue.ToString() });
            if (dt == null) { return; }
            lblSLTon.Text = Convert.ToDecimal(dt.slton).ToString();
        }
        private void cbo_ncc_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    grvView_NhapKho.Focus();
                    grvView_NhapKho.FocusedRowHandle = GridControl.NewItemRowHandle;
                    grvView_NhapKho.FocusedColumn = grvView_NhapKho.VisibleColumns[1];
                    break;
            }
        }

    }
}
