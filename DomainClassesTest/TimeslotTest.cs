using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class TimeSlotTest {
		private TimeSlot timeslot;

		[SetUp]
		public void SetUp() {
			timeslot = new TimeSlot();
		}

		[Test]
		public void TestId() {
			int id = 4;
			timeslot.Id = id;
			Assert.AreEqual(id, timeslot.Id);
		}

		[Test]
		public void TestStart() {
			timeslot.Start = 13;
			Assert.AreEqual(13, timeslot.Start);
		}

		[Test]
		public void TestEnd() {
			timeslot.End = 14;
			Assert.AreEqual(14, timeslot.End);
		}

		[Test]
		public void TestToString() {
			timeslot.Id = 3;
			timeslot.Start = 13;
			timeslot.End = 15;

			string expected = $"[3] {timeslot.Start} {timeslot.End}";
			Assert.AreEqual(expected, timeslot.ToString());
		}
	}
}