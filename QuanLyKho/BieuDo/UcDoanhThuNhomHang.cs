using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcDoanhThuNhomHang : DevExpress.XtraEditors.XtraUserControl
    {
        public UcDoanhThuNhomHang()
        {
            InitializeComponent();
            Load += UcDoanhThu_Load;
        }

        private void UcDoanhThu_Load(object sender, EventArgs e)
        {
            //var tong = ExecSQL.ExecQuerySacalar("SELECT SUM(slnhap*dongia) AS tong FROM dbo.tbl_ct_phieunhapxuat");
            Text = @"Giá trị: "; //+ string.Format("{0:#,##}", tong) + @" VND";
            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            panelControl.Controls.Add(chartControl1);
            Task.Factory.StartNew(() =>
            {
                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_NHOMHANG" });
                chartControl1.DataSource = dataTable;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesGiatri = new Series("Doanh thu", ViewType.Bar);
                    seriesGiatri.LabelsVisibility = DefaultBoolean.True;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        seriesGiatri.Points.Add(new SeriesPoint(dr["nhomhang"], dr["thanhtien"]));
                    }

                    seriesGiatri.Label.TextPattern = "{V:#,##0}";

                    chartControl1.Series.AddRange(new[] { seriesGiatri });
                    chartControl1.Legend.Visibility = DefaultBoolean.True;

                    Legend legend = chartControl1.Legend;
                    // chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
                    legend.Margins.All = 8;
                    legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                    legend.AlignmentVertical = LegendAlignmentVertical.Top;
                    legend.Direction = LegendDirection.LeftToRight;
                }));
            });
        }
    }
}
