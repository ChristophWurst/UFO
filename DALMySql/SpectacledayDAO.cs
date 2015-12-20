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

	public class SpectacledayDAO : ISpectacledayDAO {
		private const string SQL_SELECT_ALL = @"SELECT * FROM `spectacleday`";

		private const string SQL_SELECT = @"SELECT * FROM `spectacleday` WHERE `id` = @Id";

		private const string SQL_SELECT_FOR_PERFORMANCE = @"SELECT sd.*
															FROM `performance` p
															JOIN `spectacleday_timeslot` sts
															ON p.`spectacleday_timeslot_id` = sts.`id`
															JOIN `spectacleday` sd
															ON sts.`spectacleday_id` = sd.`id`
															WHERE p.`id` = @PerformanceId;";

		private DbCommand createSelectAll() {
			return db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand createSelectByIdCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", System.Data.DbType.Int32, id);
			return cmd;
		}

		private DbCommand createSelectForPerformanceCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT_FOR_PERFORMANCE);
			db.DefineParameter(cmd, "@PerformanceId", System.Data.DbType.Int32, id);
			return cmd;
		}

		private Spectacleday createSpectacledayFromReader(IDataReader reader) {
			return new Spectacleday() {
				Id = (int)reader["id"],
				Day = (DateTime)reader["day"]
			};
		}

		private IDatabase db;

		public SpectacledayDAO(IDatabase db) {
			this.db = db;
		}

		public IEnumerable<Spectacleday> GetAll() {
			var spectacledays = new List<Spectacleday>();
			DbCommand cmd = createSelectAll();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					spectacledays.Add(createSpectacledayFromReader(reader));
				}
			}
			return spectacledays;
		}

		public Spectacleday GetById(int id) {
			Spectacleday spectacleday = null;
			DbCommand cmd = createSelectByIdCommand(id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					spectacleday = createSpectacledayFromReader(reader);
				}
			}
			if (spectacleday == null) {
				throw new EntityNotFoundException();
			}
			return spectacleday;
		}

		public Spectacleday GetForPerformance(Performance performance) {
			Spectacleday spectacleday = null;
			DbCommand cmd = createSelectForPerformanceCommand(performance.Id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					spectacleday = createSpectacledayFromReader(reader);
				}
			}
			if (spectacleday == null) {
				throw new EntityNotFoundException();
			}
			return spectacleday;
		}
	}
}