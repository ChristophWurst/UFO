using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	/// <summary>
	/// Data-access-object for the entity 'User'
	/// </summary>
	public interface IUserDAO {

		/// <summary>
		/// Get user by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		User GetById(int id);

		/// <summary>
		/// Get all users
		/// </summary>
		/// <returns></returns>
		IEnumerable<User> GetAll();

		/// <summary>
		/// Get user by name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		User GetByName(string name);
	}
}