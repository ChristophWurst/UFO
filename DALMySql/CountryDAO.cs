using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class CountryDAO : ICountryDAO {

		private const string SQL_SELECT_ALL = "SELECT * "
											+ "FROM `country`";

		private const string SQL_SELECT = "SELECT * "
										+ "FROM `country` "
										+ "WHERE `id` = @Id";

		private DbCommand createSelectAllCommand() {
			return db.CreateCommand(SQL_SELECT_ALL);
		}

		private DbCommand createSelectByIdCommand(int id) {
			DbCommand cmd = db.CreateCommand(SQL_SELECT);
			db.DefineParameter(cmd, "@Id", DbType.Int32, id);
			return cmd;
		}

		private IDatabase db;

		public CountryDAO(IDatabase db) {
			this.db = db;
		}

		private Country createCountryFromReader(IDataReader reader) {
			return new Country() {
				Id = (int)reader["id"],
				Name = (string)reader["name"]
			};
		}

		public IEnumerable<Country> GetAll() {
			var countries = new List<Country>();
			DbCommand cmd = createSelectAllCommand();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					countries.Add(createCountryFromReader(reader));
				}
			}
			return countries;
		}

		public Country GetById(int id) {
			Country country = null;
			DbCommand cmd = createSelectByIdCommand(id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					country = createCountryFromReader(reader);
				}
			}
			return country;
		}
	}
}