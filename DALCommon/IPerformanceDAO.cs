using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	public interface IPerformanceDAO {

		Performance GetById(int id);

		IEnumerable<Performance> GetAll();

		Performance Create(Performance Performance);

		Performance Update(Performance Performance);

		void Delete(Performance Performance);
	}
}