using System;
using System.Data;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using QuanLyKho.Data;

namespace QuanLyKho.BieuDo
{
    public partial class UcLoiNhuanTheoThang : XtraUserControl
    {
        public UcLoiNhuanTheoThang()
        {
            InitializeComponent();
            Load += UcLoiNhuanTheoThang_Load;
        }

        private void UcLoiNhuanTheoThang_Load(object sender, EventArgs e)
        {
            Text = "Lợi Nhuận Theo Tháng";
            Task.Factory.StartNew(() =>
            {
                var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBaoCaoLoiNhuan", new { action = "GET_DATA", option = 2, tungay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), denngay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") });
                if (dataTable1.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không tìm thấy dữ liệu.", "Cảnh Báo");
                    return;
                }
                chartControl1.DataSource = dataTable1;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesChiPhi1 = new Series("Lợi nhuận " + dataTable1.Rows[0]["nam"], ViewType.Bar);
                    seriesChiPhi1.LabelsVisibility = DefaultBoolean.True;

                    // Add points to them, with their arguments different.
                    foreach (DataRow dr in dataTable1.Rows)
                        seriesChiPhi1.Points.Add(new SeriesPoint(dr["thang"], dr["loinhuangop2"]));
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
