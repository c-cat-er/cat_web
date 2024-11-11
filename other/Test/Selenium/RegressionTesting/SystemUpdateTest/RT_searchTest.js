import { Builder, By, until } from 'selenium-webdriver';

const searchTest = async () => {
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        await driver.get('http://your-ecommerce-site-url.com');
        await driver.findElement(By.name('search')).sendKeys('laptop');
        await driver.findElement(By.id('searchButton')).click();
        await driver.wait(until.elementLocated(By.css('.product-list .product-item')), 10000);
        const firstProduct = await driver.findElement(By.css('.product-list .product-item'));
        if (!firstProduct) {
            throw new Error('Search test failed: No products found.');
        }
        console.log('Search test passed!');
    } catch (err) {
        console.error(`Search test failed: ${err}`);
    } finally {
        await driver.quit();
    }
};

export default searchTest;
