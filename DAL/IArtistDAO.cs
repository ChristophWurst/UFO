using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DomainClasses;

namespace UFO.DAL {
    public interface IArtistDAO {
        Artist GetById(int id);
        IEnumerable<Artist> GetAll();
        Artist Create(Artist artist);
        Artist Update(Artist artist);
        Artist Delete(Artist artist);
    }
}
