using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchitectsLibrary.MonoBehaviours;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// Use this to allow/disallow items from your mod to be placed in Architect Library's display buildables (currently just relic tanks and pedestals).
    /// </summary>
    public static class DisplayCaseServices
    {
        internal static readonly List<TechType> whitelistedTechTypes = new List<TechType>();
        internal static readonly List<TechType> blacklistedTechTypes = new List<TechType>();
        internal static readonly Dictionary<TechType, float> overrideItemScaleInRelicTank = new Dictionary<TechType, float>();
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
            overrideItemScaleInRelicTank[techType] = newScale;
        }

        /// <summary>
        /// Set the scale of <paramref name="techType"/> when put on top of an item pedestal. Default scale is 1.
        /// </summary>
        /// <param name="techType"></param>
        /// <param name="newScale"></param>
        public static void SetScaleInPedestal(TechType techType, float newScale)
        {
            overrideItemScaleInPedestal[techType] = newScale;
        }

        internal static float GetScaleForItem(TechType techType, DisplayCaseType displayCaseType)
        {
            switch (displayCaseType)
            {
                default:
                    return 1f;
                case DisplayCaseType.RelicTank:
                    return overrideItemScaleInRelicTank.GetOrDefault(techType, 1.25f);
                case DisplayCaseType.Pedestal:
                    return overrideItemScaleInPedestal.GetOrDefault(techType, 1f);
            }
        }
    }
}
