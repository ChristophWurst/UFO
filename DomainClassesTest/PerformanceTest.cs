using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

    [TestFixture]
    class PerformanceTest {

        private Performance performance;

        [SetUp]
        public void SetUp() {
            performance = new Performance();
        }

        [Test]
        public void TestId() {
            int id = 1;

            performance.Id = id;
            Assert.AreEqual(id, performance.Id);
        }

        [Test]
        public void TestStart() {
            DateTime start = DateTime.Now;

            performance.Start = start;
            Assert.AreEqual(start, performance.Start);
        }

        [Test]
        public void TestEnd() {
            DateTime end = DateTime.Now;

            performance.End = end;
            Assert.AreEqual(end, performance.End);
        }

        [Test]
        public void TestArtistId() {
            int artistId = 144;

            performance.ArtistId = artistId;
            Assert.AreEqual(artistId, performance.ArtistId);
        }

        [Test]
        public void TestVenueId() {
            int venueId = 32;

            performance.VenueId = venueId;
            Assert.AreEqual(venueId, performance.VenueId);
        }


        [Test]
        public void TestToString() {
            performance.Id = 154;
            performance.Start = new DateTime(2015, 10, 15, 18, 30, 00);
            performance.End = new DateTime(2015, 10, 15, 19, 30, 00);

            string expected = "[154] 15.10.2015 18:30:00-15.10.2015 19:30:00";
            Assert.AreEqual(expected, performance.ToString());
        }

    }

}
