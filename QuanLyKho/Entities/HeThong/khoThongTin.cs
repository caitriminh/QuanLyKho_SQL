using Dapper.Contrib.Extensions;

namespace QuanLyKho.Entities.HeThong
{
    [Table("khoThongTin")]
    public class khoThongTin
    {
        [ExplicitKey]
        public string tencty { get; set; }
        public string tencty2 { get; set; }
        public string diachi { get; set; }
        public string sodt { get; set; }
        public string sofax { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public string masothue { get; set; }
        public string nguoidaidien { get; set; }
    }
}
