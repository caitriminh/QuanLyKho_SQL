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
    public partial class UcLoiNhuanCungKy : XtraUserControl
    {
        public UcLoiNhuanCungKy()
        {
            InitializeComponent();
            Load += UcLoiNhuanCungKy_Load;
        }

        private void UcLoiNhuanCungKy_Load(object sender, EventArgs e)
        {
            Text = "Lợi Nhuận Cùng Kỳ";

            var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBaoCaoLoiNhuan", new { action = "GET_DATA", option = 3, tungay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), denngay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") });

            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBaoCaoLoiNhuan", new { action = "GET_DATA", option = 2, tungay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), denngay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") });

            Task.Factory.StartNew(() =>
            {

                chartControl1.DataSource = dataTable1;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesChiPhi1 = new Series("Lợi nhuận " + dataTable1.Rows[0]["nam"], ViewType.Bar);
                    seriesChiPhi1.LabelsVisibility = DefaultBoolean.True;

                    Series seriesChiPhi2 = new Series("Lợi nhuận " + dataTable2.Rows[0]["nam"], ViewType.Bar);
                    seriesChiPhi2.LabelsVisibility = DefaultBoolean.True;

                    // Add points to them, with their arguments different.
                    foreach (DataRow dr in dataTable1.Rows)
                        seriesChiPhi1.Points.Add(new SeriesPoint(dr["thang"], dr["loinhuangop2"]));
                    seriesChiPhi1.Label.TextPattern = "{V:#,##0}";


                    foreach (DataRow dr in dataTable2.Rows)
                        seriesChiPhi2.Points.Add(new SeriesPoint(dr["thang"], dr["loinhuangop2"]));
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
