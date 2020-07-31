using System;
using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity.Base
{
    public abstract class ActivatableStat : IActivatable, IStat
    {
        [JsonProperty("id")]
        public virtual string Id { get; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonIgnore]
        public TimeSpan TimeElapsed { get => (Activation != null) ? DateTime.UtcNow - Activation : TimeSpan.Zero; }

        [JsonIgnore]
        public abstract WFStatType StatType { get; }
    }
}
