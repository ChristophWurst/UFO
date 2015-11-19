using NUnit.Framework;
using System;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class PerformanceTest {
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

			string expected = "[154]";
			Assert.AreEqual(expected, performance.ToString());
		}
	}
}