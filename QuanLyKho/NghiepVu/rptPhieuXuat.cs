using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System;

namespace QuanLyKho.NghiepVu
{
    public partial class rptPhieuXuat : XtraReport
    {
        public rptPhieuXuat()
        {
            InitializeComponent();
            DataSource = Data.Data._dtreport;
        }


        public void BindData()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoThongTin>("prokhoThongTin", new { action = "GET_DATA" });
            if (dt != null)
            {
                lbl_tencty.Text = dt.tencty;
                lblDiaChi.Text = "Đ/c: " + dt.diachi;
            }

            lbl_tendonvi.Text = "- Khách hàng: " + Data.Data._dtreport.Rows[0]["tenkh"].ToString();
            lbl_ngaylap.Text = "- Ngày xuất: " + Convert.ToDateTime(Data.Data._dtreport.Rows[0]["ngayxuat"]).ToString("dd/MM/yyyy");
            col_tenhang.DataBindings.Add("Text", DataSource, "tenhanghoa");

            col_soluong.DataBindings.Add("Text", DataSource, "soluong");
            col_dongia.DataBindings.Add("Text", DataSource, "dongia");
            col_thanhtien.DataBindings.Add("Text", DataSource, "thanhtien").FormatString = "{0:#,##}";

            col_tongthanhtien.DataBindings.Add("Text", DataSource, "thanhtien");
            col_tongthanhtien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##}");


            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");
        }

    }
}
