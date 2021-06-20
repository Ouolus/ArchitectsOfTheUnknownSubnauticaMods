using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace ArchitectsLibrary.Config
{
    class AchievementData : ConfigFile
    {
        public AchievementData() : base("achievements")
        {
        }

        public Dictionary<string, int> achievements = new Dictionary<string, int>();
    }
}
