using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class TimeslotTest {
		private Timeslot timeslot;

		[SetUp]
		public void SetUp() {
			timeslot = new Timeslot();
		}

		[Test]
		public void TestId() {
			int id = 4;
			timeslot.Id = id;
			Assert.AreEqual(id, timeslot.Id);
		}

		[Test]
		public void TestStart() {
			TimeSpan ts = new TimeSpan(1, 2, 3);
			timeslot.Start = ts;
			Assert.AreEqual(ts, timeslot.Start);
		}

		[Test]
		public void TestEnd() {
			TimeSpan ts = new TimeSpan(3, 2, 1);
			timeslot.End = ts;
			Assert.AreEqual(ts, timeslot.End);
		}

		[Test]
		public void TestToString() {
			timeslot.Id = 3;
			timeslot.Start = new TimeSpan(1, 2, 3);
			timeslot.End = new TimeSpan(3, 2, 1);

			string expected = $"[3] {timeslot.Start} {timeslot.End}";
			Assert.AreEqual(expected, timeslot.ToString());
		}
	}
}