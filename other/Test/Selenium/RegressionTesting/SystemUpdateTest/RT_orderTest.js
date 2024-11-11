import { Builder, By, until } from 'selenium-webdriver';

const orderTest = async () => {
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        await driver.get('http://your-ecommerce-site-url.com');
        await driver.findElement(By.name('search')).sendKeys('laptop');
        await driver.findElement(By.id('searchButton')).click();
        await driver.wait(until.elementLocated(By.css('.product-list .product-item')), 10000);
        await driver.findElement(By.css('.product-list .product-item')).click();
        await driver.wait(until.elementLocated(By.id('addToCartButton')), 10000);
        await driver.findElement(By.id('addToCartButton')).click();
        await driver.wait(until.elementLocated(By.id('cartNotification')), 10000);
        await driver.findElement(By.id('cartLink')).click();
        await driver.wait(until.elementLocated(By.id('checkoutButton')), 10000);
        await driver.findElement(By.id('checkoutButton')).click();
        await driver.wait(until.elementLocated(By.name('address')), 10000);
        await driver.findElement(By.name('address')).sendKeys('123 Test St, Test City');
        await driver.findElement(By.name('payment')).sendKeys('4111111111111111');
        await driver.findElement(By.id('submitOrderButton')).click();
        await driver.wait(until.elementLocated(By.id('orderConfirmationMessage')), 10000);
        const confirmationMessage = await driver.findElement(By.id('orderConfirmationMessage')).getText();
        if (!confirmationMessage.includes('Thank you for your order')) {
            throw new Error('Order test failed: Confirmation message not found.');
        }
        console.log('Order test passed!');
    } catch (err) {
        console.error(`Order test failed: ${err}`);
    } finally {
        await driver.quit();
    }
};

export default orderTest;
