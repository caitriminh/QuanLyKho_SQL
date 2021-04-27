using System;
using System.Data;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using QuanLyKho.Data;

namespace QuanLyKho.BieuDo
{
    public partial class UcChiPhiTheoThang : XtraUserControl
    {
        public UcChiPhiTheoThang()
        {
            InitializeComponent();
            Load += UcChiPhiTheoThang_Load;
        }

        private void UcChiPhiTheoThang_Load(object sender, EventArgs e)
        {
            Text = "Chi Phí Theo Tháng";
            Task.Factory.StartNew(() =>
            {
                var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI_THEOTHANG2" });

                chartControl1.DataSource = dataTable1;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesChiPhi1 = new Series("Chi phí " + dataTable1.Rows[0]["nam"], ViewType.Bar);
                    seriesChiPhi1.LabelsVisibility = DefaultBoolean.True;

                    // Add points to them, with their arguments different.
                    foreach (DataRow dr in dataTable1.Rows)
                        seriesChiPhi1.Points.Add(new SeriesPoint(dr["thang"], dr["thanhtien"]));
                    seriesChiPhi1.Label.TextPattern = "{V:#,##0}";
                    seriesChiPhi1.ToolTipPointPattern = "{A}: {V:#,##0}";

                    chartControl1.Series.AddRange(new Series[] { seriesChiPhi1 });

                    chartControl1.Legend.Visibility = DefaultBoolean.True;

                    XYDiagram diagram = chartControl1.Diagram as XYDiagram;
                    diagram.AxisY.Label.TextPattern = "{V:#,##0}";

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
