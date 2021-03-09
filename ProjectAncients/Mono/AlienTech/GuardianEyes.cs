using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace ProjectAncients.Mono.AlienTech
{
    public class GuardianEyes : MonoBehaviour
    {
        Renderer renderer;
        Material material;
        Light[] lights;
        private float lightBaseIntensity = 1.2f;
        private float minFlickerSpeed = 0.1f;
        private float maxFlickerSpeed = 0.3f;

        void Start()
        {
            renderer = gameObject.SearchChild("Leviathan", ECCStringComparison.Equals).GetComponent<Renderer>();
            material = renderer.sharedMaterials[1];
            lights = GetComponentsInChildren<Light>();
            StartCoroutine(Flicker());
        }

        IEnumerator Flicker()
        {
            float intensity = 0f;
            for (; ; )
            {
                intensity = Random.Range(0f, 1f);
                material.SetFloat("_GlowStrength", intensity);
                SetLightsIntensity(intensity);
                yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
                intensity = 0f;
                material.SetFloat("_GlowStrength", intensity);
                SetLightsIntensity(intensity);
                yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
            }
        }

        void SetLightsIntensity(float intensity)
        {
            foreach (Light light in lights)
            {
                light.intensity = intensity * lightBaseIntensity;
            }
        }
    }
}
