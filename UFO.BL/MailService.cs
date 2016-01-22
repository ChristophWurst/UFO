using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using UFO.BL.execptions;
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

		public void MailToArtists(IEnumerable<Artist> artists, Spectacleday day, byte[] file) {
			using (SmtpClient smtpClientt = new SmtpClient(smtpServer, port))
			using (MailMessage mailMessage = new MailMessage()) {
				try {
					artists.ToList().ForEach(artist => mailMessage.To.Add(artist.Email));
					mailMessage.Body = CreateBody(day);
					mailMessage.From = mailAddress;
					Stream stream = new MemoryStream(file);
					Attachment attachment = new Attachment(stream, "Schedule.pdf");
					mailMessage.Attachments.Add(attachment);
					smtpClientt.UseDefaultCredentials = false;
					smtpClientt.Credentials = new NetworkCredential(user, pwd);
					smtpClientt.Send(mailMessage);
				} catch (Exception e) {
					throw new BusinessLogicException("Mailt to Artists failed. " + e.Message);
				}
			}
		}

		private string CreateBody(Spectacleday day) {
			return $"The Schedule for {day.Day} has been changed.";
		}
	}
}