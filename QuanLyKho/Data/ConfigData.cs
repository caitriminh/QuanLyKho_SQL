using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKho.Data
{
    public class ConfigData
    {
        public static string CONNECTION_STRINGS = "server=triminh.xyz;uid=TriMinh; database=TriMinh; password=Diemthuong@2809;";
        //public static string CONNECTION_STRINGS = "server=.;uid=sa; database=TriMinh; password=minh123;";
        public static List<string> GetListDatabase(string serverName, string userNameDB, string passwordDB)
        {
            var listDB = new List<string>();
            try
            {
                string Conn = $"server={serverName};User Id={serverName}; pwd={passwordDB};";
                //string Conn = "server=triminh.xyz;uid=TriMinh; database=TriMinh; password=Diemthuong@2809;";
                //string Conn = "server=triminh.xyz;uid=TriMinh; database=TriMinh; password=Diemthuong@2809;";
                SqlConnection con = new SqlConnection(Conn);
                con.Open();

                //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM sys.database", con);
                //da.Fill(listDB);
                SqlCommand SqlCom = new SqlCommand();
                SqlCom.Connection = con;
                SqlCom.CommandType = CommandType.Text;
                SqlCom.CommandText = "SELECT name FROM sys.sysdatabases ORDER BY dbid desc";

                SqlDataReader SqlDR;
                SqlDR = SqlCom.ExecuteReader();

                while (SqlDR.Read())
                {
                    listDB.Add(SqlDR.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return listDB;
        }
    }
}
