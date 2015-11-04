using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

    public interface IPerformanceDAO {

        Performance GetById(int id, int locationId, int locationAreaId);

        IEnumerable<Performance> GetAll();

        Performance Create(Performance Performance);

        Performance Update(Performance Performance);

        Performance Delete(Performance Performance);
    }
}