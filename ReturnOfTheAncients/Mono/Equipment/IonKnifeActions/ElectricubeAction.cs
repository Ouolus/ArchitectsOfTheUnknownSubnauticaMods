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
        private float maxWarpDistance = 20f;
        private float maxDistanceFromTerrain = 2f;

        private void Awake()
        {

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
            ionKnife.Damage = 25f;
            ionKnife.AttackDistance = 1.2f;
            ionKnife.DamageType = DamageType.Electrical;
            ionKnife.PlaySwitchSound("event:/env/green_artifact_loop");
            ionKnife.VfxEventType = VFXEventTypes.diamondBlade;
            ionKnife.ResourceBonus = 1;
            // Electrical pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            if (ionKnife.IsCreature(hitLiveMixin))
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
            if (UWE.Utils.TraceForTerrain(ray, maxWarpDistance, out RaycastHit info))
            {
                warpPosition = info.point - (ray.direction * maxDistanceFromTerrain);
            }
            else
            {
                warpPosition = ray.origin + ray.GetPoint(maxWarpDistance - maxDistanceFromTerrain);
            }

            lm.transform.position = warpPosition;

            var vfx = GameObject.Instantiate(warpOutPrefab);
            vfx.transform.position = lm.transform.position;
            vfx.SetActive(true);

            var vfx2 = GameObject.Instantiate(warpInPrefab);
            vfx2.transform.position = warpPosition;
            vfx2.SetActive(true);
        }
    }
}