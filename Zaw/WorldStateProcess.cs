﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FirebaseAdmin.Messaging;
using WarframeStatService.Entity;
using yaws.Common;
using yaws.Common.Extension;
using Serilog;
using WarframeStatService.Entity.Base;
using WarframeStatService.Constant;

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
            var cetusCycleState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(WFStatType.CetusCycle));
            ProcessCetusCycle(cetusCycleState, worldState.CetusCycle);

            var vallisCycleState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(WFStatType.VallisCycle));
            ProcessVallisCycle(vallisCycleState, worldState.VallisCycle);

            var earthCycleState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(WFStatType.EarthCycle));
            ProcessEarthCycle(earthCycleState, worldState.EarthCycle);

            var arbitrationState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(WFStatType.Arbitration));
            ProcessArbitration(arbitrationState, worldState.Arbitration);

            //var sentientOutpostState = notificationStates.FirstOrDefault(p => p.WfStatType == nameof(WFStatType.SentientOutpost));
            //ProcessSentientOutpost(sentientOutpostState, worldState.SentientOutpost);

            var fissureStates = notificationStates.Where(p => p.WfStatType == nameof(WFStatType.Fissure)).ToList();
            ProcessFissures(fissureStates, worldState.Fissures);
        }


        private bool ProcessSingleExpirable(NotificationState state, ExpirableStat expirableStat, WFStatType statType)
        {
            if (expirableStat == null)
            {
                logger.Warning($"Empty {statType}");
                return false;
            }

            if (state == null)
            {
                logger.Information($"Adding first {statType}");
                NewNotificationStates.Add(new NotificationState { WfStatId = expirableStat.Id, WfStatType = statType.ToString() });
                return false;
            }

            if (state.WfStatId != expirableStat.Id && !expirableStat.IsExpired)
            {
                logger.Information($"New {statType} with Id {expirableStat.Id}");
                NewNotificationStates.Add(new NotificationState { WfStatId = expirableStat.Id, WfStatType = statType.ToString() });
                ExpiredNotificationStates.Add(state);
                return true;
            }

            logger.Debug($"No new {statType}");
            return false;
        }

        private void ProcessCetusCycle(NotificationState state, CetusCycle cetus)
        {
            var sendFCM = ProcessSingleExpirable(state, cetus, cetus.StatType);
            if (sendFCM)
            {
                var ttl = cetus.TimeLeft;
                var message = FCM.CreateMessage($"{cetus.State.ToUpperFirst()} in Cetus", cetus.ShortString, YawsNotification.Topic.CetusCycle, nameof(CetusCycle), ttl);
                NotificationMessages.Add(message);
            }
        }

        private void ProcessVallisCycle(NotificationState state, VallisCycle vallis)
        {
            var sendFCM = ProcessSingleExpirable(state, vallis, vallis.StatType);
            if (sendFCM)
            {
                var ttl = vallis.TimeLeft;
                var message = FCM.CreateMessage($"Orb Vallis {vallis.State.ToUpperFirst()}", vallis.ShortString, YawsNotification.Topic.VallisCycle, nameof(VallisCycle), ttl);
                NotificationMessages.Add(message);
            }
        }

        private void ProcessEarthCycle(NotificationState state, EarthCycle earth)
        {
            var sendFCM = ProcessSingleExpirable(state, earth, earth.StatType);
            if (sendFCM)
            {
                var ttl = earth.TimeLeft;
                var message = FCM.CreateMessage($"{earth.State.ToUpperFirst()} on Earth", $"{earth.TimeLeft.ToFormattedString()} left", YawsNotification.Topic.EarthCycle, nameof(EarthCycle), ttl);
                NotificationMessages.Add(message);
            }
        }


        private void ProcessArbitration(NotificationState state, Arbitration arbi)
        {
            var sendFCM = ProcessSingleExpirable(state, arbi, arbi.StatType);
            if (sendFCM)
            {
                var ttl = arbi.TimeLeft;
                var message = FCM.CreateMessage($"Arbitration: {arbi.Enemy} {arbi.Type}", $"Expired in {ttl.ToFormattedString()}", YawsNotification.Topic.Arbitration, nameof(Arbitration), ttl);
                NotificationMessages.Add(message);
            }
        }

        private void ProcessSentientOutpost(NotificationState state, SentientOutpost so)
        {
            var sendFCM = ProcessSingleExpirable(state, so, so.StatType);
            if (sendFCM && so.Expiry != null && so.Mission != null)
            {
                var ttl = so.TimeLeft;
                var message = FCM.CreateMessage("Sentient Anomaly", $"{so.Mission.Node}", YawsNotification.Topic.SentientOutpost, nameof(SentientOutpost), ttl);
                NotificationMessages.Add(message);
            }
        }


        private void ProcessFissures(List<NotificationState> states, List<Fissure> fissures)
        {
            if (fissures == null)
            {
                logger.Warning($"Empty Fissures");
                return;
            }

            if (states == null || !states.Any())
            {
                logger.Information($"Adding First Fissures");
                var newFissureStates = fissures.Select(f => new NotificationState { WfStatId = f.Id, WfStatType = nameof(WFStatType.Fissure) });
                NewNotificationStates.AddRange(newFissureStates);
                return;
            }


            var expiredFissures = states.Where(s => !fissures.Any(f => f.Id == s.WfStatId));
            var newFissures = fissures.Where(f => !states.Any(s => s.WfStatId == f.Id));

            if (expiredFissures.Any())
            {
                logger.Information($"Expired Fissures will be removed");
                ExpiredNotificationStates.AddRange(expiredFissures);
            }

            foreach (var fissure in newFissures)
            {
                logger.Information($"New Fissures {fissure.Tier} {fissure.MissionType}");
                NewNotificationStates.Add(new NotificationState
                {
                    WfStatId = fissure.Id,
                    WfStatType = nameof(WFStatType.Fissure)
                });

                var ttl = fissure.TimeLeft;
                var topic = YawsNotification.Topic.Info;
                if (fissure.TierNum == 1)
                    topic = YawsNotification.Topic.FissureLith;
                else if (fissure.TierNum == 2)
                    topic = YawsNotification.Topic.FissureMeso;
                else if (fissure.TierNum == 3)
                    topic = YawsNotification.Topic.FissureNeo;
                else if (fissure.TierNum == 4)
                    topic = YawsNotification.Topic.FissureAxi;
                else if (fissure.TierNum == 5)
                    topic = YawsNotification.Topic.FissureRequiem;

                var message = FCM.CreateMessage($"{fissure.Tier} {fissure.MissionType} on {fissure.Node}", $"Expired in {fissure.TimeLeft.ToFormattedString()}", topic, nameof(Fissure), ttl);
                NotificationMessages.Add(message);
            }
        }
    }
}
