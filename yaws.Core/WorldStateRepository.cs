using Service.DataSource;
using Service.Entity;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using yaws.Core.ViewModel;
using System.Reactive.Subjects;

namespace yaws.Core
{
    public class WorldStateRepository
    {
        readonly WorldStateDataSource dataSource;


        public WorldStateRepository(WorldStateDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<WorldStateViewModel> GetWorldState()
        {
            var worldState = await dataSource.FetchWorldState();

            if (worldState != null)
                return new WorldStateViewModel(worldState);

            return null;
        }
    }
}
