using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using UFO.DomainClasses;

namespace UFO.BL {

	public class MailService : IMailService {
		public string smtpServer { get; set; }
		public int port { get; set; }
		public string user { private get; set; }
		public string pwd { private get; set; }
		public MailAddress sender { get; set; }

		public void MailToArtists(IEnumerable<Artist> artists, string body) {
			using (SmtpClient smtpClientt = new SmtpClient(smtpServer, port))
			using (MailMessage mailMessage = new MailMessage()) {
				artists.ToList().ForEach(artist => mailMessage.To.Add(artist.Email));
				mailMessage.Body = body;
				mailMessage.From = new MailAddress("me@me.at");
				smtpClientt.UseDefaultCredentials = false;
				smtpClientt.Credentials = new NetworkCredential(user, pwd);
				smtpClientt.Send(mailMessage);
			}
		}
	}
}