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

	public enum BLType { Default, WebService }

	public abstract class BusinessLogicFactory {
		private static BusinessLogic defaultBl;
		private static WebServiceBusinessLogic wsBl;

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

		public static IBusinessLogic GetBusinessLogic(BLType type = BLType.Default) {
			if (type == BLType.Default) {
				if (defaultBl == null) {
					DALFactory dalFactory = DALFactory.GetInstance();
					IMailService mailService = GetMailService();
					IPdfMaker pdfMaker = GetPdfMaker();
					defaultBl = new BusinessLogic(dalFactory, mailService, pdfMaker);
				}
				return defaultBl;
			} else {
				if (wsBl == null) {
					wsBl = new WebServiceBusinessLogic();
				}
				return wsBl;
			}
		}

		public static IBusinessLogicAsync GetBusinessLogicAsync(BLType type = BLType.Default) {
			return (IBusinessLogicAsync)GetBusinessLogic(type);
		}
	}
}