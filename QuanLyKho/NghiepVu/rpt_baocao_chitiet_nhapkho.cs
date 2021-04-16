using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System.Data;

namespace QuanLyKho.NghiepVu
{
    public partial class rpt_baocao_chitiet_nhapkho : XtraReport
    {
        public rpt_baocao_chitiet_nhapkho()
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


            GroupHeader1.GroupFields.Add(new GroupField("maphieu", XRColumnSortOrder.Ascending));
            col_maphieu.DataBindings.Add("Text", null, "maphieu", "Mã phiếu: {0}");

            col_ngaynhap.DataBindings.Add("Text", DataSource, "ngaynhap").FormatString = "{0:dd/MM/yyyy}";

            col_mahang.DataBindings.Add("Text", DataSource, "mahanghoa");
            col_tenhanghoa.DataBindings.Add("Text", DataSource, "tenhanghoa");

            col_soluong.DataBindings.Add("Text", DataSource, "soluong").FormatString = "{0:#,##0.0}";
            col_dvt.DataBindings.Add("Text", DataSource, "tendvt");
            col_dongia.DataBindings.Add("Text", DataSource, "dongia").FormatString = "{0:#,##0.00}";
            col_thanhtien.DataBindings.Add("Text", DataSource, "thanhtien").FormatString = "{0:#,##0.00}";

            //col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");

            col_tong_soluong.DataBindings.Add("Text", DataSource, "soluong");
            col_tong_soluong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");


            col_tong_thanhtien_phieu.DataBindings.Add("Text", DataSource, "thanhtien");
            col_tong_thanhtien_phieu.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0.00}");

            col_tong_thanhtien.DataBindings.Add("Text", DataSource, "thanhtien");
            col_tong_thanhtien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.00}");


        }
    }
}
