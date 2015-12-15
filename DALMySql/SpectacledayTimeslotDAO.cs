using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class SpectacledayTimeslotDAO : ISpectacledayTimeslotDAO {
		private IDatabase db;

		public SpectacledayTimeslotDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<SpectacledayTimeslot> GetAll() {
			throw new NotImplementedException();
		}

		public SpectacledayTimeslot GetById(int id) {
			throw new NotImplementedException();
		}
	}
}