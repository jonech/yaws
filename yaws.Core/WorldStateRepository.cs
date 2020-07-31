using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

using yaws.Core.ViewModel;
using WarframeStatService.Entity;
using WarframeStatService.API;

namespace yaws.Core
{
    public class WorldStateRepository
    {
        readonly IWarframeStatAPI api;


        public WorldStateRepository(IWarframeStatAPI api)
        {
            this.api = api;
        }

        public async Task<WorldStateViewModel> GetWorldState(string platform)
        {
            var worldState = await api.FetchWorldState(platform);

            if (worldState != null)
                return new WorldStateViewModel(worldState);

            return null;
        }
    }
}
