//驗證用戶從登錄到修改個人資料的操作。

import { Builder, By, until } from 'selenium-webdriver';

const runTest = async () => {
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        await driver.get('http://your-login-page-url.com');

        //找到用戶名和密碼輸入框，並輸入測試憑證
        await driver.findElement(By.name('username')).sendKeys('testuser');
        await driver.findElement(By.name('password')).sendKeys('password');

        //點擊登錄按鈕
        await driver.findElement(By.id('loginButton')).click();

        //等待 10000ms 並檢查是否登錄成功
        await driver.wait(until.elementLocated(By.id('welcomeMessage')), 10000);

        //驗證登錄成功信息
        const welcomeMessage = await driver.findElement(By.id('welcomeMessage')).getText();
        if (!welcomeMessage.includes('Welcome, testuser')) {
            throw new Error('Login test failed: Welcome message not found.');
        }

        //導航到個人資料頁面
        await driver.findElement(By.id('profileLink')).click();

        //等待個人資料頁面加載
        await driver.wait(until.elementLocated(By.id('profileForm')), 10000);

        //修改個人資料（如修改用戶名或電子郵件）
        const usernameField = await driver.findElement(By.name('username'));
        await usernameField.clear();
        await usernameField.sendKeys('newtestuser');

        const emailField = await driver.findElement(By.name('email'));
        await emailField.clear();
        await emailField.sendKeys('newtestuser@example.com');

        //提交修改
        await driver.findElement(By.id('saveButton')).click();

        //等待並檢查修改成功消息
        await driver.wait(until.elementLocated(By.id('successMessage')), 10000);

        //驗證修改成功信息
        const successMessage = await driver.findElement(By.id('successMessage')).getText();
        if (!successMessage.includes('Profile updated successfully')) {
            throw new Error('Profile update test failed: Success message not found.');
        }

        console.log('End-to-end test passed!');
    } catch (err) {
        console.error(`End-to-end test failed: ${err}`);
    } finally {
        await driver.quit();
    }
};

runTest();
