using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System.Data;

namespace QuanLyKho.NghiepVu
{
    public partial class rpt_baocao_chiphi : XtraReport
    {
        public rpt_baocao_chiphi()
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
            GroupHeader1.GroupFields.Add(new GroupField("thangnam", XRColumnSortOrder.Ascending));
            col_thangnam.DataBindings.Add("Text", null, "thangnam", "Tháng năm: {0}");


            col_ngaynhap.DataBindings.Add("Text", DataSource, "ngaynhap").FormatString = "{0:dd/MM/yyyy}";
            col_diengiai.DataBindings.Add("Text", DataSource, "diengiai");
            col_loaichiphi.DataBindings.Add("Text", DataSource, "loaichiphi");
            col_sotien.DataBindings.Add("Text", DataSource, "sotien").FormatString = "{0:#,##0.0}";

            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");

            col_tong_sotien.DataBindings.Add("Text", DataSource, "sotien");
            col_tong_sotien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_tong_sotien_nhom.DataBindings.Add("Text", DataSource, "sotien");
            col_tong_sotien_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");
        }
    }
}
