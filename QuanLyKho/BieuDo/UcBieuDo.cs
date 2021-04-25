using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcBieuDo : System.Windows.Forms.UserControl
    {
        public UcBieuDo()
        {
            InitializeComponent();
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

        private void tuatruyen_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

        }

        private void loaitruyen_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
            _flag = 1;
        }

        private async void mnu_loaitruyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var tong = ExecSQL.ExecQuerySacalar("SELECT COUNT(DISTINCT matruyen) AS tong FROM dbo.tbl_ct_phieunhapxuat WHERE slnhap>0");
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Clear();
            //    loaitruyen.Caption = @"Loại truyện: " + tong + @" cuốn"; ;
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Clear();
            //    giatri.Caption = @"Loại truyện: " + tong + @" cuốn"; ;
            //}

            //var chartControl1 = new ChartControl();
            //chartControl1.Dock = DockStyle.Fill;
            //var dataTable = await ExecSQL.ExecProcedureDataAsyncAsDataTable("pro_get_tongso_loaitruyen");
            //chartControl1.DataSource = dataTable;


            //Series seriesloaitruyen = new Series("Loại truyện", ViewType.Bar);
            //seriesloaitruyen.LabelsVisibility = DefaultBoolean.True;

            //// Add points to them, with their arguments different.
            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    seriesloaitruyen.Points.Add(new SeriesPoint(dr["loaitruyen"], dr["soluong"]));
            //}

            //chartControl1.Series.AddRange(new Series[] { seriesloaitruyen });
            //chartControl1.Legend.Visibility = DefaultBoolean.True;

            //Legend legend = chartControl1.Legend;
            //// chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            //legend.Margins.All = 8;
            //legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            //legend.AlignmentVertical = LegendAlignmentVertical.Top;
            //legend.Direction = LegendDirection.LeftToRight;
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Add(chartControl1);
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Add(chartControl1);
            //}
        }

        private void mnu_giatri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var tong = ExecSQL.ExecQuerySacalar("SELECT SUM(slnhap*dongia) AS tong FROM dbo.tbl_ct_phieunhapxuat");
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Clear();
            //    loaitruyen.Caption = @"Giá trị : " + string.Format("{0:#,##}", tong) + @" VND";
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Clear();
            //    giatri.Caption = @"Giá trị : " + string.Format("{0:#,##}", tong) + @" VND";
            //}

            //var dataTable = ExecSQL.ExecProcedureDataAsDataTable("pro_get_thongke_theothang");
            //var chartGiaTri = new ChartControl();
            //chartGiaTri.Dock = DockStyle.Fill;
            //chartGiaTri.DataSource = dataTable;

            //Series seriesGiatri = new Series("Giá trị", ViewType.Line);
            //seriesGiatri.LabelsVisibility = DefaultBoolean.True;

            //// Add points to them, with their arguments different.
            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    seriesGiatri.Points.Add(new SeriesPoint(dr["thangnam"], dr["thanhtien"]));
            //}

            //seriesGiatri.Label.TextPattern = "{V:#,##0}";

            //chartGiaTri.Series.AddRange(new Series[] { seriesGiatri });
            //chartGiaTri.Legend.Visibility = DefaultBoolean.True;

            //Legend legend = chartGiaTri.Legend;
            //// chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            //legend.Margins.All = 8;
            //legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            //legend.AlignmentVertical = LegendAlignmentVertical.Top;
            //legend.Direction = LegendDirection.LeftToRight;


            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Add(chartGiaTri);
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Add(chartGiaTri);
            //}
        }

        private int _flag;
        private void giatri_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            _flag = 2;
            popupMenu1.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
        }

        private async void mnu_tuatruyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Clear();
            //    loaitruyen.Caption = @"Tựa truyện";
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Clear();
            //    giatri.Caption = @"Tựa truyện";
            //}

            //var dataTable = await ExecSQL.ExecProcedureDataAsyncAsDataTable("pro_get_tonghop", new { option = 1 });
            //var chartGiaTri = new ChartControl();
            //chartGiaTri.Dock = DockStyle.Fill;
            //chartGiaTri.DataSource = dataTable;

            //Series seriesGiatri = new Series("Tựa truyện", ViewType.Line);
            //seriesGiatri.LabelsVisibility = DefaultBoolean.True;

            //// Add points to them, with their arguments different.
            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    seriesGiatri.Points.Add(new SeriesPoint(dr["tuatruyen"], dr["thanhtien"]));
            //}

            //seriesGiatri.Label.TextPattern = "{V:#,##0}";

            //chartGiaTri.Series.AddRange(new Series[] { seriesGiatri });
            //chartGiaTri.Legend.Visibility = DefaultBoolean.True;

            //Legend legend = chartGiaTri.Legend;
            //legend.Margins.All = 8;
            //legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            //legend.AlignmentVertical = LegendAlignmentVertical.Top;
            //legend.Direction = LegendDirection.LeftToRight;


            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Add(chartGiaTri);
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Add(chartGiaTri);
            //}
        }

        private async void mnu_nhaxuatban_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Clear();
            //    loaitruyen.Caption = @"Nhà Xuất Bản";
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Clear();
            //    giatri.Caption = @"Nhà Xuất Bản";
            //}

            //var chartControl1 = new ChartControl();
            //chartControl1.Dock = DockStyle.Fill;
            //var dataTable = await ExecSQL.ExecProcedureDataAsyncAsDataTable("pro_get_tonghop", new { option = 2 });
            //chartControl1.DataSource = dataTable;


            //Series seriesnxb = new Series("Nhà Xuất Bản", ViewType.Bar);
            //seriesnxb.LabelsVisibility = DefaultBoolean.True;

            //// Add points to them, with their arguments different.
            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    seriesnxb.Points.Add(new SeriesPoint(dr["nhaxuatban"], dr["soluong"]));
            //}

            //chartControl1.Series.AddRange(new Series[] { seriesnxb });
            //chartControl1.Legend.Visibility = DefaultBoolean.True;

            //Legend legend = chartControl1.Legend;
            //// chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            //legend.Margins.All = 8;
            //legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            //legend.AlignmentVertical = LegendAlignmentVertical.Top;
            //legend.Direction = LegendDirection.LeftToRight;
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Add(chartControl1);
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Add(chartControl1);
            //}
        }

        private async void mnu_quocgia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Clear();
            //    loaitruyen.Caption = @"Quốc Gia";
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Clear();
            //    giatri.Caption = @"Quốc Gia";
            //}

            //var chartControl1 = new ChartControl();
            //chartControl1.Dock = DockStyle.Fill;
            //var dataTable = await ExecSQL.ExecProcedureDataAsyncAsDataTable("pro_get_tonghop", new { option = 3 });
            //chartControl1.DataSource = dataTable;


            //Series seriesquocgia = new Series("Quốc Gia", ViewType.Bar);
            //seriesquocgia.LabelsVisibility = DefaultBoolean.True;

            //// Add points to them, with their arguments different.
            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    seriesquocgia.Points.Add(new SeriesPoint(dr["quocgia"], dr["soluong"]));
            //}

            //chartControl1.Series.AddRange(new Series[] { seriesquocgia });
            //chartControl1.Legend.Visibility = DefaultBoolean.True;

            //Legend legend = chartControl1.Legend;
            //// chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            //legend.Margins.All = 8;
            //legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            //legend.AlignmentVertical = LegendAlignmentVertical.Top;
            //legend.Direction = LegendDirection.LeftToRight;
            //if (_flag == 1)
            //{
            //    loaitruyen.Control.Controls[0].Controls.Add(chartControl1);
            //}
            //else if (_flag == 2)
            //{
            //    giatri.Control.Controls[0].Controls.Add(chartControl1);
            //}
        }
    }
}
