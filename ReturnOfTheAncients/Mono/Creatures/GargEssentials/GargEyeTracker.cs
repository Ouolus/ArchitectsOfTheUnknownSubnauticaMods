using UnityEngine;

namespace RotA.Mono.Creatures.GargEssentials
{
    public class GargEyeTracker : MonoBehaviour
    {
        Quaternion defaultLocalRotation;
        Vector3 eyeOverrideScale = new Vector3(0.95f, 0.95f, 0.95f);
        bool isWeirdBackEye;
        bool isWeirdFrontEye;
        bool isWeirdMiddleEye;

        void Start()
        {
            defaultLocalRotation = transform.localRotation;
            isWeirdBackEye = gameObject.name == "BRE";
            isWeirdFrontEye = gameObject.name == "FRE";
            isWeirdMiddleEye = gameObject.name == "MRE";
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

            if (isWeirdBackEye)
            {
                transform.localEulerAngles = new Vector3(56f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            else if (isWeirdFrontEye)
            {
                transform.localEulerAngles = new Vector3(50f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            else if (isWeirdMiddleEye)
            {
                transform.localEulerAngles = new Vector3(70f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            transform.localScale = eyeOverrideScale;
        }

        Transform GetTarget()
        {
            return MainCamera.camera.transform;
        }
    }
}
