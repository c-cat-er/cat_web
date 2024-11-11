using Microsoft.EntityFrameworkCore;
using NunitTrain1.Models;

namespace NunitTrain1
{
    public class UT_AllDbTest
    {//Unit Testing (單元測試)，所有常見數據相關測試
        //需安裝
        ///Microsoft.NET.Test.Sdk
        ///NUnit
        ///NUnit3TestAdapter
        ///Microsoft.EntityFrameworkCore
        ///Microsoft.EntityFrameworkCore.InMemory

        private DbContextOptions<macTestContext> _dbContextOptions;
        private macTestContext _dbContext;

        [SetUp]
        public void Setup()
        {//use DI this 必需的
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
        public void TestInsertCustomer()
        {//測試數據插入
            var newCustomer = new Customer { cid = 3, name = "CustomerName3", email = "33@example.com" };
            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();

            var insertedCustomer = _dbContext.Customers.Find(3);
            Assert.That(insertedCustomer, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(insertedCustomer.name, Is.EqualTo("CustomerName3"));
                Assert.That(insertedCustomer.email, Is.EqualTo("33@example.com"));
            });
        }

        [Test]
        public void TestUpdateCustomer()
        {//測試數據更新
            var customer = _dbContext.Customers.Find(1);
            Assert.That(customer, Is.Not.Null);

            customer.email = "updated@example.com";
            _dbContext.SaveChanges();

            var updatedCustomer = _dbContext.Customers.Find(1);
            Assert.That(updatedCustomer.email, Is.EqualTo("updated@example.com"));
        }

        [Test]
        public void TestGetCustomersByMultipleConditions()
        {//測試多重條件查詢
            var customers = _dbContext.Customers
                .Where(c => c.name.Contains("Customer") && c.email.EndsWith("@example.com"))
                .ToList();

            Assert.That(customers, Has.Count.EqualTo(3));
        }

        [Test]
        public void TestComplexQuery()
        {//測試包含多表聯接的複雜查詢。
            ///假設 Orders 表存在，並且 Customers 和 Orders 表有關聯
            var orders = _dbContext.Orders
                .Where(o => o.customer_cid == 1)
                .Include(o => o.customer_c)
                .ToList();

            Assert.That(orders, Is.Not.Empty);
            Assert.That(orders.First().customer_c.name, Is.EqualTo("CustomerName1"));
        }

        [Test]
        public void TestCustomerCount()
        {//測試數據聚合函數，如計數、平均值等。
            var count = _dbContext.Customers.Count();
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void TestTransaction()
        {//k-p, 測試資料庫的事務操作，以確保數據一致性。
            //資料庫事務是一組操作，可以保證這些操作要麼全部成功，要麼全部失敗。
            //這對於維護資料的一致性和完整性非常重要，特別是在涉及多個資料庫操作的情況下。

            ///開始事務
            using (var transaction = _dbContext.Database.BeginTransaction())
            {///在此處執行的資料庫操作將在同一事務中進行
                try
                {
                    var customer = new Customer { cid = 4, name = "CustomerName4", email = "44@example.com" };
                    _dbContext.Customers.Add(customer);
                    _dbContext.SaveChanges();

                    ///其他操作...

                    ///提交事務
                    transaction.Commit();
                }
                catch
                {
                    ///若異常，回滾事務
                    transaction.Rollback();
                    throw;
                }
            }

            var insertedCustomer = _dbContext.Customers.Find(4);
            Assert.That(insertedCustomer, Is.Not.Null);
        }

        [Test]
        public void TestDeleteCustomer()
        {//測試數據刪除
            var customer = _dbContext.Customers.Find(3);
            Assert.That(customer, Is.Not.Null);

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            var deletedCustomer = _dbContext.Customers.Find(3);
            Assert.That(deletedCustomer, Is.Null);
        }

        [TearDown]
        public void TearDown()
        {//use DI this 必需的

            ///刪除在 UseInMemoryDatabase 中創建的內存中數據庫。確保每次測試運行時，數據庫都是空的，從而不會受到之前測試數據的影響。
            _dbContext.Database.EnsureDeleted();
            ///釋放 DbContext 的資源。DbContext 是一個資源密集型對象，當不再需要使用時，應該調用 Dispose 方法來釋放資源，防止內存洩漏。
            _dbContext.Dispose();
        }
    }
}
