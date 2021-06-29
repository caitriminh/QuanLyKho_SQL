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
    public partial class UcTySuatLoiNhuan : XtraUserControl
    {
        private string strMaNhom = "CARD";

        [Obsolete]
        public UcTySuatLoiNhuan()
        {
            InitializeComponent();
            Load += UcTySuatLoiNhuan_Load;
            grvViewNhomHang.FocusedRowChanged += GrvViewNhomHang_FocusedRowChanged;
        }

        [Obsolete]
        private void GrvViewNhomHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = grvViewNhomHang.FocusedRowHandle;
            if (i < 0) { return; }
            strMaNhom = grvViewNhomHang.GetRowCellValue(i, "manhom").ToString();
            if (string.IsNullOrEmpty(strMaNhom)) { return; }
            panelControl.Controls.Clear();
            GetBieuDo();
        }

        public async void GetNhomHang()
        {
            var dt = await ExecSQL.ExecProcedureDataAsyncAsDataTable("proKhoNhomHang", new { action = "GET_DATA" });
            grcNhomHang.DataSource = dt;
        }

        [Obsolete]
        public void GetBieuDo()
        {
            Text = "Tỷ Suất Lợi Nhuận";
            var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoTySuatLoiNhuan", new { ngaythang = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), manhom = strMaNhom });
            if (dataTable1.Rows.Count == 0) { return; }
            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            // Create two series.
            Series seriesGiaVon = new Series("Giá vốn", ViewType.Bar);
            seriesGiaVon.LabelsVisibility = DefaultBoolean.True;

            Series seriesDoanhThu = new Series("Doanh thu", ViewType.Bar);
            seriesDoanhThu.LabelsVisibility = DefaultBoolean.True;

            Series seriesTySuat = new Series("Tỷ suất", ViewType.Line);
            seriesTySuat.LabelsVisibility = DefaultBoolean.True;

            foreach (DataRow dr in dataTable1.Rows)
            {
                seriesGiaVon.Points.Add(new SeriesPoint(dr["tenhanghoa"], dr["giavonxuat"]));
                seriesDoanhThu.Points.Add(new SeriesPoint(dr["tenhanghoa"], dr["doanhthu"]));
                seriesTySuat.Points.Add(new SeriesPoint(dr["tenhanghoa"], dr["tysuat"]));
            }
            //Format
            seriesGiaVon.Label.TextPattern = "{V:#,##0}";
            seriesDoanhThu.Label.TextPattern = "{V:#,##0}";
            seriesTySuat.Label.TextPattern = "{V:#,##0.0}";

            // Add both series to the chart.
            chartControl1.Series.AddRange(new Series[] { seriesGiaVon, seriesDoanhThu, seriesTySuat });

            // Hide the legend (optional).
            chartControl1.Legend.Visible = false;

            // Create two secondary axes, and add them to the chart's Diagram.
            SecondaryAxisX myAxisX = new SecondaryAxisX("my X-Axis");
            SecondaryAxisY myAxisY = new SecondaryAxisY("my Y-Axis");

            ((XYDiagram)chartControl1.Diagram).SecondaryAxesX.Add(myAxisX);
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Add(myAxisY);

            // Assign the series2 to the created axes.
            ((LineSeriesView)seriesTySuat.View).AxisX = myAxisX;
            ((LineSeriesView)seriesTySuat.View).AxisY = myAxisY;

            // Customize the appearance of the secondary axes (optional).
            myAxisX.Title.Text = "Tỷ suất lợi nhuận";
            myAxisX.Title.Visible = true;
            myAxisX.Title.TextColor = Color.Red;
            myAxisX.Label.TextColor = Color.Red;
            myAxisX.Color = Color.Red;

            myAxisY.Title.Text = "Tỷ suất lợi nhuận";
            myAxisY.Title.Visible = true;
            myAxisY.Title.TextColor = Color.White;
            myAxisY.Label.TextColor = Color.White;
            myAxisY.Color = Color.White;

            chartControl1.Legend.Visibility = DefaultBoolean.True;

            XYDiagram diagram = chartControl1.Diagram as XYDiagram;
            diagram.AxisY.Label.TextPattern = "{V:#,##0}";

            Legend legend = chartControl1.Legend;
            legend.Margins.All = 8;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            legend.AlignmentVertical = LegendAlignmentVertical.Top;
            legend.Direction = LegendDirection.LeftToRight;

            // Add the chart to the form.
            panelControl.Controls.Add(chartControl1);
        }

        [Obsolete]
        private void UcTySuatLoiNhuan_Load(object sender, EventArgs e)
        {
            GetNhomHang();
            GetBieuDo();
        }

    }
}
