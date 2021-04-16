using QuanLyKho.DanhMuc;
using QuanLyKho.NghiepVu;
using System;

namespace QuanLyKho.HeThong
{
    public partial class FrmHT_Report : DevExpress.XtraEditors.XtraForm
    {
        private static MainView _Instance;
        public static MainView Instance
        {
            get
            {
                if (_Instance == null || _Instance.IsDisposed)
                {
                    _Instance = new MainView();
                }
                return _Instance;
            }
            set => _Instance = value;
        }
        public FrmHT_Report()
        {
            InitializeComponent();
            try
            {
                MdiParent = MainView.Instance;
            }
            catch (Exception)
            {
                // throw;
            }

        }

        protected override void OnShown(EventArgs e)
        {
            if (Data.Data._report == 1)
            {
                this.Text = "Danh Mục Hàng Hóa";
                rpt_hanghoa rpt = new rpt_hanghoa();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 2)
            {
                this.Text = "Phiếu Nhập Kho";
                //rpt_phieunhapkho rpt = new rpt_phieunhapkho();
                rptPhieuNhap rpt = new rptPhieuNhap();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 3)
            {
                this.Text = "Phiếu Xuất Kho";
                //rpt_phieuxuatkho rpt = new rpt_phieuxuatkho();
                rptPhieuXuat rpt = new rptPhieuXuat();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 4)
            {
                this.Text = "Báo Cáo Doanh Thu";
                rpt_baocao_doanhthu rpt = new rpt_baocao_doanhthu();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 5)
            {
                this.Text = "Báo Cáo Tồn Kho";
                rpt_baocao_tonkho rpt = new rpt_baocao_tonkho();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 6)
            {
                this.Text = "Báo Cáo Chi Tiết Xuất Kho";
                rpt_baocao_chitiet_xuatkho rpt = new rpt_baocao_chitiet_xuatkho();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 7)
            {
                this.Text = "Báo Cáo Chi Tiết Nhập Kho";
                rpt_baocao_chitiet_nhapkho rpt = new rpt_baocao_chitiet_nhapkho();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 8)
            {
                this.Text = "Báo Cáo Tồn Kho (Số Lượng)";
                rpt_baocao_tonkho_soluong rpt = new rpt_baocao_tonkho_soluong();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 9)
            {
                this.Text = "Báo Cáo Lợi Nhuận";
                rpt_baocao_loinhuan rpt = new rpt_baocao_loinhuan();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 10)
            {
                this.Text = "Báo Cáo Chi Phí";
                rpt_baocao_chiphi rpt = new rpt_baocao_chiphi();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 11)
            {
                this.Text = "Bảng Báo Giá";
                rpt_bangbaogia rpt = new rpt_bangbaogia();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 12)
            {
                this.Text = "Phải Thu Khách Hàng";
                rpt_baocao_phaithu rpt = new rpt_baocao_phaithu();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
            else if (Data.Data._report == 13)
            {
                this.Text = "Phải Trả Nhà Cung Cấp";
                rpt_baocao_phaitra rpt = new rpt_baocao_phaitra();
                documentViewer1.PrintingSystem = rpt.PrintingSystem;
                rpt.DataSource = Data.Data._dtreport;
                rpt.BindData();
                rpt.CreateDocument();
            }
        }
    }
}
