using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class SpectracledayTimeSlotTest {
		private SpectacledayTimeSlot st;

		[SetUp]
		public void SetUp() {
			st = new SpectacledayTimeSlot();
		}

		[Test]
		public void TestId() {
			int id = 4;
			st.Id = id;
			Assert.AreEqual(id, st.Id);
		}

		[Test]
		public void TestTimeSlotId() {
			int id = 3;
			st.TimeSlotId = id;
			Assert.AreEqual(id, st.TimeSlotId);
		}

		[Test]
		public void TestSpectracledayId() {
			int id = 2;
			st.SpectacledayId = id;
			Assert.AreEqual(id, st.SpectacledayId);
		}

		[Test]
		public void TestToString() {
			st.Id = 1;
			st.TimeSlotId = 2;
			st.SpectacledayId = 3;

			string expected = $"[{st.Id}] TimeSlotId: [{st.TimeSlotId}] SpectacledayId: [{st.SpectacledayId}]";
			Assert.AreEqual(expected, st.ToString());
		}
	}
}