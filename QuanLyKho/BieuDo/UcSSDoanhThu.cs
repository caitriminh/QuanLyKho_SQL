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
    public partial class UcSSDoanhThu : XtraUserControl
    {
        public UcSSDoanhThu()
        {
            InitializeComponent();
        }

        private void UserSSDoanhThu_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "SS_DOANHTHU1" });

                var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "SS_DOANHTHU2" });

                chartControl1.DataSource = dataTable1;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesDoanhThu1 = new Series("Doanh Thu " + dataTable1.Rows[0]["nam"], ViewType.Bar);
                    seriesDoanhThu1.LabelsVisibility = DefaultBoolean.True;

                    Series seriesDoanhThu2 = new Series("Doanh Thu " + dataTable2.Rows[0]["nam"], ViewType.Bar);
                    seriesDoanhThu2.LabelsVisibility = DefaultBoolean.True;

                    // Add points to them, with their arguments different.
                    foreach (DataRow dr in dataTable1.Rows)
                        seriesDoanhThu1.Points.Add(new SeriesPoint(dr["thang"], dr["thanhtien"]));
                    seriesDoanhThu1.Label.TextPattern = "{V:#,##0}";

                    foreach (DataRow dr in dataTable2.Rows)
                        seriesDoanhThu2.Points.Add(new SeriesPoint(dr["thang"], dr["thanhtien"]));
                    seriesDoanhThu2.Label.TextPattern = "{V:#,##0}";

                    chartControl1.Series.AddRange(new Series[] { seriesDoanhThu1 });
                    chartControl1.Series.AddRange(new Series[] { seriesDoanhThu2 });
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
