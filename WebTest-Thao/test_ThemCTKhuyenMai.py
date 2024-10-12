import asyncio
from playwright.async_api import async_playwright

async def navigate_to_create_promotion(page):
    """Điều hướng đến trang Tạo chương trình khuyến mãi."""
    await page.click('a[href="/Admin/Promotion"]')
    await page.click('a[href="/Admin/CreatePromotion"]')

async def test_km01(page):
    await navigate_to_create_promotion(page)
    await page.click('input[type="submit"].btn.btn-primary')
    
    # Kiểm tra thông báo lỗi cho trường "Tên chương trình"
    error_message = await page.text_content("span.field-validation-error[for='TenKhuyenMai']")
    assert error_message == "Tên chương trình không được để trống"
    print("KM01: Passed")


async def test_km02(page):
    await navigate_to_create_promotion(page)
    await page.fill('input[name="TenKhuyenMai"]', "Siêu khuyến mãi hè")
    await page.click('input[type="submit"].btn.btn-primary')
    error_message = await page.text_content("span.field-validation-error[for='MoTa']")
    assert error_message == "Mô tả không được để trống"
    print("KM02: Passed")

async def test_km03(page):
    await navigate_to_create_promotion(page)
    await page.fill('input[name="TenKhuyenMai"]', "Chương trình hợp lệ")
    await page.fill('input[name="MoTa"]', "Mô tả hợp lệ")
    await page.fill('input[name="PhanTramGiamGia"]', "")  # Để trống phần trăm giảm giá
    await page.click('input[type="submit"].btn.btn-primary')
    error_message = await page.text_content("span.field-validation-error[for='PhanTramGiamGia']")
    assert error_message == "Phần trăm giảm giá không được để trống"
    print("KM03: Passed")

async def test_km04(page):
    await navigate_to_create_promotion(page)
    await page.fill('input[name="TenKhuyenMai"]', "Chương trình hợp lệ")
    await page.fill('input[name="MoTa"]', "Mô tả hợp lệ")
    await page.fill('input[name="PhanTramGiamGia"]', "50")
    await page.fill('input[name="NgayBatDau"]', "")  # Để trống ngày bắt đầu
    await page.click('input[type="submit"].btn.btn-primary')
    error_message = await page.text_content("span.field-validation-error[for='NgayBatDau']")
    assert error_message == "Ngày bắt đầu không được để trống"
    print("KM04: Passed")

async def test_km05(page):
    await navigate_to_create_promotion(page)
    await page.fill('input[name="TenKhuyenMai"]', "Chương trình hợp lệ")
    await page.fill('input[name="MoTa"]', "Mô tả hợp lệ")
    await page.fill('input[name="PhanTramGiamGia"]', "50")
    await page.fill('input[name="NgayBatDau"]', "2024-10-10")
    await page.fill('input[name="NgayKetThuc"]', "")  # Để trống ngày kết thúc
    await page.click('input[type="submit"].btn.btn-primary')
    error_message = await page.text_content("span.field-validation-error[for='NgayKetThuc']")
    assert error_message == "Ngày kết thúc không được để trống"
    print("KM05: Passed")

async def test_km06(page):
    await navigate_to_create_promotion(page)
    await page.fill('input[name="TenKhuyenMai"]', "Chương trình hợp lệ")
    await page.fill('input[name="MoTa"]', "Mô tả hợp lệ")
    await page.fill('input[name="PhanTramGiamGia"]', "50")
    await page.fill('input[name="NgayBatDau"]', "2024-10-10")
    await page.fill('input[name="NgayKetThuc"]', "2024-10-09")  # Ngày kết thúc nhỏ hơn ngày bắt đầu
    await page.click('input[type="submit"].btn.btn-primary')
    error_message = await page.text_content("span.field-validation-error[for='NgayKetThuc']")
    assert error_message == "Ngày kết thúc phải lớn hơn ngày bắt đầu."
    print("KM06: Passed")

async def test_km07(page):
    await navigate_to_create_promotion(page)
    await page.fill('input[name="TenKhuyenMai"]', "Chương trình trùng lặp")
    await page.fill('input[name="MoTa"]', "Mô tả hợp lệ")
    await page.fill('input[name="PhanTramGiamGia"]', "50")
    await page.fill('input[name="NgayBatDau"]', "2024-10-10")
    await page.fill('input[name="NgayKetThuc"]', "2024-10-20")
    await page.click('input[type="submit"].btn.btn-primary')

    # Giả sử đã có chương trình khuyến mãi "Chương trình trùng lặp"
    error_message = await page.text_content("span.field-validation-error[for='TenKhuyenMai']")
    assert error_message == "Tên chương trình đã tồn tại. Vui lòng chọn một tên khác."
    print("KM07: Passed")

async def test_km08(page):
    await navigate_to_create_promotion(page)
    await page.fill('input[name="TenKhuyenMai"]', "Chương trình hợp lệ")
    await page.fill('input[name="MoTa"]', "Mô tả hợp lệ")
    await page.fill('input[name="PhanTramGiamGia"]', "50")
    await page.fill('input[name="NgayBatDau"]', "2024-10-10")
    await page.fill('input[name="NgayKetThuc"]', "2024-10-20")
    await page.click('input[type="submit"].btn.btn-primary')

    # Kiểm tra xem có chuyển hướng về trang quản lý khuyến mãi không
    await page.wait_for_url("http://localhost:50135/Admin/Promotion")
    assert page.url == "http://localhost:50135/Admin/Promotion"
    print("KM08: Passed")

async def test_login():
    async with async_playwright() as p:
        browser = await p.chromium.launch(headless=False)  # Để xem quá trình test
        page = await browser.new_page()

        # Điều hướng đến trang đăng nhập
        await page.goto("http://localhost:50135/Login/Login")

        # Điền tài khoản vào trường "Tài khoản"
        await page.fill('input[placeholder="Tài khoản"]', 'admin')

        # Điền mật khẩu vào trường "Mật khẩu"
        await page.fill('input[placeholder="Mật khẩu"]', '123456')

        # Nhấn vào nút "Đăng nhập"
        await page.click('button[type="submit"].btn.theme-button')

        # Chờ để xem nếu trang chuyển hướng đến trang Admin
        await page.wait_for_url("http://localhost:50135/Admin")

        # In ra thông báo nếu đăng nhập thành công
        if page.url == "http://localhost:50135/Admin":
            print("Đăng nhập thành công!")

            # Gọi các test case
            await test_km01(page)
            await test_km02(page)
            await test_km03(page)
            await test_km04(page)
            await test_km05(page)
            await test_km06(page)
            await test_km07(page)
            await test_km08(page)
        else:
            print("Đăng nhập thất bại!")

        # Đóng trình duyệt
        await browser.close()

# Chạy chương trình
asyncio.run(test_login())
