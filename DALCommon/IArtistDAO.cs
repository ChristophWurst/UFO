using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	public interface IArtistDAO {

		Artist GetById(int id);

		IEnumerable<Artist> GetAll();

		Artist Create(Artist artist);

		Artist Update(Artist artist);

		void Delete(Artist artist);

		IEnumerable<Artist> GetForCategory(Category category);

		IEnumerable<Artist> GetForPerformances(IEnumerable<Performance> performances);
	}
}