using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class KhuyenMaiTest
    {
        public string TenKhuyenMai { get; set; }
        public string MoTa { get; set; }
        public int PhanTramGiamGia { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public string Validate(KhuyenMaiTest a)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(TenKhuyenMai))
                return "Tên chương trình không được để trống";

            if (string.IsNullOrEmpty(MoTa))
                return "Mô tả không được để trống";

            if (PhanTramGiamGia < 0 || PhanTramGiamGia > 100)
                return "Phần trăm giảm giá phải nằm trong khoảng từ 0 đến 100";

            if (NgayBatDau == null)
                return "Ngày bắt đầu không được để trống";

            if (NgayKetThuc == null)
                return "Ngày kết thúc không được để trống";

            if (NgayBatDau != null && NgayKetThuc != null && NgayKetThuc <= NgayBatDau)
                return "Ngày kết thúc phải lớn hơn ngày bắt đầu";

            if (TenKhuyenMai == a.TenKhuyenMai)
                return "Tên chương trình đã tồn tại. Vui lòng chọn một tên khác.";

            return null;
        }
    }
}
