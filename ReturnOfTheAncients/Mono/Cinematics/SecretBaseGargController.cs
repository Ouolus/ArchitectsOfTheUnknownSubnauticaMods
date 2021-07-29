using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RotA.Prefabs.Creatures;
using ECCLibrary;
using ArchitectsLibrary.Utility;
using RotA.Mono.Creatures.GargEssentials;
using ArchitectsLibrary.API;
using UnityEngine.UI;
    
namespace RotA.Mono.Cinematics
{
    public class SecretBaseGargController : MonoBehaviour
    {
        GameObject currentGarg;
        AudioSource growlAudio;
        Canvas blackoutCanvas;
        Image blackoutImage;

        float timeBlackoutStartFade;
        bool blackoutFadingOut;

        public static void PlayCinematic()
        {
            new GameObject("SecretBaseGargController").AddComponent<SecretBaseGargController>();
        }

        IEnumerator Start()
        {
            PlayCreakFX("Creaking1", 5f);
            yield return new WaitForSeconds(4f);
            SpawnGarg(); //garg animation lasts 33 seconds roughly
            yield return new WaitForSeconds(4f);
            PlayCreakFX(null, 6f, 0.4f);
            //PlayOpenEyeSFX();
            yield return new WaitForSeconds(4f);
            PlayCreakFX("Creaking3", 4f);
            yield return new WaitForSeconds(3f);
            SwimAwaySFX();
            yield return new WaitForSeconds(7f);
            PlayCreakFX("Creaking4", 8f);
            yield return new WaitForSeconds(11f);
            Floodlights(2f);
            yield return new WaitForSeconds(3f);
            PlayCloseRoarSFX();
            yield return new WaitForSeconds(1f);
            StartBlackOutEffect();
            yield return new WaitForSeconds(5f);
            StopBlackOutEffect();
            yield return new WaitForSeconds(1f);
            ActivateTeleporter();
        }

        void ActivateTeleporter()
        {
            TeleporterManager.ActivateTeleporter("SecretBaseAuxiliary");
            Utils.PlayFMODAsset(SNAudioEvents.GetFmodAsset("event:/env/power_teleporter"));
        }

        void SwimAwaySFX()
        {
            AudioSource source = new GameObject("SwimAwaySound").AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume();
            source.clip = Mod.gargAssetBundle.LoadAsset<AudioClip>("GargSecretBaseSwimAway");
            source.Play();
            Destroy(source.gameObject, 4f);
    }

        void PlayCloseRoarSFX()
        {
            AudioSource source = new GameObject("RoarSource").AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume() * 0.5f;
            source.clip = Mod.gargAssetBundle.LoadAsset<AudioClip>("GargSecretBaseRoar");
            source.Play();
            MainCameraControl.main.ShakeCamera(4f, 5f, MainCameraControl.ShakeMode.Sqrt);
            Destroy(source.gameObject, 21f);
        }

        void PlayOpenEyeSFX()
        {
            AudioSource source = new GameObject("OpenEyeSource").AddComponent<AudioSource>();
            source.transform.position = new Vector3(1500, -2000, -60f);
            source.volume = ECCHelpers.GetECCVolume() * 0.7f;
            source.clip = Mod.gargAssetBundle.LoadAsset<AudioClip>("GargEyeOpen");
            source.spatialBlend = 1f;
            source.minDistance = 30f;
            source.maxDistance = 100f;
            source.Play();
            Destroy(source.gameObject, 5f);
        }

        void PlayCreakFX(string clipName, float screenShakeDuration, float shakeIntensity = 2f, float shakeFrequency = 0.3f)
        {
            if (!string.IsNullOrEmpty(clipName))
            {
                AudioSource source = new GameObject("CreakSource").AddComponent<AudioSource>();
                source.volume = ECCHelpers.GetECCVolume() * 0.6f;
                source.clip = Mod.assetBundle.LoadAsset<AudioClip>(clipName);
                source.Play();
                Destroy(source.gameObject, 11f);
            }
            MainCameraControl.main.ShakeCamera(shakeIntensity, screenShakeDuration, MainCameraControl.ShakeMode.Linear, shakeFrequency);
        }

        void StartBlackOutEffect()
        {
            var canvasObj = new GameObject("CanvasObj");
            blackoutCanvas = canvasObj.AddComponent<Canvas>();
            blackoutCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            blackoutCanvas.sortingOrder = 32766;
            blackoutCanvas.gameObject.layer = 5;    
            var blObj = new GameObject("Blackout");
            blObj.transform.parent = blackoutCanvas.transform;
            blackoutImage = blObj.AddComponent<Image>();
            blackoutImage.color = Color.black;
            var rt = blObj.EnsureComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            Destroy(currentGarg, 1f);
        }
        
        void StopBlackOutEffect()
        {
            timeBlackoutStartFade = Time.time;
            blackoutFadingOut = true;
        }

        void Update()
        {
            if (blackoutImage != null && blackoutFadingOut)
            {
                blackoutImage.color = new Color(0f, 0f, 0f, 1f - (Time.time - timeBlackoutStartFade));
                if (Time.time > timeBlackoutStartFade + 1f)
                {
                    Destroy(blackoutCanvas.gameObject);
                }
            }
        }

        void SpawnGarg()
        {
            currentGarg = SpawnGargPrefab();
            currentGarg.transform.position = new Vector3(1470, -2000f, -80f);
            currentGarg.transform.eulerAngles = new Vector3(0, 90, 0);
            growlAudio = currentGarg.SearchChild("Head").AddComponent<AudioSource>();
            growlAudio.volume = ECCHelpers.GetECCVolume();
            growlAudio.clip = Mod.gargAssetBundle.LoadAsset<AudioClip>("GargPresence");
            growlAudio.spatialBlend = 1f;
            growlAudio.minDistance = 70f;
            growlAudio.maxDistance = 200f;
            growlAudio.Play();
        }

