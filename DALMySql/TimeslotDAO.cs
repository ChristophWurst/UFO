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

		private DbCommand createSelectAll() {
			return db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand createSelectByIdCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", System.Data.DbType.Int32, id);
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
			TimeSlot timeslot = null;
			DbCommand cmd = createSelectByIdCommand(id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					timeslot = createTimeSlotFromReader(reader);
				}
			}
			if (timeslot == null) {
				throw new EntityNotFoundException();
			}
			return timeslot;
		}
	}
}