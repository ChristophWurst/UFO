using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Artist'
	/// </summary>
	public interface IArtistDAO {

		/// <summary>
		/// Get artist by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Artist GetById(int id);

		/// <summary>
		/// Get all artists
		/// </summary>
		/// <returns></returns>
		IEnumerable<Artist> GetAll();

		/// <summary>
		/// Create a new artist row
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Artist Create(Artist artist);

		/// <summary>
		/// Update an existing artist
		/// </summary>
		/// <param name="artist"></param>
		/// <returns></returns>
		Artist Update(Artist artist);

		/// <summary>
		/// Delete an artist
		/// </summary>
		/// <param name="artist"></param>
		void Delete(Artist artist);

		/// <summary>
		/// Get all artist of the given category
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		IEnumerable<Artist> GetForCategory(Category category);

		/// <summary>
		/// Get all artists of the given performances
		/// </summary>
		/// <param name="performances"></param>
		/// <returns></returns>
		IEnumerable<Artist> GetForPerformances(IEnumerable<Performance> performances);
	}
}