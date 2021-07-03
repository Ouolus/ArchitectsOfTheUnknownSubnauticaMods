using ECCLibrary;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.MainMenu
{
    public class MainMenuAtmosphereUpdater : MonoBehaviour
    {
        ECCAudio.AudioClipPool clipPool;
        AudioSource source;
        WaterSurface currentWaterSurface;
        Camera currentCamera;

        float timeStart;
        void Start()
        {
            uSkyManager skyManager = FindObjectOfType<uSkyManager>();
            skyManager.Timeline = 5.25f;
            skyManager.StarIntensity = 2f;
            skyManager.NightSky = uSkyManager.NightModes.Static;
            skyManager.planetRadius = 3500f;
            skyManager.planetDistance = 10000f;
            clipPool = ECCAudio.CreateClipPool("garg_for_anth_distant");
            source = gameObject.AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume() / 2f;
            timeStart = Time.time;
            StartCoroutine(RoarLoop());
        }

        IEnumerator RoarLoop()
        {
            yield return new WaitForSeconds(1f);
            PlayRoar();
            for (; ; )
            {
                yield return new WaitForSeconds(Random.Range(22f, 44f));
                PlayRoar();
            }
        }

        void PlayRoar()
        {
            if (AlienTech.BlackHole.solarSystemDestroyed)
            {
                return;
            }
            AudioClip nextClip = clipPool.GetRandomClip();
            source.volume = ECCHelpers.GetECCVolume() / 2f;
            source.clip = nextClip;
            source.Play();
        }

        void Update()
        {
            if (AlienTech.BlackHole.solarSystemDestroyed)
            {
                if (currentWaterSurface is null)
                {
                    currentWaterSurface = Object.FindObjectOfType<WaterSurface>();
                }
                if (currentWaterSurface is not null)
                {
                    currentWaterSurface.transform.position = new Vector3(0f, -1000f, 0f);
                }
                if (currentCamera is null)
                {
                    var al = Object.FindObjectOfType<AudioListener>();
                    if (al)
                    {
                        currentCamera = al.GetComponent<Camera>();
                    }
                }
                if (currentCamera is not null)
                {
                    currentCamera.transform.position = new Vector3(0f, 500f + (Time.time - timeStart) * 0.25f * Time.deltaTime, (Time.time - timeStart) * -0.5f * Time.deltaTime);
                }
            }
        }
    }
}
