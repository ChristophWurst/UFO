using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UFO.DomainClasses;

namespace UFO.Test.DomainClasses {

    [TestFixture]
    class UserTest {

        private User user;

        [SetUp]
        public void SetUp() {
            this.user = new User();
        }

        [Test]
        public void TestId() {
            int id = 13;
            user.Id = id;

            Assert.AreEqual(id, user.Id);
        }

        [Test]
        public void TestEmail() {
            string email = "user@example.com";
            user.Email = email;

            Assert.AreEqual(email, user.Email);
        }

        [Test]
        public void TestPassword() {
            string password = "123456";
            user.Password = password;

            Assert.AreEqual(password, user.Password);
        }

        [Test]
        public void TestToString() {
            user.Id = 1;
            user.Email = "florian@tremmel.at";
            user.Password = "4567";

            Assert.AreEqual("[1] florian@tremmel.at", user.ToString());
        }

    }
}
