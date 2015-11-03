using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL {
    interface IPerformanceDAO {
        Performance GetById(int id, int locationId, int locationAreaId);
        IEnumerable<Performance> GetAll();
        Performance Create(Performance Performance);
        Performance Update(Performance Performance);
        Performance Delete(Performance Performance);
    }
}
