using Newtonsoft.Json;
using QuanLyKho.Data;
using QuanLyKho.Entities.HeThong;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace QuanLyKho.HeThong
{
    public partial class FrmHT_GioiThieu
    {
        public FrmHT_GioiThieu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create by Tri Minh, Date: 21/11/2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAbout_Load(object sender, System.EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lbl_phienban.Text = ": " + fvi.FileVersion;
            var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoThongTin>("prokhoThongTin", new { action = "GET_DATA" });
            if (dt == null) { return; }
            lbl_tencty.Text = dt.tencty;
            lblDonVi.Text = ": " + dt.tencty;
            lbl_diachi.Text = dt.diachi;
            lbl_email.Text = dt.email;
            txtWebsite.Text = dt.website;
           
        }

        private void lbl_web_Click(object sender, System.EventArgs e)
        {
            //Module1.OpenUri(busThongTin.GetThongTin().Rows[0]["web"].ToString());
        }
    }
}

