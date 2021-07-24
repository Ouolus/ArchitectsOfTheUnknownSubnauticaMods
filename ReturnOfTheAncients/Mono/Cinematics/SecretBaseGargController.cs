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
    
namespace RotA.Mono.Cinematics
{
    public class SecretBaseGargController : MonoBehaviour
    {
        GameObject currentGarg;
        AudioSource growlAudio;

        IEnumerator Start()
        {
            PlayCreakSFX("Creaking1");
            yield return new WaitForSeconds(3f);
            SpawnGarg(); //garg animation lasts 33 seconds roughly
            yield return new WaitForSeconds(8f);
            PlayCreakSFX("Creaking3");
            yield return new WaitForSeconds(10f);
            PlayCreakSFX("Creaking4");
            yield return new WaitForSeconds(12f);
            PlayCreakSFX("Creaking5");
            Floodlights(2f);
            yield return new WaitForSeconds(4f);
            PlayCloseRoarSFX();
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


        void PlayCreakSFX(string clipName)
        {
            AudioSource source = new GameObject("CreakSource").AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume();
            source.clip = Mod.assetBundle.LoadAsset<AudioClip>(clipName);
            source.Play();
            Destroy(source.gameObject, 11f);
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
            FixRotationMultipliers(CreateTrail(prefab, prefab.SearchChild("Spine"), spines.ToArray(), lod), 0.26f, 0.26f);
            prefab.SetActive(true);
            return prefab;
        }

        TrailManager CreateTrail(GameObject prefab, GameObject trailRoot, Transform[] trails, BehaviourLOD lod)
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
    }
}
