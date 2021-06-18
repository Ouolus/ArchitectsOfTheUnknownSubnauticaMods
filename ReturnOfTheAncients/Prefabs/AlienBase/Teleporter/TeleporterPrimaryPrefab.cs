using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase.Teleporter
{
    public class TeleporterPrimaryPrefab : Spawnable
    {
        const string referenceClassId = "63e69987-7d34-41f0-aab9-1187ea06e740";
        private TeleporterFramePrefab frame;

        public TeleporterPrimaryPrefab(string classId, string teleporterId, Vector3 teleportPosition, float teleportAngle, bool disablePlatform) : base(classId, "", "")
        {
            frame = new TeleporterFramePrefab(string.Format("{0}Frame", classId), teleporterId, teleportPosition, teleportAngle, disablePlatform);
            frame.Patch();
        }

#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab(referenceClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);

            obj.SetActive(false);
            var prefabPlaceholder = obj.GetComponent<PrefabPlaceholdersGroup>();
            prefabPlaceholder.prefabPlaceholders[0].prefabClassId = frame.ClassID;
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(referenceClassId);
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            var prefabPlaceholder = obj.GetComponent<PrefabPlaceholdersGroup>();
            prefabPlaceholder.prefabPlaceholders[0].prefabClassId = frame.ClassID;

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
