using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	internal static class SpectacledayExtension {

		public static bool IsEqualTo(this Spectacleday s1, Spectacleday s2) {
			return s1.Id == s2.Id &&
				s1.Day == s2.Day;
		}
	}

	[TestFixture]
	internal class SpectacledayDAOTest : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private ISpectacledayDAO dao;
		private Spectacleday spectacleday1;
		private Spectacleday spectacleday2;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();
			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreateSpectacledayDAO(db);
			spectacleday1 = new Spectacleday {
				Id = 1,
				Day = new DateTime(1985, 11, 13)
			};

			spectacleday2 = new Spectacleday {
				Id = 2,
				Day = new DateTime(2015, 12, 15)
			};

			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `spectacleday`",
				"INSERT INTO `spectacleday` VALUES (1, '1985-11-13')",
				"INSERT INTO `spectacleday` VALUES (2, '2015-12-15')"
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
			var spectacledays = dao.GetAll();
			Assert.AreEqual(2, spectacledays.Count());
			var e = spectacledays.GetEnumerator();
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.Current.IsEqualTo(spectacleday1));
		}

		[Test]
		public void TestGetById() {
			var spec = dao.GetById(1);
			Assert.IsTrue(spec.IsEqualTo(spectacleday1));
		}

		[Test]
		public void TestGetByIdFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.GetById(3));
		}
	}
}