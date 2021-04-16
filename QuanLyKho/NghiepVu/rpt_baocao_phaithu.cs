using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System.Data;

namespace QuanLyKho.NghiepVu
{
    public partial class rpt_baocao_phaithu : XtraReport
    {
        public rpt_baocao_phaithu()
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


            col_ngayban.DataBindings.Add("Text", DataSource, "ngayxuat").FormatString = "{0:dd/MM/yyyy}";
            col_maphieu.DataBindings.Add("Text", DataSource, "maphieu");
            col_khachhang.DataBindings.Add("Text", DataSource, "tenkh");
            col_sotien.DataBindings.Add("Text", DataSource, "phaithu").FormatString = "{0:#,##0}";

            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");

            col_tong_sotien.DataBindings.Add("Text", DataSource, "phaithu");
            col_tong_sotien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");


        }
    }
}
