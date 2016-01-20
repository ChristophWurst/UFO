using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class VenueDAO : IVenueDAO {

		private const string SQL_SELECT_ALL = "SELECT * "
											+ "FROM `venue`";

		private const string SQL_SELECT = "SELECT * "
										+ "FROM `venue` "
										+ "WHERE `id` = @Id";

		private const string SQL_SELECT_FOR_AREA = "SELECT * "
												 + "FROM `venue`"
												 + "WHERE `area_id` = @AreaId";

		private const string SQL_INSERT = "INSERT INTO `venue` "
										+ "(`area_id`, `desc`, `short_desc`, `latitude`, `longitude`) VALUES "
										+ "(@AreaId, @Desc, @ShortDesc, @Latitude, @Longitude)";

		private const string SQL_UPDATE = "UPDATE `venue` SET "
										+ "`area_id` = @AreaId, "
										+ "`desc` = @Desc, "
										+ "`short_desc` = @ShortDesc, "
										+ "`latitude` = @Latitude, "
										+ "`longitude` = @Longitude "
										+ "WHERE `id` = @id";

		private const string SQL_SELECT_FOR_PERFORMANCES = @"SELECT * FROM `venue` v WHERE v.id IN ({0})";

		private IDatabase db;

		private DbCommand CreateSelectAllCommand() {
			return this.db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand CreateSelectByIdCommand(int id) {
			DbCommand cmd = this.db.CreateCommand(SQL_SELECT);
			this.db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			return cmd;
		}

		private DbCommand CreateSelectForAreaCommand(Area area) {
			DbCommand cmd = this.db.CreateCommand(SQL_SELECT_FOR_AREA);
			this.db.DefineParameter(cmd, "@AreaId", DbType.Int32, area.Id);
			return cmd;
		}

		private DbCommand CreateInsertCommand(int areaId, string desc, string shortDesc, double lat, double lon) {
			DbCommand cmd = this.db.CreateCommand(SQL_INSERT);
			this.db.DefineParameter(cmd, "@AreaId", DbType.Int32, areaId);
			this.db.DefineParameter(cmd, "@Desc", DbType.String, desc);
			this.db.DefineParameter(cmd, "@ShortDesc", DbType.String, shortDesc);
			this.db.DefineParameter(cmd, "@Latitude", DbType.Double, lat);
			this.db.DefineParameter(cmd, "@Longitude", DbType.Double, lon);
			return cmd;
		}

		private DbCommand CreateUpdateCommand(int id, int areaId, string desc, string shortDesc, double lat, double lon) {
			DbCommand cmd = this.db.CreateCommand(SQL_UPDATE);
			this.db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			this.db.DefineParameter(cmd, "@AreaId", DbType.Int32, areaId);
			this.db.DefineParameter(cmd, "@Desc", DbType.String, desc);
			this.db.DefineParameter(cmd, "@ShortDesc", DbType.String, shortDesc);
			this.db.DefineParameter(cmd, "@Latitude", DbType.Double, lat);
			this.db.DefineParameter(cmd, "@Longitude", DbType.Double, lon);
			return cmd;
		}

		private Venue CreateVenueFromReader(IDataReader reader) {
			return new Venue() {
				Id = (int)reader["id"],
				AreaId = (int)reader["area_id"],
				ShortDescription = reader["short_desc"] as string,
				Description = reader["desc"] as string,
				Latitude = (double)reader["latitude"],
				Longitude = (double)reader["longitude"]
			};
		}

		private DbCommand createSelectForPerformances(IEnumerable<Performance> performances) {
			var parameters = performances.Select(p => p.VenueId.ToString()).ToArray();
			var commandString = string.Format(SQL_SELECT_FOR_PERFORMANCES, string.Join(", ", parameters));
			DbCommand cmd = db.CreateCommand(commandString);
			return cmd;
		}

		public VenueDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<Venue> GetAll() {
			var result = new List<Venue>();
			var cmd = this.CreateSelectAllCommand();
			using (IDataReader reader = this.db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					result.Add(this.CreateVenueFromReader(reader));
				}
			}
			return result;
		}

		public Venue GetById(int id) {
			Venue venue = null;
			var cmd = this.CreateSelectByIdCommand(id);
			using (IDataReader reader = this.db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					venue = this.CreateVenueFromReader(reader);
				}
			}
			if (venue == null) {
				throw new EntityNotFoundException();
			}
			return venue;
		}

		public IEnumerable<Venue> GetForArea(Area area) {
			var result = new List<Venue>();
			var cmd = this.CreateSelectForAreaCommand(area);
			using (IDataReader reader = this.db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					result.Add(this.CreateVenueFromReader(reader));
				}
			}
			return result;
		}

		public Venue Create(Venue venue) {
			var cmd = this.CreateInsertCommand(venue.AreaId, venue.Description, venue.ShortDescription, venue.Latitude, venue.Longitude);
			db.ExecuteNonQuery(cmd);
			venue.Id = (int)((MySqlCommand)cmd).LastInsertedId;
			return venue;
		}

		public Venue Update(Venue venue) {
			var cmd = this.CreateUpdateCommand(venue.Id, venue.AreaId, venue.Description, venue.ShortDescription, venue.Latitude, venue.Longitude);
			if (db.ExecuteNonQuery(cmd) != 1) {
				throw new EntityNotFoundException();
			}
			return venue;
		}

		public IEnumerable<Venue> GetForPerformances(IEnumerable<Performance> performances) {
			var venue = new List<Venue>();
			if (performances.Count() == 0) {
				return venue;
			}
			DbCommand cmd = createSelectForPerformances(performances);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					venue.Add(CreateVenueFromReader(reader));
				}
			}
			return venue;
		}
	}
}