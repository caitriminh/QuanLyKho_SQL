using DevExpress.Utils;
using DevExpress.XtraCharts;
using QuanLyKho.Data;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKho.BieuDo
{
    public partial class UcDoanhThuHangHoa : DevExpress.XtraEditors.XtraUserControl
    {
        private string strMaNhom = "CARD";
        public UcDoanhThuHangHoa()
        {
            InitializeComponent();
            Load += UcDoanhThuHangHoa_Load;
            grvViewNhomHang.FocusedRowChanged += GrvViewNhomHang_FocusedRowChanged;
            GetNhomHang();
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
            Text = @"Doanh Thu Hàng Hóa";

            var chartControl1 = new ChartControl
            {
                Dock = DockStyle.Fill
            };
            panelControl.Controls.Add(chartControl1);
            Task.Factory.StartNew(() =>
            {
                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("prokhoBieuDo_DoanhThu", new { action = "DOANHTHU_HANGHOA", manhom = strMaNhom });
                chartControl1.DataSource = dataTable;
                chartControl1.BeginInvoke(new Action(() =>
                {
                    Series seriesGiatri = new Series("Doanh thu", ViewType.Bar);
                    seriesGiatri.LabelsVisibility = DefaultBoolean.True;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        seriesGiatri.Points.Add(new SeriesPoint(dr["tenhanghoa"], dr["thanhtien"]));
                    }
                    seriesGiatri.Label.TextPattern = "{V:#,##0}";

                    chartControl1.Series.AddRange(new[] { seriesGiatri });
                    chartControl1.Legend.Visibility = DefaultBoolean.True;

                    //Format Cột dọc
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

        private void UcDoanhThuHangHoa_Load(object sender, EventArgs e)
        {
            GetBieuDo();
        }

    }
}