        void Floodlights(float intensity)
        {
            Utils.PlayFMODAsset(SNAudioEvents.GetFmodAsset("event:/sub/cyclops/floodlights_on"), Player.main.transform.position);
            SpawnLight(new Vector3(1500f, -1980f, -60f), intensity);
            SpawnLight(new Vector3(1500f, -2010f, -60f), intensity);
        }

        void SpawnLight(Vector3 pos, float intensity)
        {
            GameObject lightObj = new GameObject();
            lightObj.transform.position = pos;
            var l = lightObj.AddComponent<Light>();
            l.color = new Color(0.54f, 1f, 0.54f);
            l.intensity = intensity;
            l.range = 60f;
            l.type = LightType.Point;
            l.shadows = LightShadows.Hard;
            Destroy(lightObj, 60f);
        }

        #region Ugly garg code
        public GameObject SpawnGargPrefab()
        {
            GameObject prefab = GameObject.Instantiate(Mod.gargAssetBundle.LoadAsset<GameObject>("SecretBaseGarg_Prefab"));
            prefab.SetActive(false);
            prefab.transform.localScale = Vector3.one;
            prefab.transform.GetChild(0).localScale = new Vector3(2f, 2f, 2f);
            MaterialUtils.ApplySNShaders(prefab);
            foreach(var renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                if (renderer.gameObject.name != "Gargantuan.002")
                {
                    foreach (Material material in renderer.materials)
                    {
                        material.SetFloat("_SpecInt", 0f);
                    }
                }
            }
            BehaviourLOD lod = prefab.EnsureComponent<BehaviourLOD>();
            lod.veryCloseThreshold = 9999997;
            lod.closeThreshold = 9999998;
            lod.farThreshold = 9999999;
            List<Transform> spines = new List<Transform>();
            GameObject currentSpine = prefab.SearchChild("Spine");
            while (currentSpine != null)
            {
                currentSpine = currentSpine.SearchChild("Spine", ECCStringComparison.StartsWith);
                if (currentSpine)
                {
                    if (currentSpine.name.Contains("59"))
                    {
                        break;
                    }
                    else
                    {
                        spines.Add(currentSpine.transform);
                    }
                }
            }
            spines.Add(prefab.SearchChild("Tail", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail1", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail2", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail3", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail4", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail5", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail6", ECCStringComparison.Equals).transform);
            prefab.SearchChild("BLE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("BRE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("MLE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("MRE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("FLE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("FRE").AddComponent<GargEyeFixer>();
            FixRotationMultipliers(CreateTentacleTrail(prefab, prefab.SearchChild("BLT"), lod), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTentacleTrail(prefab, prefab.SearchChild("BRT"), lod), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTentacleTrail(prefab, prefab.SearchChild("TLT"), lod), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTentacleTrail(prefab, prefab.SearchChild("TRT"), lod), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTentacleTrail(prefab, prefab.SearchChild("MLT"), lod), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTentacleTrail(prefab, prefab.SearchChild("MRT"), lod), 0.25f, 0.26f);
            FixRotationMultipliers(CreateBodyTrail(prefab, prefab.SearchChild("Spine"), spines.ToArray(), lod), 0.26f, 0.26f);
            prefab.EnsureComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();
            prefab.SetActive(true);
            return prefab;
        }

        TrailManager CreateTentacleTrail(GameObject prefab, GameObject trailRoot, BehaviourLOD lod)
        {
            trailRoot.gameObject.SetActive(false);
            TrailManager trail = trailRoot.AddComponent<TrailManager>();
            List<Transform> trails = new List<Transform>(trailRoot.GetComponentsInChildren<Transform>());
            trails.Remove(trailRoot.transform);
            trail.trails = trails.ToArray();
            trail.rootTransform = prefab.transform;
            trail.rootSegment = trail.transform;
            trail.levelOfDetail = lod;
            trail.segmentSnapSpeed = 8f;
            trail.maxSegmentOffset = 10f;
            trail.allowDisableOnScreen = false;
            AnimationCurve decreasing = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 0.75f) });
            trail.pitchMultiplier = decreasing;
            trail.rollMultiplier = decreasing;
            trail.yawMultiplier = decreasing;
            trail.Initialize();
            trailRoot.gameObject.SetActive(true);
            return trail;
        }

        TrailManager CreateBodyTrail(GameObject prefab, GameObject trailRoot, Transform[] trails, BehaviourLOD lod)
        {
            trailRoot.gameObject.SetActive(false);
            TrailManager trail = trailRoot.AddComponent<TrailManager>();
            trail.trails = trails;
            trail.rootTransform = prefab.transform;
            trail.rootSegment = trail.transform;
            trail.levelOfDetail = lod;
            trail.segmentSnapSpeed = 0.075f / 2f;
            trail.maxSegmentOffset = 700f;
            trail.allowDisableOnScreen = false;
            AnimationCurve decreasing = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 0.75f) });
            trail.pitchMultiplier = decreasing;
            trail.rollMultiplier = decreasing;
            trail.yawMultiplier = decreasing;
            trail.Initialize();
            trailRoot.gameObject.SetActive(true);
            return trail;
        }

        void FixRotationMultipliers(TrailManager tm, float min, float max)
        {
            AnimationCurve curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, min), new Keyframe(1f, max) });
            tm.pitchMultiplier = curve;
            tm.rollMultiplier = curve;
            tm.yawMultiplier = curve;
        }
        #endregion
    }
}
