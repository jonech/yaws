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

        public static readonly Topic[] AvailableTopics =
        {
            Topic.Arbitration,
            Topic.EarthCycle,
            Topic.CetusCycle,
            Topic.VallisCycle,
            Topic.FissureLith,
            Topic.FissureMeso,
            Topic.FissureNeo,
            Topic.FissureAxi,
            Topic.FissureRequiem
        };

        public const string ChannelId = "yaws_notification_channel";
    }


}
