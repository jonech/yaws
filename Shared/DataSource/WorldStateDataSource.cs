using Shared.API;
using Shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataSource
{
    public class WorldStateDataSource
    {
        private IWareframeStatAPI api;
        private string platform;

        public WorldStateDataSource(IWareframeStatAPI api)
        {
            this.api = api;
            this.platform = "pc";
        }

        public Task<WorldState> FetchWorldState()
        {
            return api.FetchWorldState(platform);
        }
    }
}
