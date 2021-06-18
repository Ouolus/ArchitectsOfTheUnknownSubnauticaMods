using ECCLibrary;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase.Teleporter
{
    public class TeleporterFramePrefab : Spawnable
    {
        const string referenceClassId = "f0429c44-a387-42e6-b621-74ba4dd8c2da";
        private string teleporterId;
        private Vector3 teleportPosition;
        private float teleportAngle;
        private bool disablePlatform;

        public TeleporterFramePrefab(string classId, string teleporterId, Vector3 teleportPosition, float teleportAngle, bool disablePlatform) : base(classId, "", "")
        {
            this.teleporterId = teleporterId;
            this.teleportPosition = teleportPosition;
            this.teleportAngle = teleportAngle;
            this.disablePlatform = disablePlatform;
        }

#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab(referenceClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);

            obj.SetActive(false);
            var teleporter = obj.GetComponent<PrecursorTeleporter>();
            teleporter.teleporterIdentifier = teleporterId;
            teleporter.warpToPos = teleportPosition;
            teleporter.warpToAngle = teleportAngle;
            obj.GetComponent<TechTag>().type = TechType.PrecursorTeleporter;
            if (disablePlatform)
            {
                obj.SearchChild("Meshes").transform.GetChild(4).gameObject.SetActive(false);
                Transform collidersParent = obj.SearchChild("Colliders").transform;
                collidersParent.GetChild(0).gameObject.SetActive(false);
                collidersParent.GetChild(5).gameObject.SetActive(false);
                collidersParent.GetChild(6).gameObject.SetActive(false);
            }
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
            var teleporter = obj.GetComponent<PrecursorTeleporter>();
            teleporter.teleporterIdentifier = teleporterId;
            teleporter.warpToPos = teleportPosition;
            teleporter.warpToAngle = teleportAngle;
            obj.GetComponent<TechTag>().type = TechType.PrecursorTeleporter;
            if (disablePlatform)
            {
                obj.SearchChild("Meshes").transform.GetChild(4).gameObject.SetActive(false);
                Transform collidersParent = obj.SearchChild("Colliders").transform;
                collidersParent.GetChild(0).gameObject.SetActive(false);
                collidersParent.GetChild(5).gameObject.SetActive(false);
                collidersParent.GetChild(6).gameObject.SetActive(false);
            }
            gameObject.Set(obj);
        }
#endif

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = TechType.PrecursorTeleporter
        };
    }
}
