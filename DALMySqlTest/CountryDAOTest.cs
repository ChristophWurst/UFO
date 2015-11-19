using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	internal static class CountryExtensions {

		public static bool IsEqualTo(this Country c1, Country c2) {
			return c1.Id == c2.Id
				&& c1.Name == c2.Name;
		}
	}

	[TestFixture]
	internal class CountryDAOIntegrationTests : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private ICountryDAO dao;
		private Country country;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();

			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreateCountryDAO(db);

			country = new Country {
				Id = 1,
				Name = "Österreich"
			};

			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `country`",
				"INSERT INTO `country` VALUES (1, 'Österreich')",
				"INSERT INTO `country` VALUES (2, 'Belgien')"
			});
		}

		[TearDown]
		public void TearDown() {
			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=1"
			});
			transaction.Dispose();
		}

		[Test]
		public void testGetAll() {
			var countries = dao.GetAll();

			Assert.AreEqual(2, countries.Count());
			var e = countries.GetEnumerator();
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.Current.IsEqualTo(country));
		}

		[Test]
		public void TestGetById() {
			var c = dao.GetById(1);
			Assert.IsTrue(c.IsEqualTo(country));
		}

		[Test]
		public void TestGetByIdFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.GetById(3));
		}
	}
}