using RotA.Mono.Creatures.GargEssentials;
using Story;
using UnityEngine;

namespace RotA.Mono
{
    /// <summary>
    /// This goes on the Gargantuan to play a PDA line automatically when in range.
    /// </summary>
    public class GargantuanEncounterPDA : MonoBehaviour
    {
        StoryGoal goal = new StoryGoal("GargantuanEncounter", Story.GoalType.Story, 0f);
        public float maxDistance = 300f;

        void Start()
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
            if (Vector3.Distance(transform.position, Player.main.transform.position) < maxDistance)
            {
                if (!GargantuanConditions.PlayerInPrecursorBase())
                {
                    if (StoryGoalManager.main.OnGoalComplete(goal.key))
                    {
                        CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("PDAGargEncounter"), "PDAGargEncounter");
                        Destroy(this);
                    }
                }
            }
        }
    }
}
