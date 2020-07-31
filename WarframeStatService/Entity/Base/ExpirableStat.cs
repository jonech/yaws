using System;
using Newtonsoft.Json;
using WarframeStatService.Constant;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity.Base
{
    public abstract class ExpirableStat : IActivatable, IExpirable, IStat
    {
        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonProperty("id")]
        public virtual string Id { get; set; }


        [JsonIgnore]
        public TimeSpan TimeLeft { get => (Expiry != null) ? Expiry - DateTime.UtcNow : TimeSpan.Zero; }

        [JsonIgnore]
        public bool IsExpired { get => TimeLeft <= TimeSpan.Zero; }

        [JsonIgnore]
        public TimeSpan TimeElapsed { get => (Activation != null) ? DateTime.UtcNow - Activation : TimeSpan.Zero; }

        [JsonIgnore]
        public abstract WFStatType StatType { get; }
    }
}
