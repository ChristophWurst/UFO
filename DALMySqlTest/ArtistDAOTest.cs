using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	internal static class ArtistExtensions {

		public static bool IsEqualTo(this Artist a1, Artist a2) {
			return a1.Id == a2.Id
				&& a1.Name == a2.Name
				&& a1.Image == a2.Image
				&& a1.Video == a2.Video
				&& a1.CategoryId == a2.CategoryId
				&& a1.CountryId == a2.CountryId
				&& a1.Email == a2.Email;
		}
	}

	[TestFixture]
	internal class ArtistDAOIntegrationTest : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private IArtistDAO dao;
		private Artist artist1;
		private Artist artist2;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();

			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreateArtistDAO(db);

			artist1 = new Artist {
				Id = 1,
				Name = "Christoph The Wurst",
				Image = "some-image",
				Video = "some-video",
				CategoryId = 3,
				CountryId = 15,
				Email = "christoph@wurst.at",
			};
			artist2 = new Artist {
				Id = 2,
				Name = "Stefan The Rösch",
				Image = "some-image",
				Video = "some-video",
				CategoryId = 4,
				CountryId = 7,
				Email = "stefan@roesch.at",
			};

			var sqls = new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `artist`",
				"INSERT INTO `artist` VALUES (1, 'Christoph The Wurst', 'some-image', 'some-video', 3, 15, 'christoph@wurst.at')",
				"INSERT INTO `artist` VALUES (2, 'Stefan The Rösch', 'some-image', 'some-video', 4, 7, 'stefan@roesch.at')"
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
			var artists = dao.GetAll();

			Assert.AreEqual(2, artists.Count());

			var enu = artists.GetEnumerator();
			Assert.IsTrue(enu.MoveNext());
			Assert.IsTrue(enu.Current.IsEqualTo(artist1));
			Assert.IsTrue(enu.MoveNext());
			Assert.IsTrue(enu.Current.IsEqualTo(artist2));
			Assert.IsFalse(enu.MoveNext());
		}

		[Test]
		public void TestGetById() {
			var artist = dao.GetById(1);

			Assert.IsTrue(artist.IsEqualTo(artist1));
		}

		[Test]
		public void TestGetByIdFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.GetById(3));
		}

		[Test]
		public void TestCreate() {
			var newArtist = new Artist {
				Id = -1,
				Name = "Florian The Tremmel",
				Image = "the-image",
				Video = "the-video",
				CategoryId = 13,
				CountryId = 3,
				Email = "florian@tremmel.at"
			};
			newArtist = dao.Create(newArtist);
			Assert.True(newArtist.Id != -1);
			Assert.True(dao.GetById(newArtist.Id).IsEqualTo(newArtist));
		}

		[Test]
		public void TestUpdate() {
			const int id = 2;
			var artist = dao.GetById(id);

			artist.Name = "Florian The Tremmel";
			artist.Image = "new-image";
			artist.Video = "new-video";
			artist.CategoryId += 1;
			artist.CountryId += 2;
			artist.Email = "florian@tremmel.at";

			Assert.True(dao.Update(artist).IsEqualTo(artist));
			Assert.IsTrue(dao.GetById(id).IsEqualTo(artist));
		}

		[Test]
		public void TestUpdateFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.Update(new Artist { Id = 4 }));
		}

		[Test]
		public void TestDelete() {
			var oldCnt = dao.GetAll().Count();
			dao.Delete(new Artist { Id = 2 });
			Assert.AreEqual(oldCnt - 1, dao.GetAll().Count());
			Assert.IsNotNull(dao.GetById(1)); // should still be in the db
		}

		[Test]
		public void TestDeleteFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.Delete(new Artist { Id = 3 }));
		}

		[Test]
		public void TestGetForCategory() {
			var artists = dao.GetForCategory(new Category() { Id = 4 });
			Assert.AreEqual(1, artists.Count());
			Assert.AreEqual(artist2.Id, artists.FirstOrDefault().Id);
		}
	}
}