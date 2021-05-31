using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class WarperBuildableFix : MonoBehaviour
    {
        void Start()
        {
            var warpOut = GetComponentInChildren<WarpOut>();
            if (warpOut)
            {
                warpOut.evaluatePriority = 0f;
            }
            var inspectPlayer = GetComponentInChildren<WarperInspectPlayer>();
            if (inspectPlayer)
            {
                inspectPlayer.warpOutDistance = 0f;
            }
        }
    }
}
