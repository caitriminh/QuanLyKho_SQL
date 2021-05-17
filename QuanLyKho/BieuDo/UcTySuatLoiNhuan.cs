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
        public UcTySuatLoiNhuan()
        {
            InitializeComponent();
            Load += UcTySuatLoiNhuan_Load;
            grvViewNhomHang.FocusedRowChanged += GrvViewNhomHang_FocusedRowChanged;
        }

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

        public void GetBieuDo()
        {
            Text = "Tỷ Suất Lợi Nhuận";
            var dataTable1 = ExecSQL.ExecProcedureDataAsDataTable("prokhoTySuatLoiNhuan", new { ngaythang = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM01"), manhom = strMaNhom });
            var dataTable2 = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_HANGHOA", manhom = strMaNhom });
            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            panelControl.Controls.Add(chartControl1);
            Task.Factory.StartNew(() =>
            {

                chartControl1.DataSource = dataTable1;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesGiaVon = new Series("Giá vốn", ViewType.Bar);
                    seriesGiaVon.LabelsVisibility = DefaultBoolean.True;

                    Series seriesDoanhThu = new Series("Doanh thu", ViewType.Bar);
                    seriesDoanhThu.LabelsVisibility = DefaultBoolean.True;

                    // Add points to them, with their arguments different.
                    foreach (DataRow dr in dataTable1.Rows)
                        seriesGiaVon.Points.Add(new SeriesPoint(dr["tenhanghoa"], dr["giavonxuat"]));
                    seriesGiaVon.Label.TextPattern = "{V:#,##0}";


                    foreach (DataRow dr in dataTable2.Rows)
                        seriesDoanhThu.Points.Add(new SeriesPoint(dr["tenhanghoa"], dr["thanhtien"]));
                    seriesDoanhThu.Label.TextPattern = "{V:#,##0}";


                    chartControl1.Series.AddRange(new Series[] { seriesGiaVon });
                    chartControl1.Series.AddRange(new Series[] { seriesDoanhThu });
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

        private void UcTySuatLoiNhuan_Load(object sender, EventArgs e)
        {
            GetNhomHang();
            GetBieuDo();
        }

    }
}
