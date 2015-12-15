using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class TimeslotDAO : ITimeslotDAO {
		private IDatabase db;

		public TimeslotDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<Timeslot> GetAll() {
			throw new NotImplementedException();
		}

		public Timeslot GetById(int id) {
			throw new NotImplementedException();
		}
	}
}