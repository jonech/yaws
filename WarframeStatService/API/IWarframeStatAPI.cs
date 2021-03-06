﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using WarframeStatService.Entity;

namespace WarframeStatService.API
{
    public interface IWarframeStatAPI
    {
        [Get("/{platform}")]
        Task<WorldState> FetchWorldState(
            [AliasAs("platform")] string platform
        );
    }
}
