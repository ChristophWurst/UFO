using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	public interface IPerformanceDAO {

		Performance GetById(int id);

		IEnumerable<Performance> GetAll();

		IEnumerable<Performance> GetForArtist(Artist artist);

		IEnumerable<Performance> GetForSpectacleDay(Spectacleday spectacleDay);

		Performance Create(Performance Performance);

		Performance Update(Performance Performance);

		void Delete(Performance Performance);
	}
}