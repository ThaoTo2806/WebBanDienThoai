from playwright.sync_api import sync_playwright
import re

def sanitize_filename(filename):
    # Thay thế các ký tự không hợp lệ trong tên file
    return re.sub(r'[<>:"/\\|?*]', '_', filename)

def test_google_search():
    # Khởi tạo Playwright
    with sync_playwright() as p:
        # Mở trình duyệt Chromium
        browser = p.chromium.launch(headless=False)
        page = browser.new_page()
        
        test_cases = [
            ("Playwright Python", True),  # Test Case 1
            ("@Playwright!@#", True),  # Test Case 2
            ("How to use Playwright with Python?", True),  # Test Case 3
            ("asdjklasdjklasdjkl", False),  # Test Case 4
        ]
        
        for query, should_have_results in test_cases:
            # Điều hướng đến Google
            page.goto("https://www.google.com")
            
            # Chấp nhận cookie (nếu có)
            if page.locator("button:has-text('I agree')").is_visible():
                page.click("button:has-text('I agree')")
            
            # Tìm kiếm với từ khóa
            page.fill("textarea[name='q']", query)
            page.press("textarea[name='q']", "Enter")
            
            # Chờ kết quả tìm kiếm tải xong
            page.wait_for_selector("h3")
            
            # Kiểm tra kết quả
            results_visible = page.locator("h3").count() > 0
            if should_have_results:
                assert results_visible, f"Expected results for query '{query}', but none were found."
            else:
                assert not results_visible, f"Expected no results for query '{query}', but some were found."
            
            # Chụp ảnh màn hình của trang kết quả
            safe_query = sanitize_filename(query)
            page.screenshot(path=f"google_search_results_{safe_query}.png")
        
        # Đóng trình duyệt
        browser.close()

# Chạy hàm test
test_google_search()
