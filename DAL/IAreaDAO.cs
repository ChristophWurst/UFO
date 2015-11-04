using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

    public interface IAreaDAO {

        Area GetById(int id);

        IEnumerable<Area> GetAll();

        Area Create(Area Area);

        Area Update(Area Area);
    }
}