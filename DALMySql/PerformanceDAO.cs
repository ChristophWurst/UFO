using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class PerformanceDAO : IPerformanceDAO {

		private const string SQL_SELECT_ALL = "SELECT * "
											+ "FROM `performance`";

		private const string SQL_SELECT = "SELECT * "
										+ "FROM `performance` "
										+ "WHERE `id` = @Id";

		private const string SQL_INSERT = "INSERT INTO `performance` "
										+ "(`start`, `end`, `artist_id`, `venue_id`) VALUES "
										+ "(@Start, @End, @ArtistId, @VenueId)";

		private const string SQL_UPDATE = "UPDATE `performance` SET "
										+ "`start` = @Start, "
										+ "`end` = @End, "
										+ "`artist_id` = @ArtistId, "
										+ "`venue_id`= @VenueId "
										+ "WHERE `id` = @Id";

		private const string SQL_DELETE = "DELETE FROM `performance` "
										+ "WHERE `id` = @Id";

		private IDatabase db;

		private DbCommand CreateSelectAllCommand() {
			return this.db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand CreateSelectByIdCommand(int id) {
			var cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			return cmd;
		}

		private DbCommand CreateUpdateCommand(int id, DateTime start, DateTime end, int artistId, int venueId) {
			var cmd = db.CreateCommand(SQL_UPDATE);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			db.DefineParameter(cmd, "@Start", DbType.DateTime, start);
			db.DefineParameter(cmd, "@End", DbType.DateTime, end);
			db.DefineParameter(cmd, "@ArtistId", DbType.Int32, artistId);
			db.DefineParameter(cmd, "@VenueId", DbType.Int32, venueId);
			return cmd;
		}

		private DbCommand CreateInsertCommand(DateTime start, DateTime end, int artistId, int venueId) {
			var cmd = db.CreateCommand(SQL_INSERT);
			db.DefineParameter(cmd, "@Start", DbType.DateTime, start);
			db.DefineParameter(cmd, "@End", DbType.DateTime, end);
			db.DefineParameter(cmd, "@ArtistId", DbType.Int32, artistId);
			db.DefineParameter(cmd, "@VenueId", DbType.Int32, venueId);
			return cmd;
		}

		private DbCommand CreateDeleteCommand(int id) {
			var cmd = db.CreateCommand(SQL_DELETE);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			return cmd;
		}

		private Performance CreatePerformanceFromReader(IDataReader reader) {
			return new Performance() {
				Id = (int)reader["id"],
				Start = (DateTime)reader["start"],
				End = (DateTime)reader["end"],
				ArtistId = (int)reader["artist_id"],
				VenueId = (int)reader["venue_id"]
			};
		}

		public PerformanceDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<Performance> GetAll() {
			var result = new List<Performance>();
			var cmd = CreateSelectAllCommand();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					result.Add(CreatePerformanceFromReader(reader));
				}
				return result;
			}
		}

		public Performance GetById(int id) {
			Performance performance = null;
			var cmd = CreateSelectByIdCommand(id);
			using (IDataReader reader = this.db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					performance = this.CreatePerformanceFromReader(reader);
				}
			}
			if (performance == null) {
				throw new EntityNotFoundException();
			}
			return performance;
		}

		public Performance Update(Performance performance) {
			var cmd = CreateUpdateCommand(performance.Id, performance.Start, performance.End, performance.ArtistId, performance.VenueId);
			if (db.ExecuteNonQuery(cmd) != 1) {
				throw new EntityNotFoundException();
			}
			return performance;
		}

		public Performance Create(Performance performance) {
			var cmd = CreateInsertCommand(performance.Start, performance.End, performance.ArtistId, performance.VenueId);
			db.ExecuteNonQuery(cmd);
			performance.Id = (int)((MySqlCommand)cmd).LastInsertedId;
			return performance;
		}

		public void Delete(Performance Performance) {
			var cmd = CreateDeleteCommand(Performance.Id);
			if (db.ExecuteNonQuery(cmd) != 1) {
				throw new EntityNotFoundException();
			}
		}
	}
}