from playwright.sync_api import sync_playwright
import re
import os
import time  # Thêm import cho thư viện time

def sanitize_filename(filename):
    # Thay thế các ký tự không hợp lệ trong tên file
    return re.sub(r'[<>:"/\\|?*]', '_', filename)

def test_localhost():
    # Khởi tạo Playwright
    with sync_playwright() as p:
        # Danh sách các trình duyệt để kiểm tra
        browsers = {
            "Chromium": p.chromium,
            "Firefox": p.firefox,
            "WebKit": p.webkit
        }
        
        for browser_name, browser_type in browsers.items():
            print(f"Testing on {browser_name}...")
            
            browser = browser_type.launch(headless=False)
            page = browser.new_page()
            
            start_time = time.time()

            page.goto("http://localhost:50135/")
            
            page.wait_for_load_state("networkidle")
            
            end_time = time.time()

            load_time = end_time - start_time
            print(f"Load time for {browser_name}: {load_time:.2f} seconds")

            safe_browser_name = sanitize_filename(browser_name)
            screenshot_path = os.path.join("D:\\HOCTAP", f"localhost_page_{safe_browser_name}.png")
            page.screenshot(path=screenshot_path)
            print(f"Screenshot taken for {browser_name} and saved to {screenshot_path}.")
            
            browser.close()

# Chạy hàm test
test_localhost()
