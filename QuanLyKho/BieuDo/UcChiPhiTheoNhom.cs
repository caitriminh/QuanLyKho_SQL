using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcChiPhiTheoNhom : DevExpress.XtraEditors.XtraUserControl
    {
        public UcChiPhiTheoNhom()
        {
            InitializeComponent();
            Load += UcChiPhiTheoNhom_Load;
        }

        private void UcChiPhiTheoNhom_Load(object sender, EventArgs e)
        {
            Text = @"Loại Chi Phí";
            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            panelControl.Controls.Add(chartControl1);
            Task.Factory.StartNew(() =>
            {
                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI" });
                chartControl1.DataSource = dataTable;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesLoaiChiPhi = new Series("Loại chi phí", ViewType.Pie);
                    seriesLoaiChiPhi.LabelsVisibility = DefaultBoolean.True;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        seriesLoaiChiPhi.Points.Add(new SeriesPoint(dr["loaichiphi"], dr["thanhtien"]));
                    }
                    seriesLoaiChiPhi.Label.TextPattern = "{V:#,##0}";
                    seriesLoaiChiPhi.LegendTextPattern = "{A}";

                    chartControl1.Series.AddRange(new[] { seriesLoaiChiPhi });
                    chartControl1.Legend.Visibility = DefaultBoolean.True;

                    //Format Cột dọc
                    //XYDiagram diagram = chartControl1.Diagram as XYDiagram;
                    //diagram.AxisY.Label.TextPattern = "{V:#,##0}";

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
