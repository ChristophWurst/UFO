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

	public class SpectacledayTimeSlotDAO : ISpectacledayTimeSlotDAO {
		private IDatabase db;

		private const string SQL_SELECT_FOR_SPECTACLEDAY = "SELECT * "
														 + "FROM `spectacleday_timeslot` "
														 + "WHERE `spectacleday_id` = @SpectacleDayId";

		private const string SQL_SELECT_ALL = "SELECT * "
											+ "FROM `spectacleday_timeslot`";

		private const string SQL_SELECT_FOR_PERFORMANCES = @"SELECT * FROM `spectacleday_timeslot` s WHERE s.id IN ({0})";

		private DbCommand CreateSelectAllCommand() {
			return db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand CreateSelectForSpectacleDayCommand(Spectacleday spectacleDay) {
			var cmd = db.CreateCommand(SQL_SELECT_FOR_SPECTACLEDAY);
			db.DefineParameter(cmd, "@SpectacleDayId", DbType.Int32, spectacleDay.Id);
			return cmd;
		}

		private DbCommand createSelectForPerformances(IEnumerable<Performance> performances) {
			var parameters = performances.Select(p => p.SpectacledayTimeSlot.ToString()).ToArray();
			var commandString = string.Format(SQL_SELECT_FOR_PERFORMANCES, string.Join(", ", parameters));
			DbCommand cmd = db.CreateCommand(commandString);
			return cmd;
		}

		public SpectacledayTimeSlotDAO(IDatabase db) {
			this.db = db;
		}

		private SpectacledayTimeSlot CreateSpectacleTimeslotFromReader(IDataReader reader) {
			return new SpectacledayTimeSlot {
				Id = (int)reader["id"],
				SpectacledayId = (int)reader["spectacleday_id"],
				TimeSlotId = (int)reader["timeslot_id"]
			};
		}

		public IEnumerable<SpectacledayTimeSlot> GetAll() {
			var spectacledayTimeSlots = new List<SpectacledayTimeSlot>();
			DbCommand cmd = CreateSelectAllCommand();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					spectacledayTimeSlots.Add(CreateSpectacleTimeslotFromReader(reader));
				}
			}
			return spectacledayTimeSlots;
		}

		public SpectacledayTimeSlot GetById(int id) {
			throw new NotImplementedException();
		}

		public IEnumerable<SpectacledayTimeSlot> GetForSpectacleDay(Spectacleday spectacleDay) {
			var result = new List<SpectacledayTimeSlot>();
			var cmd = CreateSelectForSpectacleDayCommand(spectacleDay);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					result.Add(CreateSpectacleTimeslotFromReader(reader));
				}
			}
			return result;
		}

		public IEnumerable<SpectacledayTimeSlot> GetForPerformances(IEnumerable<Performance> performances) {
			var spectacledayTimeSlot = new List<SpectacledayTimeSlot>();
			DbCommand cmd = createSelectForPerformances(performances);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					spectacledayTimeSlot.Add(CreateSpectacleTimeslotFromReader(reader));
				}
			}
			return spectacledayTimeSlot;
		}
	}
}