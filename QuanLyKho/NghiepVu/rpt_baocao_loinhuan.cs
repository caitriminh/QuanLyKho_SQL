using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System.Data;

namespace QuanLyKho.NghiepVu
{
    public partial class rpt_baocao_loinhuan : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_baocao_loinhuan()
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
            }

            GroupHeader1.GroupFields.Add(new GroupField("nam", XRColumnSortOrder.Ascending));
            col_nam.DataBindings.Add("Text", null, "nam", "Năm: {0}");


            col_thangnam.DataBindings.Add("Text", DataSource, "ngaythang1");
            col_chiphi.DataBindings.Add("Text", DataSource, "chiphi").FormatString = "{0:#,##0.0}";
            col_loinhuangop.DataBindings.Add("Text", DataSource, "loinhuangop").FormatString = "{0:#,##0.0}";
            col_doanhthu.DataBindings.Add("Text", DataSource, "doanhthu").FormatString = "{0:#,##0.0}";
            col_loinhuanrong.DataBindings.Add("Text", DataSource, "loinhuanrong").FormatString = "{0:#,##0.0}";
            col_giavon.DataBindings.Add("Text", DataSource, "giavon").FormatString = "{0:#,##0.0}";

            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");

            col_tong_doanhthu.DataBindings.Add("Text", DataSource, "doanhthu");
            col_tong_doanhthu.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_giavon.DataBindings.Add("Text", DataSource, "giavon");
            col_tong_giavon.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_loinhuan.DataBindings.Add("Text", DataSource, "loinhuanrong");
            col_tong_loinhuan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_chiphi.DataBindings.Add("Text", DataSource, "chiphi");
            col_tong_chiphi.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_loinhuangop.DataBindings.Add("Text", DataSource, "loinhuangop");
            col_tong_loinhuangop.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_doanhthu_nhom.DataBindings.Add("Text", DataSource, "doanhthu");
            col_tong_doanhthu_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_giavon_nhom.DataBindings.Add("Text", DataSource, "giavon");
            col_tong_giavon_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_loinhuan_nhom.DataBindings.Add("Text", DataSource, "loinhuanrong");
            col_tong_loinhuan_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_chiphi_nhom.DataBindings.Add("Text", DataSource, "chiphi");
            col_tong_chiphi_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_loinhuangop_nhom.DataBindings.Add("Text", DataSource, "loinhuangop");
            col_tong_loinhuangop_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");
        }
    }
}
