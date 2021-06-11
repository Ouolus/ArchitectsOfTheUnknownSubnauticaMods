using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class DataTerminalAnalyzeTech : MonoBehaviour
    {
        public TechType techToUnlock;

        public void OnStoryHandTarget()
        {
            KnownTech.Analyze(techToUnlock, true);
        }
    }
}
