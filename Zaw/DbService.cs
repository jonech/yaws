using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Dapper;
using Serilog;

namespace Zaw
{
    public class DbService
    {
        private readonly string tableName;
        private readonly string connectionString;
        private readonly ILogger logger;

        public DbService(string connectionString, ILogger logger)
        {
            this.connectionString = connectionString;
            this.tableName = $"{nameof(NotificationState)}s";
            this.logger = logger;
        }

        public SQLiteConnection DefaultConnection => new SQLiteConnection(connectionString);

        public void CreateNotificationStateTable()
        {
            var query = @$"CREATE TABLE IF NOT EXISTS {tableName}(
                            {nameof(NotificationState.Id)} INTEGER PRIMARY KEY, 
                            {nameof(NotificationState.WfStatId)} TEXT NOT NULL,
                            {nameof(NotificationState.WfStatType)} TEXT NOT NULL
                          );";

            using var conn = DefaultConnection;
            conn.Open();
            conn.Execute(query);
            conn.Close();
        }

        public IEnumerable<NotificationState> FetchAllNotificationStates()
        {
            using var conn = DefaultConnection;
            conn.Open();

            var query = @$"SELECT * FROM {tableName};";
            var res = conn.Query<NotificationState>(query);
            conn.Close();

            return res;
        }

        public int InsertNotificationState(params NotificationState[] notificationStates)
        {
            using var conn = DefaultConnection;
            conn.Open();

            var query = @$"INSERT INTO {tableName} 
                           ({nameof(NotificationState.WfStatId)}, {nameof(NotificationState.WfStatType)})
                           VALUES (@{nameof(NotificationState.WfStatId)}, @{nameof(NotificationState.WfStatType)});";

            var res = conn.Execute(query, notificationStates);
            logger.Debug($"Inserted {res} rows");
            conn.Close();

            return res;
        }

        public int DeleteNotificationState(params NotificationState[] notificationStates)
        {
            using var conn = DefaultConnection;
            conn.Open();

            var query = @$"DELETE FROM {tableName} WHERE {nameof(NotificationState.Id)} = @{nameof(NotificationState.Id)};";

            var res = conn.Execute(query, notificationStates);
            logger.Debug($"Deleted {res} rows");
            conn.Close();

            return res;
        }
    }
}
