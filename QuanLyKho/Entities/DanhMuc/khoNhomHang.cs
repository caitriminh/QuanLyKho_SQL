using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.DanhMuc
{
    [Table("KhoNhomHang")]
    public class khoNhomHang
    {
        [ExplicitKey]
        public string manhom { get; set; }
        public string nhomhang { get; set; }
        public bool? sudung { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
