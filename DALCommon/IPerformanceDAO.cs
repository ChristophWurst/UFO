using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Performance'
	/// </summary>
	public interface IPerformanceDAO {

		/// <summary>
		/// Get performance by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Performance GetById(int id);

		/// <summary>
		/// Get all performances
		/// </summary>
		/// <returns></returns>
		IEnumerable<Performance> GetAll();

		/// <summary>
		/// Get app performances of the given artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		IEnumerable<Performance> GetForArtist(Artist artist);

		/// <summary>
		/// Get performances of the given spectacleday
		/// </summary>
		/// <param name="spectacleDay"></param>
		/// <returns></returns>
		IEnumerable<Performance> GetForSpectacleDay(Spectacleday spectacleDay);

		/// <summary>
		/// Create a new performance row
		/// </summary>
		/// <param name="Performance"></param>
		/// <returns></returns>
		Performance Create(Performance Performance);

		/// <summary>
		/// Update an existing performance
		/// </summary>
		/// <param name="Performance"></param>
		/// <returns></returns>
		Performance Update(Performance Performance);

		/// <summary>
		/// Delete an existing performance
		/// </summary>
		/// <param name="Performance"></param>
		void Delete(Performance Performance);

		/// <summary>
		/// Get all performances for the given venue
		/// </summary>
		/// <param name="venue"></param>
		/// <returns></returns>
		IEnumerable<Performance> GetForVenue(Venue venue);
	}
}