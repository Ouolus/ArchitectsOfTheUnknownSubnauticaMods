using SMLHelper.V2.Assets;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;
using UWE;
using System.Collections;

namespace ProjectAncients.Prefabs.AlienBase
{
    //For the small doors inside the void base
    public class VoidInteriorForcefield : Spawnable
    {
        public VoidInteriorForcefield() : base("VoidInteriorForcefield", "Forcefield", ".")
        {
        }

#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab("18f2fbaa-78df-46a9-805a-79ac4d942125", out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            //GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Walk", ECCStringComparison.Equals));
            //GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Swim", ECCStringComparison.Equals));
            obj.SetActive(false);
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync("2d72ad6c-d30d-41be-baa7-0c1dba757b7c");
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            
            GameObject obj = GameObject.Instantiate(prefab);
            //GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Walk", ECCStringComparison.Equals));
            //GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Swim", ECCStringComparison.Equals));
            obj.SetActive(false);
            
            gameObject.Set(obj);
        }
#endif

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };
    }
}
