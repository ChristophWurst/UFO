using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	internal static class AreaExtensions {

		public static bool IsEqualTo(this Area a1, Area a2) {
			return a1.Id == a2.Id
				&& a1.Name == a2.Name;
		}
	}

	[TestFixture]
	internal class AreaDAOIntegrationTest : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private IAreaDAO dao;
		private Area area1;
		private Area area2;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();

			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreateAreaDAO(db);

			area1 = new Area {
				Id = 1,
				Name = "Hauptplatz"
			};
			area2 = new Area {
				Id = 2,
				Name = "Rathausplatz"
			};

			var sqls = new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `area`",
				"INSERT INTO `area` VALUES (1, 'Hauptplatz')",
				"INSERT INTO `area` VALUES (2, 'Rathausplatz')"
			};
			runDbCommands(db, sqls);
		}

		[TearDown]
		public void TearDown() {
			transaction.Dispose();

			var sqls = new List<string> {
				"SET FOREIGN_KEY_CHECKS=1"
			};
			runDbCommands(db, sqls);
		}

		[Test]
		public void TestGetAll() {
			var areas = dao.GetAll();

			Assert.AreEqual(2, areas.Count());

			var e = areas.GetEnumerator();
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.Current.IsEqualTo(area1));
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.Current.IsEqualTo(area2));
			Assert.IsFalse(e.MoveNext());
		}

		[Test]
		public void TestGetById() {
			var area = dao.GetById(1);
			var notFound = dao.GetById(3);

			Assert.IsTrue(area.IsEqualTo(area1));
			Assert.IsNull(notFound);
		}
	}
}