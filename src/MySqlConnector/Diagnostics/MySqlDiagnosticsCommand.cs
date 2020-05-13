using System.Data;
using MySql.Data.MySqlClient;

namespace MySqlConnector.Diagnostics
{
	public class MySqlDiagnosticsCommand
	{
	 	public int CommandId { get; set; }
	 	public int CommandTimeout { get; set; }
		public string? CommandText { get; set; }
		public CommandType CommandType { get; set; }
		public bool AllowUserVariables { get; set; }
		public CommandBehavior CommandBehavior { get; set; }
	 	public MySqlParameterCollection? RawParameters { get; set; }
		public MySqlConnection? Connection { get; set; }
	}
}
