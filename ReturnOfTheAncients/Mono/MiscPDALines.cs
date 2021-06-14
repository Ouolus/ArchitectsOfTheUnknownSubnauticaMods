using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Story;

namespace RotA.Mono
{
    public class MiscPDALines : MonoBehaviour, IStoryGoalListener
    {
        StoryGoal colorfulIonCubeStoryGoal = new StoryGoal("ColorfulIonCubePickupGoal", Story.GoalType.PDA, 0f);

        public void NotifyGoalComplete(string key)
        {
            switch (key)
            {
                default:
                    return;
                case "Pickup_WarpCannon":
                    CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("WarpCannonPickup"), "WarpCannonPickupVoiceline", "The Handheld Warping Device enables both short and long range teleportation for the user. Be cautious of the various side effects of teleportation, including sudden death or failure of proper materialization.");
                    return;
                case "Pickup_Electricube":
                    OnIonCubePickedUp();
                    return;
                case "Pickup_RedIonCube":
                    OnIonCubePickedUp();
                    return;
                case "Pickup_GargantuanEgg":
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
    }
}
