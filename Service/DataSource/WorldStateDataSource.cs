using Service.API;
using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataSource
{
    public class WorldStateDataSource
    {
        private IWareframeStatAPI api;
        private string platform;

        private WorldState cachedData;

        public WorldStateDataSource(IWareframeStatAPI api)
        {
            this.api = api;
            this.platform = "pc";
        }

        public async Task<WorldState> FetchWorldState()
        {
            return await api.FetchWorldState(platform);
        }
    }
}
