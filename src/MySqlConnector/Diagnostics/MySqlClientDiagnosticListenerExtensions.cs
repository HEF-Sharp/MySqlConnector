using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using MySqlConnector.Core;

namespace MySqlConnector.Diagnostics
{
	internal static class MySqlClientDiagnosticListenerExtensions
	{
		public const string DiagnosticListenerName = "MySqlClientDiagnosticListener";

		private const string MySqlClientPrefix = "MySql.Data.MySqlClient.";

		public const string MySqlBeforeExecuteCommand = MySqlClientPrefix + nameof(WriteCommandBefore);
		public const string MySqlAfterExecuteCommand = MySqlClientPrefix + nameof(WriteCommandAfter);
		public const string MySqlErrorExecuteCommand = MySqlClientPrefix + nameof(WriteCommandError);

		public const string MySqlBeforeOpenConnection = MySqlClientPrefix + nameof(WriteConnectionOpenBefore);
		public const string MySqlAfterOpenConnection = MySqlClientPrefix + nameof(WriteConnectionOpenAfter);
		public const string MySqlErrorOpenConnection = MySqlClientPrefix + nameof(WriteConnectionOpenError);

		public const string MySqlBeforeCloseConnection = MySqlClientPrefix + nameof(WriteConnectionCloseBefore);
		public const string MySqlAfterCloseConnection = MySqlClientPrefix + nameof(WriteConnectionCloseAfter);
		public const string MySqlErrorCloseConnection = MySqlClientPrefix + nameof(WriteConnectionCloseError);

		public const string MySqlBeforeCommitTransaction = MySqlClientPrefix + nameof(WriteTransactionCommitBefore);
		public const string MySqlAfterCommitTransaction = MySqlClientPrefix + nameof(WriteTransactionCommitAfter);
		public const string MySqlErrorCommitTransaction = MySqlClientPrefix + nameof(WriteTransactionCommitError);

		public const string MySqlBeforeRollbackTransaction = MySqlClientPrefix + nameof(WriteTransactionRollbackBefore);
		public const string MySqlAfterRollbackTransaction = MySqlClientPrefix + nameof(WriteTransactionRollbackAfter);
		public const string MySqlErrorRollbackTransaction = MySqlClientPrefix + nameof(WriteTransactionRollbackError);

