using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using QuanLyKho.Data;

namespace QuanLyKho.BieuDo
{
    public partial class UcChiPhiCungKy : XtraUserControl
    {
        public UcChiPhiCungKy()
        {
            InitializeComponent();
            Load += UcChiPhiCungKy_Load;
        }

        private void UcChiPhiCungKy_Load(object sender, EventArgs e)
        {
            Text = "Chi Phí Cùng Kỳ";
            Task.Factory.StartNew(() =>
            {
                var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI_THEOTHANG1" });

                var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI_THEOTHANG2" });

                chartControl1.DataSource = dataTable1;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesChiPhi1 = new Series("Chi phí " + dataTable1.Rows[0]["nam"], ViewType.Bar);
                    seriesChiPhi1.LabelsVisibility = DefaultBoolean.True;

                    Series seriesChiPhi2 = new Series("Chi phí " + dataTable2.Rows[0]["nam"], ViewType.Bar);
                    seriesChiPhi2.LabelsVisibility = DefaultBoolean.True;

                    // Add points to them, with their arguments different.
                    foreach (DataRow dr in dataTable1.Rows)
                        seriesChiPhi1.Points.Add(new SeriesPoint(dr["thang"], dr["thanhtien"]));
                    seriesChiPhi1.Label.TextPattern = "{V:#,##0}";

                    foreach (DataRow dr in dataTable2.Rows)
                        seriesChiPhi2.Points.Add(new SeriesPoint(dr["thang"], dr["thanhtien"]));
                    seriesChiPhi2.Label.TextPattern = "{V:#,##0}";

                    chartControl1.Series.AddRange(new Series[] { seriesChiPhi1 });
                    chartControl1.Series.AddRange(new Series[] { seriesChiPhi2 });
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
