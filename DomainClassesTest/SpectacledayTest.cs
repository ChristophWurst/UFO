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
			spectacleday.day = day;
			Assert.AreEqual(day, spectacleday.day);
		}

		[Test]
		public void TestToString() {
			spectacleday.Id = 1;
			spectacleday.day = new DateTime(1985, 11, 13);

			Console.WriteLine(spectacleday.ToString());
			Assert.AreEqual("[1] 13.11.1985 00:00:00", spectacleday.ToString());
		}
	}
}