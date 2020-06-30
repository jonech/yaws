using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Core.Constant
{
    /// <summary>
    /// Syndicates in Warframe.
    /// </summary>
    /// <remarks>
    /// Refer to https://warframe.fandom.com/wiki/Syndicate.
    /// </remarks>
    public class Syndicate
    {
        /// <summary>
        /// Ostrons, people that enjoy early lunch.
        /// </summary>
        public const string Ostrons = "Ostrons";

        /// <summary>
        /// Solaris United, people of Fortuna.
        /// </summary>
        public const string SolarisUnited = "Solaris United";

        /// <summary>
        /// Arbiters of Hexis
        /// </summary>
        public const string ArbiterOfHexis = "Arbiters of Hexis";

        /// <summary>
        /// Assassins. What is this??
        /// </summary>
        public const string Assassins = "Assassins";

        /// <summary>
        /// Cephalon Suda
        /// </summary>
        public const string CephalonSuda = "Cephalon Suda";

        /// <summary>
        /// New Loka
        /// </summary>
        public const string NewLoka = "New Loka";

        /// <summary>
        /// Perrin Sequence
        /// </summary>
        public const string PerrinSequence = "Perrin Sequence";

        /// <summary>
        /// Red Veil, edgy space ninja.
        /// </summary>
        public const string RedVeil = "Red Veil";

        /// <summary>
        /// Steel Metidan, has an outpost at Earth called Iron Wake.
        /// </summary>
        public const string SteelMeridian = "Steel Meridian";

        /// <summary>
        /// The main 6 Faction Syndicates
        /// </summary>
        public static readonly string[] Factions = { ArbiterOfHexis, CephalonSuda, NewLoka, PerrinSequence, RedVeil, SteelMeridian };
    }
}
