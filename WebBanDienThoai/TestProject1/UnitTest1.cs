﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanDienThoai.Controllers;
using WebBanDienThoai.Models;

namespace TestProject1
{
    public class TestDbContext
    {
        public List<KHUYENMAI> KHUYENMAIs { get; set; }

        public TestDbContext(IEnumerable<KHUYENMAI> khuyenMais)
        {
            KHUYENMAIs = khuyenMais.ToList();
        }

        public IQueryable<KHUYENMAI> KHUYENMAIsQueryable => KHUYENMAIs.AsQueryable();
    }

    public class PromotionTests
    {
        private AdminController _control;

        [SetUp]
        public void Setup()
        {
            // Tạo danh sách khuyến mãi mẫu
            var existingPromotions = new List<KHUYENMAI>
            {
                new KHUYENMAI
                {
                    TenKhuyenMai = "Existing Promo",
                    MoTa = "Khuyến mãi cho mùa hè 1",
                    PhanTramGiamGia = 30,
                    NgayBatDau = DateTime.Parse("2024-06-01"),
                    NgayKetThuc = DateTime.Parse("2024-06-30")
                }
            };

            // Tạo cơ sở dữ liệu thử nghiệm với danh sách khuyến mãi
            var testDb = new TestDbContext(existingPromotions);
        }

        [TearDown]
        public void TearDown()
        {
            // Giải phóng tài nguyên nếu cần
            _control.Dispose();  // Nếu AdminController implements IDisposable
        }

        [Test]
        public void CheckDuplicatePromotion_ReturnsTrueIfDuplicateExists()
        {
            // Arrange
            string duplicateName = "Existing Promo";

            // Act
            var result = _control.CheckDuplicatePromotion(duplicateName) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Data); // Kiểm tra trả về true nếu có tên bị trùng
        }

        [Test]
        public void CheckDuplicatePromotion_ReturnsFalseIfNoDuplicateExists()
        {
            // Arrange
            string uniqueName = "Unique Promo";

            // Act
            var result = _control.CheckDuplicatePromotion(uniqueName) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.Data); // Kiểm tra trả về false nếu không có tên bị trùng
        }
    }
}