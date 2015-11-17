﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;

namespace DALIndependentTest {
    abstract class IntegrationTest {
        protected void runDbCommands(IDatabase db, IList<string> sqls) {
            foreach (string sql in sqls) {
                DbCommand cmd = db.CreateCommand(sql);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}
