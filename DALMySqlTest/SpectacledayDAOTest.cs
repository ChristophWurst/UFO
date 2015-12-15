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

	internal static class SpectacledayExtension {

		public static bool IsEqualTo(this Spectacleday s1, Spectacleday s2) {
			return s1.Id == s2.Id &&
				s1.day == s2.day;
		}
	}

	[TestFixture]
	internal class SpectacledayDAOTest {
		private TransactionScope transaction;
		private IDatabase db;
		private ISpectacledayDAO dao;
		private Spectacleday spectacleday;

		[SetUp]
		public void SetUp() {
			transaction = new TransactionScope();
			DALFactory factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
		}
	}
}