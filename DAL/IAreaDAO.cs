using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL {
    interface IAreaDAO {
        Area GetById(int id);
        IEnumerable<Area> GetAll();
        Area Create(Area Area);
        Area Update(Area Area);
    }
}
