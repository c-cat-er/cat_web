using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NunitTrain1.Models;

namespace NunitTrain1
{
    public class UT_QueryTestTrain
    {//Unit Testing (�椸����)�A�ƾڬ����d�ߴ��սm�ߤ@

        private DbContextOptions<macTestContext> _dbContextOptions;
        private macTestContext _dbContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //�o�Ӥ�k�b�Ҧ����դ��e����@��
        }

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<macTestContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new macTestContext(_dbContextOptions);

            //��l�Ƽƾ�
            _dbContext.Customers.Add(new Customer { cid = 1, name = "CustomerName1", email = "11@example.com" });
            _dbContext.Customers.Add(new Customer { cid = 2, name = "CustomerName2", email = "22@example.com" });
            _dbContext.SaveChanges();
        }

        [Test]
        public void TestGetCustomerById()
        {//���լY id ���

            //���լd�ߤ�k id=1
            var customer = _dbContext.Customers.Find(1);

            //�ˬd customer ��H�O�_���� null
            Assert.That(customer, Is.Not.Null);

            //�ˬd�Ȥ᪺ name �M email �ݩʬO�_�ŦX�w����
            //�g�k�@ (���g�k�A�䤤�@���_�����ѡA���մN�|�ߧY���ѡA�åB�����_�����|�Q����C)
            //Assert.That(customer.name, Is.EqualTo("CustomerName1"));
            //Assert.That(customer.email, Is.EqualTo("11@example.com"));

            //�u�� �g�k�G (���g�k�A�䤤�@���_�����ѡA���|�~�����C)
            Assert.Multiple(() =>
            {
                Assert.That(customer.name, Is.EqualTo("CustomerName1"));
                Assert.That(customer.email, Is.EqualTo("11@example.com"));
            });
        }

        [TestCase(1, "CustomerName1")]
        [TestCase(2, "CustomerName2")]
        public void TestGetCustomerById(int id, string expectedName)
        {//���լY id ���

            var customer = _dbContext.Customers.Find(id);

            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.name, Is.EqualTo(expectedName));
        }

        [Test]
        public void TestGetCustomerById_InvalidCase()
        {//���դ��s�b id �O�_���T��^ null �Ӥ��O error

            var customer = _dbContext.Customers.Find(99);
            Assert.That(customer, Is.Null);
        }

        [TearDown]
        public void TearDown()
        {
            //�R���b UseInMemoryDatabase ���Ыت����s���ƾڮw�C�T�O�C�����չB��ɡA�ƾڮw���O�Ū��A�q�Ӥ��|���줧�e���ռƾڪ��v�T�C
            _dbContext.Database.EnsureDeleted();
            //���� DbContext ���귽�CDbContext �O�@�Ӹ귽�K������H�A���A�ݭn�ϥήɡA���ӽե� Dispose ��k������귽�A����s���|�C
            _dbContext.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //�o�Ӥ�k�b�Ҧ����դ������@��
        }

        [Test, Ignore("���������աA��]�O...")]
        public void IgnoredTest()
        {
            //���դ��e
        }

        [Test, Category("DatabaseTests")]
        public void DatabaseTest()
        {
            //���դ��e
        }

        [Test, Timeout(1000)]
        public void TestWithTimeout()
        {
            //���դ��e
        }

        [Test, Repeat(5)]
        public void TestMultipleTimes()
        {
            //���դ��e
        }
    }
}
