using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

    public interface ICountryDAO {

        Country GetById(int id);

        IEnumerable<Country> GetAll();
    }
}