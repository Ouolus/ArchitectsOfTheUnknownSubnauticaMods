using System.Collections;
using SMLHelper.V2.Assets;
using ECCLibrary;
using RotA.Mono;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase
{
    public class TabletTerminalPrefab : Spawnable
    {
        private PrecursorKeyTerminal.PrecursorKeyType keyType;
        private float triggerRadius;

        public TabletTerminalPrefab(string classId, PrecursorKeyTerminal.PrecursorKeyType keyType, float triggerRadius = 12f)
            : base(classId, "Forcefield Control", ".")
        {
            this.keyType = keyType;
            this.triggerRadius = triggerRadius;
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };
        
#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab("c718547d-fe06-4247-86d0-efd1e3747af0", out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            obj.GetComponent<PrecursorKeyTerminal>().acceptKeyType = keyType;
            obj.SetActive(false);
            SphereCollider trigger = obj.GetComponentInChildren<SphereCollider>();
            if (trigger)
            {
                trigger.radius = triggerRadius;
            }
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync("c718547d-fe06-4247-86d0-efd1e3747af0");
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            
            GameObject obj = GameObject.Instantiate(prefab);
            obj.GetComponent<PrecursorKeyTerminal>().acceptKeyType = keyType;
            obj.SetActive(false);
            SphereCollider trigger = obj.GetComponentInChildren<SphereCollider>();
            if (trigger)
            {
                trigger.radius = triggerRadius;
            }
            gameObject.Set(obj);
        }
#endif
    }
}
