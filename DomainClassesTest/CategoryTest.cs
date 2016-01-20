using NUnit.Framework;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class CategoryTest {
		private Category category;

		[SetUp]
		public void SetUp() {
			category = new Category();
		}

		[Test]
		public void TestId() {
			int id = 4;

			category.Id = id;
			Assert.AreEqual(id, category.Id);
		}

		[Test]
		public void TestDescription() {
			string desc = "Test description";

			category.Description = desc;
			Assert.AreEqual(desc, category.Description);
		}

		[Test]
		public void TestColor() {
			string color = "Test color";

			category.Color = color;
			Assert.AreEqual(color, category.Color);
		}

		[Test]
		public void TestToString() {
			category.Id = 3;
			category.Description = "Test Category";

			string expected = "[3] Test Category";
			Assert.AreEqual(expected, category.ToString());
		}
	}
}