using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcBieuDoChiPhi : UserControl
    {
        private int _flag;
        public UcBieuDoChiPhi()
        {
            InitializeComponent();
            chiphi_cungky.CustomButtonClick += Chiphi_theothang_CustomButtonClick;
            chiphi_theonhom.CustomButtonClick += Chiphi_theonhom_CustomButtonClick;
            chiphi_theothang.CustomButtonClick += Chiphi_cungky_CustomButtonClick;
            mnu_chiphi_theoloai.ItemClick += Mnu_chiphi_theoloai_ItemClick;
            mnu_chiphi_cungky.ItemClick += Mnu_chiphi_cungky_ItemClick;
            mnu_chiphi_theoloai_pie.ItemClick += Mnu_chiphi_theoloai_pie_ItemClick;
            mnu_doanhthu_theothang.ItemClick += Mnu_doanhthu_theothang_ItemClick1;
        }

        private void Mnu_doanhthu_theothang_ItemClick1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Clear();
                chiphi_theonhom.Caption = @"Chi Phí Theo Loại";
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Clear();
                chiphi_cungky.Caption = @"Chi Phí Cùng Kỳ";
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Clear();
                chiphi_theothang.Caption = @"Chi Phí Theo Tháng";
            }

            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI_THEOTHANG2" });
            chartControl1.DataSource = dataTable2;

            Series seriesDoanhThu2 = new Series("Chi phí " + dataTable2.Rows[0]["nam"], ViewType.Bar);
            seriesDoanhThu2.LabelsVisibility = DefaultBoolean.True;

            // Add points to them, with their arguments different.

            foreach (DataRow dr in dataTable2.Rows)
                seriesDoanhThu2.Points.Add(new SeriesPoint(dr["thang"], dr["thanhtien"]));
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
            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_chiphi_theoloai_pie_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Clear();
                chiphi_theonhom.Caption = @"Loại Chi Phí";
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Clear();
                chiphi_cungky.Caption = @"Doanh Thu Theo Ngày";
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Clear();
                chiphi_theothang.Caption = @"Doanh Thu Cùng Kỳ";
            }

            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI" });
            chartControl1.DataSource = dataTable2;

            Series seriesLoaiChiPhi = new Series("Loại Chi Phí", ViewType.Pie);
            seriesLoaiChiPhi.LabelsVisibility = DefaultBoolean.True;

            // Add points to them, with their arguments different.

            foreach (DataRow dr in dataTable2.Rows)
                seriesLoaiChiPhi.Points.Add(new SeriesPoint(dr["loaichiphi"], dr["thanhtien"]));
            seriesLoaiChiPhi.Label.TextPattern = "{VP:p1}";//Format percent
            seriesLoaiChiPhi.LegendTextPattern = "{A}";

            chartControl1.Series.AddRange(new Series[] { seriesLoaiChiPhi });
            chartControl1.Legend.Visibility = DefaultBoolean.True;

            //XYDiagram diagram = chartControl1.Diagram as XYDiagram;
            //diagram.AxisY.Label.TextPattern = "{V:#,##0}";

            Legend legend = chartControl1.Legend;
            // chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            legend.Margins.All = 8;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            legend.AlignmentVertical = LegendAlignmentVertical.Top;
            legend.Direction = LegendDirection.LeftToRight;
            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_chiphi_cungky_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Text = @"Doanh Thu Cùng Kỳ";
            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Clear();
            }

            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI_THEOTHANG1" });
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI_THEOTHANG2" });

            chartControl1.DataSource = dataTable1;
            chartControl1.DataSource = dataTable2;

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
            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_chiphi_theoloai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Clear();
                chiphi_theonhom.Caption = @"Loại Chi Phí";
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Clear();
                chiphi_cungky.Caption = @"Doanh Thu Theo Ngày";
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Clear();
                chiphi_theothang.Caption = @"Doanh Thu Cùng Kỳ";
            }

            var chartControl1 = new ChartControl();
            chartControl1.Dock = DockStyle.Fill;
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_ChiPhi", new { action = "LOAICHIPHI" });
            chartControl1.DataSource = dataTable2;

            Series seriesLoaiChiPhi = new Series("Loại Chi Phí", ViewType.Bar);
            seriesLoaiChiPhi.LabelsVisibility = DefaultBoolean.True;

            // Add points to them, with their arguments different.

            foreach (DataRow dr in dataTable2.Rows)
                seriesLoaiChiPhi.Points.Add(new SeriesPoint(dr["loaichiphi"], dr["thanhtien"]));
            seriesLoaiChiPhi.Label.TextPattern = "{V:#,##0}";



            chartControl1.Series.AddRange(new Series[] { seriesLoaiChiPhi });
            chartControl1.Legend.Visibility = DefaultBoolean.True;

            XYDiagram diagram = chartControl1.Diagram as XYDiagram;
            diagram.AxisY.Label.TextPattern = "{V:#,##0}";



            Legend legend = chartControl1.Legend;
            // chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            legend.Margins.All = 8;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            legend.AlignmentVertical = LegendAlignmentVertical.Top;
            legend.Direction = LegendDirection.LeftToRight;

            chartControl1.ToolTipEnabled = DefaultBoolean.True;
            seriesLoaiChiPhi.ToolTipPointPattern = "{A} : {V:#,##0}";

            if (_flag == 1)
            {
                chiphi_theonhom.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                chiphi_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                chiphi_theothang.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Chiphi_cungky_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 3;
        }

        private void Chiphi_theonhom_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 1;
        }

        private void Chiphi_theothang_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 2;
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
