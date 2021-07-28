using ArchitectsLibrary.Utility;
using ECCLibrary;
using RotA.Mono.VFX;
using RotA.Prefabs.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RotA.Mono.Cinematics
{
    public class SunbeamGargController : MonoBehaviour
    {
        private Vector3 gargsSpawnPosition = new Vector3(945f, 0f, 3000);
        public bool forceSpecialCutscene = false;
        private BoundingSphere secretCutsceneBounds = new BoundingSphere(new Vector3(372, 0, 1113), 100f);
        private GameObject spawnedGarg;
        private float defaultFarplane;
        private float farplaneTarget;
        private float timeStart;
        private bool initialized = false;
        private SunbeamWreck wreck;

        private bool setTimeScaleLateUpdate = false;
        private float targetTimeScale;

        private float CurrentFarplaneDistance
        {
            get
            {
                return SNCameraRoot.main.mainCamera.farClipPlane;
            }
        }

        private void Start()
        {
            initialized = true;
            defaultFarplane = CurrentFarplaneDistance;
            farplaneTarget = 20000f;
            GameObject gargPrefab = GetSunbeamGargPrefab();
            Vector3 spawnPos = gargsSpawnPosition;
            spawnedGarg = GameObject.Instantiate(gargPrefab, spawnPos, Quaternion.Euler(Vector3.up * 180f));
            spawnedGarg.SetActive(true);
            spawnedGarg.transform.parent = transform;
            spawnedGarg.GetComponentInChildren<Animator>().SetBool("mouth_open", true);
            Invoke(nameof(StartFadingOut), 20f);
            Invoke(nameof(EndCinematic), 30f);
            timeStart = Time.time;
            Invoke(nameof(SpawnWreckPrefab), 7.4f);
        }

        private void SpawnWreckPrefab()
        {
            GameObject prefab = GetSunbeamWreckPrefab();
            var spawned = GameObject.Instantiate(prefab);
            wreck = spawned.EnsureComponent<SunbeamWreck>();
            spawned.transform.position = new Vector3(1107, 3843, 4369);
            spawned.transform.localScale = new Vector3(20f, 20f, 20f);
            spawned.transform.localEulerAngles = new Vector3(0, 180, 0);
            spawned.SetActive(true);
        }

        public GameObject GetSunbeamWreckPrefab()
        {
            GameObject prefab = GameObject.Instantiate(Mod.gargAssetBundle.LoadAsset<GameObject>("SunbeamWreck_Prefab"));
            prefab.SetActive(false);
            MaterialUtils.ApplySNShaders(prefab);
            return prefab;
        }

        private IEnumerator WellBeRightBack()
        {
            yield return new WaitForSeconds(5.49f);
            setTimeScaleLateUpdate = true;
            targetTimeScale = 0.001f;
            AudioClip secretSound = ECCAudio.LoadAudioClip("GargSunbeamSecretSFX");
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(secretSound);
            GameObject imgObj = CreateMemeOverlay();
            yield return new WaitForSecondsRealtime(2.8f);
            Destroy(imgObj);
            yield return IngameMenu.main.SaveGameAsync();
            setTimeScaleLateUpdate = false;
            Time.timeScale = 1f;
            CutToCredits();
        }

        private void CutToCredits()
        {
            SceneManager.LoadSceneAsync("EndCreditsSceneCleaner", LoadSceneMode.Single);
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

        void LateUpdate()
        {
            if (!initialized)
            {
                return;
            }
            SNCameraRoot.main.SetFarPlaneDistance(Mathf.MoveTowards(CurrentFarplaneDistance, farplaneTarget, Time.deltaTime * 4000f));
            if (setTimeScaleLateUpdate)
            {
                Time.timeScale = targetTimeScale;
            }
        }

        #region Messy prefab stuff
        public GameObject GetSunbeamGargPrefab()
        {
            GameObject prefab = GameObject.Instantiate(Mod.gargAssetBundle.LoadAsset<GameObject>("SunbeamGarg_Prefab"));
            prefab.SetActive(false);
            prefab.transform.forward = Vector3.up;
            prefab.transform.localScale = Vector3.one * 5.5f;
            MaterialUtils.ApplySNShaders(prefab);
            Renderer renderer = prefab.SearchChild("Gargantuan.001").GetComponent<SkinnedMeshRenderer>();
            Renderer eyeRenderer = prefab.SearchChild("Gargantuan.002").GetComponent<SkinnedMeshRenderer>();
            Renderer insidesRenderer = prefab.SearchChild("Gargantuan.003").GetComponent<SkinnedMeshRenderer>();
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[0]);
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[1]);
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[2]);
            AdultGargantuan.UpdateGargSolidMaterial(renderer.materials[3]);
            AdultGargantuan.UpdateGargEyeMaterial(eyeRenderer.materials[0]);
            AdultGargantuan.UpdateGargSkeletonMaterial(insidesRenderer.materials[0]);
            AdultGargantuan.UpdateGargGutsMaterial(insidesRenderer.materials[1]);
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

        private GameObject CreateMemeOverlay()
        {
            Canvas canvas = uGUI.main.screenCanvas;
            GameObject imageObj = new GameObject("WellBeRightBack");
            imageObj.layer = 5;
            Image img = imageObj.EnsureComponent<Image>();
            img.sprite = Mod.gargAssetBundle.LoadAsset<Sprite>("WellBeRightBack");
            img.raycastTarget = false;
            var rect = imageObj.EnsureComponent<RectTransform>();
            rect.SetParent(canvas.transform);
            rect.anchoredPosition = new Vector2(-600f, 0f);
            rect.localPosition = new Vector3(-600f, 0f, 0f);
            rect.sizeDelta = new Vector2(336f, 494f);
            rect.localScale = Vector3.one * 1.4f;
            rect.localEulerAngles = Vector3.zero;
            return imageObj;
        }
        #endregion
    }
}