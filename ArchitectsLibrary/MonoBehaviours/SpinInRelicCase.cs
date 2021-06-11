using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class SpinInRelicCase : MonoBehaviour
    {
        void Update()
        {
            transform.localEulerAngles += Vector3.up * (45f * Time.deltaTime);
        }
    }
}
