using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class DataTerminalAnalyzeTech : MonoBehaviour
    {
        public TechType techToUnlock;

        public float delay;

        public void OnStoryHandTarget()
        {
            Invoke(nameof(Analyze), delay);
        }

        void Analyze()
        {
            KnownTech.Analyze(techToUnlock);
        }
    }
}
