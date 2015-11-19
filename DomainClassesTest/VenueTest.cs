using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class VenueTest {
		private Venue venue;

		[SetUp]
		public void SetUp() {
			venue = new Venue();
		}

		[SetUp]
		public void TestId() {
			int id = 1234;

			venue.Id = id;
			Assert.AreEqual(id, venue.Id);
		}

		[Test]
		public void TestAreaId() {
			int areaId = 1;

			venue.AreaId = areaId;
			Assert.AreEqual(areaId, venue.AreaId);
		}

		[Test]
		public void TestDescription() {
			string desc = "Description";

			venue.Description = desc;
			Assert.AreEqual(desc, venue.Description);
		}

		[Test]
		public void TestShortDescription() {
			string shortDesc = "short desc";

			venue.ShortDescription = shortDesc;
			Assert.AreEqual(shortDesc, venue.ShortDescription);
		}

		[Test]
		public void TestLatitude() {
			double lat = 48.3;

			venue.Latitude = lat;
			Assert.AreEqual(lat, venue.Latitude);
		}

		[Test]
		public void TestLongitude() {
			double lon = 14.2;

			venue.Longitude = lon;
			Assert.AreEqual(lon, venue.Longitude);
		}

		[Test]
		public void TestToString() {
			venue.Id = 134;
			venue.ShortDescription = "abc";
			venue.Description = "def";

			string expected = "[134] abc def";
			Assert.AreEqual(expected, venue.ToString());
		}
	}
}