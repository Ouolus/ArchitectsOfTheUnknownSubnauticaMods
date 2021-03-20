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

        float _damage = 1f;
        float _radius = 15f;
        float _chargeRadius = 1f;
        float _chargeDamage = 2f;

        IEnumerator Start()
        {
            yield return new WaitUntil(() => fxElectSpheres is not null);

            foreach (var electSphere in fxElectSpheres)
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

            int getObjIndex = Mathf.Clamp((int)(chargeScalar * fxElectSpheres.Length), 0,
                fxElectSpheres.Length - 1);

            Utils.SpawnZeroedAt(fxElectSpheres[getObjIndex], transform);

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
                    liveMixin.TakeDamage(originalDamage, transform.position, DamageType.Electrical, gameObject);
                }
            }
            Destroy(gameObject, 5f);
        }
    }
}