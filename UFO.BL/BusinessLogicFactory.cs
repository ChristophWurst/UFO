using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;

namespace UFO.BL {

	public abstract class BusinessLogicFactory {
		private static IBusinessLogic bl;

		public static IBusinessLogic GetBusinessLogic() {
			if (bl == null) {
				DALFactory dalFactory = DALFactory.GetInstance();
				bl = new BusinessLogic(dalFactory);
			}

			return bl;
		}
	}
}