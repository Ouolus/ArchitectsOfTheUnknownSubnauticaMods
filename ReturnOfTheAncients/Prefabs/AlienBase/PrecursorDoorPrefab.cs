using ECCLibrary;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase
{
    public class PrecursorDoorPrefab : Spawnable
    {
        GameObject _processedPrefab;
        string terminalClassId;
        bool overrideTerminalPosition;
        Vector3 terminalPosition;
        Vector3 terminalRotation;
        string rootClassId;
        string doorKey;
        private bool voidInteriorDoor;

        public PrecursorDoorPrefab(string classId, string displayName, string terminalClassId, string doorKey, bool overrideTerminalPosition = false, Vector3 terminalLocalPosition = default, Vector3 terminalLocalRotation = default, string rootPrefabClassId = "b816abb4-8f6c-4d70-b4c5-662e69696b23", bool replaceForcefield = false)
            : base(classId, displayName, ".")
        {
            this.terminalClassId = terminalClassId;
            this.overrideTerminalPosition = overrideTerminalPosition;
            this.terminalPosition = terminalLocalPosition;
            this.terminalRotation = terminalLocalRotation;
            this.rootClassId = rootPrefabClassId;
            this.doorKey = doorKey;
            this.voidInteriorDoor = replaceForcefield;
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = TechType
        };

#if SN1
        public override GameObject GetGameObject()
        {
            if (_processedPrefab)
            {
                _processedPrefab.SetActive(true);
                return _processedPrefab;
            }
            
            PrefabDatabase.TryGetPrefab(rootClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            GameObject terminalPrefabPlaceholder = obj.SearchChild("KeyTerminal(Placeholder)", ECCStringComparison.Contains);
            terminalPrefabPlaceholder.GetComponent<PrefabPlaceholder>().prefabClassId = terminalClassId;
            if (overrideTerminalPosition)
            {
                terminalPrefabPlaceholder.transform.localPosition = terminalPosition;
                terminalPrefabPlaceholder.transform.localEulerAngles = terminalRotation;
            }
            if (string.IsNullOrEmpty(doorKey))
            {
                PrecursorGlobalKeyActivator globalKeyActivator = obj.GetComponent<PrecursorGlobalKeyActivator>();
                if (globalKeyActivator)
                {
                    Object.Destroy(globalKeyActivator);
                }
            }
            else
            {
                PrecursorGlobalKeyActivator globalKeyActivator = obj.EnsureComponent<PrecursorGlobalKeyActivator>();
                globalKeyActivator.doorActivationKey = doorKey;
            }
            if (obj.transform.childCount >= 1)
            {
                Transform firstChild = obj.transform.GetChild(0);
                if (firstChild != null)
                {
                    if (firstChild.name == "pedestal")
                    {
                        GameObject.DestroyImmediate(firstChild.gameObject);
                    }
                }
            }
            if (voidInteriorDoor)
            {
                obj.SearchChild("BeachEntry(Placeholder)", ECCStringComparison.Contains).GetComponent<PrefabPlaceholder>().prefabClassId = Mod.voidInteriorForcefield.ClassID;
            }
            obj.SetActive(false);
            
            _processedPrefab = GameObject.Instantiate(obj);
            _processedPrefab.SetActive(false);
            return obj;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (_processedPrefab)
            {
                _processedPrefab.SetActive(true);
                gameObject.Set(_processedPrefab);
                yield break;
            }

            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(rootClassId);
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            
            GameObject obj = GameObject.Instantiate(prefab);
            GameObject terminalPrefabPlaceholder = obj.SearchChild("KeyTerminal(Placeholder)", ECCStringComparison.Contains);
            terminalPrefabPlaceholder.GetComponent<PrefabPlaceholder>().prefabClassId = terminalClassId;
            if (overrideTerminalPosition)
            {
                terminalPrefabPlaceholder.transform.localPosition = terminalPosition;
                terminalPrefabPlaceholder.transform.localEulerAngles = terminalRotation;
            }
            if (string.IsNullOrEmpty(doorKey))
            {
                PrecursorGlobalKeyActivator globalKeyActivator = obj.GetComponent<PrecursorGlobalKeyActivator>();
                if (globalKeyActivator)
                {
                    Object.Destroy(globalKeyActivator);
                }
            }
            else
            {
                PrecursorGlobalKeyActivator globalKeyActivator = obj.EnsureComponent<PrecursorGlobalKeyActivator>();
                globalKeyActivator.doorActivationKey = doorKey;
            }
            if (obj.transform.childCount >= 1)
            {
                Transform firstChild = obj.transform.GetChild(0);
                if (firstChild != null)
                {
                    if (firstChild.name == "pedestal")
                    {
                        GameObject.DestroyImmediate(firstChild.gameObject);
                    }
                }
            }
            if (voidInteriorDoor)
            {
                obj.SearchChild("BeachEntry(Placeholder)", ECCStringComparison.Contains).GetComponent<PrefabPlaceholder>().prefabClassId = Mod.voidInteriorForcefield.ClassID;
            }
            obj.SetActive(false);
            
            _processedPrefab = GameObject.Instantiate(obj);
            _processedPrefab.SetActive(false);
            gameObject.Set(obj);
        }
#endif
    }
}
