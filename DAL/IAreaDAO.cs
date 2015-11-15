using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

    public interface IAreaDAO {

        Area GetById(int id);

        IEnumerable<Area> GetAll();

        Area Create(Area area);

        Area Update(Area area);
    }
}