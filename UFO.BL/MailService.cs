using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using UFO.DomainClasses;

namespace UFO.BL {

	public class MailService : IMailService {
		private string smtpServer;
		private int port;
		private string user;
		private string pwd;
		private MailAddress mailAddress;

		public MailService(string smtpServer, int port, string user, string pwd, MailAddress mailAddress) {
			this.smtpServer = smtpServer;
			this.port = port;
			this.user = user;
			this.pwd = pwd;
			this.mailAddress = mailAddress;
		}

		public void MailToArtists(IEnumerable<Artist> artists, string body) {
			using (SmtpClient smtpClientt = new SmtpClient(smtpServer, port))
			using (MailMessage mailMessage = new MailMessage()) {
				artists.ToList().ForEach(artist => mailMessage.To.Add(artist.Email));
				mailMessage.Body = body;
				mailMessage.From = mailAddress;
				smtpClientt.UseDefaultCredentials = false;
				smtpClientt.Credentials = new NetworkCredential(user, pwd);
				smtpClientt.Send(mailMessage);
			}
		}
	}
}