using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using UFO.DomainClasses;

namespace UFO.BL.TestClient {

	internal class Program {

		private static void Main(string[] args) {
			IBusinessLogic bl = BusinessLogicFactory.GetBusinessLogic();
			var appSettings = ConfigurationManager.AppSettings;
			var file = TestPdfMaker(appSettings, bl);
			TestMailService(appSettings, bl, file);
		}

		private static byte[] TestPdfMaker(NameValueCollection appSettings, IBusinessLogic bl) {
			List<Spectacleday> days = new List<Spectacleday>(bl.GetSpectacleDays());
			return bl.CreatePdfScheduleForSpectacleDay(days[1]);
		}

		private static void TestMailService(System.Collections.Specialized.NameValueCollection appSettings, IBusinessLogic bl, byte[] file) {
			var smtpServer = appSettings["smtpServer"];
			var mailAddress = new MailAddress(appSettings["mailAddress"], appSettings["sender"]);
			var user = appSettings["user"];
			var pwd = appSettings["pwd"];
			var port = int.Parse(appSettings["port"]);
			var mailClient = new MailService(smtpServer, port, user, pwd, mailAddress);
			var pdfPath = appSettings["pdfPath"];
			var pdfName = appSettings["pdfName"];

			IEnumerable<Artist> artists = bl.GetArtistsForCategory(new Category() { Id = 1 });
			var days = new List<Spectacleday>(bl.GetSpectacleDays());
			mailClient.MailToArtists(artists, days[0], file);
		}
	}
}