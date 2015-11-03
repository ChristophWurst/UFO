using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL {

    internal interface ICategoryDAO {

        Category GetById(int id);

        IEnumerable<Category> GetAll();
    }
}