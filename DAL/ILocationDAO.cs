using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL {
    interface ILocationDAO {
        Location GetById(int id, int areaId);
        IEnumerable<Location> GetAll();
        Location Create(Location Location);
        Location Update(Location Location);
    }
}
