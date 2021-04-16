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
    public partial class frm_loinhuan : XtraForm
    {
        public frm_loinhuan()
        {
            InitializeComponent();

            grvView_BaoCaoLoiNhuan.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcBaoCaoLoiNhuan, Name); };
            GridViewHelper.SaveAndRestoreLayout(grcBaoCaoLoiNhuan, Name);

            grvView_BaoCaoLoiNhuan.CustomDrawRowIndicator += (ss, ee) => { AutoNumberGridView.GridView_CustomDrawRowIndicator(ss, ee, grcBaoCaoLoiNhuan, grvView_BaoCaoLoiNhuan); };
            grvView_BaoCaoLoiNhuan.PopupMenuShowing += (s, e) => { GridViewHelper.AddFontAndColortoPopupMenuShowing(s, e, grcBaoCaoLoiNhuan, Name); };
        }

        #region "Function"

        public void GetPhanQuyen()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 18 });
            btn_excel.Enabled = dt.inan == true;
            btn_in.Enabled = dt.inan == true;
        }

        public void GetBaoCaoLoiNhuan()
        {
            var dt = ExecSQL.ExecProcedureDataAsDataTable("prokhoBaoCaoLoiNhuan", new { action = "GET_DATA", tungay = Convert.ToDateTime(dteTuNgay.EditValue).ToString("yyyyMMdd"), denngay = Convert.ToDateTime(dteDenNgay.EditValue).ToString("yyyyMMdd") });
            grcBaoCaoLoiNhuan.DataSource = dt;
        }

        #endregion

        private void btn_NapLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetBaoCaoLoiNhuan();
        }

        private void btn_excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BaoCaoLoiNhuan_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                grcBaoCaoLoiNhuan.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(xtraSaveFileDialog1.FileName);
            }
        }

        private void btn_in_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRow = from f in grvView_BaoCaoLoiNhuan.GetSelectedRows() where grvView_BaoCaoLoiNhuan.IsDataRow(f) select grvView_BaoCaoLoiNhuan.GetDataRow(f);
            if (grvView_BaoCaoLoiNhuan.SelectedRowsCount > 0)
            {
                Data.Data._dtreport = selectedRow.CopyToDataTable();
                Data.Data._report = 9;
                FrmHT_Report frm = new FrmHT_Report();
                frm.Show();
            }
            else
            {
                XtraMessageBox.Show("Bạn vui lòng chọn danh sách để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_tim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetBaoCaoLoiNhuan();
        }

        private void frm_loinhuan_Load(object sender, EventArgs e)
        {
            dteTuNgay.EditValue = DateTime.Now.Date.ToString("01/MM/yyyy");
            dteDenNgay.EditValue = DateTime.Now.Date.ToString("dd/MM/yyyy");
            GetPhanQuyen();
            GetBaoCaoLoiNhuan();
        }

    }
}
