using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	public interface IVenueDAO {

		Venue GetById(int id);

		IEnumerable<Venue> GetAll();

		Venue Create(Venue venue);

		Venue Update(Venue venue);
	}
}