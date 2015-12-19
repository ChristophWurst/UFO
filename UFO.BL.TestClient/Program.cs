﻿using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using UFO.DomainClasses;

namespace UFO.BL.TestClient {

	internal class Program {

		private static void Main(string[] args) {
			var appSettings = ConfigurationManager.AppSettings;
			TestMailService(appSettings);
			TestPdfMaker(appSettings);
		}

		private static void TestPdfMaker(System.Collections.Specialized.NameValueCollection appSettings) {
			var pdf = new PdfMaker(appSettings["pdfName"], appSettings["pdfPath"]);
			pdf.MakeSpectacleSchedule(new List<SpectacledayTimeSlot>(), new List<Performance>());
		}

		private static void TestMailService(System.Collections.Specialized.NameValueCollection appSettings) {
			var smtpServer = appSettings["smtpServer"];
			var mailAddress = new MailAddress(appSettings["mailAddress"], appSettings["sender"]);
			var user = appSettings["user"];
			var pwd = appSettings["pwd"];
			var port = int.Parse(appSettings["port"]);
			var mailClient = new MailService(smtpServer, port, user, pwd, mailAddress);
			var artists = new List<Artist>();
			artists.Add(new Artist() { Email = "bla@bla" });
			artists.Add(new Artist() { Email = "af@adf" });
			artists.Add(new Artist() { Email = "345@bl3543a" });
			mailClient.MailToArtists(artists, "HALLLOOO");
		}
	}
}