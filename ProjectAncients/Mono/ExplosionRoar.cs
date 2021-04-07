using System.Collections;
using ECCLibrary;
using UnityEngine;

namespace ProjectAncients.Mono
{
    public class ExplosionRoar : MonoBehaviour
    {
        CrashedShipExploder _ship = CrashedShipExploder.main;
        AudioSource _audioSource;
        IEnumerator Start()
        {
            var clip = ECCAudio.LoadAudioClip("garg_for_anth_distant-009");
            _audioSource = gameObject.EnsureComponent<AudioSource>();
            _audioSource.volume = ECCHelpers.GetECCVolume();
            _audioSource.spatialBlend = 1f;
            _audioSource.minDistance = 500f;
            _audioSource.maxDistance = 20000f;
            _audioSource.clip = clip;
            yield return StartCoroutine(PlayRoarSound());
        }
        IEnumerator PlayRoarSound()
        {
            yield return new WaitUntil(() => _ship.IsExploded());
            yield return new WaitForSeconds(21f);
            _audioSource.Play();
            MainCameraControl.main.ShakeCamera(1f, 5f, MainCameraControl.ShakeMode.Sqrt, 1f);
            yield return new WaitForSeconds(5f);
            ErrorMessage.AddMessage("Something has awoken...");
        }
    }
}