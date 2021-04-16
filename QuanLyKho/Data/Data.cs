using System;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QuanLyKho.Data
{
    public class Data
    {
        public static string _strtendangnhap = "ADMIN", _strmaphieu, _str_mancc, _str_makh, _str_tungay, _str_dennngay, _str_thang, _str_mahang, _str_manhomhang, _str_madvt;
        public static bool _bol_start, _edit, _amkho;
        public static int _report, _int_flag;
        public static DataSet _dsreport;
        public static DataTable _dtreport;
        public static string TEMP_PATH = Path.GetTempPath() + Assembly.GetExecutingAssembly().GetName().Name + "\\";
        public static void _run_history_log(string _str_thaotac, string _str_form)
        {
            string _str_hedieuhanh = Environment.OSVersion.ToString();
            string _str_tenmay = Dns.GetHostName();
            ExecSQL.ExecProcedureNonData("prokhoNhatKyHoatDong", new { action = "SAVE", tendangnhap = _strtendangnhap.ToUpper(), hedieuhanh = _str_hedieuhanh, tenmay = _str_tenmay, thaotac = _str_thaotac, form = _str_form });
        }

        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
    }
}
