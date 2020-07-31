using System;
using System.Collections.Generic;
using System.Text;

namespace Zaw
{
    public class Config
    {
        public string DbConnection { get; set; }
        public string LogPath { get; set; }
        public string WFPlatform { get; set; }

        public string FirebaseServiceAccountPath { get; set; }
    }
}
