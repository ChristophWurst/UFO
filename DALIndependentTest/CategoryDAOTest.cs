using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALIndependentTest {

    public static class CategoryExtensions {
        public static bool IsEqualTo(this Category c1, Category c2) {
            return c1.Id == c2.Id
                && c1.Description == c2.Description;
        }
    }

    [TestFixture]
    class CategoryDAOTest : IntegrationTest {

        private TransactionScope transaction;
        private IDatabase db;
        private ICategoryDAO dao;
        private Category category;

        [SetUp]
        public void SetUp() {
            transaction = new TransactionScope();
            DALFactory factory = DALFactory.GetInstance();
            db = factory.CreateDatabase();
            dao = factory.CreateCategoryDAO(db);
            category = new Category {
                Id = 1,
                Description = "Musik"
            };

            runDbCommands(db, new List<string> {
                "SET FOREIGN_KEY_CHECKS=0",
                "DELETE FROM `category`",
                "INSERT INTO `category` VALUES (1, 'Musik')",
                "INSERT INTO `category` VALUES (2, 'Tanz')"
            });
        }

        [TearDown]
        public void TearDown() {
            transaction.Dispose();

            runDbCommands(db, new List<string> {
                "SET FOREIGN_KEY_CHECKS=1"
            });
        }

        [Test]
        public void TestGetAll() {
            var categories = dao.GetAll();

            Assert.AreEqual(2, categories.Count());
            var e = categories.GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.IsTrue(e.Current.IsEqualTo(category));
        }

        [Test]
        public void TestGetById() {
            var cat = dao.GetById(1);
            var notFound = dao.GetById(3);

            Assert.IsTrue(cat.IsEqualTo(category));
            Assert.IsNull(notFound);
        }

    }

}
