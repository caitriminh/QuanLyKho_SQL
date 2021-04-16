using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
namespace QuanLyKho.NghiepVu
{
    public partial class frm_tonghop_chiphi : XtraForm
    {
        #region "Function"
        public void GetLoaiChiPhi()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoLoaiChiPhi", new { action = "GET_DATA" });
            cboLoaiChiPhi.DataSource = dt;
            cboLoaiChiPhi.DisplayMember = "loaichiphi";
            cboLoaiChiPhi.ValueMember = "maloai";
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 4;
        }

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 16 });
            btn_Them.Enabled = dt.luu == true;
            btn_Xoa.Enabled = dt.xoa == true;
            btn_Luu.Enabled = dt.sua == true;
            btn_excel.Enabled = dt.inan == true;
            btn_in.Enabled = dt.inan == true;
        }

        public void GetTongHopChiPhi()
        {
            var x = grvView_TongHopChiPhi.FocusedRowHandle;
            var y = grvView_TongHopChiPhi.TopRowIndex;
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoChiPhi", new { action = "GET_DATA", tungay = Convert.ToDateTime(date_tungay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(date_denngay.EditValue).ToString("yyyyMMdd") });
            grcTongHopChiPhi.DataSource = dt;
            lbl_id.DataBindings.Clear();
            lbl_id.DataBindings.Add("text", dt, "id");
            grvView_TongHopChiPhi.FocusedRowHandle = x;
            grvView_TongHopChiPhi.TopRowIndex = y;
        }

        public void LuuTongHopChiPhi()
        {
            lbl_id.Focus();
            if (grvView_TongHopChiPhi.RowCount <= 0)
            {
                XtraMessageBox.Show("Vui lòng nhập vào các loại chi phí phát sinh.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                grcTongHopChiPhi.Focus();
            }
            else
            {
                for (var i = 0; i <= grvView_TongHopChiPhi.RowCount; i++)
                {
                    var dr = grvView_TongHopChiPhi.GetDataRow(Convert.ToInt32(i));

                    if (ReferenceEquals(dr, null))
                    {
                        break;
                    }
                    switch (dr.RowState)
                    {
                        case DataRowState.Added:

                            //Data.Data._run_cmd($@"insert into tbl_chiphi(ngaynhap, maloaichiphi, sotien, diengiai, nguoitd, thoigian) values ('{Convert.ToDateTime(dr["ngaynhap"]).ToString("yyyy-MM-dd")}','{dr["maloaichiphi"]}','{dr["sotien"]}','{dr["diengiai"]}','{Data.Data._strtendangnhap.ToUpper()}','{DateTime.Now}')");
                            //Ghi lại log
                            //Data.Data._run_history_log($@"Đã thêm chi phí {dr["diengiai"]}.", "Danh mục chi phí");
                            ExecSQL.ExecProcedureNonData("prokhoChiPhi", new { action = "SAVE", ngaynhap = Convert.ToDateTime(dr["ngaynhap"]).ToString("yyyyMMdd"), maloaichiphi = dr["maloaichiphi"].ToString(), sotien = Convert.ToDecimal(dr["sotien"]), diengiai = dr["diengiai"].ToString(), nguoitd = Data.Data._strtendangnhap.ToUpper() });
                            break;
                        case DataRowState.Modified:
                            //Data.Data._run_cmd($@"update tbl_chiphi set ngaynhap='{Convert.ToDateTime(dr["ngaynhap"]).ToString("yyyy-MM-dd")}', maloaichiphi='{dr["maloaichiphi"]}', sotien='{dr["sotien"]}', diengiai='{dr["diengiai"]}', nguoitd2='{Data.Data._strtendangnhap.ToUpper()}', thoigian2='{DateTime.Now}' where id='{dr["id"]}'");
                            //Ghi lại log
                            //Data.Data._run_history_log($@"Đã cập nhật chi phí {dr["loaichiphi"]}.", "Danh mục chi phí");
                            ExecSQL.ExecProcedureNonData("prokhoChiPhi", new { action = "UPDATE", ngaynhap = Convert.ToDateTime(dr["ngaynhap"]).ToString("yyyyMMdd"), maloaichiphi = dr["maloaichiphi"].ToString(), sotien = Convert.ToDecimal(dr["sotien"]), diengiai = dr["diengiai"].ToString(), nguoitd2 = Data.Data._strtendangnhap.ToUpper(), id = Convert.ToInt32(dr["id"]) });
                            break;
                    }
                }
                GetTongHopChiPhi();
            }
        }

        #endregion
        public frm_tonghop_chiphi()
        {
            InitializeComponent();
            grvView_TongHopChiPhi.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcTongHopChiPhi, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcTongHopChiPhi, Name);

            grvView_TongHopChiPhi.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcTongHopChiPhi, grvView_TongHopChiPhi); };
            grvView_TongHopChiPhi.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcTongHopChiPhi, Name); };
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvView_TongHopChiPhi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            GetTongHopChiPhi();
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "TongHopChiPhi_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                grvView_TongHopChiPhi.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void btn_in_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_TongHopChiPhi.GetSelectedRows() where grvView_TongHopChiPhi.IsDataRow(f) select grvView_TongHopChiPhi.GetDataRow(f);
            if (grvView_TongHopChiPhi.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 10;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn các đối tượng danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTongHopChiPhi();
        }

        private void btn_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvView_TongHopChiPhi.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
        }

        private void frm_tonghop_chiphi_Load(object sender, EventArgs e)
        {
            GetKy();
            GetLoaiChiPhi();
            GetTongHopChiPhi();
            GetPhanQuyen();
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LuuTongHopChiPhi();
            XtraMessageBox.Show("Đã cập nhật thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            int i = view.FocusedRowHandle;
            if (view.FocusedColumn.FieldName == "maloaichiphi")
            {
                view.SetRowCellValue(i, "thoigian", DateTime.Now);
                view.SetRowCellValue(i, "nguoitd", Data.Data._strtendangnhap.ToUpper());
            }
            else if (view.FocusedColumn.FieldName == "ngaynhap")
            {
                var ngaythang = Convert.ToDateTime(e.Value).ToString("MM-yyyy");
                view.SetRowCellValue(i, "ngaythang", ngaythang);
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = grvView_TongHopChiPhi.FocusedRowHandle;
            if (ReferenceEquals(e.Column, colXoa))
            {
                var dgr = XtraMessageBox.Show($@"Bạn có muốn xóa chi phí {grvView_TongHopChiPhi.GetRowCellValue(i, "loaichiphi")} này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr == DialogResult.Yes)
                {
                    ExecSQL.ExecProcedureNonData("prokhoChiPhi", new { action = "DELETE", id = Convert.ToInt32(grvView_TongHopChiPhi.GetRowCellValue(i, "id")) });
                    GetTongHopChiPhi();
                    //Ghi lại log
                    //Data.Data._run_history_log("Đã xóa chi phí " + gridView1.GetRowCellValue(i, "loaichiphi") + ".", "Danh mục chi phí");
                }
            }
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            date_tungay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            date_denngay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
        }

        private void btn_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = grvView_TongHopChiPhi.FocusedRowHandle;
            var dgr = XtraMessageBox.Show($@"Bạn có muốn xóa chi phí {grvView_TongHopChiPhi.GetRowCellValue(i, "loaichiphi")} này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr == DialogResult.Yes)
            {
                ExecSQL.ExecProcedureNonData("prokhoChiPhi", new { action = "DELETE", id = Convert.ToInt32(grvView_TongHopChiPhi.GetRowCellValue(i, "id")) });
                GetTongHopChiPhi();
                //Ghi lại log
                //Data.Data._run_history_log("Đã xóa chi phí " + gridView1.GetRowCellValue(i, "loaichiphi") + ".", "Danh mục chi phí");
            }
        }
    }
}
