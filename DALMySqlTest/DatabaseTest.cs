using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;

namespace DALMySqlTest {

	[TestFixture]
	internal class DatabaseTest {
		private IDatabase db;

		[SetUp]
		public void SetUp() {
			db = DALFactory.GetInstance().CreateDatabase();
		}

		[Test]
		public void TestCreateCommand() {
			DbCommand cmd = db.CreateCommand("SELECT bla bla");
			Assert.IsNotNull(cmd);
		}

		[Test]
		public void TestDeclareParameter() {
			DbCommand cmd = db.CreateCommand("SELECT 1 FROM DUAL");

			Assert.AreEqual(0, db.DeclareParameter(cmd, "@Param1", DbType.String));
			Assert.AreEqual(1, db.DeclareParameter(cmd, "@Param2", DbType.Int16));
		}

		[Test]
		public void TestDefineParameter() {
			DbCommand cmd = db.CreateCommand("SELECT 1 FROM DUAL");

			Assert.AreEqual(0, db.DefineParameter(cmd, "@Param1", DbType.String, "hello"));
			Assert.AreEqual(1, db.DefineParameter(cmd, "@Param2", DbType.Int16, 42));
		}

		[Test]
		public void TestExecuteNonQuery() {
			DbCommand cmd = db.CreateCommand("SELECT 1+1 FROM DUAL");

			Assert.AreEqual(-1, db.ExecuteNonQuery(cmd));
		}

		[Test]
		public void TestExecuteReader() {
			DbCommand cmd = db.CreateCommand("SELECT 3 FROM DUAL");

			IDataReader reader = db.ExecuteReader(cmd);
			Assert.IsNotNull(reader);
		}

		[Test]
		public void TestSetParameter() {
			DbCommand cmd = db.CreateCommand("Something");

			db.DeclareParameter(cmd, "param", DbType.Int16);
			db.SetParameter(cmd, "param", 3);
		}

		[Test]
		public void TestSetParameterNotDeclared() {
			DbCommand cmd = db.CreateCommand("Something");

			Assert.Throws<ArgumentException>(() => db.SetParameter(cmd, "param", 3));
		}
	}
}