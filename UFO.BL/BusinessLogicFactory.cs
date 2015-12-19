using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;

namespace UFO.BL {

	public abstract class BusinessLogicFactory {
		private static IBusinessLogic bl;

		private static IMailService GetMailService() {
			var appSettings = ConfigurationManager.AppSettings;
			var smtpServer = appSettings["smtpServer"];
			var mailAddress = new MailAddress(appSettings["mailAddress"], appSettings["sender"]);
			var user = appSettings["user"];
			var pwd = appSettings["pwd"];
			var port = int.Parse(appSettings["port"]);
			return new MailService(smtpServer, port, user, pwd, mailAddress);
		}

		private static IPdfMaker GetPdfMaker() {
			var appSettings = ConfigurationManager.AppSettings;
			return new PdfMaker(appSettings["pdfName"], appSettings["pdfPath"]);
		}

		public static IBusinessLogic GetBusinessLogic() {
			if (bl == null) {
				DALFactory dalFactory = DALFactory.GetInstance();
				IMailService mailService = GetMailService();
				IPdfMaker pdfMaker = GetPdfMaker();
				bl = new BusinessLogic(dalFactory, mailService, pdfMaker);
			}
			return bl;
		}
	}
}