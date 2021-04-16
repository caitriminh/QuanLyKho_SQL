using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.HeThong
{
    [Table("khoPhanQuyen")]
    public class khoPhanQuyen
    {
        [Key]
        public int id { get; set; }
        public string tendangnhap { get; set; }
        public int? mamenu { get; set; }
        public bool? xem { get; set; }
        public bool? luu { get; set; }
        public bool? xoa { get; set; }
        public bool? sua { get; set; }
        public bool? inan { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
