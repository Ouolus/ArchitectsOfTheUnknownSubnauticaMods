using UnityEngine;
using ECCLibrary;
using ProjectAncients.Prefabs;

namespace ProjectAncients.Mono
{
    public class MainMenuGarg : MonoBehaviour
    {
        void Start()
        {
            ECCHelpers.ApplySNShaders(gameObject, new UBERMaterialProperties(2f, 200, 3f));
            Renderer renderer = gameObject.SearchChild("Gargantuan.001").GetComponent<Renderer>();
            gameObject.AddComponent<BehaviourLOD>();
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[0]);
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[1]);
            AdultGargantuan.UpdateGargTransparentMaterial(renderer.materials[2]);
            AdultGargantuan.UpdateGargSkeletonMaterial(renderer.materials[3]);
            AdultGargantuan.UpdateGargGutsMaterial(renderer.materials[4]);
            AddTentacleTrail("BLT");
            AddTentacleTrail("BRT");
            AddTentacleTrail("TLT");
            AddTentacleTrail("TRT");
            AddTentacleTrail("MLT");
            AddTentacleTrail("MRT");
        }

        void AddTentacleTrail(string name)
        {
            FixRotationMultipliers(CreateTrail(gameObject.SearchChild(name), GetComponent<BehaviourLOD>(), 8f), 0.25f, 0.26f);
        }

        void FixRotationMultipliers(TrailManager tm, float min, float max)
        {
            AnimationCurve curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, min), new Keyframe(1f, max) });
            tm.pitchMultiplier = curve;
            tm.rollMultiplier = curve;
            tm.yawMultiplier = curve;
        }

        private TrailManager CreateTrail(GameObject trailParent, BehaviourLOD lod, float segmentSnapSpeed, float maxSegmentOffset = -1f, float multiplier = 1f)
        {
            TrailManager trail = trailParent.AddComponent<TrailManager>();
            trail.trails = trailParent.transform.GetChild(0).GetComponentsInChildren<Transform>();
            trail.rootTransform = transform;
            trail.rootSegment = trail.transform;
            trail.levelOfDetail = lod;
            trail.segmentSnapSpeed = segmentSnapSpeed;
            trail.maxSegmentOffset = maxSegmentOffset;
            trail.allowDisableOnScreen = false;
            AnimationCurve decreasing = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f * multiplier), new Keyframe(1f, 0.75f * multiplier) });
            trail.pitchMultiplier = decreasing;
            trail.rollMultiplier = decreasing;
            trail.yawMultiplier = decreasing;
            return trail;
        }

        private TrailManager CreateTrail(GameObject trailRoot, Transform[] trails, BehaviourLOD lod, float segmentSnapSpeed, float maxSegmentOffset = -1f)
        {
            trailRoot.gameObject.SetActive(false);
            TrailManager trail = trailRoot.AddComponent<TrailManager>();
            trail.trails = trails;
            trail.rootTransform = transform;
            trail.rootSegment = trail.transform;
            trail.levelOfDetail = lod;
            trail.segmentSnapSpeed = segmentSnapSpeed;
            trail.maxSegmentOffset = maxSegmentOffset;
            trail.allowDisableOnScreen = false;
            AnimationCurve decreasing = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 0.75f) });
            trail.pitchMultiplier = decreasing;
            trail.rollMultiplier = decreasing;
            trail.yawMultiplier = decreasing;
            trail.Initialize();
            trailRoot.gameObject.SetActive(true);
            return trail;
        }
    }
}
