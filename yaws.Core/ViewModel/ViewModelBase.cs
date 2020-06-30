using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Timers;

namespace yaws.Core.ViewModel
{
    /// <summary>
    /// Enum to determine the type of Stat and
    /// which ViewHolder to use for StatRecyclerAdapter
    /// </summary>
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

    /// <summary>
    /// Base of all Stat ViewModel.
    /// </summary>
    public abstract class ViewModelBase
    {
        /// <summary>
        /// StatType.
        /// </summary>
        public abstract StatType StatType { get; }

        /// <summary>
        /// Name. WarframeStat's field.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Id. WarframeStat's field.
        /// </summary>
        public string Id { get; set; }
    }
}
