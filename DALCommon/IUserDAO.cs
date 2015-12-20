using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

	public interface IUserDAO {

		User GetById(int id);

		IEnumerable<User> GetAll();

		User GetByName(string name);
	}
}