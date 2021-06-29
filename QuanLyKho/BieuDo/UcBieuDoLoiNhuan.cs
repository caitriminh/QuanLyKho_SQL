using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcBieuDoLoiNhuan : UserControl
    {
        private int _flag;
        public UcBieuDoLoiNhuan()
        {
            InitializeComponent();
            loinhuan_cungky.CustomButtonClick += Loinhuan_cungky_CustomButtonClick;
            loinhuan_theothang.CustomButtonClick += Loinhuan_theothang_CustomButtonClick;

            mnu_loinhuan_cungky.ItemClick += Mnu_loinhuan_cungky_ItemClick;
            mnu_loinhuan_theothang.ItemClick += Mnu_loinhuan_theothang_ItemClick;
        }

        private void Loinhuan_theothang_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 1;
        }

        private void Loinhuan_cungky_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 2;
        }

        private void Mnu_loinhuan_theothang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_flag == 1)
            {
                loinhuan_theothang.Control.Controls[0].Controls.Clear();
                loinhuan_theothang.Caption = @"Lợi Nhuận Theo Tháng";
            }
            else if (_flag == 2)
            {
                loinhuan_cungky.Control.Controls[0].Controls.Clear();
                loinhuan_cungky.Caption = @"Lợi Nhuận Cùng Kỳ";
            }

            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBaoCaoLoiNhuan", new { action = "GET_DATA", option = 2, tungay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), denngay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") });
            chartControl1.DataSource = dataTable2;

            Series seriesDoanhThu2 = new Series("Lợi nhuận " + dataTable2.Rows[0]["nam"], ViewType.Bar);
            seriesDoanhThu2.LabelsVisibility = DefaultBoolean.True;

            // Add points to them, with their arguments different.

            foreach (DataRow dr in dataTable2.Rows)
                seriesDoanhThu2.Points.Add(new SeriesPoint(dr["thang"], dr["loinhuangop2"]));
            seriesDoanhThu2.Label.TextPattern = "{V:#,##0}";

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
            if (_flag == 2)
            {
                loinhuan_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 1)
            {
                loinhuan_theothang.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_loinhuan_cungky_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Text = @"Lợi Nhuận Cùng Kỳ";
            if (_flag == 2)
            {
                loinhuan_cungky.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 1)
            {
                loinhuan_theothang.Control.Controls[0].Controls.Clear();
            }

            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBaoCaoLoiNhuan", new { action = "GET_DATA", option = 3, tungay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), denngay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") });
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBaoCaoLoiNhuan", new { action = "GET_DATA", option = 2, tungay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), denngay = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") });

            chartControl1.DataSource = dataTable1;
            chartControl1.DataSource = dataTable2;

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
            if (_flag == 2)
            {
                loinhuan_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 1)
            {
                loinhuan_theothang.Control.Controls[0].Controls.Add(chartControl1);
            }
        }




        private void widgetView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Document.ControlTypeName))
            {
                e.Control = Activator.CreateInstance(Type.GetType(e.Document.ControlTypeName)) as Control;
            }
            else
            {
                e.Control = new Control();
            }
        }

    }
}
