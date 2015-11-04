using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UFO.DAL.Common;

namespace UFO.DAL.Independent {

    public class Database : IDatabase {
        private string connectionString;
        private DbProviderFactory providerFactory;

        [ThreadStatic]
        private DbConnection sharedConnection;

        private DbConnection CreateOpenConnection() {
            DbConnection conn = this.providerFactory.CreateConnection();
            conn.ConnectionString = this.connectionString;
            conn.Open();
            return conn;
        }

        private DbConnection GetOpenConnection() {
            Transaction currTx = Transaction.Current;
            if (currTx == null) {
                return this.CreateOpenConnection();
            }
            if (this.sharedConnection == null) {
                this.sharedConnection = this.CreateOpenConnection();
                currTx.TransactionCompleted += (s, e) => {
                    this.sharedConnection.Close();
                    this.sharedConnection = null;
                };
            }
            return this.sharedConnection;
        }

        private void ReleaseConnection(DbConnection conn) {
            if (conn == null) {
                return;
            }
            if (Transaction.Current == null) {
                conn.Close();
            }
        }

        private bool IsSharedConnection() {
            return Transaction.Current != null;
        }

        public Database(string connectionString, string providername) {
            this.connectionString = connectionString;
            this.providerFactory = DbProviderFactories.GetFactory(providername);
        }

        public DbCommand CreateCommand(string sql) {
            DbCommand cmd = providerFactory.CreateCommand();
            cmd.CommandText = sql;
            return cmd;
        }

        public int DeclareParameter(DbCommand cmd, string name, DbType type) {
            if (cmd.Parameters.Contains(name)) {
                throw new ArgumentException($"Parameter {name} already declared.");
            }
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            return cmd.Parameters.Add(param);
        }

        public int DefineParameter(DbCommand cmd, string name, DbType type, object value) {
            int paramIdx = DeclareParameter(cmd, name, type);
            cmd.Parameters[paramIdx].Value = value;
            return paramIdx;
        }

        public int ExecuteNonQuery(DbCommand cmd) {
            DbConnection conn = null;
            try {
                conn = this.GetOpenConnection();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            } finally {
                this.ReleaseConnection(conn);
            }
        }

        public IDataReader ExecuteReader(DbCommand cmd) {
            DbConnection conn = null;
            try {
                conn = this.GetOpenConnection();
                cmd.Connection = conn;
                CommandBehavior behavior = this.IsSharedConnection() ? CommandBehavior.Default : CommandBehavior.CloseConnection;
                return cmd.ExecuteReader(behavior);
            } catch {
                ReleaseConnection(conn);
                throw;
            }
        }

        public void SetParameter(DbCommand cmd, string name, object value) {
            if (!cmd.Parameters.Contains(name)) {
                throw new ArgumentException($"Parameter {name} not declared.");
            }
            cmd.Parameters[name].Value = value;
        }
    }
}