using UnityEngine;
using ArchitectsLibrary.Utility;
using RotA.Prefabs.Creatures;
using System.Collections;
using System.Collections.Generic;
using ECCLibrary;

namespace RotA.Mono
{
    public class SunbeamGargController : MonoBehaviour
    {
        private Vector3 position = new Vector3(1162, -2500f, 4333);
        private GameObject spawnedGarg;
        private float farplaneDistanceBefore;

        public void Start()
        {
            farplaneDistanceBefore = SNCameraRoot.main.mainCamera.farClipPlane;
            SNCameraRoot.main.SetFarPlaneDistance(10000f);
            GameObject prefab = GetSunbeamGargPrefab();
            spawnedGarg = GameObject.Instantiate(prefab, position, Quaternion.Euler(Vector3.up * 190f));
            spawnedGarg.SetActive(true);
            this.Invoke(nameof(EndCinematic), 30f);
        }

        private void EndCinematic()
        {
            Destroy(spawnedGarg);
            Destroy(gameObject);
            SNCameraRoot.main.SetFarPlaneDistance(farplaneDistanceBefore);
        }

        public GameObject GetSunbeamGargPrefab()
        {
            GameObject prefab = GameObject.Instantiate(Mod.assetBundle.LoadAsset<GameObject>("SunbeamGarg_Prefab"));
            prefab.SetActive(false);
            prefab.transform.localScale = Vector3.one * 6f;
            MaterialUtils.ApplySNShaders(prefab);
            Renderer renderer = prefab.SearchChild("Gargantuan.001").GetComponent<SkinnedMeshRenderer>();
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[0]);
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[1]);
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[2]);
            AdultGargantuan.UpdateGargSolidMaterial(renderer.materials[3]);
            AdultGargantuan.UpdateGargSkeletonMaterial(renderer.materials[4]);
            AdultGargantuan.UpdateGargGutsMaterial(renderer.materials[5]);
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
            trail.segmentSnapSpeed = 0.075f;
            trail.maxSegmentOffset = 40f;
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
