using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class ArtistDAO : IArtistDAO {
		private const string SQL_SELECT_ALL = @"SELECT * FROM `artist` WHERE `deleted` = @Deleted";

		private const string SQL_SELECT = @"SELECT * FROM `artist` WHERE `id` = @Id AND `deleted` = @Deleted";

		private const string SQL_INSERT = "INSERT INTO `artist` "
										+ "(`name`, `image`, `video`, `category_id`, `country_id`, `email`) VALUES "
										+ "(@Name, @Image, @Video, @CategoryId, @CountryId, @Email)";

		private const string SQL_UPDATE = "UPDATE `artist` "
										+ "SET name = @Name, image = @Image, video = @Video, "
										+ "category_id = @CategoryId, country_id = @CountryId , email = @Email "
										+ "WHERE ID = @Id AND `deleted` = @Deleted";

		private const string SQL_DELETE = "UPDATE `artist` SET `deleted` = @Deleted WHERE `id` = @Id";

		private const string SQL_SELECT_FOR_CATEGORY = "SELECT * "
												 + "FROM `artist`"
												 + "WHERE `category_id` = @CategoryId AND `deleted` = @Deleted";

		private const string SQL_SELECT_FOR_PERFORMANCES = @"SELECT * FROM `artist` a WHERE a.id IN ({0})";

		private IDatabase db;

		public ArtistDAO(IDatabase db) {
			this.db = db;
		}

		private DbCommand createSelectAllCommand() {
			DbCommand cmd = db.CreateCommand(SQL_SELECT_ALL);
			db.DefineParameter(cmd, "@Deleted", DbType.Boolean, false);
			return cmd;
		}

		private DbCommand createSelectByIdCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			db.DefineParameter(cmd, "@Deleted", DbType.Boolean, false);
			return cmd;
		}

		private DbCommand createSelectForCategoryCommand(Category category) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT_FOR_CATEGORY);
			db.DefineParameter(cmd, "@CategoryId", DbType.Int32, category.Id);
			db.DefineParameter(cmd, "@Deleted", DbType.Boolean, false);
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
			db.DefineParameter(cmd, "@Deleted", DbType.Boolean, false);
			return cmd;
		}

		private DbCommand createDeleteCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_DELETE);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			db.DefineParameter(cmd, "@Deleted", DbType.Boolean, true);
			return cmd;
		}

		private DbCommand createSelectForPerformances(IEnumerable<Performance> performances) {
			var parameters = performances.Select(p => p.ArtistId.ToString()).ToArray();
			var commandString = string.Format(SQL_SELECT_FOR_PERFORMANCES, string.Join(", ", parameters));
			DbCommand cmd = db.CreateCommand(commandString);
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
			if (artist == null) {
				throw new EntityNotFoundException();
			}
			return artist;
		}

		public Artist Create(Artist artist) {
			DbCommand cmd = createInsertCommand(artist.Name, artist.Image, artist.Video, artist.CategoryId, artist.CountryId, artist.Email);
			db.ExecuteNonQuery(cmd);
			artist.Id = (int)(((MySqlCommand)cmd).LastInsertedId);
			return artist;
		}

		public Artist Update(Artist artist) {
			DbCommand cmd = createUpdateCommand(artist.Id, artist.Name, artist.Image, artist.Video, artist.CategoryId, artist.CountryId, artist.Email);
			if (db.ExecuteNonQuery(cmd) != 1) {
				throw new EntityNotFoundException();
			}
			return artist;
		}

		public void Delete(Artist artist) {
			DbCommand cmd = createDeleteCommand(artist.Id);
			if (db.ExecuteNonQuery(cmd) != 1) {
				throw new EntityNotFoundException();
			}
		}

		public IEnumerable<Artist> GetForCategory(Category category) {
			var result = new List<Artist>();
			DbCommand cmd = createSelectForCategoryCommand(category);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					result.Add(createArtistFromReader(reader));
				}
			}
			return result;
		}

		public IEnumerable<Artist> GetForPerformances(IEnumerable<Performance> performances) {
			var artist = new List<Artist>();
			DbCommand cmd = createSelectForPerformances(performances);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					artist.Add(createArtistFromReader(reader));
				}
			}
			return artist;
		}
	}
}