using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.BL {

	internal class BusinessLogic : IBusinessLogic {
		private DALFactory dalFactory;
		private IDatabase db;

		public BusinessLogic(DALFactory dalFactory) {
			this.db = dalFactory.CreateDatabase();
			this.dalFactory = dalFactory;
		}

		public IEnumerable<Area> GetAreas() {
			return dalFactory.CreateAreaDAO(db).GetAll();
		}
	}
}