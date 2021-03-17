using ECCLibrary;
using ECCLibrary.Internal;
using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch]
    public static class KnownTech_Patches
    {
        public static PDAData pdaData;
        [HarmonyPatch(typeof(KnownTech), nameof(KnownTech.Initialize))]
        [HarmonyPrefix]
        public static void KnownTech_Initialize_Patch(PDAData data)
        {
            pdaData = data;
            ReplaceAnalysisTech(TechType.PrecursorKey_Purple, new List<TechType>() { TechType.PrecursorKey_Purple });
            ReplaceAnalysisTech(TechType.PrecursorKey_White, new List<TechType>() { TechType.PrecursorKey_White });
            ReplaceAnalysisTech(TechType.PrecursorKey_Red, new List<TechType>() { TechType.PrecursorKey_Red });
        }

        static void ReplaceAnalysisTech(TechType techType, List<TechType> unlockTechTypes)
        {
            GetAnalysisTech(techType).unlockTechTypes = unlockTechTypes;
        }

        static void RemoveAnalysisTech(TechType techType, List<TechType> techTypesToRemove)
        {
            ECCLog.AddMessage("Removing analysis tech");
            List<TechType> allAnalysisTech = GetAnalysisTech(techType).unlockTechTypes;
            ECCLog.AddMessage("Got analysis tech");
            foreach (TechType tech in techTypesToRemove)
            {
                ECCLog.AddMessage("Attempting removal of {0}", tech.ToString());
                if (allAnalysisTech.Contains(tech))
                {
                    ECCLog.AddMessage("Removing {0}", tech.ToString());
                    allAnalysisTech.Remove(tech);
                }
            }
        }

        static void RemoveUnlockTechTypes(TechType techType, List<TechType> unlockTechTypes)
        {
            GetAnalysisTech(techType).unlockTechTypes = unlockTechTypes;
        }

        static KnownTech.AnalysisTech GetAnalysisTech(TechType techType)
        {
            foreach (KnownTech.AnalysisTech tech in pdaData.analysisTech)
            {
                if (tech.techType == techType)
                {
                    ECCLog.AddMessage("Found AnalysisTech for TT {0}", techType);
                    return tech;
                }
            }
            ECCLog.AddMessage("Creating new AnalysisTech for TT {0}", techType);
            return new KnownTech.AnalysisTech()
            {
                techType = techType,
                unlockMessage = "NotificationBlueprintUnlocked",
                unlockTechTypes = new List<TechType>()
            };
        }
    }
}
