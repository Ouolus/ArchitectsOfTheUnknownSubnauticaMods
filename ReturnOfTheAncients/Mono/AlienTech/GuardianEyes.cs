using ECCLibrary;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class GuardianEyes : MonoBehaviour
    {
        Renderer renderer;
        Material material;
        Light[] lights;
        private float lightBaseIntensity = 5f;
        private float minFlickerSpeed = 0.05f;
        private float maxFlickerSpeed = 0.5f;

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
                if (Random.value < 0.1f)
                {
                    yield return new WaitForSeconds(0.5f + Random.value);
                }
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
