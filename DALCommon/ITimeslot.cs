using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	public interface ITimeslot {

		Timeslot GetById(int id);

		IEnumerable<Timeslot> GetAll();
	}
}