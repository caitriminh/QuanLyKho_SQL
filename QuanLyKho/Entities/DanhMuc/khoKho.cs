using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.DanhMuc
{
    [Table("KhoKho")]
    public class khoKho
    {
        [ExplicitKey]
        public string makho { get; set; }
        public string tenkho { get; set; }
        public string diachi { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
