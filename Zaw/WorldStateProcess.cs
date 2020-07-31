using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FirebaseAdmin.Messaging;
using WarframeStatService.Entity;
using yaws.Common;
using yaws.Common.Extension;
using Serilog;
using WarframeStatService.Entity.Base;

namespace Zaw
{
    public class WorldStateProcess
    {
        public List<NotificationState> NewNotificationStates { get; private set; }
        public List<NotificationState> ExpiredNotificationStates { get; private set; }
        public List<Message> NotificationMessages { get; private set; }

        private readonly ILogger logger;
        public WorldStateProcess(ILogger logger)
        {
            NewNotificationStates = new List<NotificationState>();
            ExpiredNotificationStates = new List<NotificationState>();
            NotificationMessages = new List<Message>();
            this.logger = logger;
        }

        public void RunProcess(List<NotificationState> notificationStates, WorldState worldState)
        {
            var cetusCycleState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(CetusCycle));
            ProcessCetusCycle(cetusCycleState, worldState.CetusCycle);

            var vallisCycleState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(VallisCycle));
            ProcessVallisCycle(vallisCycleState, worldState.VallisCycle);

            var earthCycleState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(EarthCycle));
            ProcessEarthCycle(earthCycleState, worldState.EarthCycle);

            var arbitrationState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(Arbitration));
            ProcessArbitration(arbitrationState, worldState.Arbitration);

            var sentientOutpostState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(SentientOutpost));
            ProcessSentientOutpost(sentientOutpostState, worldState.SentientOutpost);

        }


        private bool ProcessSingleExpirable(NotificationState state, ExpirableStat expirableStat, string statType)
        {
            if (expirableStat == null)
            {
                logger.Warning($"Empty {statType}");
                return false;
            }

            if (state == null)
            {
                logger.Information($"Adding first {statType}");
                NewNotificationStates.Add(new NotificationState { WfStatId = expirableStat.Id, WfStatType = statType });
                return false;
            }

            if (state.WfStatId != expirableStat.Id && !expirableStat.IsExpired)
            {
                logger.Information($"New {statType} with Id {expirableStat.Id}");
                NewNotificationStates.Add(new NotificationState { WfStatId = expirableStat.Id, WfStatType = statType });
                ExpiredNotificationStates.Add(state);
                return true;
            }

            logger.Debug($"No new {statType}");
            return false;
        }

        private void ProcessCetusCycle(NotificationState state, CetusCycle cetus)
        {
            var sendFCM = ProcessSingleExpirable(state, cetus, nameof(CetusCycle));
            if (sendFCM)
            {
                var ttl = cetus.Expiry - DateTime.UtcNow;
                var message = FCM.CreateMessage($"{cetus.State.ToUpperFirst()} in Cetus", cetus.ShortString, YawsNotification.Topic.CetusCycle, ttl);
                NotificationMessages.Add(message);
            }
        }

        private void ProcessVallisCycle(NotificationState state, VallisCycle vallis)
        {
            var sendFCM = ProcessSingleExpirable(state, vallis, nameof(VallisCycle));
            if (sendFCM)
            {
                var ttl = vallis.Expiry - DateTime.UtcNow;
                var message = FCM.CreateMessage($"Orb Vallis {vallis.State.ToUpperFirst()}", vallis.ShortString, YawsNotification.Topic.VallisCycle, ttl);
                NotificationMessages.Add(message);
            }
        }

        private void ProcessEarthCycle(NotificationState state, EarthCycle earth)
        {
            var sendFCM = ProcessSingleExpirable(state, earth, nameof(EarthCycle));
            if (sendFCM)
            {
                var ttl = earth.Expiry - DateTime.UtcNow;
                var message = FCM.CreateMessage($"{earth.State.ToUpperFirst()} on Earth", $"{earth.TimeLeft} left", YawsNotification.Topic.EarthCycle, ttl);
                NotificationMessages.Add(message);
            }
        }


        private void ProcessArbitration(NotificationState state, Arbitration arbi)
        {
            var sendFCM = ProcessSingleExpirable(state, arbi, nameof(Arbitration));
            if (sendFCM)
            {
                var ttl = arbi.Expiry - DateTime.UtcNow;
                var message = FCM.CreateMessage("Arbitration", $"{arbi.Enemy} - {arbi.Type}", YawsNotification.Topic.Arbitration, ttl);
                NotificationMessages.Add(message);
            }
        }

        private void ProcessSentientOutpost(NotificationState state, SentientOutpost so)
        {
            var sendFCM = ProcessSingleExpirable(state, so, nameof(SentientOutpost));
            if (sendFCM && so.Expiry != null && so.Mission != null)
            {
                var ttl = so.Expiry - DateTime.UtcNow;
                var message = FCM.CreateMessage("Sentient Anomaly", $"{so.Mission.Node}", YawsNotification.Topic.SentientOutpost, ttl);
                NotificationMessages.Add(message);
            }
        }


    }
}
