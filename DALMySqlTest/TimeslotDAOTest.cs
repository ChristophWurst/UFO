using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	public static class TimeSlotExtension {

		public static bool IsEqualTo(this TimeSlot t1, TimeSlot t2) {
			return t1.Id == t2.Id &&
				t1.Start == t2.Start &&
				t1.End == t2.End;
		}
	}

	[TestFixture]
	internal class TimeSlotDAOTest : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private ITimeSlotDAO dao;
		private TimeSlot timeslot1;
		private TimeSlot timeslot2;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();
			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreateTimeSlotDAO(db);
			timeslot1 = new TimeSlot {
				Id = 1,
				Start = 13,
				End = 14
			};
			timeslot2 = new TimeSlot {
				Id = 2,
				Start = 1,
				End = 2
			};

			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `timeslot`",
				$"INSERT INTO `timeslot` VALUES (1, '{timeslot1.Start}', '{timeslot1.End}')",
				$"INSERT INTO `timeslot` VALUES (2, '{timeslot2.Start}', '{timeslot2.End}')",
				"DELETE FROM `spectacleday_timeslot`",
				$"INSERT INTO `spectacleday_timeslot` VALUES (1, 1, 1)",
				$"INSERT INTO `spectacleday_timeslot` VALUES (2, 1, 1)"
			});
		}

		[TearDown]
		public void TearDown() {
			transaction.Dispose();
			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=1"
			});
		}

		[Test]
		public void TestGetAll() {
			var timeslots = dao.GetAll();

			Assert.AreEqual(2, timeslots.Count());
			var e = timeslots.GetEnumerator();
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.Current.IsEqualTo(timeslot1));
		}

		[Test]
		public void TestGetById() {
			var timeslot = dao.GetById(1);

			Assert.IsTrue(timeslot.IsEqualTo(timeslot1));
		}

		[Test]
		public void TestGetByIdFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.GetById(3));
		}

		[Test]
		public void TestGetForPerformance() {
			Performance p = new Performance() {
				SpectacledayTimeSlot = 1
			};
			Assert.IsTrue(dao.GetForPerformance(p) != null);
		}

		[Test]
		public void TestGetTimeSlotsForPerformances() {
			Performance p1 = new Performance() {
				SpectacledayTimeSlot = 1
			};
			Performance p2 = new Performance() {
				SpectacledayTimeSlot = 2
			};
			var performances = new List<Performance>();
			performances.Add(p1);
			performances.Add(p2);
			var timeSlots = dao.GetForPerformances(performances);
			Assert.True(timeSlots.Count() == 2);
			TimeSlot ts = timeSlots.First();
			Assert.True(timeSlots.First().IsEqualTo(timeslot1));
		}
	}
}