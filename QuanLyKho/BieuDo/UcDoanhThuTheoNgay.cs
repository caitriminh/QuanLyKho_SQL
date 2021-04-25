using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcDoanhThuTheoNgay : DevExpress.XtraEditors.XtraUserControl
    {
        public UcDoanhThuTheoNgay()
        {
            InitializeComponent();
            Load += UcDoanhThuTheoNgay_Load;
        }

        private void UcDoanhThuTheoNgay_Load(object sender, EventArgs e)
        {
            Text = @"Doanh Thu Theo Ngày"; //+ string.Format("{0:#,##}", tong) + @" VND";
            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            panelControl.Controls.Add(chartControl1);
            Task.Factory.StartNew(() =>
            {
                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_THEONGAY" });
                chartControl1.DataSource = dataTable;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesThanhTien = new Series("Doanh thu", ViewType.Bar);
                    seriesThanhTien.LabelsVisibility = DefaultBoolean.True;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        seriesThanhTien.Points.Add(new SeriesPoint(dr["ngay"], dr["thanhtien"]));
                    }

                    seriesThanhTien.Label.TextPattern = "{V:#,##0}";

                    chartControl1.Series.AddRange(new[] { seriesThanhTien });
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
