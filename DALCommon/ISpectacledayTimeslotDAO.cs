using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	public interface ISpectacledayTimeslotDAO {

		SpectacledayTimeslot GetById(int id);

		IEnumerable<SpectacledayTimeslot> GetAll();
	}
}