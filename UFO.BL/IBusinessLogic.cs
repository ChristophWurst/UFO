using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.BL {

	public interface IBusinessLogic {

		IEnumerable<Area> GetAreas();

		IEnumerable<Category> GetCategories();

		IEnumerable<Venue> GetVenuesForArea(Area area);

		IEnumerable<Artist> GetArtistsForCategory(Category category);

		Artist UpdateArtist(Artist artist);
	}
}