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

	public class TimeSlotDAO : ITimeSlotDAO {
		private const string SQL_SELECT_ALL = @"SELECT * FROM `timeslot`";
		private const string SQL_SELECT = @"SELECT * FROM `timeslot` WHERE `id` = @Id";
		private const string SQL_SELECT_FOR_PERFORMANCE = @"SELECT t.id, t.Start, t.End FROM `timeslot` t, `spectacleday_timeslot` s WHERE t.id = s.timeslot_id AND s.id = @Id";

		private const string SQL_SELECT_FOR_PERFORMANCES = @"SELECT DISTINCT t.id, t.Start, t.End
														     FROM `timeslot` t, `spectacleday_timeslot` s
															 WHERE t.id = s.timeslot_id AND s.id IN ({0})";

		private string VALUES;

		private DbCommand createSelectAll() {
			return db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand createSelectByIdCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", System.Data.DbType.Int32, id);
			return cmd;
		}

		private DbCommand createSelectForPerformance(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT_FOR_PERFORMANCE);
			db.DefineParameter(cmd, "@Id", System.Data.DbType.Int32, id);
			return cmd;
		}

		private DbCommand createSelectForPerformances(IEnumerable<Performance> performances) {
			var parameters = performances.Select(p => p.SpectacledayTimeSlot.ToString()).ToArray();
			var commandString = string.Format(SQL_SELECT_FOR_PERFORMANCES, string.Join(", ", parameters));
			DbCommand cmd = db.CreateCommand(commandString);
			return cmd;
		}

		private TimeSlot createTimeSlotFromReader(IDataReader reader) {
			return new TimeSlot() {
				Id = (int)reader["id"],
				Start = (int)reader["Start"],
				End = (int)reader["End"]
			};
		}

		private IDatabase db;

		public TimeSlotDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<TimeSlot> GetAll() {
			var timeslot = new List<TimeSlot>();
			DbCommand cmd = createSelectAll();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					timeslot.Add(createTimeSlotFromReader(reader));
				}
			}
			return timeslot;
		}

		public TimeSlot GetById(int id) {
			TimeSlot timeSlot = null;
			DbCommand cmd = createSelectByIdCommand(id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					timeSlot = createTimeSlotFromReader(reader);
				}
			}
			if (timeSlot == null) {
				throw new EntityNotFoundException();
			}
			return timeSlot;
		}

		public TimeSlot GetForPerformance(Performance performance) {
			TimeSlot timeSlot = null;
			DbCommand cmd = createSelectForPerformance(performance.SpectacledayTimeSlot);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					timeSlot = createTimeSlotFromReader(reader);
				}
			}
			if (timeSlot == null) {
				throw new EntityNotFoundException();
			}
			return timeSlot;
		}

		public IEnumerable<TimeSlot> GetForPerformances(IEnumerable<Performance> performances) {
			var timeslot = new List<TimeSlot>();
			DbCommand cmd = createSelectForPerformances(performances);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					timeslot.Add(createTimeSlotFromReader(reader));
				}
			}
			return timeslot;
		}
	}
}