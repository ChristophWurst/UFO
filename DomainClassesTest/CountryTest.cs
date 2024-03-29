﻿using NUnit.Framework;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

	[TestFixture]
	internal class CountryTest {
		private Country country;

		[SetUp]
		public void SetUp() {
			country = new Country();
		}

		[Test]
		public void TestId() {
			int id = 11;

			country.Id = id;
			Assert.AreEqual(id, country.Id);
		}

		[Test]
		public void TestName() {
			string name = "Austria";

			country.Name = name;
			Assert.AreEqual(name, country.Name);
		}

		[Test]
		public void TestToString() {
			country.Id = 14;
			country.Name = "Canada";

			Assert.AreEqual("[14] Canada", country.ToString());
		}
	}
}