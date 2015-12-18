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
		public void TestSpectacledasTimeSlot() {
			performance.SpectacledayTimeSlot = 1;
			Assert.AreEqual(1, performance.SpectacledayTimeSlot);
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