using UnityEngine;
using Story;

namespace RotA.Mono
{
    public class OmegaFabricatorRoot : MonoBehaviour
    {
        public OmegaTerminal terminal;

        public StoryGoal interactFailGoal = new StoryGoal("OmegaFabricatorInteractFail", Story.GoalType.Story, 0f);
        public StoryGoal interactSuccessGoal = new StoryGoal("OmegaFabricatorInteractSuccess", Story.GoalType.Story, 0f);

        public bool FormulaUnlocked()
        {
            if (StoryGoalManager.main.IsGoalComplete(Patches.PDAScanner_Patches.scanAdultGargGoal.key))
            {
                return true;
            }
            if (PDAEncyclopedia.ContainsEntry(Mod.gargVoidPrefab.ClassID))
            {
                return true;
            }
            return false;
        }

        public bool FabricatorEnabled()
        {
            return StoryGoalManager.main.IsGoalComplete(interactSuccessGoal.key);
        }

        public bool CanGenerateCube()
        {
            if (!FabricatorEnabled())
            {
                return false;
            }
            return true;
        }
    }
}
