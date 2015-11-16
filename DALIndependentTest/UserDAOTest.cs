using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DAL.Independent;
using UFO.DomainClasses;

namespace DALIndependentTest {

    [TestFixture]
    public class UserDAOTest {

        private Mock<IDatabase> dbMock;
        private IDatabase db;
        private Mock<DbCommand> commandMock;
        private DbCommand command;
        private Mock<IDataReader> readerMock;
        private IDataReader reader;
        private IUserDAO dao;

        [SetUp]
        public void SetUp() {
            dbMock = new Mock<IDatabase>();
            db = dbMock.Object;
            commandMock = new Mock<DbCommand>();
            command = commandMock.Object;
            readerMock = new Mock<IDataReader>();
            reader = readerMock.Object;

            dao = new UserDAO(db);
        }

        [Test]
        public void TestGetAll() {
            string sql = "SELECT * FROM `user`";
            User user = new User() {
                Id = 17,
                Email = "stean@rösch.at",
                Password = "asldfjaösdf"
            };

            dbMock.Setup(db => db.CreateCommand(sql)).Returns(command);
            dbMock.Setup(db => db.ExecuteReader(command)).Returns(reader);
            var readerValues = new Queue<bool>(new[] { true, false });
            readerMock.Setup(reader => reader.Read()).Returns(readerValues.Dequeue);
            readerMock.SetupGet(reader => reader["id"]).Returns(user.Id);
            readerMock.SetupGet(reader => reader["email"]).Returns(user.Email);
            readerMock.SetupGet(reader => reader["password"]).Returns(user.Password);

            IEnumerable<User> expected = new List<User>() { user };
            IEnumerable<User> result = dao.GetAll();

            var it = result.GetEnumerator();
            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(user.Id, it.Current.Id);
            Assert.AreEqual(user.Email, it.Current.Email);
            Assert.AreEqual(user.Password, it.Current.Password);
            Assert.IsFalse(it.MoveNext());
        }
    }
}
