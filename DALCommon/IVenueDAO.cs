using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Venue'
	/// </summary>
	public interface IVenueDAO {

		/// <summary>
		/// Get venue by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Venue GetById(int id);

		/// <summary>
		/// Get all venues
		/// </summary>
		/// <returns></returns>
		IEnumerable<Venue> GetAll();

		/// <summary>
		/// Create a new venue row
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		Venue Create(Venue venue);

		/// <summary>
		/// Update an existing venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		Venue Update(Venue venue);

		/// <summary>
		/// Get all venues of the given area
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		IEnumerable<Venue> GetForArea(Area area);

		/// <summary>
		/// Get venues for the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<Venue> GetForPerformances(IEnumerable<Performance> performances);
	}
}