using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.NghiepVu
{
    [Table("khoPhieuNhap")]
    public class khoPhieuNhap
    {
        [ExplicitKey]
        public string maphieu { get; set; }
        public DateTime? ngaynhap { get; set; }
        public string mancc { get; set; }
        [Write(false)]
        public string ncc { get; set; }
        public string nguoilap { get; set; }
        public string diengiai { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
