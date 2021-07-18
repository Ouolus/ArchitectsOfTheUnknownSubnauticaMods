using RotA.Interfaces;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class ElectricubeAction : MonoBehaviour, IIonKnifeAction
    {
        private GameObject warpOutPrefab;
        private GameObject warpInPrefab;
        private const float kMaxWarpDistance = 20f;
        private const float kMaxDistanceFromTerrain = 2f;

        private void Awake()
        {
            StartCoroutine(GetPrefabs());
        }

        IEnumerator GetPrefabs()
        {
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.Warper);
            yield return task;
            var prefab = task.GetResult();
            warpOutPrefab = prefab.GetComponent<Warper>().warpOutEffectPrefab;
            warpInPrefab = prefab.GetComponent<Warper>().warpInEffectPrefab;
        }
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = new[] { 25f };
            ionKnife.AttackDistance = 1.2f;
            ionKnife.DamageType = new[] { DamageType.Electrical };
            ionKnife.PlaySwitchSound("event:/env/green_artifact_loop");
            ionKnife.VfxEventType = VFXEventTypes.diamondBlade;
            ionKnife.ResourceBonus = 1;
            
            ionKnife.SetMaterialColors(new Color(.5f, 0f, 1f) * .7f, new Color(0f, 0f, .9f),
                new Color(1f, 0f ,1f), new Color(0.5f, 0f, 0.5f));
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            if (IonKnife.IsCreature(hitLiveMixin))
            {
                if (Random.value <= 0.25f)
                {
                    WarpRandom(ionKnife, hitLiveMixin);
                }
            }
        }

        private void WarpRandom(IonKnife ionKnife, LiveMixin lm)
        {
            Utils.PlayFMODAsset(ionKnife.WarpFishSound, transform);

            Ray ray = new Ray(lm.transform.position, Random.onUnitSphere);
            Vector3 warpPosition;
            if (UWE.Utils.TraceForTerrain(ray, kMaxWarpDistance, out RaycastHit info))
            {
                warpPosition = info.point - (ray.direction * kMaxDistanceFromTerrain);
            }
            else
            {
                warpPosition = ray.origin + ray.GetPoint(kMaxWarpDistance - kMaxDistanceFromTerrain);
            }

            lm.transform.position = warpPosition;

            var vfx = Instantiate(warpOutPrefab);
            vfx.transform.position = lm.transform.position;
            vfx.SetActive(true);

            var vfx2 = Instantiate(warpInPrefab);
            vfx2.transform.position = warpPosition;
            vfx2.SetActive(true);
        }
    }
}