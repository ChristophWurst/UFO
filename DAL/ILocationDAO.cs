using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

    public interface ILocationDAO {

        Location GetById(int id);

        IEnumerable<Location> GetAll();

        Location Create(Location Location);

        Location Update(Location Location);
    }
}