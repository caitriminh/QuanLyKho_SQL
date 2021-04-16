using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.DanhMuc
{
    [Table("KhoDonViTinh")]
    public class khoDonViTinh
    {
        [Key]
        public int? madvt { get; set; }
        public string tendvt { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
