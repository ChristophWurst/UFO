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

	internal static class VenueExtensions {

		public static bool IsEqualTo(this Venue v1, Venue v2) {
			return v1.AreaId == v2.AreaId
				&& v1.Desc == v2.Desc
				&& v1.Id == v2.Id
				&& v1.Latitude == v2.Latitude
				&& v1.Longitude == v2.Longitude
				&& v1.ShortDesc == v2.ShortDesc;
		}
	}

	internal class VenueDAOTest : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private IVenueDAO dao;
		private Venue venue1;
		private Venue venue2;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();

			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreateVenueDAO(db);

			venue1 = new Venue {
				Id = 1,
				AreaId = 1,
				Desc = "venue1",
				ShortDesc = "v1",
				Latitude = 18.00023020,
				Longitude = 34.0000023
			};

			venue2 = new Venue {
				Id = 2,
				AreaId = 2,
				Desc = "venue2",
				ShortDesc = "v2",
				Latitude = 23.3020,
				Longitude = 4.0046573
			};

			IList<string> sqls = new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `venue`",
				"INSERT INTO `venue` (`id`, `area_id`, `desc`, `short_desc`, `latitude`, `longitude`) "
				+ "VALUES (1, 1, 'venue1', 'v1', 18.00023020, 34.0000023)",
				"INSERT INTO `venue` (`id`, `area_id`, `desc`, `short_desc`, `latitude`, `longitude`) "
				+ "VALUES (2, 2, 'venue2', 'v2', 23.3020, 4.0046573)"
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
			IEnumerable<Venue> venues = dao.GetAll();
			Assert.AreEqual(2, venues.Count());
			Venue v;
			IEnumerator<Venue> enu = venues.GetEnumerator();
			Assert.True(enu.MoveNext());
			v = enu.Current;
			Assert.True(v.IsEqualTo(venue1));
			Assert.True(enu.MoveNext());
			v = enu.Current;
			Assert.True(v.IsEqualTo(venue2));
			Assert.False(enu.MoveNext());
		}

		[Test]
		public void TestGetById() {
			Venue v1 = dao.GetById(1);
			Venue v2 = dao.GetById(2);
			Venue nonExistingVenue = dao.GetById(3);

			Assert.True(v1.IsEqualTo(venue1));
			Assert.True(v2.IsEqualTo(venue2));
			Assert.IsNull(nonExistingVenue);
		}

		[Test]
		public void TestUpdate() {
			Venue v1Tmp = new Venue {
				Id = venue1.Id,
				AreaId = venue1.AreaId,
				Desc = venue1.Desc,
				ShortDesc = venue1.ShortDesc,
				Longitude = venue1.Longitude,
				Latitude = venue1.Latitude
			};
			Assert.True(venue1.IsEqualTo(v1Tmp));
			venue1.AreaId = 3;
			venue1.Desc = "x";
			venue1.ShortDesc = "x";
			venue1.Longitude = 1.0;
			venue1.Latitude = 1.0;
			Assert.False(venue1.IsEqualTo(v1Tmp));
			Assert.True(dao.Update(venue1).IsEqualTo(venue1));
			Assert.True(dao.GetById(venue1.Id).IsEqualTo(venue1));
		}

		[Test]
		public void TestUpdateFail() {
			var venue = new Venue();
			Assert.Throws<EntityNotFoundException>(() => dao.Update(venue));
		}

		[Test]
		public void TestCreate() {
			var newVenue = new Venue {
				Id = -1,
				AreaId = 1,
				Desc = "venue1",
				ShortDesc = "v1",
				Latitude = 18.00023020,
				Longitude = 34.0000023
			};
			newVenue = dao.Create(newVenue);
			Assert.True(newVenue.Id != -1);
			Assert.True(dao.GetById(newVenue.Id).IsEqualTo(newVenue));
		}
	}
}