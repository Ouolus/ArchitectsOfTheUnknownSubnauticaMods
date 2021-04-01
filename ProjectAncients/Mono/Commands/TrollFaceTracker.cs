using UnityEngine;

namespace ProjectAncients.Mono
{
    public class TrollFaceTracker : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(MainCameraControl.main.transform);
        }
    }
}
