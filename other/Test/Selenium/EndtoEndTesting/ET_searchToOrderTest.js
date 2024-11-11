//測試從商品搜索到下訂單的流程是否正常。

import { Builder, By, until } from 'selenium-webdriver';

const runTest = async () => {
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        await driver.get('http://your-ecommerce-site-url.com');

        //搜索商品
        await driver.findElement(By.name('search')).sendKeys('laptop'); //laptop 是搜索關鍵詞
        await driver.findElement(By.id('searchButton')).click();

        //等待並選擇搜索結果中的第一個商品
        await driver.wait(until.elementLocated(By.css('.product-list .product-item')), 10000);
        const firstProduct = await driver.findElement(By.css('.product-list .product-item'));
        await firstProduct.click();

        //等待並添加商品到購物車
        await driver.wait(until.elementLocated(By.id('addToCartButton')), 10000);
        await driver.findElement(By.id('addToCartButton')).click();

        //確認商品已添加到購物車 (use 通知框)
        await driver.wait(until.elementLocated(By.id('cartNotification')), 10000);

        //確認商品已添加到購物車 (use 數量判定)
        await driver.wait(until.elementLocated(By.css('.cart-icon .item-count')), 10000);
        const itemCount = await driver.findElement(By.css('.cart-icon .item-count')).getText();
        if (parseInt(itemCount) < 1) {
            throw new Error('Item was not added to the cart.');
        }

        //前往購物車頁面
        await driver.findElement(By.id('cartLink')).click();

        //等待購物車頁面加載
        await driver.wait(until.elementLocated(By.id('checkoutButton')), 10000);

        //進行結帳
        await driver.findElement(By.id('checkoutButton')).click();

        //填寫結帳信息
        await driver.wait(until.elementLocated(By.name('address')), 10000);
        await driver.findElement(By.name('address')).sendKeys('123 Test St, Test City');
        await driver.findElement(By.name('payment')).sendKeys('4111111111111111');

        //提交訂單
        await driver.findElement(By.id('submitOrderButton')).click();

        //等待並檢查訂單確認頁面
        await driver.wait(until.elementLocated(By.id('orderConfirmationMessage')), 10000);
        const confirmationMessage = await driver.findElement(By.id('orderConfirmationMessage')).getText();
        if (!confirmationMessage.includes('Thank you for your order')) {
            throw new Error('Order test failed: Confirmation message not found.');
        }

        console.log('End-to-end order test passed!');
    } catch (err) {
        console.error(`End-to-end order test failed: ${err}`);
    } finally {
        await driver.quit();
    }
};

runTest();
