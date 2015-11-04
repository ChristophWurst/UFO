using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

    [TestFixture]
    public class ArtistTest {

        private Artist artist;

        [SetUp]
        public void SetUp() {
            artist = new Artist();
        }

        [Test]
        public void TestId() {
            int id = 122;
            artist.Id = id;

            Assert.AreEqual(id, artist.Id);
        }

        [Test]
        public void TestFirstname() {
            string firstname = "Fritz";
            artist.Firstname = firstname;

            Assert.AreEqual(firstname, artist.Firstname);
        }

        [Test]
        public void TestLastname() {
            string lastname = "Phantom";
            artist.Lastname = lastname;

            Assert.AreEqual(lastname, artist.Lastname);
        }

        [Test]
        public void TestCountry() {
            string country = "USA";
            artist.Firstname = country;

            Assert.AreEqual(country, artist.Firstname);
        }

        [Test]
        public void TestImage() {
            string imagePath = "C:\\cat.jpg";
            artist.Image = imagePath;

            Assert.AreEqual(imagePath, artist.Image);
        }

        [Test]
        public void TestVideo() {
            string videoPath = "C:\\cat.mp4";
            artist.Video = videoPath;

            Assert.AreEqual(videoPath, artist.Video);
        }

        [Test]
        public void TestCategoryId() {
            int catId = 123;
            artist.CategoryId = catId;

            Assert.AreEqual(catId, artist.CategoryId);
        }

        [Test]
        public void TestToString() {
            artist.Id = 33;
            artist.Firstname = "Florian";
            artist.Lastname = "Tremmel";

            string expected = "[33] Florian Tremmel";
            Assert.AreEqual(expected, artist.ToString());
        }

    }
}
