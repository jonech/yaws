using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WarframeStatService.Constant;
using Xunit;
using Zaw;

namespace yaws.Test.Zaw
{
    public class DbServiceTest : IDisposable
    {
        private readonly DbService dbService;

        public DbServiceTest()
        {
            dbService = new DbService("Data Source=:memory:", null);
        }

        public void Dispose()
        {

        }

        [Fact]
        public void DbServiceCanCRDTest()
        {
            dbService.CreateNotificationStateTable();

            dbService.InsertNotificationState(new NotificationState { WfStatId = "111", WfStatType = nameof(WFStatType.VallisCycle)});
            var states = dbService.FetchAllNotificationStates();
            Assert.NotEmpty(states);

            var state0 = states.ToList()[0];
            Assert.Equal("111", state0.WfStatId);

            dbService.DeleteNotificationState(state0);
            var empty = dbService.FetchAllNotificationStates();
            Assert.Empty(empty);
        }
    }
}
