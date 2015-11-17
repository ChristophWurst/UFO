using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.DAL.Common;
using UFO.DomainClasses;

namespace UFO.DAL.Independent {
    public class UserDAO : IUserDAO {

        private const string SQL_SELECT_ALL = "SELECT * "
                                            + "FROM `user`";

        private const string SQL_SELECT = "SELECT * "
                                        + "FROM `user` "
                                        + "WHERE `id` = @Id";

        private IDatabase db;

        private DbCommand createSelectAllCommand() {
            return db.CreateCommand(SQL_SELECT_ALL);
        }

        private DbCommand createSelectByIdCommand(int id) {
            DbCommand cmd = db.CreateCommand(SQL_SELECT);
            db.DefineParameter(cmd, "@Id", DbType.Int32, id);
            return cmd;
        }

        public UserDAO(IDatabase db) {
            this.db = db;
        }

        private User createUserFromReader(IDataReader reader) {
            return new User() {
                Id = (int)reader["id"],
                Email = (string)reader["email"],
                Password = (string)reader["password"]
            };
        }

        public IEnumerable<User> GetAll() {
            var userList = new List<User>();
            DbCommand cmd = createSelectAllCommand();
            using (IDataReader reader = db.ExecuteReader(cmd)) {
                while (reader.Read()) {
                    userList.Add(createUserFromReader(reader));
                }
            }
            return userList;
        }

        public User GetById(int id) {
            User user = null;
            DbCommand cmd = createSelectByIdCommand(id);
            using (IDataReader reader = db.ExecuteReader(cmd)) {
                if (reader.Read()) {
                    user = createUserFromReader(reader);
                }
            }
            return user;
        }
    }
}
