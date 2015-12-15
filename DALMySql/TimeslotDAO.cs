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

	public class TimeslotDAO : ITimeslotDAO {
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

		private Timeslot createTimeslotFromReader(IDataReader reader) {
			return new Timeslot() {
				Id = (int)reader["id"],
				Start = (TimeSpan)reader["Start"],
				End = (TimeSpan)reader["End"]
			};
		}

		private IDatabase db;

		public TimeslotDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<Timeslot> GetAll() {
			var timeslot = new List<Timeslot>();
			DbCommand cmd = createSelectAll();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					timeslot.Add(createTimeslotFromReader(reader));
				}
			}
			return timeslot;
		}

		public Timeslot GetById(int id) {
			Timeslot timeslot = null;
			DbCommand cmd = createSelectByIdCommand(id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					timeslot = createTimeslotFromReader(reader);
				}
			}
			if (timeslot == null) {
				throw new EntityNotFoundException();
			}
			return timeslot;
		}
	}
}