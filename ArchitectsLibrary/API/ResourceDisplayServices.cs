using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// Use this to allow/disallow items from your mod to be placed in Architect Library's display items (currently just relic tanks and pedestals).
    /// </summary>
    public static class ResourceDisplayServices
    {
        internal static readonly List<TechType> whitelistedTechTypes = new List<TechType>();
        internal static readonly List<TechType> blacklistedTechTypes = new List<TechType>();
        internal static readonly Dictionary<TechType, float> overrideItemScaleInDisplayCase = new Dictionary<TechType, float>();
        internal static readonly Dictionary<TechType, float> overrideItemScaleInPedestal = new Dictionary<TechType, float>();

        /// <summary>
        /// Allow <paramref name="techType"/> to be added into relic tanks and onto pedestals.
        /// </summary>
        /// <param name="techType"></param>
        public static void WhitelistTechtype(TechType techType)
        {
            whitelistedTechTypes.Add(techType);
            if (blacklistedTechTypes.Contains(techType))
            {
                blacklistedTechTypes.Remove(techType);
            }
        }

        /// <summary>
        /// Disallow <paramref name="techType"/> from being added into relic tanks and onto pedestals. This would rarely be used, given most troublesome items are already whitelisted, but is here just in case.
        /// </summary>
        /// <param name="techType"></param>
        public static void BlackListTechType(TechType techType)
        {
            blacklistedTechTypes.Add(techType);
            if (whitelistedTechTypes.Contains(techType))
            {
                whitelistedTechTypes.Remove(techType);
            }
        }

        /// <summary>
        /// Set the scale of <paramref name="techType"/> when put in a display case. Default scale is 1.25.
        /// </summary>
        /// <param name="techType"></param>
        /// <param name="newScale"></param>
        public static void SetScaleInRelicTank(TechType techType, float newScale)
        {
            overrideItemScaleInDisplayCase[techType] = newScale;
        }

        public static void SetScaleInPedestal(TechType techType, float newScale)
        {
            overrideItemScaleInPedestal[techType] = newScale;
        }
    }
}
