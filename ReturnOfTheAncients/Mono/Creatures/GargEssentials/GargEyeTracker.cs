using UnityEngine;

namespace RotA.Mono.Creatures.GargEssentials
{
    public class GargEyeTracker : MonoBehaviour
    {
        Quaternion defaultLocalRotation;
        Vector3 eyeOverrideScale = new Vector3(0.95f, 0.95f, 0.95f);

        void Start()
        {
            defaultLocalRotation = transform.localRotation;
        }

        void LateUpdate()
        {
            Transform target = GetTarget();
            if (target)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(-transform.up, direction);
            }
            else
            {
                transform.localRotation = defaultLocalRotation;
            }

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
            transform.localScale = eyeOverrideScale;
        }

        Transform GetTarget()
        {
            return MainCamera.camera.transform;
        }
    }
}
