using System.Data;
using System.Data.Common;

namespace UFO.DAL {

    public interface IDatabase {

        DbCommand CreateCommand(string sql);

        int DeclareParameter(DbCommand cmd, string name, DbType type);

        int DefineParameter(DbCommand cmd, string name, DbType type, object value);

        void SetParameter(DbCommand cmd, string name, object value);

        IDataReader ExecuteQuery(DbCommand cmd);

        int ExecuteNonQuery(DbCommand cmd);
    }
}