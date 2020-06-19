using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace Core.ViewModel
{
    public enum StatType
    {
        Arbitration,
        CetusBounty,
        CetusCycle,
        EarthCycle,
        Fissure,
        Nightwave,
        SentientOutpost,
        Sortie,
        SyndicateMission,
        VallisBounty,
        VallisCycle
    }

    public abstract class ViewModelBase
    {
        public abstract StatType StatType { get; }

        public string Id { get; set; }
    }
}
