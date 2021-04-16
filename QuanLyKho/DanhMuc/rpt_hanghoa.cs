using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;

namespace QuanLyKho.DanhMuc
{
    public partial class rpt_hanghoa : XtraReport
    {
        public rpt_hanghoa()
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
            GroupHeader1.GroupFields.Add(new GroupField("nhomhang", XRColumnSortOrder.Ascending));
            col_nhomhang2.DataBindings.Add("Text", null, "nhomhang", "Nhóm hàng: {0}");

            col_mahanghoa.DataBindings.Add("Text", DataSource, "mathamchieu");
            col_tenhanghoa.DataBindings.Add("Text", DataSource, "tenhanghoa");
            col_nhomhang.DataBindings.Add("Text", DataSource, "nhomhang");
            col_dvt.DataBindings.Add("Text", DataSource, "tendvt");
            col_ghichu.DataBindings.Add("Text", DataSource, "ghichu");

            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");
        }
    }
}
