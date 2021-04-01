using UnityEngine;

namespace ProjectAncients.Mono.Commands
{
    public class TrollFaceTracker : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(MainCameraControl.main.transform);
        }
    }
}
