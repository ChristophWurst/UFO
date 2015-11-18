using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALIndependentTest {

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
    class ArtistDAOIntegrationTest : IntegrationTest {

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
            var notFound = dao.GetById(3);

            Assert.IsTrue(artist.IsEqualTo(artist1));
            Assert.IsNull(notFound);
        }

        [Test]
        public void TestCreate() {
            var newArtist = new Artist {
                Name = "Florian The Tremmel",
                Image = "the-image",
                Video = "the-video",
                CategoryId = 13,
                CountryId = 3,
                Email = "florian@tremmel.at"
            };

            var cntBefore = dao.GetAll().Count();

            // Insert it
            dao.Create(newArtist);

            var cntAfter = dao.GetAll().Count();
            Assert.AreEqual(cntBefore + 1, cntAfter);
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

            dao.Update(artist);
            var updatedArist = dao.GetById(id);

            Assert.IsTrue(updatedArist.IsEqualTo(artist));
        }
        
        [Test]
        public void TestDelete() {
            var oldCnt = dao.GetAll().Count();
            dao.Delete(new Artist { Id = 2 });
            Assert.AreEqual(oldCnt - 1, dao.GetAll().Count());
            Assert.IsNotNull(dao.GetById(1)); // should still be in the db
        }

    }

}
