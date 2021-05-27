using UnityEngine;

namespace RotA.Mono
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
