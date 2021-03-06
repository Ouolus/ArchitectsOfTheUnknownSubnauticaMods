using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public abstract class AlienBaseSpawner : MonoBehaviour
    {
        public const string box2x2x2 = "2f996953-bd0a-48e2-8aae-57dd6b6a2507";
        public const string box2x1x2 = "96edb813-c7c7-4c44-9bf4-5f1975edeff8";
        public const string box2x1x2_variant1 = "5ecd846d-1629-4d3c-9119-f4f16179a408";
        public const string box2x1x2_variant2 = "fa5e644a-777b-4b54-a92a-0241752b8e06";
        public const string box2x1x2_variant3 = "3c9344a2-4715-4773-9c58-dc0437002278";
        public const string light_small = "742b410c-14d4-42c6-ac84-0e2bcaff09c1";
        public const string starfish = "d571d3dc-6229-430e-a513-0dcafc2c41f3";
        public const string outpost1 = "c5512e00-9959-4f57-98ae-9a9962976eaa";
        public const string outpost2 = "$542aaa41-26df-4dba-b2bc-3fa3aa84b777";
        public const string pedestal_empty1 = "78009225-a9fa-4d21-9580-8719a3368373";
        public const string pedestal_empty2 = "3bbf8830-e34f-43a1-bbb3-743c7e6860ac";
        public const string light_rectangle = "3bbf8830-e34f-43a1-bbb3-743c7e6860ac";
        public const string column = "640f57a6-6436-4132-a9bb-d914f3e19ef5";
        public const string pedestal_ionCrystal = "7e1e5d12-7169-4ff9-abcd-520f11196764";
        public const string tabletDoor_orange = "83a70a58-f7da-4f18-b9b2-815dc8a7ffb4";
        public const string prop_microscope = "a30d0115-c37e-40ec-a5d9-318819e94f81";
        public const string prop_specimens = "da8f10dd-e181-4f28-bf98-9b6de4a9976a";
        public const string prop_claw = "6a01a336-fb46-469a-9f7d-1659e07d11d7";
        public const string doorway = "db5a85f5-a5fe-43f8-b71e-7b1f0a8636fe";
        public const string genericDataTerminal = "b629c806-d3cd-4ee4-ae99-7b1359b60049";

        private List<GameObject> spawnedChildren;

        private void Start()
        {
            spawnedChildren = new List<GameObject>();
            ConstructBase();
            foreach(GameObject obj in spawnedChildren)
            {
                obj.transform.parent = null;
                LargeWorld.main.streamer.cellManager.RegisterEntity(obj.GetComponent<LargeWorldEntity>());
            }
        }

        /// <summary>
        /// Override this method to spawn the prefabs;
        /// </summary>
        public abstract void ConstructBase();

        public void SpawnPrefab(string classId, Vector3 localPosition)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.localRotation = Quaternion.identity;
                spawnedObject.transform.localScale = Vector3.one;
                spawnedObject.SetActive(true);
                spawnedChildren.Add(spawnedObject);
            }
        }

        public void SpawnPrefab(string classId, Vector3 localPosition, Vector3 localRotation)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.localRotation = Quaternion.LookRotation(localRotation);
                spawnedObject.transform.localScale = Vector3.one;
                spawnedObject.SetActive(true);
                spawnedChildren.Add(spawnedObject);
            }
        }

        public void SpawnPrefab(string classId, Vector3 localPosition, Vector3 localRotation, Vector3 scale)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.localRotation = Quaternion.LookRotation(localRotation);
                spawnedObject.transform.localScale = scale;
                spawnedObject.SetActive(true);
                spawnedChildren.Add(spawnedObject);
            }
        }

        public void SpawnPrefabsArray(string classId, float spacing, Vector3 size, Vector3 individualScale, Vector3 offset = default)
        {
            for(int x = 0; x < size.x; x++)
            {
                for(int y = 0; y < size.y; y++)
                {
                    for(int z = 0; z < size.z; z++)
                    {
                        Vector3 rawPosition = new Vector3(x, y, z);
                        Vector3 spacedPosition = Vector3.Scale(rawPosition, spacing * individualScale);
                        Vector3 positionWithOffset = spacedPosition - (Vector3.Scale(size, (spacing * individualScale) / 2f)) + offset - (individualScale * 0.5f);
                        SpawnPrefab(classId, positionWithOffset, Quaternion.identity.eulerAngles, individualScale);
                    }
                }
            }
        }
    }
}
