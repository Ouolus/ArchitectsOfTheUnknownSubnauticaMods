using System.Collections;
using UnityEngine;

namespace ProjectAncients.Mono.Modules
{
    public class ElectricalDefenseMK2 : MonoBehaviour
    {
        public GameObject[] fxElectSpheres;
        public float charge;
        public float chargeScalar;
        public AttackType attackType;

        float _damageElec = 1f;
        float _radiusElec = 10f;
        float _chargeRadiusElec = 1f;
        float _chargeDamageElec = 2f;

        float _damageArchElec = 7f;
        float _radiusArchElec = 18f;
        float _chargeRadiusArchElec = 1.5f;
        float _chargeDamageArchElec = 3.5f;

        private static FMODAsset normalDefenseSound;
        private static FMODAsset architectElectricityDefenseSound;

        IEnumerator Start()
        {
            yield return new WaitUntil(() => fxElectSpheres is not null);

            var fxElects = (GameObject[])fxElectSpheres.Clone();
            if (attackType != AttackType.SmallElectricity)
            {
                foreach (var electSphere in fxElects)
                {
                    var renderers = electSphere.GetComponentsInChildren<Renderer>();
                    foreach (var renderer in renderers)
                    {
                        renderer.material.SetColor("_Color", Color.green);
                    }
                }
            }
            else
            {
                foreach (var electSphere in fxElects)
                {
                    var renderers = electSphere.GetComponentsInChildren<Renderer>();
                    foreach (var renderer in renderers)
                    {
                        renderer.material.SetColor("_Color", new Color(0.23f, 0.42f, 1f));
                    }
                }
            }

            float radius;
            if(attackType == AttackType.ArchitectElectricity || attackType == AttackType.Both)
            {
                radius = _radiusArchElec + charge * _chargeRadiusArchElec;
            }
            else
            {
                radius = _radiusElec + charge * _chargeRadiusElec;
            }
            float originalDamage;
            if(attackType == AttackType.ArchitectElectricity || attackType == AttackType.Both)
            {
                originalDamage = _damageArchElec + charge * _chargeDamageArchElec;
            }
            else
            {
                originalDamage = _damageElec + charge * _chargeDamageElec;
            }

            EnsureDefenseSounds();
            if (attackType == AttackType.ArchitectElectricity || attackType == AttackType.Both)
            {
                Utils.PlayFMODAsset(architectElectricityDefenseSound, transform);
            }
            else
            {
                Utils.PlayFMODAsset(normalDefenseSound, transform);
            }

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
                    if(attackType == AttackType.ArchitectElectricity)
                    {
                        liveMixin.TakeDamage(originalDamage, transform.position, Mod.architectElect, gameObject);
                    }
                    else if(attackType == AttackType.SmallElectricity)
                    {
                        liveMixin.TakeDamage(originalDamage, transform.position, DamageType.Electrical, gameObject);
                    }
                    else if(attackType == AttackType.Both)
                    {
                        liveMixin.TakeDamage(originalDamage / 2f, transform.position, Mod.architectElect, gameObject);
                        liveMixin.TakeDamage(originalDamage / 2f, transform.position, DamageType.Electrical, gameObject);
                    }
                }
            }
            Destroy(gameObject, 5f);
        }

        private void EnsureDefenseSounds()
        {
            if(normalDefenseSound == null)
            {
                normalDefenseSound = ScriptableObject.CreateInstance<FMODAsset>();
                normalDefenseSound.path = "event:/sub/seamoth/pulse";
            }
            if(architectElectricityDefenseSound == null)
            {
                architectElectricityDefenseSound = ScriptableObject.CreateInstance<FMODAsset>();
                architectElectricityDefenseSound.path = "event:/tools/stasis_gun/fire";
            }
        }

        public enum AttackType
        {
            SmallElectricity,
            ArchitectElectricity,
            Both
        }
    }
}
