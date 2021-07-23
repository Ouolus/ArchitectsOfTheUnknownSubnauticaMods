using UnityEngine;

namespace RotA.Mono
{
    public class TrollFaceTracker : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(MainCameraControl.main.transform);
        }
    }
}
