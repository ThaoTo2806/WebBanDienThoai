using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace TestProject1
{
    public class PromotionTests
    {
        private List<KhuyenMaiTest> promotions;

        [SetUp]
        public void Setup()
        {
            promotions = new List<KhuyenMaiTest>
            {
                new KhuyenMaiTest { TenKhuyenMai = "Existing Promo", MoTa = "Description", PhanTramGiamGia = 20, NgayBatDau = DateTime.Now, NgayKetThuc = DateTime.Now.AddDays(10) }
            };
        }

        [Test]
        public void KiemTraTenChuongTrinhTrong_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest { TenKhuyenMai = "", MoTa = "Một mô tả", PhanTramGiamGia = 10, NgayBatDau = DateTime.Now, NgayKetThuc = DateTime.Now.AddDays(1) };
            string ex_result = "Tên chương trình không được để trống";
            string ac_result = promotion.Validate(promotion);
            Assert.AreEqual(ex_result, ac_result);
        }

        [Test]
        public void KiemTraMoTaTrong_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest { TenKhuyenMai = "Promo", MoTa = "" };
            string ex_result = "Mô tả không được để trống";
            string ac_result = promotion.Validate(promotion);
            Assert.AreEqual(ex_result, ac_result);
        }

        [Test]
        public void KiemTraPhanTramGiamGiaKhongHopLe_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest { TenKhuyenMai = "Existing Promo", MoTa = "Description", PhanTramGiamGia = 110 };
            string ex_result = "Phần trăm giảm giá phải nằm trong khoảng từ 0 đến 100";
            string ac_result = promotion.Validate(promotion);
            Assert.AreEqual(ex_result, ac_result);
        }

        [Test]
        public void KiemTraPhanTramGiamGiaAm_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest { TenKhuyenMai = "Existing Promo", MoTa = "Description", PhanTramGiamGia = -10 };
            string result = promotion.Validate(promotion);
            Assert.That(result, Is.EqualTo("Phần trăm giảm giá phải nằm trong khoảng từ 0 đến 100"));
        }

        [Test]
        public void KiemTraNgayBatDauTrong_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest { TenKhuyenMai = "Existing Promo", MoTa = "Description", PhanTramGiamGia = 10, NgayBatDau = null };
            string result = promotion.Validate(promotion);
            Assert.That(result, Is.EqualTo("Ngày bắt đầu không được để trống"));
        }

        [Test]
        public void KiemTraNgayKetThucTrong_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest { TenKhuyenMai = "Existing Promo", MoTa = "Description", PhanTramGiamGia = 20, NgayBatDau = DateTime.Now, NgayKetThuc = null };
            string result = promotion.Validate(promotion);
            Assert.That(result, Is.EqualTo("Ngày kết thúc không được để trống"));
        }

        [Test]
        public void KiemTraTenChuongTrinhTrung_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest { TenKhuyenMai = "Existing Promo" };
            string result = promotion.Validate(promotion);
            Assert.IsTrue(promotions.Any(p => p.TenKhuyenMai == promotion.TenKhuyenMai), "Tên chương trình đã tồn tại. Vui lòng chọn một tên khác.");
        }

        [Test]
        public void KiemTraNgayKetThucNhoHonNgayBatDau_TraVeThongBaoLoi()
        {
            var promotion = new KhuyenMaiTest
            {
                TenKhuyenMai = "Existing Promo",
                MoTa = "Description",
                PhanTramGiamGia = 20,
                NgayBatDau = DateTime.Parse("2024-06-30"),
                NgayKetThuc = DateTime.Parse("2024-06-01")
            };
            string result = promotion.Validate(promotion);
            Assert.That(result, Is.EqualTo("Ngày kết thúc phải lớn hơn ngày bắt đầu"));
        }
    }
}