using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.MySql {

	public class CategoryDAO : ICategoryDAO {

		private const string SQL_SELECT_ALL = "SELECT * "
											+ "FROM `category` ";

		private const string SQL_SELECT = "SELECT * "
										+ "FROM `category` "
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

		public CategoryDAO(IDatabase db) {
			this.db = db;
		}

		private Category createCategoryFromReader(IDataReader reader) {
			return new Category() {
				Id = (int)reader["id"],
				Description = (string)reader["description"],
				Color = (string)reader["color"]
			};
		}

		public IEnumerable<Category> GetAll() {
			var categories = new List<Category>();
			DbCommand cmd = createSelectAllCommand();
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				while (reader.Read()) {
					categories.Add(createCategoryFromReader(reader));
				}
			}
			return categories;
		}

		public Category GetById(int id) {
			Category category = null;
			DbCommand cmd = createSelectByIdCommand(id);
			using (IDataReader reader = db.ExecuteReader(cmd)) {
				if (reader.Read()) {
					category = createCategoryFromReader(reader);
				}
			}
			if (category == null) {
				throw new EntityNotFoundException();
			}
			return category;
		}
	}
}