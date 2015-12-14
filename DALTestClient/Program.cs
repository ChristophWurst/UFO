using System;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALTestClient {

	internal class Program {

		private static void TestArea(IDatabase db, DALFactory dalFactory) {
			Console.WriteLine("*************************************");
			Console.WriteLine("AREA TEST");

			IAreaDAO areaDAO = dalFactory.CreateAreaDAO(db);

			Console.WriteLine("\nAll areas:");
			foreach (var area in areaDAO.GetAll()) {
				Console.WriteLine(area);
			}

			Console.WriteLine("\nArea with ID=1:");
			Area area1 = areaDAO.GetById(1);
			Console.WriteLine(area1);

			Area area2 = new Area() {
				Name = "Rathausplatz"
			};
		}

		private static void TestArtist(IDatabase db, DALFactory dalFactory) {
			Console.WriteLine("*************************************");
			Console.WriteLine("ARTIST TEST");

			IArtistDAO artistDAO = dalFactory.CreateArtistDAO(db);

			Console.WriteLine("\nAll artists:");
			foreach (var artist in artistDAO.GetAll()) {
				Console.WriteLine(artist);
			}

			Console.WriteLine("\nArtist with ID=1:");
			Console.WriteLine(artistDAO.GetById(1));

			Artist artist1 = new Artist() {
				Name = "Stefan the Rösch",
				CategoryId = 1,
				CountryId = 25
			};
			Artist newArtist = artistDAO.Create(artist1);
			Console.WriteLine("New Artist: " + newArtist);
			Console.WriteLine("Inserted Artist: " + artist1);

			artist1.Name = "Christoph the Wurst";
			artistDAO.Update(artist1);
			Console.WriteLine("Updated artist: " + artist1);

			artistDAO.Delete(artist1);
			Console.WriteLine("\nArtist deleted");
		}

		private static void TestPerformance(IDatabase db, DALFactory dalFactory) {
			Console.WriteLine("*************************************");
			Console.WriteLine("PERFORMANCE TEST");

			IPerformanceDAO performanceDAO = dalFactory.CreatePerformanceDAO(db);

			Console.WriteLine("\nAll performances:");
			foreach (var performance in performanceDAO.GetAll()) {
				Console.WriteLine(performance);
			}

			Console.WriteLine("\nPerformance with ID=1:");
			Console.WriteLine(performanceDAO.GetById(1));

			Performance performance1 = new Performance() {
				SpectacledayTimeslot = 1,
				ArtistId = 1
			};
			Performance newPerformance = performanceDAO.Create(performance1);
			Console.WriteLine("New Artist: " + newPerformance);
			Console.WriteLine("Inserted Artist: " + newPerformance);

			newPerformance.ArtistId = 3;
			performanceDAO.Update(newPerformance);
			Console.WriteLine("Updated performance: " + newPerformance);

			performanceDAO.Delete(performance1);
			Console.WriteLine("\nPerformance deleted");
		}

		private static void TestCategory(IDatabase db, DALFactory dalFactory) {
			Console.WriteLine("*************************************");
			Console.WriteLine("CATEGORY TEST");

			ICategoryDAO categoryDAO = dalFactory.CreateCategoryDAO(db);

			Console.WriteLine("\nAll categories:");
			foreach (var category in categoryDAO.GetAll()) {
				Console.WriteLine(category);
			}

			Console.WriteLine("\nCategory with ID=1:");
			Console.WriteLine(categoryDAO.GetById(1));
		}

		private static void TestCountry(IDatabase db, DALFactory dalFactory) {
			Console.WriteLine("*************************************");
			Console.WriteLine("COUNTY TEST");

			ICountryDAO countryDAO = dalFactory.CreateCountryDAO(db);

			Console.WriteLine("\nAll countries:");
			foreach (var country in countryDAO.GetAll()) {
				Console.WriteLine(country);
			}

			Console.WriteLine("\nCountry with ID=1:");
			Console.WriteLine(countryDAO.GetById(1));
		}

		private static void TestUser(IDatabase db, DALFactory dalFactory) {
			Console.WriteLine("*************************************");
			Console.WriteLine("ARTIST TEST");

			IUserDAO userDAO = dalFactory.CreateUserDAO(db);

			Console.WriteLine("\nAll users:");
			foreach (var user in userDAO.GetAll()) {
				Console.WriteLine(user);
			}

			Console.WriteLine("\nUser with ID=1:");
			Console.WriteLine(userDAO.GetById(1));
		}

		private static void TestVenue(IDatabase db, DALFactory dalFactory) {
			Console.WriteLine("*************************************");
			Console.WriteLine("VENUE TEST");

			IVenueDAO venueDAO = dalFactory.CreateVenueDAO(db);

			Console.WriteLine("\nAll VENUES:");
			foreach (var venue in venueDAO.GetAll()) {
				Console.WriteLine(venue);
			}

			Console.WriteLine("\nVENUE with ID=1:");
			Console.WriteLine(venueDAO.GetById(1));

			Venue venue1 = new Venue() {
				AreaId = 1,
				Description = "TestVenue",
				ShortDescription = "TV",
				Latitude = 0.0f,
				Longitude = 0.0f
			};
			Venue newVenue = venueDAO.Create(venue1);
			Console.WriteLine("New VENUE: " + newVenue);
			Console.WriteLine("Inserted VENUE: " + newVenue);

			venue1.Description = "Winzerhof Wurst";
			venueDAO.Update(venue1);
			Console.WriteLine("Updated VENUE: " + venue1);
		}

		private static void Main(string[] args) {
			Console.WriteLine("Start DALTestClient");
			Console.WriteLine("Create Database ...");
			DALFactory dalFactory = DALFactory.GetInstance();
			IDatabase db = dalFactory.CreateDatabase();

			TestArea(db, dalFactory);
			Console.WriteLine();
			TestArtist(db, dalFactory);
			Console.WriteLine();
			TestCategory(db, dalFactory);
			Console.WriteLine();
			TestCountry(db, dalFactory);
			Console.WriteLine();
			TestUser(db, dalFactory);
			Console.WriteLine();
			TestVenue(db, dalFactory);
		}
	}
}