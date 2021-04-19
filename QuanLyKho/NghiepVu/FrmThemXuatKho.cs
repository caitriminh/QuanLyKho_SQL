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
using VB = Microsoft.VisualBasic.Strings;

namespace QuanLyKho.NghiepVu
{
    public partial class FrmThemXuatKho : XtraForm
    {
        #region "Function"
        public void GetKhachHang()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoKhachHang", new { action = "GET_DATA" });
            cbo_khachhang.Properties.DataSource = dt;
            cbo_khachhang.Properties.DisplayMember = "tenkh";
            cbo_khachhang.Properties.ValueMember = "makh";
            cbo_khachhang.EditValue = "KH0000";
        }

        public void GetHangHoa()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("proKhoHangHoa", new { action = "GET_DATA" });
            cbo_hanghoa.DataSource = dt;
            cbo_hanghoa.DisplayMember = "mathamchieu";
            cbo_hanghoa.ValueMember = "mathamchieu";
        }

        public async void GetChiTietPhieuXuat()
        {
            var x = grvViewPhieuXuat.FocusedRowHandle;
            var y = grvViewPhieuXuat.TopRowIndex;
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("prokhoChiTietPhieuXuat", new { action = "GET_DATA_MAPHIEU", maphieu = txt_maphieu.Text });
            dt.Columns["maphieu"].AllowDBNull = true;
            //dt.Columns["soducuoiky"].AllowDBNull = true;
            dgv_PhieuXuat.DataSource = dt;
            grvViewPhieuXuat.FocusedRowHandle = x;
            grvViewPhieuXuat.TopRowIndex = y;
        }


        public void LuuChiTietPhieuXuatKho()
        {
            cbo_khachhang.Focus();
            for (var i = 0; i <= grvViewPhieuXuat.RowCount; i++)
            {
                var dr = grvViewPhieuXuat.GetDataRow(Convert.ToInt32(i));
                if (dr is null)
                {
                    break;
                }
                switch (dr.RowState)
                {
                    case DataRowState.Added:
                        ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuXuat", new { action = "SAVE", maphieu = txt_maphieu.Text, mahanghoa = dr["mahanghoa"].ToString(), soluong = Convert.ToDouble(dr["soluong"]), dongia = Convert.ToDouble(dr["dongia"]), ghichu = dr["ghichu"].ToString(), nguoitd = Data.Data._strtendangnhap.ToUpper() });
                        //Ghi log
                        Data.Data._run_history_log($"Thêm chi tiết phiếu xuất ({txt_maphieu.Text}) có các tên hàng ({dr["mahanghoa"]} - {dr["tenhanghoa"]})), SL: {dr["soluong"]}", "Phiếu Xuất");
                        break;
                    case DataRowState.Modified:
                        ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuXuat", new { action = "UPDATE", maphieu = txt_maphieu.Text, mahanghoa = dr["mahanghoa"].ToString(), soluong = Convert.ToDouble(dr["soluong"]), dongia = Convert.ToDouble(dr["dongia"]), ghichu = dr["ghichu"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper() });

                        //Ghi log
                        Data.Data._run_history_log($"Thay đổi thông tin chi tiết phiếu xuất ({txt_maphieu.Text}) có các tên hàng ({dr["mahanghoa"]} - {dr["tenhanghoa"]})), SL: {dr["soluong"]}", "Phiếu Xuất");
                        break;

                }
            }
        }

        public void LuuPhieuXuatKho2()
        {
            if (string.IsNullOrEmpty(txt_maphieu.Text))
            {
                XtraMessageBox.Show("Bạn phải nhập vào mã phiếu để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbo_khachhang.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn khách hàng để xuất kho.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Data.Data._edit == false) //Chỉnh sửa phiếu không tạo
            {
                var i = Convert.ToInt32(ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CHECK_ID", maphieu = txt_maphieu.Text }));
                if (i > 0)
                {
                    //Tạo lại mã mới
                    txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CREATE_ID", ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd") }).ToString();
                }
            }
            ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "SAVE", maphieu = txt_maphieu.Text, ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd"), makh = cbo_khachhang.EditValue.ToString(), diengiai = txt_diengiai.Text, nguoitd = Data.Data._strtendangnhap.ToUpper() });
            //Ghi log
            Data.Data._run_history_log($"Thêm phiếu xuất ({txt_maphieu.Text}) của khách hàng ({cbo_khachhang.Text})", "Phiếu Xuất");
            LuuChiTietPhieuXuatKho();

            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "phieuxuat"
            };
            msgBroker.Publish();
        }
        #endregion
        public FrmThemXuatKho()
        {
            InitializeComponent();
            date_ngayxuat.EditValue = DateTime.Now.Date;
            Shown += Frm_them_xuatkho_Shown;
        }

        private void Frm_them_xuatkho_Shown(object sender, EventArgs e)
        {
            cbo_khachhang.Focus();
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            int i = view.FocusedRowHandle;
            if (view.FocusedColumn.FieldName == "mathamchieu")
            {
                if (string.IsNullOrEmpty(cbo_khachhang.Text)) { e.Valid = false; e.ErrorText = "Bạn phải chọn khách hàng để thực hiện. Nhấn ESC để nhập lại"; return; }
                if (string.IsNullOrEmpty(txt_maphieu.Text)) { e.Valid = false; e.ErrorText = "Bạn phải nhập vào mã phiếu xuất kho. Nhấn ESC để nhập lại"; return; }
                var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoHangHoa>("proKhoHangHoa", new { action = "GET_DATA_MAHANG", mathamchieu = e.Value.ToString() });
                if (dt == null)
                {
                    e.ErrorText = "Mã hàng hóa nhập không tồn tại.";
                    e.Valid = false;
                }
                if (Convert.ToDecimal(dt.soducuoiky) <= 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Số dư của mã hàng " + e.Value + " không đủ để thực hiện xuất kho";
                    return;
                }
                view.SetRowCellValue(i, "tenhanghoa", dt.tenhanghoa);
                view.SetRowCellValue(i, "tendvt", dt.tendvt);
                view.SetRowCellValue(i, "mahanghoa", dt.mahanghoa);
                view.SetRowCellValue(i, "soducuoiky", dt.soducuoiky);
                view.SetRowCellValue(i, "soluong", 0);
                view.SetRowCellValue(i, "dongia", dt.dongiaxuat);
                view.SetRowCellValue(i, "thanhtien", 0);
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
                    view.SetRowCellValue(i, "thanhtien", Convert.ToDouble(e.Value) * Convert.ToDouble(view.GetRowCellValue(i, "dongia")));
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

        private void btn_lammoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetChiTietPhieuXuat();
        }

        private void btn_them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvViewPhieuXuat.RowCount > 0)
            {
                LuuPhieuXuatKho2();
            }
            //Tạo mã phiếu
            txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CREATE_ID", ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd") }).ToString();
            Data.Data._edit = false;
            cbo_khachhang.EditValue = DBNull.Value;
            txt_diengiai.Text = "";
        }

        private void txt_maphieu_TextChanged(object sender, EventArgs e)
        {
            GetChiTietPhieuXuat();
        }

        private void btn_xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Bạn có muốn xóa phiếu xuất kho " + txt_maphieu.Text + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "DELETE", maphieu = txt_maphieu.Text });
                if (VB.Left(txt_maphieu.Text, 2) == "XT")
                {
                    ExecSQL.ExecProcedureNonData("proKhoPhieuNhap", new { action = "DELETE", maphieu = txt_maphieu.Text });
                }
                GetChiTietPhieuXuat();
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "phieuxuat"
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
            if (cbo_khachhang.EditValue == null)
            {
                XtraMessageBox.Show("Bạn phải chọn khách hàng để xuất kho.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Data.Data._edit == false)
            {
                var i = Convert.ToInt32(ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CHECK_ID", maphieu = txt_maphieu.Text }));
                if (i > 0)
                {
                    //Tạo lại mã mới
                    txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CREATE_ID", ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd") }).ToString();
                }
            }
            if (grvViewPhieuXuat.RowCount == 0)
            {
                XtraMessageBox.Show("Bạn vui lòng nhập vào chi tiết mã hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExecSQL.ExecProcedureNonData("prokhoPhieuXuat", new { action = "SAVE", maphieu = txt_maphieu.Text, ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd"), makh = cbo_khachhang.EditValue.ToString(), diengiai = txt_diengiai.Text, nguoitd = Data.Data._strtendangnhap.ToUpper(), thanhtoan = Convert.ToBoolean(chkThanhToan.Checked) });
            //Ghi log
            Data.Data._run_history_log($"Thêm phiếu xuất ({txt_maphieu.Text}) của khách hàng ({cbo_khachhang.Text})", "Phiếu Xuất");
            LuuChiTietPhieuXuatKho();
            GetChiTietPhieuXuat();
            Data.Data._edit = true;
            XtraMessageBox.Show("Đã cập nhật thành công phiếu xuất.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Gửi dữ liệu
            var msgBroker = new MessageBroker
            {
                data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                task = "phieuxuat"
            };
            msgBroker.Publish();
        }

        private void gridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = grvViewPhieuXuat.FocusedRowHandle;
            if (ReferenceEquals(e.Column, col_xoa))
            {
                var dgr = XtraMessageBox.Show("Bạn có muốn xóa mã hàng " + grvViewPhieuXuat.GetRowCellValue(i, "mahanghoa") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoChiTietPhieuXuat", new { action = "DELETE", maphieu = txt_maphieu.Text, mahanghoa = grvViewPhieuXuat.GetRowCellValue(i, "mahanghoa").ToString() });
                    grvViewPhieuXuat.DeleteRow(i);
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "phieuxuat"
                    };
                    msgBroker.Publish();
                }
            }
        }

        private void btn_in_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvViewPhieuXuat.RowCount == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LuuPhieuXuatKho2();
            Data.Data._dtreport = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "GET_DATA_MAPHIEU", maphieu = txt_maphieu.Text });
            if (Data.Data._dtreport.Rows.Count > 0)
            {
                rptPhieuXuat report = new rptPhieuXuat();
                /// In trực tiếp
#if (!DEBUG)
                report.BindData();
                ReportPrintTool rpt = new ReportPrintTool(report, false);
                rpt.Print();
#endif


#if (DEBUG)
                Data.Data._report = 3;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
#endif
            }
            else
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Tạo mã phiếu mới
            txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CREATE_ID", ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd") }).ToString();
            txt_maphieu.Properties.ReadOnly = false;
            Data.Data._edit = false;
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuXuatKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiTietPhieuXuat", new { action = "EXPORT_EXCEL", maphieu = txt_maphieu.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void frm_them_nhapkho_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Data._edit = false;
            var i = Convert.ToInt32(ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CHECK_ID", maphieu = txt_maphieu.Text }));
            if (i == 0) //Nếu chưa lưu 
            {
                if (grvViewPhieuXuat.RowCount > 0)
                {
                    var dgr = XtraMessageBox.Show("Bạn có muốn lưu lại không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dgr != DialogResult.Yes) { return; }
                    LuuPhieuXuatKho2();
                }
            }
        }

        private void frm_them_xuatkho_Load(object sender, EventArgs e)
        {
            GetKhachHang();
            GetHangHoa();
            if (Data.Data._edit == false)
            {
                txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CREATE_ID", ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd") }).ToString();
            }
            else
            {
                var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhieuXuat>("prokhoPhieuXuat", new { action = "GET_DATA_MAPHIEU", maphieu = Data.Data._strmaphieu });
                if (dt == null) { return; }
                txt_maphieu.Text = Data.Data._strmaphieu;
                txt_diengiai.Text = dt.diengiai;
                cbo_khachhang.EditValue = dt.makh;
                date_ngayxuat.EditValue = dt.ngayxuat;
                txt_maphieu.Properties.ReadOnly = true;
                date_ngayxuat.Properties.ReadOnly = true;
            }
        }

        private void cbo_khachhang_Click(object sender, EventArgs e)
        {
            gridLookUpEdit1View.FocusedRowHandle = GridControl.AutoFilterRowHandle;
            gridLookUpEdit1View.FocusedColumn = gridLookUpEdit1View.Columns["tenkh"];
            gridLookUpEdit1View.ShowEditor();
        }

        private void cbo_khachhang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Data.Data._int_flag = 2;
                frm_them_khachhang frm = new frm_them_khachhang();
                frm.ShowDialog();
            }
        }

        private void date_ngayxuat_TextChanged(object sender, EventArgs e)
        {
            if (Data.Data._edit == false) { txt_maphieu.Text = ExecSQL.ExecProcedureSacalar("prokhoPhieuXuat", new { action = "CREATE_ID", ngayxuat = Convert.ToDateTime(date_ngayxuat.EditValue).ToString("yyyyMMdd") }).ToString(); }
        }

        private void cbo_khachhang_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    grvViewPhieuXuat.Focus();
                    grvViewPhieuXuat.FocusedRowHandle = GridControl.NewItemRowHandle;
                    grvViewPhieuXuat.FocusedColumn = grvViewPhieuXuat.VisibleColumns[1];
                    break;
            }
        }

    }
}
