using SMLHelper.V2.Assets;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;
using UWE;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class PrecursorDoorPrefab : Spawnable
    {
        string terminalClassId;

        public PrecursorDoorPrefab(string classId, string displayName, string terminalClassId)
            : base(classId, displayName, ".")
        {
            this.terminalClassId = terminalClassId;
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };

        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab("b816abb4-8f6c-4d70-b4c5-662e69696b23", out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            GameObject terminalPrefabPlaceholder = obj.SearchChild("PurpleKeyTerminal", ECCStringComparison.Contains);
            terminalPrefabPlaceholder.GetComponent<PrefabPlaceholder>().prefabClassId = terminalClassId;
            obj.SetActive(false);
            return obj;
        }
    }
}
