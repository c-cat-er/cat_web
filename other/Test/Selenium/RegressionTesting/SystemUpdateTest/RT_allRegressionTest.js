//在進行系統升級後或其他時候，驗證整個系統的測試穩定性。

//引入所有測試檔
import loginTest from './loginTest.js';
import searchTest from './searchTest.js';
import orderTest from './orderTest.js';

const runRegressionTests = async () => {
    try {
        //執行所有測試
        await loginTest();
        await searchTest();
        await orderTest();
        console.log('All regression tests passed!');
    } catch (err) {
        console.error(`Regression tests failed: ${err}`);
    }
};

runRegressionTests();
