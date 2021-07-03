using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;
using Story;
using UnityEngine;
using UWE;

namespace RotA.Mono
{
    public class MiscPDALines : MonoBehaviour
    {
        StoryGoal colorfulIonCubeStoryGoal = new StoryGoal("ColorfulIonCubePickupGoal", Story.GoalType.PDA, 0f);

        private void OnCompleteGoal(Goal goal)
        {
            switch (goal.customGoalName)
            {
                default:
                    return;
                case "Pickup_WarpCannon":
                    CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("WarpCannonPickup"), "WarpCannonPickupVoiceline", "The Handheld Warping Device enables both short and long range teleportation for the user. Be cautious of the various side effects of teleportation, including sudden death and failure of proper materialization.");
                    AchievementServices.CompleteAchievement("CraftWarpCannon");
                    return;
                case "Pickup_Electricube":
                    OnIonCubePickedUp();
                    return;
                case "Pickup_RedIonCube":
                    OnIonCubePickedUp();
                    return;
                case "Pickup_GargantuanEggUndiscovered":
                    CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("GargEggPickup"), "GargantuanEggPickupVoiceline", "This egg is unusually large, and likely contains a leviathan class lifeform within. Take caution in handling it.");
                    return;

            }
        }

        private void OnIonCubePickedUp()
        {
            if (StoryGoalManager.main.OnGoalComplete(colorfulIonCubeStoryGoal.key))
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("IonCubePickup"), "ColorIonCubePickupVoiceline", "The various types of energy cubes are essential in most alien technology, but require large quantities of power to create. Despite their colorful appearance, please understand they are not edible.");
            }
        }

        private void Start()
        {
            var goalManager = GoalManager.main;
            goalManager.onCompleteGoalEvent.AddHandler(gameObject, new Event<Goal>.HandleFunction(OnCompleteGoal));
            AddPickupGoal(Mod.warpCannon.TechType);
            AddPickupGoal("GargantuanEggUndiscovered");
            AddPickupGoal(AUHandler.ElectricubeTechType);
            AddPickupGoal(AUHandler.RedIonCubeTechType);
        }

        private void AddPickupGoal(TechType itemTechType)
        {
            GoalManager.main.goals.Add(new Goal() { customGoalName = $"Pickup_{itemTechType.AsString()}", displayed = false, itemType = itemTechType, goalType = GoalType.Custom });
        }

        private void AddPickupGoal(string techTypeName)
        {
            GoalManager.main.goals.Add(new Goal() { customGoalName = $"Pickup_{techTypeName}", displayed = false, goalType = GoalType.Custom });
        }
    }
}
