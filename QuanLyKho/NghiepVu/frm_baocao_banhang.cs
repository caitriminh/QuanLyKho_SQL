using DevExpress.XtraEditors;
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
    public partial class FrmBaocaoBanhang : XtraForm
    {
        #region "Function"
        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 13 });
            btn_excel.Enabled = dt.inan == true;
            btn_in.Enabled = dt.inan == true;
        }

        public void GetBaoCaoBanHang()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoLoiNhuan", new { action = "GET_DATA", tungay = Convert.ToDateTime(dteTuNgay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(dteDenNgay.EditValue).ToString("yyyyMMdd") });
            grcBaoCaoDoanhThu.DataSource = dt;
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 1;
        }
        #endregion
        public FrmBaocaoBanhang()
        {
            InitializeComponent();
            grvView_BaoCaoDoanhThu.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcBaoCaoDoanhThu, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcBaoCaoDoanhThu, Name);

            grvView_BaoCaoDoanhThu.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcBaoCaoDoanhThu, grvView_BaoCaoDoanhThu); };
            grvView_BaoCaoDoanhThu.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcBaoCaoDoanhThu, Name); };
            dteTuNgay.EditValue = DateTime.Now.Date;
            dteDenNgay.EditValue = DateTime.Now.Date;
            GetKy();
        }

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetBaoCaoBanHang();
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = @"Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BaoCaoDoanhThu_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                grvView_BaoCaoDoanhThu.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void btn_in_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_BaoCaoDoanhThu.GetSelectedRows() where grvView_BaoCaoDoanhThu.IsDataRow(f) select grvView_BaoCaoDoanhThu.GetDataRow(f);
            if (grvView_BaoCaoDoanhThu.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 4;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn các mặt hàng trong danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetBaoCaoBanHang();
        }

        private void frm_baocao_banhang_Load(object sender, EventArgs e)
        {
            GetBaoCaoBanHang();
            GetPhanQuyen();
            //Ghi log
            Data.Data._run_history_log("Xem danh muc Báo Cáo Bán Hàng.", "Báo Cáo Bán Hàng");
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
        }
    }
}
