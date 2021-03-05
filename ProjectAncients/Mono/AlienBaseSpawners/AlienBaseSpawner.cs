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
        public const string box2x2 = "2f996953-bd0a-48e2-8aae-57dd6b6a2507";
        public const string smallLight = "742b410c-14d4-42c6-ac84-0e2bcaff09c1";
        public const string starfish = "d571d3dc-6229-430e-a513-0dcafc2c41f3";

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
            if(PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.rotation = Quaternion.identity;
                spawnedChildren.Add(spawnedObject);
            }
        }

        public void SpawnPrefab(string classId, Vector3 localPosition, Vector3 localRotation)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.localPosition = localRotation;
                spawnedChildren.Add(spawnedObject);
            }
        }
    }
}
