using System.Collections.Generic;
using System.Data.Common;
using UFO.DAL.Common;

namespace DALMySqlTest {

	internal abstract class IntegrationTest {

		protected void runDbCommands(IDatabase db, IList<string> sqls) {
			foreach (string sql in sqls) {
				DbCommand cmd = db.CreateCommand(sql);
				db.ExecuteNonQuery(cmd);
			}
		}
	}
}