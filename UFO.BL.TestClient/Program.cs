using System.Collections.Generic;
using System.Net.Mail;
using UFO.DomainClasses;

namespace UFO.BL.TestClient {

	internal class Program {

		private static void Main(string[] args) {
			var mailClient = new MailService("127.0.0.1", 5000, new MailAddress("muesli@muesli", "muesli"), "ufo", "ofu");
			var artists = new List<Artist>();
			artists.Add(new Artist() { Email = "bla@bla" });
			artists.Add(new Artist() { Email = "af@adf" });
			artists.Add(new Artist() { Email = "345@bl3543a" });
			mailClient.MailToArtists(artists, "HALLLOOO");
		}
	}
}