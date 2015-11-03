using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL {
    interface IUserDAO {
        User GetById(int id);
        IEnumerable<User> GetAll();
    }
}
