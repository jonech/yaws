using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarframeStatService.Constant;
using WarframeStatService.Entity;
using Xunit;
using Zaw;

namespace yaws.Test.Zaw
{
    public class WorldStateProcessTest
    {
        private readonly WorldStateProcess worldStateProcess;
        public WorldStateProcessTest()
        {
            worldStateProcess = new WorldStateProcess("pc");
        }

        [Fact]
        public void NoExistingCetusCycleNotificationStates()
        {
            var cetusCycle = new CetusCycle { Id = "1" };
            worldStateProcess.ProcessCetusCycle(null, cetusCycle);

            // new states will be added
            var expected = new NotificationState { WfStatId = "1", WfStatType = nameof(WFStatType.CetusCycle) };
            var actual = worldStateProcess.NewNotificationStates[0];
            Assert.Equal(expected.WfStatId, actual.WfStatId);
            Assert.Equal(expected.WfStatType, actual.WfStatType);

            // nothing to delete
            Assert.Empty(worldStateProcess.ExpiredNotificationStates);

            // no FCM
            Assert.Empty(worldStateProcess.NotificationMessages);
        }

        [Fact]
        public void ExistingCetusCycleNotificationStateHasExpiredAndNewCetusCycleDetecetd()
        {
            var expired = new NotificationState { WfStatId = "0" };
            var cetusCycle = new CetusCycle 
            { 
                Id = "1",
                State = "test",
                ShortString = "test",
                Expiry = DateTime.UtcNow + TimeSpan.FromMinutes(1)
            };

            worldStateProcess.ProcessCetusCycle(expired, cetusCycle);

            // new states will be added
            var expected = new NotificationState { WfStatId = "1", WfStatType = nameof(WFStatType.CetusCycle) };
            var actual = worldStateProcess.NewNotificationStates[0];
            Assert.Equal(expected.WfStatId, actual.WfStatId);
            Assert.Equal(expected.WfStatType, actual.WfStatType);

            // old states will be deleted
            Assert.Equal("0", worldStateProcess.ExpiredNotificationStates[0].WfStatId);

            // fcm will be added
            var actualFCM = worldStateProcess.NotificationMessages[0];
            Assert.Contains("pc-CetusCycle", actualFCM.Topic);
        }

        [Fact]
        public void ExistingCetusCycleNotificationStateStillActive()
        {
            var state = new NotificationState { WfStatId = "1" };
            var cetusCycle = new CetusCycle
            {
                Id = "1",
                State = "test",
                ShortString = "test",
                Expiry = DateTime.UtcNow + TimeSpan.FromMinutes(1)
            };

            worldStateProcess.ProcessCetusCycle(state, cetusCycle);

            // nothing to add
            Assert.Empty(worldStateProcess.NewNotificationStates);

            // nothing to delete
            Assert.Empty(worldStateProcess.ExpiredNotificationStates);

            // no fcm
            Assert.Empty(worldStateProcess.NotificationMessages);
        }

    }
}
