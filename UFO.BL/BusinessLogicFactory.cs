using System;
using System.Collections.Generic;
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

		public static IBusinessLogic GetBusinessLogic() {
			if (bl == null) {
				DALFactory dalFactory = DALFactory.GetInstance();
				IMailService mailService = GetMailService();
				bl = new BusinessLogic(dalFactory, mailService);
			}

			return bl;
		}
	}
}