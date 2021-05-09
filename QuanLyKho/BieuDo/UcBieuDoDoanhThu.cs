using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcBieuDoDoanhThu : UserControl
    {
        private int _flag;
        public UcBieuDoDoanhThu()
        {
            InitializeComponent();
            doanhthu_theongay.CustomButtonClick += Doanhthu_theongay_CustomButtonClick;
            doanhthu_nhomhang.CustomButtonClick += Doanhthu_nhomhang_CustomButtonClick;
            doanhthu_cungky.CustomButtonClick += Doanhthu_cungky_CustomButtonClick;
            mnu_doanhthu_nhomhang.ItemClick += Mnu_doanhthu_nhomhang_ItemClick;
            mnu_doanhthu_theongay.ItemClick += Mnu_doanhthu_theongay_ItemClick;
            mnu_doanhthu_cungky.ItemClick += Mnu_doanhthu_cungky_ItemClick;
            mnu_doanhthu_theothang.ItemClick += Mnu_doanhthu_theothang_ItemClick;
            mnu_doanhthu_nhomhang_pie.ItemClick += Mnu_doanhthu_nhomhang_pie_ItemClick;
            mnu_doanhthu_theonam.ItemClick += Mnu_doanhthu_theonam_ItemClick;
        }

        private void Mnu_doanhthu_theonam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Text = @"Doanh Thu Theo Năm";
            if (_flag == 1)
            {
                doanhthu_nhomhang.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Clear();
            }
            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_THEONAM" });
            chartControl1.DataSource = dataTable;


            Series seriesNam = new Series("Năm", ViewType.Bar)
            {
                LabelsVisibility = DefaultBoolean.True
            };

            // Add points to them, with their arguments different.
            foreach (DataRow dr in dataTable.Rows)
            {
                seriesNam.Points.Add(new SeriesPoint(dr["nam"], dr["thanhtien"]));
            }
            seriesNam.Label.TextPattern = "{V:#,##0}";

            chartControl1.Series.AddRange(new Series[] { seriesNam });
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
                doanhthu_nhomhang.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_doanhthu_nhomhang_pie_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Text = @"Doanh Thu Nhóm Hàng";
            if (_flag == 1)
            {
                doanhthu_nhomhang.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Clear();
            }
            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_NHOMHANG" });
            chartControl1.DataSource = dataTable;


            Series seriesNhomHang = new Series("Nhóm Hàng", ViewType.Pie)
            {
                LabelsVisibility = DefaultBoolean.True
            };

            // Add points to them, with their arguments different.
            foreach (DataRow dr in dataTable.Rows)
            {
                seriesNhomHang.Points.Add(new SeriesPoint(dr["nhomhang"], dr["thanhtien"]));
            }
            seriesNhomHang.Label.TextPattern = "{VP:p1}";
            seriesNhomHang.LegendTextPattern = "{A}";

            chartControl1.Series.AddRange(new Series[] { seriesNhomHang });
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
                doanhthu_nhomhang.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_doanhthu_theothang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_flag == 1)
            {
                doanhthu_nhomhang.Control.Controls[0].Controls.Clear();
                doanhthu_nhomhang.Caption = @"Nhóm Hàng";
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Clear();
                doanhthu_theongay.Caption = @"Doanh Thu Theo Ngày";
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Clear();
                doanhthu_cungky.Caption = @"Doanh Thu Cùng Kỳ";
            }

            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "SS_DOANHTHU2" });
            chartControl1.DataSource = dataTable2;

            Series seriesDoanhThu2 = new Series("Doanh Thu " + dataTable2.Rows[0]["nam"], ViewType.Bar)
            {
                LabelsVisibility = DefaultBoolean.True
            };

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
                doanhthu_nhomhang.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Doanhthu_nhomhang_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 1;
        }

        private void Doanhthu_theongay_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 2;
        }


        private void Doanhthu_cungky_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 3;
        }

        private void Mnu_doanhthu_cungky_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Text = @"Doanh Thu Cùng Kỳ";
            if (_flag == 1)
            {
                doanhthu_nhomhang.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Clear();
            }

            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "SS_DOANHTHU1" });
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "SS_DOANHTHU2" });

            chartControl1.DataSource = dataTable1;
            chartControl1.DataSource = dataTable2;

            Series seriesDoanhThu1 = new Series("Doanh Thu " + dataTable1.Rows[0]["nam"], ViewType.Bar)
            {
                LabelsVisibility = DefaultBoolean.True
            };

            Series seriesDoanhThu2 = new Series("Doanh Thu " + dataTable2.Rows[0]["nam"], ViewType.Bar)
            {
                LabelsVisibility = DefaultBoolean.True
            };

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
            if (_flag == 1)
            {
                doanhthu_nhomhang.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_doanhthu_theongay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Text = @"Doanh Thu Theo Ngày";
            if (_flag == 1)
            {
                doanhthu_nhomhang.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Clear();
            }

            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_THEONGAY" });
            chartControl1.DataSource = dataTable;


            Series seriesNhomHang = new Series("Ngày", ViewType.Bar)
            {
                LabelsVisibility = DefaultBoolean.True
            };

            // Add points to them, with their arguments different.
            foreach (DataRow dr in dataTable.Rows)
            {
                seriesNhomHang.Points.Add(new SeriesPoint(dr["ngay"], dr["thanhtien"]));
            }
            seriesNhomHang.Label.TextPattern = "{V:#,##0}";

            chartControl1.Series.AddRange(new Series[] { seriesNhomHang });
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
                doanhthu_nhomhang.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
        }

        private void Mnu_doanhthu_nhomhang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Text = @"Doanh Thu Nhóm Hàng";
            if (_flag == 1)
            {
                doanhthu_nhomhang.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Clear();
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Clear();

            }
            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_NHOMHANG" });
            chartControl1.DataSource = dataTable;


            Series seriesNhomHang = new Series("Nhóm Hàng", ViewType.Bar)
            {
                LabelsVisibility = DefaultBoolean.True
            };

            // Add points to them, with their arguments different.
            foreach (DataRow dr in dataTable.Rows)
            {
                seriesNhomHang.Points.Add(new SeriesPoint(dr["nhomhang"], dr["thanhtien"]));
            }
            seriesNhomHang.Label.TextPattern = "{V:#,##0}";

            chartControl1.Series.AddRange(new Series[] { seriesNhomHang });
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
                doanhthu_nhomhang.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 2)
            {
                doanhthu_theongay.Control.Controls[0].Controls.Add(chartControl1);
            }
            else if (_flag == 3)
            {
                doanhthu_cungky.Control.Controls[0].Controls.Add(chartControl1);
            }
        }


        private void WidgetView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
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
