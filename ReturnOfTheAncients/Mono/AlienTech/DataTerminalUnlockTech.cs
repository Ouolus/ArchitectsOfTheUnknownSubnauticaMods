using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class DataTerminalUnlockTech : MonoBehaviour
    {
        public TechType techToUnlock;

        public void OnStoryHandTarget()
        {
            KnownTech.Add(techToUnlock, true);
        }
    }
}
