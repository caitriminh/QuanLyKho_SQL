using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.NghiepVu
{
    [Table("khoChiTietPhieuXuat")]
    public class khoChiTietPhieuXuat
    {
        [ExplicitKey]
        public string maphieu { get; set; }
        public string mahanghoa { get; set; }
        public decimal? soluong { get; set; }
        public decimal? dongia { get; set; }
        [Write(false)]
        public decimal? thanhtien { get; set; }
        public decimal? soducuoiky { get; set; }
        public string ghichu { get; set; }
        public string makho { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
