using System.Collections;
using UnityEngine;
using UWE;

namespace RotA.Mono.AlienTech
{
    public class DataTerminalUnlockTech : MonoBehaviour
    {
        public TechType[] techsToUnlock;

        public void OnStoryHandTarget()
        {
            CoroutineHost.StartCoroutine(DelayedUnlock());
        }

        IEnumerator DelayedUnlock()
        {
            for (int i = 0; i < techsToUnlock.Length; i++)
            {
                KnownTech.Add(techsToUnlock[i]);
                yield return new WaitForSeconds(7f);
            }
        }
    }
}
