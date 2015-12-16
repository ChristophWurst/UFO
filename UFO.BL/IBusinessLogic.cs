using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	public interface IBusinessLogic {

		IEnumerable<Area> GetAreas();

		IEnumerable<Venue> GetVenuesForArea(Area area);

		Venue CreateVenue(Venue venue);

		Venue UpdateVenue(Venue venue);
	}
}