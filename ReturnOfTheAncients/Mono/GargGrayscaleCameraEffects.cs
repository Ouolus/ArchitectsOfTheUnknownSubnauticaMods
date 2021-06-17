using UnityStandardAssets.ImageEffects;
using RotA.Mono.Singletons;
using UnityEngine;

namespace RotA.Mono
{
    class GargGrayscaleCameraEffects : MonoBehaviour
    {
        Grayscale grayscale;

        void Start()
        {
            grayscale = MainCamera.camera.GetComponent<Grayscale>();
        }

        void Update()
        {
            if(uGUI_FeedbackCollector.main.state == false)
            {
                grayscale.effectAmount = GetGrayscaleStrength();
                grayscale.enabled = true;
            }
        }

        float GetGrayscaleStrength()
        {
            if (!VoidGargSingleton.AdultGargExists)
            {
                return 0f;
            }
            return (Mathf.PingPong(Time.time / 5f, 1f) * 0.5f);
        }
    }
}
