using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class SpectracledayTimeslotTest {
		private SpectacledayTimeslot st;

		[SetUp]
		public void SetUp() {
			st = new SpectacledayTimeslot();
		}

		[Test]
		public void TestId() {
			int id = 4;
			st.Id = id;
			Assert.AreEqual(id, st.Id);
		}

		[Test]
		public void TestTimeslotId() {
			int id = 3;
			st.TimeslotId = id;
			Assert.AreEqual(id, st.TimeslotId);
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
			st.TimeslotId = 2;
			st.SpectacledayId = 3;

			string expected = "[1] TimeslotId: [2] SpectacledayId: [3]";
			Assert.AreEqual(expected, st.ToString());
		}
	}
}