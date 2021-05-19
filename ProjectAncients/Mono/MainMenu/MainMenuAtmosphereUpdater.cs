using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ECCLibrary;

namespace ProjectAncients.Mono
{
    public class MainMenuAtmosphereUpdater : MonoBehaviour
    {
        ECCAudio.AudioClipPool clipPool;
        AudioSource source;
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
            StartCoroutine(RoarLoop());
        }

        IEnumerator RoarLoop()
        {
            yield return new WaitForSeconds(1f);
            PlayRoar();
            for(; ; )
            {
                yield return new WaitForSeconds(Random.Range(22f, 44f));
                PlayRoar();
            }
        }

        void PlayRoar()
        {
            AudioClip nextClip = clipPool.GetRandomClip();
            source.volume = ECCHelpers.GetECCVolume() / 2f;
            source.clip = nextClip;
            source.Play();
        }
    }
}
