using NUnit.Framework;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	public class AreaTest {
		private Area area;

		[SetUp]
		public void SetUp() {
			area = new Area();
		}

		[Test]
		public void TestId() {
			int id = 17;
			area.Id = id;

			Assert.AreEqual(id, area.Id);
		}

		[Test]
		public void TestName() {
			string name = "Hauptplatz";
			area.Name = name;

			Assert.AreEqual(name, area.Name);
		}

		[Test]
		public void TestToString() {
			area.Id = 13;
			area.Name = "Festivalgelände";

			string expected = "[13] Festivalgelände";
			Assert.AreEqual(expected, area.ToString());
		}
	}
}