using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	internal static class PerformanceExtensions {

		public static bool IsEqualTo(this Performance p1, Performance p2) {
			return p1.Id == p2.Id
				&& p1.Start == p2.Start
				&& p1.End == p2.End
				&& p1.VenueId == p2.VenueId
				&& p1.ArtistId == p2.ArtistId;
		}
	}

	internal class PerformanceDAOTest : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private IPerformanceDAO dao;
		private Performance performance1;
		private Performance performance2;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();

			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreatePerformanceDAO(db);

			performance1 = new Performance {
				Id = 1,
				ArtistId = 1,
				Start = new DateTime(2015, 12, 31),
				End = new DateTime(2015, 12, 31),
				VenueId = 1
			};

			performance2 = new Performance {
				Id = 2,
				ArtistId = 2,
				Start = new DateTime(2015, 12, 31),
				End = new DateTime(2015, 12, 31),
				VenueId = 2
			};

			IList<string> sqls = new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `performance`",
				"INSERT INTO `performance` (`id`, `artist_id`, `venue_id`, `start`, `end`) "
				+ "VALUES (1, 1, 1, '2015-12-31', '2015-12-31')",
				"INSERT INTO `performance` (`id`, `artist_id`, `venue_id`, `start`, `end`) "
				+ "VALUES (2, 2, 2, '2015-12-31', '2015-12-31')"
			};
			runDbCommands(db, sqls);
		}

		[TearDown]
		public void TearDown() {
			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=1"
			});
			transaction.Dispose();
		}

		[Test]
		public void TestGetAll() {
			IEnumerable<Performance> performances = dao.GetAll();

			Assert.AreEqual(2, performances.Count());

			Performance v;
			IEnumerator<Performance> enu = performances.GetEnumerator();
			Assert.True(enu.MoveNext());
			v = enu.Current;
			Assert.True(v.IsEqualTo(performance1));
			Assert.True(enu.MoveNext());
			v = enu.Current;
			Assert.True(v.IsEqualTo(performance2));
			Assert.False(enu.MoveNext());
		}

		[Test]
		public void TestGetById() {
			Performance v1 = dao.GetById(1);
			Performance v2 = dao.GetById(2);
			Performance nonExistingVenue = dao.GetById(3);

			Assert.True(v1.IsEqualTo(performance1));
			Assert.True(v2.IsEqualTo(performance2));
			Assert.IsNull(nonExistingVenue);
		}

		[Test]
		public void TestUpdate() {
			var v1Tmp = new Performance {
				Id = performance1.Id,
				ArtistId = performance1.ArtistId,
				Start = performance1.Start,
				End = performance1.End,
				VenueId = 1
			};
			Assert.True(performance1.IsEqualTo(v1Tmp));
			performance1.ArtistId = 3;
			performance1.Start = new DateTime(2015, 12, 31);
			performance1.End = new DateTime(2015, 12, 31);
			performance1.VenueId = 3;
			Assert.False(performance1.IsEqualTo(v1Tmp));
			Assert.True(dao.Update(performance1).IsEqualTo(performance1));
			Assert.True(dao.GetById(performance1.Id).IsEqualTo(performance1));
		}

		[Test]
		public void TestUpdateFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.Update(new Performance { Id = 4 }));
		}

		[Test]
		public void TestCreate() {
			var newPerformance = new Performance {
				Id = -1,
				ArtistId = 1,
				Start = new DateTime(2015, 12, 31),
				End = new DateTime(2015, 12, 31),
				VenueId = 1
			};
			newPerformance = dao.Create(newPerformance);
			Assert.True(newPerformance.Id != -1);
			Assert.True(dao.GetById(newPerformance.Id).IsEqualTo(newPerformance));
		}

		[Test]
		public void TestDelete() {
			var oldCnt = dao.GetAll().Count();
			dao.Delete(new Performance { Id = 2 });
			Assert.AreEqual(oldCnt - 1, dao.GetAll().Count());
			Assert.IsNotNull(dao.GetById(1));
		}

		[Test]
		public void TestDeleteFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.Delete(new Performance { Id = 3 }));
		}
	}
}