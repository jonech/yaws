using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json;
using Serilog;
using WarframeStatService.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Zaw
{
    class Program
    {
        public static Config Config { get; set; }

        static Config GetConfigSettings(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} was not found or is not accesible");
            }

            var appSettingsContent = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(appSettingsContent))
                throw new Exception("File is empty");

            return JsonConvert.DeserializeObject<Config>(appSettingsContent);
        }

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var start = DateTime.Now;

            if (args.Length <= 0)
            {
                throw new ArgumentException("Config path is required.");
            }
            Config = GetConfigSettings(args[0]);

            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.Console()
                                .WriteTo.File(Config.LogPath, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                                .CreateLogger();

            Log.Information("Process starts");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile(Config.FirebaseServiceAccountPath),
            });

            var api = new WarframeStatAPIFactory().Create();
            var worldState = await api.FetchWorldState(Config.WFPlatform);
            if (worldState == null)
            {
                Log.Warning("No WorldState detected. Exiting Process.");
                Environment.Exit(0);
            }

            var dbService = new DbService(Config.DbConnection, Log.Logger);
            dbService.CreateNotificationStateTable();
            var notificationStates = dbService.FetchAllNotificationStates();
            if (notificationStates == null)
            {
                Log.Error("Failed to retrieve NotificationStates. Exiting Process.");
                Environment.Exit(0);
            }


            var process = new WorldStateProcess(Log.Logger);
            process.RunProcess(notificationStates.ToList(), worldState);

            if (process.NewNotificationStates.Any())
                dbService.InsertNotificationState(process.NewNotificationStates.ToArray());

            if (process.ExpiredNotificationStates.Any())
                dbService.InsertNotificationState(process.ExpiredNotificationStates.ToArray());

            // send FCM
            if (process.NotificationMessages.Any())
            {
                Log.Information($"Sending {process.NotificationMessages.Count} FCM messages");
                var responses = await FirebaseMessaging.DefaultInstance.SendAllAsync(process.NotificationMessages);
                Log.Information($"FCM Send Completed - [{responses.SuccessCount}], Send Failed - [{responses.FailureCount}]");
            }

            var end = DateTime.Now - start;
            Log.Information($"Process complete. Total execution time: {end.TotalSeconds}s");

            Console.ReadLine();
        }

    }
}
