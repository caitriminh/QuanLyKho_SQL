using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.NghiepVu
{
    [Table("khoPhieuXuat")]
    public class khoPhieuXuat
    {
        [ExplicitKey]
        public string maphieu { get; set; }
        public string makh { get; set; }
        public string makho { get; set; }
        public DateTime? ngayxuat { get; set; }
        public string nguoilap { get; set; }
        public string diengiai { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
