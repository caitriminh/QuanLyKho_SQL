using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System.Data;

namespace QuanLyKho.NghiepVu
{
    public partial class rpt_baocao_tonkho_soluong : XtraReport
    {
        public rpt_baocao_tonkho_soluong()
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
            lbl_thang.DataBindings.Add("Text", null, "ngaythang", "(Tháng {0:MM/yyyy})");
            GroupHeader1.GroupFields.Add(new GroupField("nhomhang", XRColumnSortOrder.Ascending));
            col_nhomhang.DataBindings.Add("Text", null, "nhomhang", "Nhóm hàng: {0}");


            col_mahanghoa.DataBindings.Add("Text", DataSource, "mathamchieu");
            col_tenhanghoa.DataBindings.Add("Text", DataSource, "tenhanghoa");
            col_dvt.DataBindings.Add("Text", DataSource, "tendvt");
            col_sodudauky.DataBindings.Add("Text", DataSource, "sodudauky").FormatString = "{0:#,##0.0}";

            col_soducuoiky.DataBindings.Add("Text", DataSource, "soducuoiky").FormatString = "{0:#,##0.0}";


            col_slnhap.DataBindings.Add("Text", DataSource, "slnhap").FormatString = "{0:#,##0.0}";

            col_slxuat.DataBindings.Add("Text", DataSource, "slxuat").FormatString = "{0:#,##0.0}";


            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");

            col_tong_sodudauky.DataBindings.Add("Text", DataSource, "sodaudauky");
            col_tong_sodudauky.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");



            col_tong_slnhap.DataBindings.Add("Text", DataSource, "slnhap");
            col_tong_slnhap.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");



            col_tong_slxuat.DataBindings.Add("Text", DataSource, "slxuat");
            col_tong_slxuat.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_cuoiky.DataBindings.Add("Text", DataSource, "soducuoiky");
            col_tong_cuoiky.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.00}");


            //----------------------
            col_tong_sodudauky_nhom.DataBindings.Add("Text", DataSource, "sodaudauky");
            col_tong_sodudauky_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");



            col_tong_slnhap_nhom.DataBindings.Add("Text", DataSource, "slnhap");
            col_tong_slnhap_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");



            col_tong_slxuat_nhom.DataBindings.Add("Text", DataSource, "slxuat");
            col_tong_slxuat_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");



            col_tong_soducuoiky_nhom.DataBindings.Add("Text", DataSource, "soducuoiky");
            col_tong_soducuoiky_nhom.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.0}");



        }
    }
}
