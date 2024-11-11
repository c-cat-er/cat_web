//用於測試登錄頁面是否正確驗證用戶憑證。

//required
import { Builder, By, until } from 'selenium-webdriver';

const loginTest = async () => {
    //創建一個新的 WebDriver 實例，並使用 Chrome 瀏覽器
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        //打開登錄頁面
        await driver.get('http://your-login-page-url.com'); //本地亦可 http://localhost:

        //找到用戶名和密碼輸入框，並輸入測試憑證
        await driver.findElement(By.name('username')).sendKeys('testuser');
        await driver.findElement(By.name('password')).sendKeys('password');

        //點擊登錄按鈕
        await driver.findElement(By.id('loginButton')).click();

        //等待 10000ms 並檢查是否登錄成功，根據頁面特徵確認登錄成功
        //until 是一組用來描述條件的工具，告訴 WebDriver 需要等待什麼條件滿足。通常和 wait 一起使用。
        await driver.wait(until.elementLocated(By.id('welcomeMessage')), 10000);

        //驗證登錄成功信息
        const welcomeMessage = await driver.findElement(By.id('welcomeMessage')).getText();
        if (welcomeMessage.includes('Welcome, testuser')) {
            console.log('Login test passed!');
        } else {
            console.log('Login test failed: Welcome message not found.');
        }
    } catch (err) {
        console.error(`Login test failed: ${err}`);
    } finally {
        //關閉瀏覽器
        await driver.quit();
    }
};

loginTest();
