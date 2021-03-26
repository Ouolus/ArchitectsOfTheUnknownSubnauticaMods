using System.Collections;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class InfectionTesterTerminal : Spawnable
    {
        const string baseClassId = "b1f54987-4652-4f62-a983-4bf3029f8c5b";

        public InfectionTesterTerminal(string classId)
            : base(classId, "Forcefield Control", ".")
        {
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
            PrefabDatabase.TryGetPrefab(baseClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            PrecursorDisableGunTerminal disableGun = obj.GetComponentInChildren<PrecursorDisableGunTerminal>();
            obj.SetActive(false);
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(baseClassId);
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            PrecursorDisableGunTerminal disableGun = obj.GetComponentInChildren<PrecursorDisableGunTerminal>();
            obj.SetActive(false);
            
            gameObject.Set(obj);
        }
#endif
    }
}
