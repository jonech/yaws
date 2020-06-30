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

        public WorldStateDataSource(IWareframeStatAPI api)
        {
            this.api = api;
        }

        public async Task<WorldState> FetchWorldState(string platform)
        {
            return await api.FetchWorldState(platform);
        }
    }
}
