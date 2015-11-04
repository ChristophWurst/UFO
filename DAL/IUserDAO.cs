﻿using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL {

    internal interface IUserDAO {

        User GetById(int id);

        IEnumerable<User> GetAll();
    }
}