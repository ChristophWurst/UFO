﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	internal static class UserExtensions {

		public static bool IsEqualTo(this User u1, User u2) {
			return u1.Id == u2.Id
				&& u1.Email == u2.Email
				&& u1.Password == u1.Password;
		}
	}

	internal class UserDAOIntegrationTests : IntegrationTest {
		private TransactionScope transaction;
		private IUserDAO dao;
		private User user1;
		private User user2;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();

			DALFactory factory = DALFactory.GetInstance();
			IDatabase db = factory.CreateDatabase();
			dao = factory.CreateUserDAO(db);

			user1 = new User {
				Id = 1,
				Email = "christoph@wurst.at",
				Password = "abcdefg"
			};
			user2 = new User {
				Id = 2,
				Email = "stefan@roesch.at",
				Password = "123456"
			};

			IList<string> sqls = new List<string> {
				"DELETE FROM `user`",
				"INSERT INTO `user` VALUES (1, 'christoph@wurst.at', 'abcdefg')",
				"INSERT INTO `user` VALUES (2, 'stefan@roesch.at', '123456')"
			};
			runDbCommands(db, sqls);
		}

		[TearDown]
		public void TearDown() {
			transaction.Dispose();
		}

		[Test]
		public void TestGetAll() {
			IEnumerable<User> users = dao.GetAll();

			Assert.AreEqual(2, users.Count());

			User u;
			IEnumerator<User> enu = users.GetEnumerator();
			Assert.True(enu.MoveNext());
			u = enu.Current;
			Assert.True(u.IsEqualTo(user1));
			Assert.True(enu.MoveNext());
			u = enu.Current;
			Assert.True(u.IsEqualTo(user2));
			Assert.False(enu.MoveNext());
		}

		[Test]
		public void TestGetById() {
			User u1 = dao.GetById(1);
			User u2 = dao.GetById(2);

			Assert.True(u1.IsEqualTo(user1));
			Assert.True(u2.IsEqualTo(user2));
		}

		[Test]
		public void TestGetByIdFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.GetById(3));
		}
	}
}