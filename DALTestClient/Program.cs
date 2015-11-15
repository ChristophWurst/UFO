using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            areaDAO.Create(area2);
            Console.WriteLine("\nAreas after adding an entry:");
            foreach (var area in areaDAO.GetAll()) {
                Console.WriteLine(area);
            }

            string oldName = area1.Name;
            area1.Name = "Festhalle";
            areaDAO.Update(area1);
            Console.WriteLine("\nArea with ID=1 after update:");
            Console.WriteLine(area1);
            area1.Name = oldName;
            areaDAO.Update(area1);
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
        }
    }
}