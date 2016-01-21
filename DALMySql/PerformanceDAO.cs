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

		private const string SQL_SELECT_FOR_SPECTACLE_DAY = @"SELECT p.`id`, p.`artist_id`, p.`venue_id`, p.`spectacleday_timeslot_id`
															FROM `performance` p, `spectacleday_timeslot` ts
															WHERE p.`spectacleday_timeslot_id` = ts.`id`
															AND ts.`spectacleday_id` = @SpectacleDayId";

		private const string SQL_SELECT = "SELECT * "
										+ "FROM `performance` "
										+ "WHERE `id` = @Id";

		private const string SQL_SELECT_FOR_ARTIST = "SELECT * "
													+ "FROM `performance` "
													+ "WHERE `artist_id` = @ArtistId";

		private const string SQL_SELECT_FOR_VENUE = "SELECT * "
											+ "FROM `performance` "
											+ "WHERE `venue_id` = @VenueId";

		private const string SQL_INSERT = "INSERT INTO `performance` "
										+ "(`artist_id`, `venue_id`, `spectacleday_timeslot_id`) VALUES "
										+ "(@ArtistId, @VenueId, @SpectacledayTimeSlotId)";

		private const string SQL_UPDATE = "UPDATE `performance` SET "
										+ "`artist_id` = @ArtistId, "
										+ "`venue_id`= @VenueId, "
										+ "`spectacleday_timeslot_id` = @SpectacledayTimeSlotId "
										+ "WHERE `id` = @Id";

		private const string SQL_DELETE = "DELETE FROM `performance` "
										+ "WHERE `id` = @Id";

		private IDatabase db;

		private DbCommand CreateSelectAllCommand() {
			return this.db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand CreateSelectForSpectacleDay(int id) {
			var cmd = db.CreateCommand(SQL_SELECT_FOR_SPECTACLE_DAY);
			db.DefineParameter(cmd, "@SpectacleDayId", DbType.Int32, id);
			return cmd;
		}

		private DbCommand CreateSelectForArtist(int id) {
			var cmd = db.CreateCommand(SQL_SELECT_FOR_ARTIST);
			db.DefineParameter(cmd, "@ArtistId", DbType.Int32, id);
			return cmd;
		}

		private DbCommand CreateSelectForVenue(int id) {
			var cmd = db.CreateCommand(SQL_SELECT_FOR_VENUE);
			db.DefineParameter(cmd, "@VenueId", DbType.Int32, id);
			return cmd;
		}

		private DbCommand CreateSelectByIdCommand(int id) {
			var cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			return cmd;
		}

		private DbCommand CreateUpdateCommand(int id, int artistId, int venueId, int timeslot) {
			var cmd = db.CreateCommand(SQL_UPDATE);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			db.DefineParameter(cmd, "@ArtistId", DbType.Int32, artistId);
			db.DefineParameter(cmd, "@VenueId", DbType.Int32, venueId);
			db.DefineParameter(cmd, "@SpectacledayTimeSlotId", DbType.Int32, timeslot);
			return cmd;
		}

		private DbCommand CreateInsertCommand(int artistId, int venueId, int timeslot) {
			var cmd = db.CreateCommand(SQL_INSERT);
			db.DefineParameter(cmd, "@ArtistId", DbType.Int32, artistId);
			db.DefineParameter(cmd, "@VenueId", DbType.Int32, venueId);
			db.DefineParameter(cmd, "@SpectacledayTimeSlotId", DbType.Int32, timeslot);
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
				ArtistId = (int)reader["artist_id"],
				VenueId = (int)reader["venue_id"],
				SpectacledayTimeSlot = (int)reader["spectacleday_timeslot_id"]
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

		public IEnumerable<Performance> GetForSpectacleDay(Spectacleday spectacleDay) {
			var result = new List<Performance>();
			var cmd = CreateSelectForSpectacleDay(spectacleDay.Id);
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
			var cmd = CreateUpdateCommand(performance.Id, performance.ArtistId, performance.VenueId, performance.SpectacledayTimeSlot);
			if (db.ExecuteNonQuery(cmd) != 1) {
				throw new EntityNotFoundException();
			}
			return performance;
		}

		public Performance Create(Performance performance) {
			var cmd = CreateInsertCommand(performance.ArtistId, performance.VenueId, performance.SpectacledayTimeSlot);
			db.ExecuteNonQuery(cmd);
			performance.Id = (int)((MySqlCommand)cmd).LastInsertedId;
			return performance;
		}

		public void Delete(Performance performance) {
			var cmd = CreateDeleteCommand(performance.Id);
			if (db.ExecuteNonQuery(cmd) != 1) {
				throw new EntityNotFoundException($"Performance with id {performance.Id} not found.");
			}
		}

		public IEnumerable<Performance> GetForArtist(Artist artist) {
			var result = new List<Performance>();
			var cmd = CreateSelectForArtist(artist.Id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					result.Add(CreatePerformanceFromReader(reader));
				}
				return result;
			}
		}

		public IEnumerable<Performance> GetForVenue(Venue venue) {
			var result = new List<Performance>();
			var cmd = CreateSelectForVenue(venue.Id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					result.Add(CreatePerformanceFromReader(reader));
				}
				return result;
			}
		}
	}
}