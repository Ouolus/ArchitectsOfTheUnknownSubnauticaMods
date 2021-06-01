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
            return StoryGoalManager.main.IsGoalComplete(Patches.PDAScanner_Patches.scanAdultGargGoal.key);
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
