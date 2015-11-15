using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace DALTestClient {

    internal class Program {

        private static void testArtist(IDatabase db, DALFactory dalFactory) {
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

        private static void Main(string[] args) {
            Console.WriteLine("Start DALTestClient");
            Console.WriteLine("Create Database ...");
            DALFactory dalFactory = DALFactory.GetInstance();
            IDatabase db = dalFactory.CreateDatabase();

            testArtist(db, dalFactory);
        }
    }
}