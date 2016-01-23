using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Country'
	/// </summary>
	public interface ICountryDAO {

		/// <summary>
		/// Get country by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Country GetById(int id);

		/// <summary>
		/// Get all countries
		/// </summary>
		/// <returns></returns>
		IEnumerable<Country> GetAll();
	}
}