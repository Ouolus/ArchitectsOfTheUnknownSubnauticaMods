using HarmonyLib;
using Story;
using UnityEngine;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(PDAScanner))]
    public class PDAScanner_Patches
    {
        public static StoryGoal scanAdultGargGoal = new StoryGoal("ScanAdultGargantuan", Story.GoalType.Story, 0f);

        [HarmonyPatch(nameof(PDAScanner.Unlock))]
        [HarmonyPostfix]
        public static void Unlock_Postfix(PDAScanner.EntryData entryData)
        {
            if (entryData is not null && entryData.key == Mod.gargVoidPrefab.TechType)
            {
                if (StoryGoalManager.main.OnGoalComplete(scanAdultGargGoal.key) && !uGUI.isLoading)
                {
                    CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDAGargScan"), "PDAScanAdultGargantuan", "The odds of surviving an encounter with an apex class leviathan are three thousand seven hundred twenty to one. Are you certain - \u259b\u2584\u2596\u2505\u2517\u2596\u2523\u2517\u250f\u259b\u2584\u2596\u259c\u250f\u2523 - you are ready to die?");
                }
            }
        }
    }
}
