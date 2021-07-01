using ArchitectsLibrary.Utility;
using ECCLibrary;
using RotA.Mono.VFX;
using RotA.Prefabs.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotA.Mono
{
    public class SunbeamGargController : MonoBehaviour
    {
        private Vector3 position = new Vector3(945f, -5000, 3000);
        private GameObject spawnedGarg;
        private float defaultFarplane;
        private float farplaneTarget;
        private float timeStart;
        private FMODAsset splashSound;

        private float FarplaneDistance
        {
            get
            {
                return SNCameraRoot.main.mainCamera.farClipPlane;
            }
        }
        public void Start()
        {
            splashSound = ScriptableObject.CreateInstance<FMODAsset>();
            splashSound.path = "event:/tools/constructor/sub_splash";
            defaultFarplane = FarplaneDistance;
            farplaneTarget = 20000f;
            GameObject prefab = GetSunbeamGargPrefab();
            spawnedGarg = GameObject.Instantiate(prefab, position, Quaternion.Euler(Vector3.up * 180f));
            spawnedGarg.SetActive(true);
            spawnedGarg.transform.parent = transform;
            Invoke(nameof(StartFadingOut), 20f);
            Invoke(nameof(EndCinematic), 30f);
            Invoke(nameof(Splash), 10f);
            timeStart = Time.time;
        }

        private void StartFadingOut()
        {
            farplaneTarget = defaultFarplane;
        }

        private void EndCinematic()
        {
            Destroy(spawnedGarg);
            Destroy(gameObject);
        }

        private void Splash()
        {
            Utils.PlayFMODAsset(splashSound, new Vector3(411f, 0f, 1213f), 6000f);
            StartCoroutine(PlaySplashVfx(new Vector3(position.x, 0f, position.z), 75f));
        }

        void LateUpdate()
        {
            SNCameraRoot.main.SetFarPlaneDistance(Mathf.MoveTowards(FarplaneDistance, farplaneTarget, Time.deltaTime * 4000f));
        }

        public GameObject GetSunbeamGargPrefab()
        {
            GameObject prefab = GameObject.Instantiate(Mod.gargAssetBundle.LoadAsset<GameObject>("SunbeamGarg_Prefab"));
            prefab.SetActive(false);
            prefab.transform.forward = Vector3.up;
            prefab.transform.localScale = Vector3.one * 5.5f;
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

        private IEnumerator PlaySplashVfx(Vector3 position, float scale)
        {
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.Exosuit);
            yield return task;
            GameObject exosuitPrefab = task.GetResult();
            var vfxSplash = exosuitPrefab.GetComponent<VFXConstructing>().surfaceSplashFX.GetComponent<VFXSplash>();
            GameObject newVfx = Instantiate(vfxSplash.surfacePrefab);
            newVfx.name = "SunbeamGargSplash";
            newVfx.transform.parent = transform;
            newVfx.transform.localScale = Vector3.one * scale;
            newVfx.transform.position = position;
            Destroy(newVfx.GetComponent<VFXDestroyAfterSeconds>());
            var customSplash = newVfx.EnsureComponent<CustomSplash>();
            customSplash.surfMaskCurve = vfxSplash.surfMaskCurve;
            customSplash.surfScaleX = vfxSplash.surfScaleX;
            customSplash.surfScaleY = vfxSplash.surfScaleY;
            customSplash.surfScaleZ = vfxSplash.surfScaleZ;
            customSplash.scale = scale;
            customSplash.GetComponentInChildren<Renderer>().material = Object.Instantiate(vfxSplash.surfacePrefab.GetComponentInChildren<Renderer>().material);
            newVfx.SetActive(true);
        }
    }
}
