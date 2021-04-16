using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.DanhMuc
{
    [Table("KhoHangHoa")]
    public class khoHangHoa
    {
        [ExplicitKey]
        public string mahanghoa { get; set; }
        public string mathamchieu { get; set; }
        public string manhom { get; set; }
        public string tenhanghoa { get; set; }
        public int? madvt { get; set; }
        [Write(false)]
        public string tendvt { get; set; }
        [Write(false)]
        public decimal dongia { get; set; }
        public string nguoitd2 { get; set; }
        [Write(false)]
        public DateTime? thoigian2 { get; set; }
        public string ghichu { get; set; }
        public string nguoitd { get; set; }
        public DateTime? thoigian { get; set; }
        public bool? sudung { get; set; }
        [Write(false)]
        public decimal? soducuoiky { get; set; }
        [Write(false)]
        public decimal? dongiaxuat { get; set; }
    }
}
