using NUnit.Framework;
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
            string name = "Fritz";
            artist.Name = name;

            Assert.AreEqual(name, artist.Name);
        }

        [Test]
        public void TestCountry() {
            string country = "USA";
            artist.Country = country;

            Assert.AreEqual(country, artist.Country);
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
            artist.Name = "Florian";

            string expected = "[33] Florian";
            Assert.AreEqual(expected, artist.ToString());
        }
    }
}