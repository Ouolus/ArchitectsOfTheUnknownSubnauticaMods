using HarmonyLib;
using Story;
using UnityEngine;

namespace ProjectAncients.Patches
{
    [HarmonyPatch(typeof(PDAScanner_Patches))]
    public class PDAScanner_Patches
    {
        static StoryGoal scanAdultGargGoal = new StoryGoal("ScanAdultGargantuan", Story.GoalType.Story, 0f);

        [HarmonyPatch(nameof(PDAScanner.Unlock))]
        [HarmonyPostfix]
        public static void Unlock_Postfix(PDAScanner.EntryData entryData)
        {
            if (entryData is not null && entryData.key == Mod.gargVoidPrefab.TechType)
            {
                if (StoryGoalManager.main.OnGoalComplete(scanAdultGargGoal.key))
                {
                    CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDAGargScan"), "PDAScanAdultGargantuan", "Are you certain whatever you're doing is worth it?");
                }
            }
        }
    }
}
