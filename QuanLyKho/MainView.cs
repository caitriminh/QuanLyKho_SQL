using AutoUpdaterDotNET;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using QuanLyKho.BieuDo;
using QuanLyKho.DanhMuc;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using QuanLyKho.Extension;
using QuanLyKho.HeThong;
using QuanLyKho.NghiepVu;
using QuanLyKho.Properties;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace QuanLyKho
{
    public partial class MainView : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //ITelegramBotClient botClient;
        public MainView()
        {
            InitializeComponent();
            InitSkins();
            InitSkinGallery();
            Assembly asm = typeof(OfficeSkins).Assembly;
            SkinManager.Default.RegisterAssembly(asm);
            //Load giao diện
            UserLookAndFeel.Default.SkinName = ConfigAppSetting.GetSetting("ApplicationSkinName");
            var skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            DevExpress.Utils.Svg.SvgPalette fireBall = skin.CustomSvgPalettes[ConfigAppSetting.GetSetting("ApplicationPalletName")];
            if (fireBall != null)
            {
                skin.SvgPalettes[Skin.DefaultSkinPaletteName].SetCustomPalette(fireBall);
            }
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

            FormClosing += (s, e) =>
            {
                ConfigAppSetting.SetSetting("ApplicationSkinName", UserLookAndFeel.Default.SkinName);
                ConfigAppSetting.SetSetting("ApplicationPalletName", UserLookAndFeel.Default.ActiveSvgPaletteName);
            };

            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //botClient = new TelegramBotClient("1613158640:AAGCE6BfRZ16S5av6TmaCj8qlFtgWJV2Bhs");
            //var me = botClient.GetMeAsync().Result;
            //Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
            //botClient.OnMessage += BotClient_OnMessage;
            //botClient.StartReceiving();
        }

        private static MainView _defaultInstance;
        public static MainView Instance
        {
            get
            {
                if (_defaultInstance == null)
                {
                    _defaultInstance = new MainView();
                }
                return _defaultInstance;
            }
            set => _defaultInstance = value;
        }

        //private async void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        //{
        //    if (e.Message.Text != null)
        //    {
        //        Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

        //        await botClient.SendTextMessageAsync(
        //          chatId: e.Message.Chat,
        //          text: "You said:\n" + e.Message.Text
        //        );
        //        //barTrangThai.BeginInvoke(new Action(() =>
        //        //{
        //        //    label1.Text = $"Ông chủ Thảo Meo nói mở {e.Message.Text}";
        //        //}));

        //        if (e.Message.Text.ToLower() == "close")
        //        {
        //            Application.Exit();
        //        }
        //        else if (e.Message.Text.ToLower() == "maygioroi")
        //        {
        //            await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "Result said:\n" + $"Bây giờ là: {DateTime.Now}");
        //        }
        //        else if (e.Message.Text.ToLower() == "update")
        //        {
        //            AutoUpdater.Start("http://triminh.xyz/update/update.xml");
        //        }
        //        else if (e.Message.Text.ToLower() == "vinagiay")
        //        {
        //            Process.Start("C:\\Program Files (x86)\\VINAGIAY\\VINAGIAY\\VINAGIAY.exe");
        //        }

        //    }
        //}

        public void InitSkins()
        {
            SkinManager.EnableFormSkins();
            BonusSkins.Register();
        }

        private void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rib_skin, true);
        }

        public void OpenForm(Type typeform)
        {
            foreach (var frm in MdiChildren.Where(frm => frm.GetType() == typeform))
            {
                frm.Activate();
                return;
            }

            BeginInvoke(new Action(() =>
            {
                var form = (Form)(Activator.CreateInstance(typeform));
                form.MdiParent = this;
                form.Show();
            }));
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("http://triminh.xyz/update/update.xml");

        }

        public void GetPhanQuyen()
        {
            //Hệ thống
            var dt1 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 1 });
            btn_NguoiDung.Enabled = dt1.xem == true;
            btnPhanQuyen.Enabled = dt1.xem == true;
            btnXoaDuLieu.Enabled = dt1.xem == true;

            var dt2 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 2 });
            btn_nhatky_hethong.Enabled = dt2.xem == true;

            //Danh mục
            var dt4 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 4 });
            btn_donvitinh.Enabled = dt4.xem == true;
            btnDonViTinh2.Enabled = dt4.xem == true;

            var dt5 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 5 });
            btn_nhomhang.Enabled = dt5.xem == true;

            var dt6 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 6 });
            btn_ncc.Enabled = dt6.xem == true;

            var dt7 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 7 });
            btn_khachhang.Enabled = dt7.xem == true;

            var dt8 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 8 });
            btn_hanghoa.Enabled = dt8.xem == true;

            //Nghiệp vụ
            var dt9 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 9 });
            btn_dinhmuc.Enabled = dt9.xem == true;

            var dt10 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 10 });
            btn_nhapkho.Enabled = dt10.xem == true;
            if (dt10.xem == true)
            {
                OpenForm(typeof(FrmNhapkho));
            }

            var dt11 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 11 });
            btn_xuatkho.Enabled = dt11.xem == true;

            //Báo cáo
            var dt12 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 12 });
            btn_tonkho.Enabled = dt12.xem == true;
            btnXuLyAmKho.Enabled = dt12.xem == true;
            btnChuyenSoDu.Enabled = dt12.xem == true;

            var dt13 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 13 });
            btn_baocao_banhang.Enabled = dt13.xem == true;

            var dt14 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 14 });
            btn_nhapxuatkho.Enabled = dt14.xem == true;

            var dt16 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 16 });
            btnLoaiChiPhi.Enabled = dt16.xem == true;
            btnLoaiChiPhi2.Enabled = dt16.xem == true;

            var dt17 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 17 });
            btnTongHopChiPhi.Enabled = dt17.xem == true;

            var dt18 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 18 });
            btnLoiNhuan.Enabled = dt18.xem == true;

            //Công nợ
            var dt19 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 19 });
            btnPhaiThu.Enabled = dt19.xem == true;
            btnPhaiTra.Enabled = dt19.xem == true;

            //Biều đồ doanh thu
            var dt20 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 20 });
            barDoanhThu.Enabled = dt20.xem == true;
            barDoanhThu.Enabled = dt20.xem == true;

            //Biều đồ chi phí
            var dt21 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 21 });
            barChiPhi.Enabled = dt21.xem == true;
            barChiPhi.Enabled = dt21.xem == true;

            //Biểu đồ lợi nhuận
            var dt22 = ExecSQL.ExecProcedureDataFistOrDefault<khoPhanQuyen>("prokhoPhanQuyen", new { action = "PHANQUYEN", tendangnhap = Data.Data._strtendangnhap.ToUpper(), mamenu = 22 });
            barLoiNhuan.Enabled = dt22.xem == true;
            barLoiNhuan.Enabled = dt22.xem == true;
        }


        private void Btn_thongtin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FrmThongTin();
            frm.ShowDialog();
        }

        private void Btn_Dong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Bạn có muốn thoát khỏi chương trình không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            Application.Exit();
        }

        private void Btn_NguoiDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmNguoiDung));
        }

        private void Btn_doimatkhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FrmDoiMatKhau();
            frm.ShowDialog();
        }

        private void Btn_nhatky_hethong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmHT_NhatKyHeThong));
        }

        private void Btn_donvitinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmDonViTinh));
        }

        private void Btn_nhomhang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmNhomHang));
        }

        private void Btn_khachhang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmKhachHang));
        }

        private void Btn_khoaungdung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data.Data._flag_login = false;
            FrmHT_DangNhap frm = new FrmHT_DangNhap();
            FrmHT_MaskedDialog.ShowDialog(this, frm);
        }

        private void Btn_ncc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmNCC));
        }

        private void Btn_hanghoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmHangHoa));
        }

        private void Btn_quydoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmDinhMucLinhKien));
        }

        private void Btn_nhapkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmNhapkho));
        }

        private void Btn_xuatkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmXuatKho));
        }

        private void Btn_tonkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmTonkho));
        }

        private void Btn_baocao_banhang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmBaoCaoBanHang));
        }

        private void Btn_nhapxuatkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmNhapXuatKho));
        }

        private void BtnPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmPhanQuyen));
        }

        private void BtnLoaiChiPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmLoaiChiPhi));
        }

        private void BtnTongHopChiPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmTongHopChiPhi));
        }

        private void BtnLoiNhuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmLoiNhuan));
        }

        private void BtnDonViTinh2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmDonViTinh));
        }

        private void BtnLoaiChiPhi2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmLoaiChiPhi));
        }

        private void BtnChuyenSoDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FrmKetChuyenSoDu();
            frm.ShowDialog();
        }

        private void BtnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AutoUpdater.Start("http://triminh.xyz/update/update.xml");
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                var dialog = XtraMessageBox.Show($"Phần mềm quản lý kho có phiên bản mới {args.CurrentVersion}. Phiên bản bạn đang sử dụng hiện tại  {args.InstalledVersion}. Bạn có muốn cập nhật phần mềm không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog.Equals(DialogResult.Yes) || dialog.Equals(DialogResult.OK))
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception exception)
                    {
                        XtraMessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    using (FrmHT_DangNhap frm = new FrmHT_DangNhap())
                    {
                        if (FrmHT_MaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                        {
                            GetPhanQuyen();

                        }
                    }
                }
            }
            else
            {
                //XtraMessageBox.Show("Phiên bản bạn đang sử dụng đã được cập nhật mới nhất.", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (FrmHT_DangNhap frm = new FrmHT_DangNhap())
                {
                    if (FrmHT_MaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                    {
                        GetPhanQuyen();
                    }
                }
            }
        }

        private void BtnXuLyAmKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmXuLyAmKho));
        }


        private void BtnPhaiTra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmPhaiTra));
        }


        private void BtnPhaiThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmPhaiThu));
        }

        private void BtnTacGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmHT_GioiThieu frm = new FrmHT_GioiThieu();
            frm.ShowDialog();
        }

        private void RibbonControl1_Merge(object sender, RibbonMergeEventArgs e)
        {
            ribbonControl1.SelectedPage = e.MergeOwner.MergedPages["Print Preview"];
            RibbonControl parentRRibbon = sender as RibbonControl;
            RibbonControl childRibbon = e.MergedChild;
            parentRRibbon.StatusBar.MergeStatusBar(childRibbon.StatusBar);
        }

        private void RibbonControl1_UnMerge(object sender, RibbonMergeEventArgs e)
        {
            RibbonControl parentRRibbon = sender as RibbonControl;
            parentRRibbon.StatusBar.UnMergeStatusBar();
        }

        private void barDoanhThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmBieuDoDoanhThu));
        }

        private void barChiPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmBieuDoChiPhi));
        }

        private void barLoiNhuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmBieuDoLoiNhuan));
        }
    }
}
