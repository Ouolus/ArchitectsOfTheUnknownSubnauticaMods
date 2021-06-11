using RotA.Mono.Creatures.GargEssentials;
using UnityEngine;
using Story;

namespace RotA.Mono
{
    /// <summary>
    /// This goes on the Gargantuan to play a PDA line automatically when in range.
    /// </summary>
    public class GargantuanEncounterPDA : MonoBehaviour
    {
        private StoryGoal goal = new StoryGoal("GargantuanEncounter", Story.GoalType.Story, 0f);
        public float maxDistance = 125f;

        private void Start()
        {
            if (StoryGoalManager.main.IsGoalComplete(goal.key))
            {
                Destroy(this);
            }
            else
            {
                InvokeRepeating("CheckDistance", Random.value, 0.5f);
            }
        }

        void CheckDistance()
        {
            if(Vector3.Distance(transform.position, Player.main.transform.position) < maxDistance)
            {
                if (!GargantuanBehaviour.PlayerInPrecursorBase())
                {
                    if (StoryGoalManager.main.OnGoalComplete(goal.key))
                    {
                        CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDAGargEncounter"), "PDAGargEncounter", "Warning: passive bio scan is limited to 500 meters in any direction. Please manually scan the object for a more comprehensive measurement");
                        Destroy(this);
                    }
                }
            }
        }
    }
}
