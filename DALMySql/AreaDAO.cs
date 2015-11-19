using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class AreaDAO : IAreaDAO {

		private const string SQL_SELECT_ALL = "SELECT * "
											+ "FROM `area`";

		private const string SQL_SELECT = "SELECT * "
										+ "FROM `area` "
										+ "WHERE `id` = @Id";

		private IDatabase db;

		private DbCommand createSelectAllCommand() {
			return db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand createSelectCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			return cmd;
		}

		public AreaDAO(IDatabase db) {
			this.db = db;
		}

		private Area createAreaFromReader(IDataReader reader) {
			return new Area() {
				Id = (int)reader["id"],
				Name = (string)reader["name"]
			};
		}

		public IEnumerable<Area> GetAll() {
			var areas = new List<Area>();
			DbCommand cmd = createSelectAllCommand();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					areas.Add(createAreaFromReader(reader));
				}
			}
			return areas;
		}

		public Area GetById(int id) {
			Area area = null;
			DbCommand cmd = createSelectCommand(id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					area = createAreaFromReader(reader);
				}
			}
			return area;
		}
	}
}