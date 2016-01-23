using System.Data;
using System.Data.Common;

namespace UFO.DAL.Common {

	/// <summary>
	/// Database-independent database interface
	///
	/// Needs to be implemented by all DAL-assemblies
	/// </summary>
	public interface IDatabase {

		/// <summary>
		/// Create a new DbCommand for a specific database
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		DbCommand CreateCommand(string sql);

		/// <summary>
		/// Declare a query parameter
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="name"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		int DeclareParameter(DbCommand cmd, string name, DbType type);

		/// <summary>
		/// Declare and set the value of a query parameter
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="name"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		int DefineParameter(DbCommand cmd, string name, DbType type, object value);

		/// <summary>
		/// Set the value of a query parameter
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		void SetParameter(DbCommand cmd, string name, object value);

		/// <summary>
		/// Execute a command and get the reader to iterate the result
		/// </summary>
		/// <param name="cmd"></param>
		/// <returns></returns>
		IDataReader ExecuteReader(DbCommand cmd);

		/// <summary>
		/// Execute a non-query command (insert, update, delete)
		/// </summary>
		/// <param name="cmd"></param>
		/// <returns></returns>
		int ExecuteNonQuery(DbCommand cmd);
	}
}