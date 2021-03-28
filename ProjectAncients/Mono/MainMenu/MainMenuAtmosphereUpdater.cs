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
            skyManager.Timeline = 4f;
            skyManager.StarIntensity = 2f;
            skyManager.NightSky = uSkyManager.NightModes.Rotation;
            skyManager.planetRadius = 500f;
            clipPool = ECCAudio.CreateClipPool("garg_for_anth_distant");
            source = gameObject.AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume();
            StartCoroutine(RoarLoop());
        }

        IEnumerator RoarLoop()
        {
            yield return new WaitForSeconds(6f);
            PlayRoar();
            for(; ; )
            {
                yield return new WaitForSeconds(Random.Range(22f, 34f));
                PlayRoar();
            }
        }

        void PlayRoar()
        {
            AudioClip nextClip = clipPool.GetRandomClip();
            source.volume = ECCHelpers.GetECCVolume();
            source.clip = nextClip;
            source.Play();
        }
    }
}
