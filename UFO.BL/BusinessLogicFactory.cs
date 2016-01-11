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
		private const string DEFAULT = "default";
		private const string WEBSERVICE = "webservice";
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

		public static IBusinessLogic GetBusinessLogic() {
			string BusinessLogicType = "";
			if (defaultBl == null) {
				BusinessLogicType = GetBusinessLogicType();
				if (BusinessLogicType == DEFAULT) {
					return CreateDefaultBL();
				}
			}
			if (wsBl == null) {
				if (BusinessLogicType == "") {
					BusinessLogicType = GetBusinessLogicType();
				}
				if (BusinessLogicType == WEBSERVICE) {
					return CreateWebServiceBL();
				}
			}
			if (BusinessLogicType == "") {
				BusinessLogicType = GetBusinessLogicType();
			}
			if (BusinessLogicType == WEBSERVICE) {
				return wsBl;
			}
			return defaultBl;
		}

		private static IBusinessLogic CreateWebServiceBL() {
			wsBl = new WebServiceBusinessLogic();
			return wsBl;
		}

		private static IBusinessLogic CreateDefaultBL() {
			DALFactory dalFactory = DALFactory.GetInstance();
			IMailService mailService = GetMailService();
			IPdfMaker pdfMaker = GetPdfMaker();
			defaultBl = new BusinessLogic(dalFactory, mailService, pdfMaker);
			return defaultBl;
		}

		private static string GetBusinessLogicType() {
			string BusinessLogicType;
			try {
				BusinessLogicType = ConfigurationManager.AppSettings["BusinessLogicType"];
			} catch {
				BusinessLogicType = DEFAULT;
			}
			return BusinessLogicType == null ? DEFAULT : BusinessLogicType;
		}

		public static IBusinessLogicAsync GetBusinessLogicAsync() {
			return (IBusinessLogicAsync)GetBusinessLogic();
		}
	}
}