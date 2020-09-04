using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Dapper;
using Serilog;

namespace Zaw
{
    public class DbService : IDisposable
    {
        private readonly string tableName;
        private readonly string connectionString;
        private readonly ILogger logger;
        private readonly SQLiteConnection connection;

        public DbService(string connectionString, ILogger logger)
        {
            this.connectionString = connectionString;
            this.tableName = $"{nameof(NotificationState)}s";
            this.logger = logger;

            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }


        public void CreateNotificationStateTable()
        {
            var query = @$"CREATE TABLE IF NOT EXISTS {tableName}(
                            {nameof(NotificationState.Id)} INTEGER PRIMARY KEY, 
                            {nameof(NotificationState.WfStatId)} TEXT NOT NULL,
                            {nameof(NotificationState.WfStatType)} TEXT NOT NULL
                          );";

            connection.Execute(query);
        }

        public IEnumerable<NotificationState> FetchAllNotificationStates()
        {
            var query = @$"SELECT * FROM {tableName};";
            var res = connection.Query<NotificationState>(query);

            return res;
        }

        public int InsertNotificationState(params NotificationState[] notificationStates)
        {
            var query = @$"INSERT INTO {tableName} 
                           ({nameof(NotificationState.WfStatId)}, {nameof(NotificationState.WfStatType)})
                           VALUES (@{nameof(NotificationState.WfStatId)}, @{nameof(NotificationState.WfStatType)});";

            var res = connection.Execute(query, notificationStates);
            if (logger != null)
                logger.Debug($"Inserted {res} rows");

            return res;
        }

        public int DeleteNotificationState(params NotificationState[] notificationStates)
        {
            var query = @$"DELETE FROM {tableName} WHERE {nameof(NotificationState.Id)} = @{nameof(NotificationState.Id)};";

            var res = connection.Execute(query, notificationStates);
            if (logger != null)
                logger.Debug($"Deleted {res} rows");

            return res;
        }

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }
    }
}
