using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Area'
	/// </summary>
	public interface IAreaDAO {

		/// <summary>
		/// Get area by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Area GetById(int id);

		/// <summary>
		/// Get all areas
		/// </summary>
		/// <returns></returns>
		IEnumerable<Area> GetAll();
	}
}