using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;

namespace DALCommonTest {

	[TestFixture]
	internal class DALFactoryTest {
		private DALFactory factory;
		private IDatabase db;

		[SetUp]
		public void SetUp() {
			factory = DALFactory.GetInstance();
			db = factory.CreateDatabase();
		}

		[Test]
		public void TestIsSingleton() {
			DALFactory factory2 = DALFactory.GetInstance();
			Assert.AreSame(factory2, factory);
		}

		[Test]
		public void TestCreateDatabase() {
			IDatabase db = factory.CreateDatabase();
			Assert.IsNotNull(db);
		}

		[Test]
		public void TestCreateAreaDAO() {
			IAreaDAO dao = factory.CreateAreaDAO(db);
			Assert.IsNotNull(dao);
		}

		[Test]
		public void TestCreateArtist() {
			IArtistDAO dao = factory.CreateArtistDAO(db);
			Assert.IsNotNull(dao);
		}

		[Test]
		public void TestCategoryDAO() {
			ICategoryDAO dao = factory.CreateCategoryDAO(db);
			Assert.IsNotNull(dao);
		}

		[Test]
		public void TestCountryDAO() {
			ICountryDAO dao = factory.CreateCountryDAO(db);
			Assert.IsNotNull(dao);
		}

		[Test]
		public void TestVenueDAO() {
			IVenueDAO dao = factory.CreateVenueDAO(db);
			Assert.IsNotNull(dao);
		}

		[Test]
		public void TestPerformanceDAO() {
			IPerformanceDAO dao = factory.CreatePerformanceDAO(db);
			Assert.IsNotNull(dao);
		}

		[Test]
		public void TestUserDAO() {
			IUserDAO dao = factory.CreateUserDAO(db);
			Assert.IsNotNull(dao);
		}
	}
}