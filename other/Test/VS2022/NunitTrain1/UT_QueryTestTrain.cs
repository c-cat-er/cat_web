using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NunitTrain1.Models;

namespace NunitTrain1
{
    public class UT_QueryTestTrain
    {//Unit Testing (單元測試)，數據相關查詢測試練習一

        private DbContextOptions<macTestContext> _dbContextOptions;
        private macTestContext _dbContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //這個方法在所有測試之前執行一次
        }

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<macTestContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new macTestContext(_dbContextOptions);

            //初始化數據
            _dbContext.Customers.Add(new Customer { cid = 1, name = "CustomerName1", email = "11@example.com" });
            _dbContext.Customers.Add(new Customer { cid = 2, name = "CustomerName2", email = "22@example.com" });
            _dbContext.SaveChanges();
        }

        [Test]
        public void TestGetCustomerById()
        {//測試某 id 資料

            //測試查詢方法 id=1
            var customer = _dbContext.Customers.Find(1);

            //檢查 customer 對象是否不為 null
            Assert.That(customer, Is.Not.Null);

            //檢查客戶的 name 和 email 屬性是否符合預期值
            //寫法一 (此寫法，其中一個斷言失敗，測試就會立即失敗，並且後續的斷言不會被執行。)
            //Assert.That(customer.name, Is.EqualTo("CustomerName1"));
            //Assert.That(customer.email, Is.EqualTo("11@example.com"));

            //優化 寫法二 (此寫法，其中一個斷言失敗，仍會繼續執行。)
            Assert.Multiple(() =>
            {
                Assert.That(customer.name, Is.EqualTo("CustomerName1"));
                Assert.That(customer.email, Is.EqualTo("11@example.com"));
            });
        }

        [TestCase(1, "CustomerName1")]
        [TestCase(2, "CustomerName2")]
        public void TestGetCustomerById(int id, string expectedName)
        {//測試某 id 資料

            var customer = _dbContext.Customers.Find(id);

            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.name, Is.EqualTo(expectedName));
        }

        [Test]
        public void TestGetCustomerById_InvalidCase()
        {//測試不存在 id 是否正確返回 null 而不是 error

            var customer = _dbContext.Customers.Find(99);
            Assert.That(customer, Is.Null);
        }

        [TearDown]
        public void TearDown()
        {
            //刪除在 UseInMemoryDatabase 中創建的內存中數據庫。確保每次測試運行時，數據庫都是空的，從而不會受到之前測試數據的影響。
            _dbContext.Database.EnsureDeleted();
            //釋放 DbContext 的資源。DbContext 是一個資源密集型對象，當不再需要使用時，應該調用 Dispose 方法來釋放資源，防止內存洩漏。
            _dbContext.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //這個方法在所有測試之後執行一次
        }

        [Test, Ignore("忽略此測試，原因是...")]
        public void IgnoredTest()
        {
            //測試內容
        }

        [Test, Category("DatabaseTests")]
        public void DatabaseTest()
        {
            //測試內容
        }

        [Test, Timeout(1000)]
        public void TestWithTimeout()
        {
            //測試內容
        }

        [Test, Repeat(5)]
        public void TestMultipleTimes()
        {
            //測試內容
        }
    }
}
