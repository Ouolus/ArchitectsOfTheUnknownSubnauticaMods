using ArchitectsLibrary.API;
using ECCLibrary;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase
{
    //For the large doors inside the void base
    public class VoidInteriorForcefield : Spawnable
    {
        public VoidInteriorForcefield() : base("VoidInteriorForcefield", LanguageSystem.Default, LanguageSystem.Default)
        {
        }

#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab("2d72ad6c-d30d-41be-baa7-0c1dba757b7c", out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            obj.name = "VoidInteriorForcefield";
            GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Walk", ECCStringComparison.Equals));
            GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Swim", ECCStringComparison.Equals));
            obj.SetActive(false);
            return obj;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync("2d72ad6c-d30d-41be-baa7-0c1dba757b7c");
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            
            GameObject obj = GameObject.Instantiate(prefab);
            obj.name = "VoidInteriorForcefield";
            GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Walk", ECCStringComparison.Equals));
            GameObject.DestroyImmediate(obj.SearchChild("DoorSetMotorModeCollider_Swim", ECCStringComparison.Equals));
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
