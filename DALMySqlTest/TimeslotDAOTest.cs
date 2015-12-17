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
				Start = new TimeSpan(1, 2, 3),
				End = new TimeSpan(4, 5, 6)
			};
			timeslot2 = new TimeSlot {
				Id = 2,
				Start = new TimeSpan(3, 2, 1),
				End = new TimeSpan(6, 5, 4)
			};

			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `timeslot`",
				$"INSERT INTO `timeslot` VALUES (1, '{timeslot1.Start}', '{timeslot1.End}')",
				$"INSERT INTO `timeslot` VALUES (2, '{timeslot2.Start}', '{timeslot2.End}')"
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
	}
}