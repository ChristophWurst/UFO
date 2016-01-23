using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'Category'
	/// </summary>
	public interface ICategoryDAO {

		/// <summary>
		/// Get category by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Category GetById(int id);

		/// <summary>
		/// Get all categories
		/// </summary>
		/// <returns></returns>
		IEnumerable<Category> GetAll();
	}
}