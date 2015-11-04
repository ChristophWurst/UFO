using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

    public interface ICategoryDAO {

        Category GetById(int id);

        IEnumerable<Category> GetAll();
    }
}