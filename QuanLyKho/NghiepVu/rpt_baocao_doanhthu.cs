using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System.Data;

namespace QuanLyKho.NghiepVu
{
    public partial class rpt_baocao_doanhthu : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_baocao_doanhthu()
        {
            InitializeComponent();
        }


        public void BindData()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoThongTin>("prokhoThongTin", new { action = "GET_DATA" });
            if (dt != null)
            {
                xrLabel1.Text = dt.tencty;
                xrLabel2.Text = "Đ/c: " + dt.diachi;
                xrLabel3.Text = "Số ĐT: " + dt.sodt;
                lbl_tencty_duoi.Text = dt.tencty + " - " + dt.diachi;
            }

            GroupHeader1.GroupFields.Add(new GroupField("ngayxuat", XRColumnSortOrder.Ascending));
            col_ngayxuat.DataBindings.Add("Text", null, "ngayxuat", "Ngày bán hàng: {0:dd/MM/yyyy}");


            col_mahanghoa.DataBindings.Add("Text", DataSource, "mahanghoa");
            col_tenhanghoa.DataBindings.Add("Text", DataSource, "tenhanghoa");
            col_slban.DataBindings.Add("Text", DataSource, "soluong").FormatString = "{0:#,##0.0}";
            col_dvt.DataBindings.Add("Text", DataSource, "tendvt");
            col_doanhthu.DataBindings.Add("Text", DataSource, "doanhthu").FormatString = "{0:#,##0.00}";
            col_loinhuan.DataBindings.Add("Text", DataSource, "loinhuan").FormatString = "{0:#,##0.00}";
            col_giavon.DataBindings.Add("Text", DataSource, "giavon").FormatString = "{0:#,##0.00}";

            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");

            col_tong_doanhthu.DataBindings.Add("Text", DataSource, "doanhthu");
            col_tong_doanhthu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_giavon.DataBindings.Add("Text", DataSource, "giavon");
            col_tong_giavon.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_loinhuan.DataBindings.Add("Text", DataSource, "loinhuan");
            col_tong_loinhuan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_slban.DataBindings.Add("Text", DataSource, "soluong");
            col_tong_slban.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_doanhthu_nhom.DataBindings.Add("Text", DataSource, "doanhthu");
            col_tong_doanhthu_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_giavon_nhom.DataBindings.Add("Text", DataSource, "giavon");
            col_tong_giavon_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_loinhuan_nhom.DataBindings.Add("Text", DataSource, "loinhuan");
            col_tong_loinhuan_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_slban_nhom.DataBindings.Add("Text", DataSource, "soluong");
            col_tong_slban_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.00}");
        }
    }
}
