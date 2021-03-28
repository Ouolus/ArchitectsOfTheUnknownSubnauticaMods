using UnityEngine;

namespace ProjectAncients.Mono
{
    public class MainMenuAtmosphereUpdater : MonoBehaviour
    {
        void Start()
        {
            Material material = RenderSettings.skybox;
            material.SetVector("_SunDir", new Vector4(0f, 1f, 0f, 0f));
        }
    }
}
