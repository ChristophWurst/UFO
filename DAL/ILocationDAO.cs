using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL {

    internal interface ILocationDAO {

        Location GetById(int id, int areaId);

        IEnumerable<Location> GetAll();

        Location Create(Location Location);

        Location Update(Location Location);
    }
}