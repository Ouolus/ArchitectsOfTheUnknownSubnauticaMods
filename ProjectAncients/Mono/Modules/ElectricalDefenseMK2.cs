using System.Collections;
using UnityEngine;

namespace ProjectAncients.Mono.Modules
{
    public class ElectricalDefenseMK2 : MonoBehaviour
    {
        public FMODAsset defenseSound;
        public GameObject[] fxElectSpheres;
        public float charge;
        public float chargeScalar;

        float _damage = 7f;
        float _radius = 18f;
        float _chargeRadius = 1.5f;
        float _chargeDamage = 3.5f;

        IEnumerator Start()
        {
            yield return new WaitUntil(() => fxElectSpheres is not null);

            var fxElects = (GameObject[]) fxElectSpheres.Clone();
            foreach (var electSphere in fxElects)
            {
                var renderers = electSphere.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    renderer.material.SetColor("_Color", Color.green);
                }
            }
            
            float radius = _radius + charge * _chargeRadius;
            float originalDamage = _damage + charge * _chargeDamage;
            
            Utils.PlayFMODAsset(defenseSound, transform);

            int getObjIndex = Mathf.Clamp((int)(chargeScalar * fxElects.Length), 0,
                fxElects.Length - 1);

            Utils.SpawnZeroedAt(fxElects[getObjIndex], transform);

            int fxSpawnedIn = UWE.Utils.OverlapSphereIntoSharedBuffer(transform.position, radius);

            for (int i = 0; i < fxSpawnedIn; i++)
            {
                var collider = UWE.Utils.sharedColliderBuffer[i];
                var obj = UWE.Utils.GetEntityRoot(collider.gameObject);
                if (obj is null)
                    obj = collider.gameObject;

                var creature = obj.GetComponent<Creature>();
                var liveMixin = obj.GetComponent<LiveMixin>();

                if (creature is not null && liveMixin is not null)
                {
                    liveMixin.TakeDamage(originalDamage, transform.position, Mod.architectElect, gameObject);
                }
            }
            Destroy(gameObject, 5f);
        }
    }
}
