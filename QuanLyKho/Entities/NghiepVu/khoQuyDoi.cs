using Dapper.Contrib.Extensions;
using System;

namespace QuanLyKho.Entities.NghiepVu
{
    [Table("khoQuyDoi")]
    public class khoQuyDoi
    {
        [Key]
        public int? id { get; set; }
        public string mahanghoa { get; set; }
        [Write(false)]
        public string tenhanghoa1 { get; set; }
        [Write(false)]
        public string tendvt1 { get; set; }
        public string mahanghoa_qd { get; set; }
        public decimal? heso { get; set; }
        public decimal? thamsoquydoi { get; set; }
        [Write(false)]
        public decimal? slton { get; set; }
        public string ghichu { get; set; }
        public string nguoitd { get; set; }
        [Write(false)]
        public DateTime? thoigian { get; set; }
        public string nguoitd2 { get; set; }
        public DateTime? thoigian2 { get; set; }
    }
}
