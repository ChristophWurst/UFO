using System.Collections.Generic;
using UFO.DomainClasses;

namespace UFO.DAL.Common {

    public interface IArtistDAO {

        Artist GetById(int id);

        IEnumerable<Artist> GetAll();

        Artist Create(Artist artist);

        Artist Update(Artist artist);

        Artist Delete(Artist artist);
    }
}