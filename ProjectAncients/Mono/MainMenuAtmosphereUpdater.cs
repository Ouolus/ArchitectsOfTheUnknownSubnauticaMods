using UnityEngine;

namespace ProjectAncients.Mono
{
    public class MainMenuAtmosphereUpdater : MonoBehaviour
    {
        void Start()
        {
            uSkyManager skyManager = FindObjectOfType<uSkyManager>();
            skyManager.Timeline = 2f;
            skyManager.NightSky = uSkyManager.NightModes.Rotation;
        }
    }
}
