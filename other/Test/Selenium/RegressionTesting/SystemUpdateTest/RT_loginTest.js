import { Builder, By, until } from 'selenium-webdriver';

const loginTest = async () => {
    const driver = await new Builder().forBrowser('chrome').build();

    try {
        await driver.get('http://your-login-page-url.com');
        await driver.findElement(By.name('username')).sendKeys('testuser');
        await driver.findElement(By.name('password')).sendKeys('password');
        await driver.findElement(By.id('loginButton')).click();
        await driver.wait(until.elementLocated(By.id('welcomeMessage')), 10000);
        const welcomeMessage = await driver.findElement(By.id('welcomeMessage')).getText();
        if (!welcomeMessage.includes('Welcome, testuser')) {
            throw new Error('Login test failed: Welcome message not found.');
        }
        console.log('Login test passed!');
    } catch (err) {
        console.error(`Login test failed: ${err}`);
    } finally {
        await driver.quit();
    }
};

export default loginTest;
