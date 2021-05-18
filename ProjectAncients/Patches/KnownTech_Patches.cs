using ECCLibrary;
using ECCLibrary.Internal;
using HarmonyLib;
using ProjectAncients.Prefabs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(KnownTech))]
    public static class KnownTech_Patches
    {
        private static bool _initialized = false;

        private static FMODAsset _unlockSound;

        [HarmonyPatch(nameof(KnownTech.Deinitialize))]
        [HarmonyPostfix]
        static void DeInitialize_Postfix()
        {
            _initialized = false;
        }

        [HarmonyPatch(nameof(KnownTech.Initialize))]
        [HarmonyPostfix]
        static void Initialize_Postfix()
        {
            if (_initialized)
                return;

            _initialized = true;

            foreach (var i in KnownTech.analysisTech)
            {
                if (i.unlockSound != null && i.techType == TechType.BloodOil)
                    _unlockSound = i.unlockSound;

                if (i.techType == TechType.PrecursorKey_Purple)
                {
                    i.unlockTechTypes.Clear();
                }

                if(i.techType == Mod.architectElectricityMasterTech)
                {
                    i.unlockPopup = Mod.assetBundle.LoadAsset<Sprite>("IonicPulse_Popup");
                }
            }

            var analysisTech = KnownTech.analysisTech.ToHashSet();

            AddAnalysisTech(analysisTech, TechType.PrecursorKey_Purple);
            AddAnalysisTech(analysisTech, TechType.PrecursorKey_Red);
            AddAnalysisTech(analysisTech, TechType.PrecursorKey_White);

            KnownTech.analysisTech = analysisTech.ToList();
        }

        static void AddAnalysisTech(HashSet<KnownTech.AnalysisTech> analysisTech, TechType techTypeToAnalyze)
        {
            analysisTech.Add(new KnownTech.AnalysisTech()
            {
                techType = techTypeToAnalyze,
                unlockMessage = "NotificationBlueprintUnlocked",
                unlockPopup = null,
                unlockSound = _unlockSound,
                unlockTechTypes = new List<TechType>() { techTypeToAnalyze },
            });
        }
    }
}
