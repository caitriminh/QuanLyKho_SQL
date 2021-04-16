using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System;

namespace QuanLyKho.DanhMuc
{
    public partial class rpt_bangbaogia : XtraReport
    {
        public rpt_bangbaogia()
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
            lblNgayCapNhat.Text = "Ngày cập nhật: " + DateTime.Now.ToString("dd/MM/yyyy");
            GroupHeader1.GroupFields.Add(new GroupField("nhomhang", XRColumnSortOrder.Ascending));
            col_nhomhang2.DataBindings.Add("Text", null, "nhomhang", "Nhóm hàng: {0}");

            col_mahanghoa.DataBindings.Add("Text", DataSource, "mathamchieu");
            col_tenhanghoa.DataBindings.Add("Text", DataSource, "tenhanghoa");
            col_nhomhang.DataBindings.Add("Text", DataSource, "nhomhang");
            col_dvt.DataBindings.Add("Text", DataSource, "tendvt");
            colDonGia.DataBindings.Add("Text", DataSource, "dongia").FormatString = "{0:#,##}";

            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");
        }
    }
}
