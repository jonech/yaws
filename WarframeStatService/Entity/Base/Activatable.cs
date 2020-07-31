using Newtonsoft.Json;
using System;
using WarframeStatService.Entity.Interface;

namespace WarframeStatService.Entity.Base
{
    public abstract class Activatable : IActivatable, IStat
    {
        [JsonProperty("id")]
        public virtual string Id { get; }

        [JsonProperty("activation")]
        public DateTime Activation { get; set; }

        [JsonIgnore]
        public TimeSpan TimeElapsed { get => (Activation != null) ? DateTime.UtcNow - Activation : TimeSpan.Zero; }
    }
}
