using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace yaws.Common
{
    /// <summary>
    /// Constant for Notification Topic
    /// </summary>
    public class YawsNotification
    {
        public enum Topic
        {
            CetusCycle,
            VallisCycle,
            EarthCycle,
            CetusBounty,
            VallisBounty,
            SentientOutpost,
            Arbitration,
            Invasion,
            FissureLith,
            FissureMeso,
            FissureNeo,
            FissureAxi,
            FissureRequiem,

            Info,
            Error
        }

        //public static readonly string[] AvailableTopics =
        //{
        //    nameof(Topic.EarthCycle),
        //    nameof(Topic.Arbitration),
        //    nameof(Topic.SentientOutpost),
        //    nameof(Topic.CetusCycle),
        //    nameof(Topic.CetusBounty),
        //    nameof(Topic.VallisCycle),
        //    nameof(Topic.VallisBounty),
        //    nameof(Topic.Invasion),
        //    nameof(Topic.FissureLith),
        //    nameof(Topic.FissureMeso),
        //    nameof(Topic.FissureNeo),
        //    nameof(Topic.FissureAxi),
        //    nameof(Topic.FissureRequiem)
        //};

        public static readonly Topic[] AvailableTopics =
        {
            Topic.EarthCycle,
            Topic.Arbitration,
            Topic.SentientOutpost,
            Topic.CetusCycle,
            Topic.CetusBounty,
            Topic.VallisCycle,
            Topic.VallisBounty,
            Topic.Invasion,
            Topic.FissureLith,
            Topic.FissureMeso,
            Topic.FissureNeo,
            Topic.FissureAxi,
            Topic.FissureRequiem
        };
    }


}
