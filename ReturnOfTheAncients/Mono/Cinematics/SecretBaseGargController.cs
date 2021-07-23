using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RotA.Prefabs.Creatures;
using ECCLibrary;
using ArchitectsLibrary.Utility;
using ArchitectsLibrary.API;
    
namespace RotA.Mono.Cinematics
{
    public class SecretBaseGargController : MonoBehaviour
    {
        GameObject currentGarg;
        AudioSource growlAudio;

        IEnumerator Start()
        {
            SpawnGarg();
            yield return new WaitForSeconds(31f);
            StartCoroutine(Floodlights());
        }

        void SpawnGarg()
        {
            currentGarg = SpawnGargPrefab();
            currentGarg.transform.position = new Vector3(1488, -2000f, -80f);
            currentGarg.transform.eulerAngles = new Vector3(0, 90, 0);
            growlAudio = currentGarg.SearchChild("Head").AddComponent<AudioSource>();
            growlAudio.volume = ECCHelpers.GetECCVolume();
            growlAudio.clip = Mod.gargAssetBundle.LoadAsset<AudioClip>("GargPresence");
            growlAudio.spatialBlend = 1f;
            growlAudio.minDistance = 10f;
            growlAudio.maxDistance = 100f;
            growlAudio.Play();
        }

        IEnumerator Floodlights()
        {
            Utils.PlayFMODAsset(SNAudioEvents.GetFmodAsset("event:/sub/cyclops/floodlights_on"), transform.position);
            GameObject lightObj = new GameObject();
            lightObj.transform.position = new Vector3(1500f, -2000f, -50f);
            var l = lightObj.AddComponent<Light>();
            l.color = Color.white;
            l.intensity = 2f;
            l.range = 40f;
            l.type = LightType.Point;
            yield return new WaitForSeconds(5f);
            Destroy(lightObj);
        }

        public GameObject SpawnGargPrefab()
        {
            GameObject prefab = GameObject.Instantiate(Mod.gargAssetBundle.LoadAsset<GameObject>("SecretBaseGarg_Prefab"));
            prefab.SetActive(false);
            prefab.transform.localScale = Vector3.one;
            MaterialUtils.ApplySNShaders(prefab);
            BehaviourLOD lod = prefab.EnsureComponent<BehaviourLOD>();
            lod.veryCloseThreshold = 5000f;
            lod.closeThreshold = 7500f;
            lod.farThreshold = 10000f;
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
            trail.segmentSnapSpeed = 0.075f / 4.5f;
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
