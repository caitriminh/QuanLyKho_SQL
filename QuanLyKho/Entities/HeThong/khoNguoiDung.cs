using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.HeThong
{
    [Table("khoNguoiDung")]
    public class khoNguoiDung
    {
        [ExplicitKey]
        public string tendangnhap { get; set; }
        public string hoten { get; set; }
        public string matkhau { get; set; }
        public string ghichu { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
        [Write(false)]
        public string status { get; set; }
    }
}
