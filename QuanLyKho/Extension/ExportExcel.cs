using DevExpress.XtraEditors;
using OfficeOpenXml;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace QuanLyKho.Extension
{
    public class ExportExcel
    {
        public static void ExportExcelFromDataTable(DataTable table, string fullPathFileName, string SheetName = "Sheet 1", string WriteBeginCell = "A2", string passwordFile = "", bool isPrintHeader = true, bool isOpenFileExcel = true)
        {
            if (table.Rows.Count <= 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(fullPathFileName))
            {
                XtraMessageBox.Show($"Tên file {Path.GetFileName(fullPathFileName)} đã tồn tại! Đặt lại tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fileInfo = new FileInfo(Path.GetFileName(fullPathFileName));
            using (ExcelPackage pck = new ExcelPackage(fileInfo, passwordFile))
            {
                pck.Workbook.Properties.Author = "Cái Trí Minh";
                pck.Workbook.Properties.Company = "HOME";
                pck.Workbook.Properties.Title = "QUAN LY KHO (Excel)";
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(SheetName);
                ws.Cells[WriteBeginCell].LoadFromDataTable(table, isPrintHeader);
                pck.Save(passwordFile);
            }

            if (isOpenFileExcel)
            {
                Process.Start(fullPathFileName);
            }
        }
    }
}
