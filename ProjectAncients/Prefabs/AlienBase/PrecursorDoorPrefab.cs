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
        bool overrideTerminalPosition;
        Vector3 terminalPosition;
        Vector3 terminalRotation;
        string rootClassId;
        string doorKey;
        private bool voidInteriorDoor;

        public PrecursorDoorPrefab(string classId, string displayName, string terminalClassId, string doorKey, bool overrideTerminalPosition = false, Vector3 terminalLocalPosition = default, Vector3 terminalLocalRotation = default, string rootPrefabClassId = "b816abb4-8f6c-4d70-b4c5-662e69696b23", bool voidInteriorDoor = false)
            : base(classId, displayName, ".")
        {
            this.terminalClassId = terminalClassId;
            this.overrideTerminalPosition = overrideTerminalPosition;
            this.terminalPosition = terminalLocalPosition;
            this.terminalRotation = terminalLocalRotation;
            this.rootClassId = rootPrefabClassId;
            this.doorKey = doorKey;
            this.voidInteriorDoor = voidInteriorDoor;
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = TechType
        };

        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab(rootClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            GameObject terminalPrefabPlaceholder = obj.SearchChild("PurpleKeyTerminal", ECCStringComparison.Contains);
            terminalPrefabPlaceholder.GetComponent<PrefabPlaceholder>().prefabClassId = terminalClassId;
            if (overrideTerminalPosition)
            {
                terminalPrefabPlaceholder.transform.localPosition = terminalPosition;
                terminalPrefabPlaceholder.transform.localEulerAngles = terminalRotation;
            }
            PrecursorGlobalKeyActivator globalKeyActivator = prefab.GetComponent<PrecursorGlobalKeyActivator>();
            if (globalKeyActivator)
            {
                globalKeyActivator.doorActivationKey = doorKey;
            }
            if(prefab.transform.childCount >= 1)
            {
                Transform firstChild = prefab.transform.GetChild(0);
                if (firstChild != null)
                {
                    if(firstChild.name == "pedestal")
                    {
                        GameObject.Destroy(firstChild.gameObject);
                    }
                }
            }
            if (voidInteriorDoor)
            {
                obj.SearchChild("Precursor_Gun_BeachEntry(Placeholder)", ECCStringComparison.Equals).GetComponent<PrefabPlaceholder>().prefabClassId = Mod.voidInteriorForcefield.ClassID;
            }
            obj.SetActive(false);
            return obj;
        }
    }
}
