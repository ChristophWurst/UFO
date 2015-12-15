using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class SpectacledayTest {
		private Spectacleday spectacleday;

		[SetUp]
		public void SetUp() {
			spectacleday = new Spectacleday();
		}

		[Test]
		public void TestId() {
			int id = 4;
			spectacleday.Id = id;
			Assert.AreEqual(id, spectacleday.Id);
		}

		[Test]
		public void TestDay() {
			DateTime day = new DateTime(1985, 11, 13);
			spectacleday.Day = day;
			Assert.AreEqual(day, spectacleday.Day);
		}

		[Test]
		public void TestToString() {
			spectacleday.Id = 1;
			spectacleday.Day = new DateTime(1985, 11, 13);
			string expected = $"[1] {spectacleday.Day}";
			Assert.AreEqual(expected, spectacleday.ToString());
		}
	}
}