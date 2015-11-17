using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UFO.DAL.Common;
using UFO.DAL.Independent;
using UFO.DomainClasses;

namespace DALIndependentTest {

    internal static class Extensions {
        public static bool IsEqualTo(this User u1, User u2) {
            return u1.Id == u2.Id
                && u1.Email == u2.Email
                && u1.Password == u1.Password;
        }
    }

    [TestFixture]
    public class UserDAOUnitTests {

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

    public class UserDAOIntegrationTests {

        private TransactionScope transaction;
        private IUserDAO dao;
        private User user1;
        private User user2;

        private void runDbCommands(IDatabase db, IList<string> sqls) {
            foreach (string sql in sqls) {
                DbCommand cmd = db.CreateCommand(sql);
                db.ExecuteNonQuery(cmd);
            }
        }

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
            User nonExistingUser = dao.GetById(3);

            Assert.True(u1.IsEqualTo(user1));
            Assert.True(u2.IsEqualTo(user2));
            Assert.IsNull(nonExistingUser);
        }
    }
}
