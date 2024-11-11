//在添加新功能後，測試現有功能是否受影響。

import { Builder, By, until } from 'selenium-webdriver';

//定義測試登錄功能的函數
const testLogin = async (driver) => {
    await driver.get('http://your-ecommerce-site-url.com/login');
    await driver.findElement(By.name('username')).sendKeys('testuser');
    await driver.findElement(By.name('password')).sendKeys('password');
    await driver.findElement(By.id('loginButton')).click();
    await driver.wait(until.elementLocated(By.id('welcomeMessage')), 10000);
    const welcomeMessage = await driver.findElement(By.id('welcomeMessage')).getText();
    if (!welcomeMessage.includes('Welcome, testuser')) {
        throw new Error('Login test failed: Welcome message not found.');
    }
    console.log('Login test passed');
};

//定義測試搜索功能的函數
const testSearch = async (driver) => {
    await driver.get('http://your-ecommerce-site-url.com');
    await driver.findElement(By.name('search')).sendKeys('laptop');
    await driver.findElement(By.id('searchButton')).click();
    await driver.wait(until.elementLocated(By.css('.product-list .product-item')), 10000);
    console.log('Search test passed');
};

//定義測試新增評論功能的函數
const testAddReview = async (driver) => {
    await driver.get('http://your-ecommerce-site-url.com/product/1');
    await driver.findElement(By.name('review')).sendKeys('Great product!');
    await driver.findElement(By.id('submitReviewButton')).click();
    await driver.wait(until.elementLocated(By.id('reviewSuccessMessage')), 10000);
    const reviewSuccessMessage = await driver.findElement(By.id('reviewSuccessMessage')).getText();
    if (!reviewSuccessMessage.includes('Review added successfully')) {
        throw new Error('Add review test failed: Success message not found.');
    }
    console.log('Add review test passed');
};

//主測試函數，運行所有回歸測試
const runRegressionTests = async () => {
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        //運行現有功能測試
        await testLogin(driver);
        await testSearch(driver);

        //添加新功能後運行新功能測試
        await testAddReview(driver);
    } catch (err) {
        console.error(`Test failed: ${err}`);
    } finally {
        await driver.quit();
    }
};

runRegressionTests();
