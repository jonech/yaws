using System;

namespace WarframeStatService.Entity.Interface
{
    public interface IExpirable
    {
        DateTime Expiry { get; }
    }
}
