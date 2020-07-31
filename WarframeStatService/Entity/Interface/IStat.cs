using WarframeStatService.Constant;

namespace WarframeStatService.Entity.Interface
{
    public interface IStat
    {
        string Id { get; }

        WFStatType StatType { get; }
    }
}
