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

	/// <summary>
	/// Factory for creating business logic instances
	/// This factory is implemented as singleton
	///
	/// Use 'GetBusinessLogic' for a synchronous interface
	/// Use 'GetBusinessLogicAsync' for an asynchronous interface
	/// </summary>
	public class BusinessLogicFactory {
		private const string DEFAULT = "default";
		private const string WEBSERVICE = "webservice";
		private static BusinessLogic defaultBl;
		private static WebServiceBusinessLogic wsBl;

		private BusinessLogicFactory() {
			// Singleton
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
			defaultBl = new BusinessLogic(dalFactory);
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