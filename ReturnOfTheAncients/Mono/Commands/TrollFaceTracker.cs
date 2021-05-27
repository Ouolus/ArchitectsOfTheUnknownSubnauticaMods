using UnityEngine;

namespace RotA.Mono.Commands
{
    public class TrollFaceTracker : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(MainCameraControl.main.transform);
        }
    }
}
