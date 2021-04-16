using DevExpress.XtraReports.UI;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System;

namespace QuanLyKho.NghiepVu
{
    public partial class rpt_phieuxuatkho : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_phieuxuatkho()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoThongTin>("prokhoThongTin", new { action = "GET_DATA" });
            if (dt != null)
            {
                lbl_tencty.Text = dt.tencty;
                lbl_diachi.Text = "Đ/c: " + dt.diachi;
                lbl_sodt_sofax.Text = "Số ĐT: " + dt.sodt;
            }
            lbl_maphieu.Text = "Số: " + Data.Data._dtreport.Rows[0]["maphieu"].ToString();
            lbl_tendonvi.Text = " - Họ tên người nhận hàng: " + Data.Data._dtreport.Rows[0]["tenkh"].ToString();
            lbl_kho.Text = " - Xuất tại kho (ngăn lô): " + Data.Data._dtreport.Rows[0]["tenkho"].ToString() + "  - Địa chỉ: " + Data.Data._dtreport.Rows[0]["diachikho"].ToString();
            lbl_diachi.Text = " - Địa chỉ: " + Data.Data._dtreport.Rows[0]["diachi"].ToString();
            lbl_diengiai.Text = " - Lý do xuất: " + Data.Data._dtreport.Rows[0]["diengiai"].ToString();
            lbl_ngaylap.Text = "Ngày " + Convert.ToDateTime(Data.Data._dtreport.Rows[0]["ngayxuat"]).ToString("dd") + " tháng " + Convert.ToDateTime(Data.Data._dtreport.Rows[0]["ngayxuat"]).ToString("MM") + " năm " + Convert.ToDateTime(Data.Data._dtreport.Rows[0]["ngayxuat"]).ToString("yyyy");

            col_mahanghoa.DataBindings.Add("Text", DataSource, "mahanghoa");
            col_tenhang.DataBindings.Add("Text", DataSource, "tenhanghoa");
            col_dvt.DataBindings.Add("Text", DataSource, "tendvt");
            col_soluong.DataBindings.Add("Text", DataSource, "soluong").FormatString = "{0:#,##}";
            col_dongia.DataBindings.Add("Text", DataSource, "dongia").FormatString = "{0:#,##}";
            col_thanhtien.DataBindings.Add("Text", DataSource, "thanhtien").FormatString = "{0:#,##}";


            col_tongsoluong.DataBindings.Add("Text", DataSource, "soluong");
            col_tongsoluong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##}");


            col_tongthanhtien.DataBindings.Add("Text", DataSource, "thanhtien");
            col_tongthanhtien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##}");


            col_stt.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##}");

            //lbl_thanhtien_bangchu.Text = "- Tổng số tiền (viết bằng chữ): " + Data.read_number._read_number(Convert.ToDouble(Data.Data._get_data("SELECT sum(soluong*dongia) from tbl_chitiet_phieuxuat where maphieu='" + Data.Data._dtreport.Rows[0]["maphieu"].ToString() + "'")));

        }
    }
}