		#region Command
		public static Guid WriteCommandBefore(this DiagnosticListener @this,
			IReadOnlyList<IMySqlCommand> sqlCommands, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlBeforeExecuteCommand))
			{
				Guid operationId = Guid.NewGuid();

				@this.Write(
					MySqlBeforeExecuteCommand,
					new
					{
						OperationId = operationId,
						Operation = operation,
						Commands = sqlCommands,
						Timestamp = Stopwatch.GetTimestamp()
					});

				return operationId;
			}
			else
				return Guid.Empty;
		}

		public static void WriteCommandAfter(this DiagnosticListener @this, Guid operationId,
			IReadOnlyList<IMySqlCommand> sqlCommands, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlAfterExecuteCommand))
			{
				@this.Write(
					MySqlAfterExecuteCommand,
					new
					{
						OperationId = operationId,
						Operation = operation,
						Commands = sqlCommands,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}

		public static void WriteCommandError(this DiagnosticListener @this, Guid operationId,
			IReadOnlyList<IMySqlCommand> sqlCommands, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlErrorExecuteCommand))
			{
				@this.Write(
					MySqlErrorExecuteCommand,
					new
					{
						OperationId = operationId,
						Operation = operation,
						Commands = sqlCommands,
						Exception = ex,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}
		#endregion

		#region Connection
		public static Guid WriteConnectionOpenBefore(this DiagnosticListener @this,
			MySqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlBeforeOpenConnection))
			{
				Guid operationId = Guid.NewGuid();

				@this.Write(
					MySqlBeforeOpenConnection,
					new
					{
						OperationId = operationId,
						Operation = operation,
						Connection = sqlConnection,
						Timestamp = Stopwatch.GetTimestamp()
					});

				return operationId;
			}
			else
				return Guid.Empty;
		}

		public static void WriteConnectionOpenAfter(this DiagnosticListener @this, Guid operationId,
			MySqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlAfterOpenConnection))
			{
				@this.Write(
					MySqlAfterOpenConnection,
					new
					{
						OperationId = operationId,
						Operation = operation,
						Connection = sqlConnection,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}

		public static void WriteConnectionOpenError(this DiagnosticListener @this, Guid operationId,
			MySqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlErrorOpenConnection))
			{
				@this.Write(
					MySqlErrorOpenConnection,
					new
					{
						OperationId = operationId,
						Operation = operation,
						Connection = sqlConnection,
						Exception = ex,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}

		public static Guid WriteConnectionCloseBefore(this DiagnosticListener @this,
			MySqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlBeforeCloseConnection))
			{
				Guid operationId = Guid.NewGuid();

				@this.Write(
					MySqlBeforeCloseConnection,
					new
					{
						OperationId = operationId,
						Operation = operation,
						Connection = sqlConnection,
						Timestamp = Stopwatch.GetTimestamp()
					});

				return operationId;
			}
			else
				return Guid.Empty;
		}

		public static void WriteConnectionCloseAfter(this DiagnosticListener @this, Guid operationId,
			string clientConnectionId, MySqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlAfterCloseConnection))
			{
				@this.Write(
					MySqlAfterCloseConnection,
					new
					{
						OperationId = operationId,
						Operation = operation,
						ConnectionId = clientConnectionId,
						Connection = sqlConnection,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}

		public static void WriteConnectionCloseError(this DiagnosticListener @this, Guid operationId,
			string clientConnectionId, MySqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlErrorCloseConnection))
			{
				@this.Write(
					MySqlErrorCloseConnection,
					new
					{
						OperationId = operationId,
						Operation = operation,
						ConnectionId = clientConnectionId,
						Connection = sqlConnection,
						Exception = ex,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}
		#endregion

		#region Transaction
		public static Guid WriteTransactionCommitBefore(this DiagnosticListener @this,
			IsolationLevel isolationLevel, MySqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlBeforeCommitTransaction))
			{
				Guid operationId = Guid.NewGuid();

				@this.Write(
					MySqlBeforeCommitTransaction,
					new
					{
						OperationId = operationId,
						Operation = operation,
						IsolationLevel = isolationLevel,
						Connection = connection,
						Timestamp = Stopwatch.GetTimestamp()
					});

				return operationId;
			}
			else
				return Guid.Empty;
		}

		public static void WriteTransactionCommitAfter(this DiagnosticListener @this, Guid operationId,
			IsolationLevel isolationLevel, MySqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlAfterCommitTransaction))
			{
				@this.Write(
					MySqlAfterCommitTransaction,
					new
					{
						OperationId = operationId,
						Operation = operation,
						IsolationLevel = isolationLevel,
						Connection = connection,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}

		public static void WriteTransactionCommitError(this DiagnosticListener @this, Guid operationId,
			IsolationLevel isolationLevel, MySqlConnection connection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlErrorCommitTransaction))
			{
				@this.Write(
					MySqlErrorCommitTransaction,
					new
					{
						OperationId = operationId,
						Operation = operation,
						IsolationLevel = isolationLevel,
						Connection = connection,
						Exception = ex,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}

		public static Guid WriteTransactionRollbackBefore(this DiagnosticListener @this,
			IsolationLevel isolationLevel, MySqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlBeforeRollbackTransaction))
			{
				Guid operationId = Guid.NewGuid();

				@this.Write(
					MySqlBeforeRollbackTransaction,
					new
					{
						OperationId = operationId,
						Operation = operation,
						IsolationLevel = isolationLevel,
						Connection = connection,
						Timestamp = Stopwatch.GetTimestamp()
					});

				return operationId;
			}
			else
				return Guid.Empty;
		}

		public static void WriteTransactionRollbackAfter(this DiagnosticListener @this, Guid operationId,
			IsolationLevel isolationLevel, MySqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlAfterRollbackTransaction))
			{
				@this.Write(
					MySqlAfterRollbackTransaction,
					new
					{
						OperationId = operationId,
						Operation = operation,
						IsolationLevel = isolationLevel,
						Connection = connection,						
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}

		public static void WriteTransactionRollbackError(this DiagnosticListener @this, Guid operationId,
			IsolationLevel isolationLevel, MySqlConnection connection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled(MySqlErrorRollbackTransaction))
			{
				@this.Write(
					MySqlErrorRollbackTransaction,
					new
					{
						OperationId = operationId,
						Operation = operation,
						IsolationLevel = isolationLevel,
						Connection = connection,						
						Exception = ex,
						Timestamp = Stopwatch.GetTimestamp()
					});
			}
		}
		#endregion
	}
}
