//用於驗證購物車功能是否正確添加和移除商品。

//required
import { Builder, By, until } from 'selenium-webdriver';

const runTest = async () => {
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        await driver.get('http://your-shopping-site-url.com');

        //添加商品到購物車
        await driver.findElement(By.id('addItemButton')).click();

        //等待購物車更新 5000ms
        await driver.wait(until.elementLocated(By.id('cartItemCount')), 5000);

        //驗證商品是否添加到購物車
        let itemCount = await driver.findElement(By.id('cartItemCount')).getText();
        if (parseInt(itemCount) === 1) {
            console.log('Item added to cart successfully.');
        } else {
            console.error('Failed to add item to cart.');
        }

        //移除商品從購物車
        await driver.findElement(By.id('removeItemButton')).click();

        //等待購物車更新 5000ms
        await driver.wait(until.elementLocated(By.id('cartItemCount')), 5000);

        //驗證商品是否從購物車移除
        itemCount = await driver.findElement(By.id('cartItemCount')).getText();
        if (parseInt(itemCount) === 0) {
            console.log('Item removed from cart successfully.');
        } else {
            console.error('Failed to remove item from cart.');
        }
    } catch (err) {
        console.error(`Test failed: ${err}`);
    } finally {
        await driver.quit();
    }
};

runTest();
