using HarmonyLib;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace RotA.Patches
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
            }

            UWE.CoroutineHost.StartCoroutine(RemovePurpleTabletAnalysisDelayed());

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

        static IEnumerator RemovePurpleTabletAnalysisDelayed()
        {
            //wait 3 frames just in case
            yield return null;
            yield return null;
            yield return null;
            foreach (var i in KnownTech.analysisTech)
            {
                if (i.techType == TechType.PrecursorKey_Purple)
                {
                    i.unlockTechTypes = new List<TechType>() { TechType.PrecursorKey_Purple };
                }
            }
        }
    }
}
