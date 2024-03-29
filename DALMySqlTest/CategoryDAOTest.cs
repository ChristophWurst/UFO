﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALMySqlTest {

	public static class CategoryExtensions {

		public static bool IsEqualTo(this Category c1, Category c2) {
			return c1.Id == c2.Id
				&& c1.Description == c2.Description
				&& c1.Color == c2.Color;
		}
	}

	[TestFixture]
	internal class CategoryDAOTest : IntegrationTest {
		private TransactionScope transaction;
		private IDatabase db;
		private ICategoryDAO dao;
		private Category category;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();
			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
			dao = factory.CreateCategoryDAO(db);
			category = new Category {
				Id = 1,
				Description = "Musik",
				Color = "red"
			};

			runDbCommands(db, new List<string> {
				"SET FOREIGN_KEY_CHECKS=0",
				"DELETE FROM `category`",
				"INSERT INTO `category` VALUES (1, 'Musik', 'red')",
				"INSERT INTO `category` VALUES (2, 'Tanz', 'blue')"
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
			var categories = dao.GetAll();

			Assert.AreEqual(2, categories.Count());
			var e = categories.GetEnumerator();
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.Current.IsEqualTo(category));
		}

		[Test]
		public void TestGetById() {
			var cat = dao.GetById(1);

			Assert.IsTrue(cat.IsEqualTo(category));
		}

		[Test]
		public void TestGetByIdFail() {
			Assert.Throws<EntityNotFoundException>(() => dao.GetById(3));
		}
	}
}