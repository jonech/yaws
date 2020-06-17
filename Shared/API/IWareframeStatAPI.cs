using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using Shared.Entity;

namespace Shared.API
{
    public interface IWareframeStatAPI
    {
        [Get("/{platform}")]
        Task<WorldState> FetchWorldState(
            [AliasAs("platform")] string platform
        );
    }
}
