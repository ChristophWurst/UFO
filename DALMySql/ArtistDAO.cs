using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

    public class ArtistDAO : IArtistDAO {

        private const string SQL_SELECT_ALL = "SELECT * "
                                        + "FROM `artist`";

        private const string SQL_SELECT = "SELECT * "
                                        + "FROM `artist` "
                                        + "WHERE `id` = @Id";

        private const string SQL_INSERT = "INSERT INTO `artist` "
                                        + "(`name`, `image`, `video`, `category_id`, `country_id`, `email`) VALUES "
                                        + "(@Name, @Image, @Video, @CategoryId, @CountryId, @Email)";

        private const string SQL_UPDATE = "UPDATE `artist` "
                                        + "SET name = @Name, image = @Image, video = @Video, "
                                        + "category_id = @CategoryId, country_id = @CountryId , email = @Email "
                                        + "WHERE ID = @Id";

        private const string SQL_DELETE = "DELETE FROM `artist` "
                                        + "WHERE `id` = @Id";

        private IDatabase db;

        public ArtistDAO(IDatabase db) {
            this.db = db;
        }

        private DbCommand createSelectAllCommand() {
            return db.CreateCommand(SQL_SELECT_ALL);
        }

        private DbCommand createSelectByIdCommand(int id) {
            DbCommand cmd = db.CreateCommand(SQL_SELECT);
            db.DefineParameter(cmd, "@Id", DbType.Int32, id);
            return cmd;
        }

        private DbCommand createInsertCommand(string name, string image, string video, int catId, int cntId, string email) {
            DbCommand cmd = db.CreateCommand(SQL_INSERT);
            db.DefineParameter(cmd, "@Name", DbType.String, name);
            db.DefineParameter(cmd, "@Image", DbType.String, image);
            db.DefineParameter(cmd, "@Video", DbType.String, video);
            db.DefineParameter(cmd, "@CategoryId", DbType.Int32, catId);
            db.DefineParameter(cmd, "@CountryId", DbType.Int32, cntId);
            db.DefineParameter(cmd, "@Email", DbType.String, email);
            return cmd;
        }

        private DbCommand createUpdateCommand(int id, string name, string image, string video, int catId, int cntId, string email) {
            DbCommand cmd = db.CreateCommand(SQL_UPDATE);
            db.DefineParameter(cmd, "@Id", DbType.Int32, id);
            db.DefineParameter(cmd, "@Name", DbType.String, name);
            db.DefineParameter(cmd, "@Image", DbType.String, image);
            db.DefineParameter(cmd, "@Video", DbType.String, video);
            db.DefineParameter(cmd, "@CategoryId", DbType.Int32, catId);
            db.DefineParameter(cmd, "@CountryId", DbType.Int32, cntId);
            db.DefineParameter(cmd, "@Email", DbType.String, email);
            return cmd;
        }

        private DbCommand createDeleteCommand(int id) {
            DbCommand cmd = db.CreateCommand(SQL_DELETE);
            db.DefineParameter(cmd, "@Id", DbType.Int32, id);
            return cmd;
        }

        private Artist createArtistFromReader(IDataReader reader) {
            return new Artist() {
                Id = (int)reader["id"],
                Name = (string)reader["name"] as string,
                Image = reader["image"] as string,
                Video = reader["video"] as string,
                CategoryId = (int)reader["category_id"],
                CountryId = (int)reader["country_id"],
                Email = reader["email"] as string
            };
        }

        public IEnumerable<Artist> GetAll() {
            var result = new List<Artist>();
            DbCommand cmd = createSelectAllCommand();
            using (IDataReader reader = db.ExecuteReader(cmd)) {
                while (reader.Read()) {
                    result.Add(createArtistFromReader(reader));
                }
            }
            return result;
        }

        public Artist GetById(int id) {
            Artist artist = null;
            DbCommand cmd = createSelectByIdCommand(id);
            using (IDataReader reader = db.ExecuteReader(cmd)) {
                if (reader.Read()) {
                    artist = createArtistFromReader(reader);
                }
            }
            return artist;
        }

        public Artist Create(Artist artist) {
            DbCommand cmd = createInsertCommand(artist.Name, artist.Image, artist.Video, artist.CategoryId, artist.CountryId, artist.Email);
            artist.Id = (int)(((MySqlCommand)cmd).LastInsertedId);
            db.ExecuteNonQuery(cmd);
            return artist;
        }

        public Artist Update(Artist artist) {
            DbCommand cmd = createUpdateCommand(artist.Id, artist.Name, artist.Image, artist.Video, artist.CategoryId, artist.CountryId, artist.Email);
            if (db.ExecuteNonQuery(cmd) != 1) {
                throw new EntityNotFoundException();
            }
            return artist;
        }

        public Artist Delete(Artist artist) {
            DbCommand cmd = createDeleteCommand(artist.Id);
            db.ExecuteNonQuery(cmd);
            return artist;
        }
    }
}