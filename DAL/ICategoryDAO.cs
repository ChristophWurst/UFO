using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL {
    interface ICategoryDAO {
        Category GetById(int id);
        IEnumerable<Category> GetAll();
    }
}
