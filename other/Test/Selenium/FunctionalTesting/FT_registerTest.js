//用於測試註冊頁面是否正確驗證用戶憑證。

//required
import { Builder, By, until } from 'selenium-webdriver';

const runRegisterTest = async () => {
    //創建一個新的 WebDriver 實例，並使用 Chrome 瀏覽器
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        //打開註冊頁面
        await driver.get('http://your-register-page-url.com'); //本地亦可使用 http://localhost:

        //找到註冊表單的各個輸入框，並輸入測試資料
        await driver.findElement(By.name('username')).sendKeys('testuser');
        await driver.findElement(By.name('email')).sendKeys('testuser@example.com');
        await driver.findElement(By.name('password')).sendKeys('password');
        await driver.findElement(By.name('confirmPassword')).sendKeys('password');

        //點擊註冊按鈕
        await driver.findElement(By.id('registerButton')).click();

        //等待 10000ms 並檢查是否註冊成功，根據頁面特徵確認註冊成功
        await driver.wait(until.elementLocated(By.id('welcomeMessage')), 10000);

        //驗證註冊成功信息
        const welcomeMessage = await driver.findElement(By.id('welcomeMessage')).getText();
        if (welcomeMessage.includes('Welcome, testuser')) {
            console.log('Registration test passed!');
        } else {
            console.error('Registration test failed: Welcome message not found.');
        }
    } catch (err) {
        console.error(`Registration test failed: ${err}`);
    } finally {
        //關閉瀏覽器
        await driver.quit();
    }
};

runRegisterTest();
