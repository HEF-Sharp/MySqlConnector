using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector.Core;

namespace MySqlConnector.Diagnostics
{
	internal static class MySqlCommandExtensions
	{
		internal static MySqlDiagnosticsCommand ToDiagnosticsCommand(this IMySqlCommand sqlCommand)
		{
			if (sqlCommand == null)
				throw new ArgumentNullException(nameof(sqlCommand));

			return new MySqlDiagnosticsCommand
			{
				CommandId = sqlCommand.CancellableCommand.CommandId,
				CommandTimeout = sqlCommand.CancellableCommand.CommandTimeout,
				CommandText = sqlCommand.CommandText,
				CommandType = sqlCommand.CommandType,
				AllowUserVariables = sqlCommand.AllowUserVariables,
				CommandBehavior = sqlCommand.CommandBehavior,
				RawParameters = sqlCommand.RawParameters,
				Connection = sqlCommand.Connection
			};
		}

		internal static IReadOnlyList<MySqlDiagnosticsCommand> ToDiagnosticsCommands(this IEnumerable<IMySqlCommand> sqlCommands)
		{
			if (sqlCommands == null || !sqlCommands.Any())
				throw new ArgumentNullException(nameof(sqlCommands));

			return sqlCommands.Select(c => c.ToDiagnosticsCommand()).ToArray();
		}
	}
}
