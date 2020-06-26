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
        FactionMission,
        VallisBounty,
        VallisCycle,
        Invasion,
        BountyJob
    }

    public abstract class ViewModelBase
    {
        public abstract StatType StatType { get; }
        public abstract string Name { get; }

        public string Id { get; set; }
    }
}
