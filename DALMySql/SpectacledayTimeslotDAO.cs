using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class SpectacledayTimeSlotDAO : ISpectacledayTimeSlotDAO {
		private IDatabase db;

		public SpectacledayTimeSlotDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<SpectacledayTimeSlot> GetAll() {
			throw new NotImplementedException();
		}

		public SpectacledayTimeSlot GetById(int id) {
			throw new NotImplementedException();
		}
	}
}