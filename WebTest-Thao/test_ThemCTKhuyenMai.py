import asyncio
from playwright.async_api import async_playwright

async def run_browser_test(task_id):
    async with async_playwright() as p:
        browser = await p.chromium.launch(headless=True)
        page = await browser.new_page()

        # Điều hướng đến Google
        await page.goto("https://www.google.com")

        # Điền từ khóa tìm kiếm với task_id để theo dõi
        search_term = f"Playwright Python {task_id}"
        await page.fill("textarea[name='q']", search_term)
        await page.press("textarea[name='q']", "Enter")

        # Chờ kết quả tìm kiếm tải xong
        await page.wait_for_selector("h3")

        # Chụp ảnh màn hình với tên file khác nhau cho mỗi task
        screenshot_path = f"google_search_results_{task_id}.png"
        await page.screenshot(path=screenshot_path)
        print(f"Task {task_id}: Search complete, screenshot saved at {screenshot_path}")

        await browser.close()

async def main():
    tasks = [run_browser_test(i) for i in range(5)]  # Chạy 5 task song song
    await asyncio.gather(*tasks)

# Chạy chương trình
asyncio.run(main())
