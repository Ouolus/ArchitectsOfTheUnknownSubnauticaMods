using ECCLibrary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace ProjectAncients.Mono
{
    public class DataTerminalUnlockPing : MonoBehaviour
    {
        public string classId;
        public Vector3 pos;
        public string pingTypeName;

        public void OnStoryHandTarget()
        {
            foreach (SignalPingDelayedInitialize spawnedPing in SignalPingDelayedInitialize.spawnedPings) {
                {
                    if (spawnedPing == null)
                    {
                        continue;
                    }
                    if (spawnedPing.pingTypeName == pingTypeName)
                    {
                        return;
                    }
                }
            }
#if SN1
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject pingObj = Instantiate(prefab);
                pingObj.transform.position = pos;
                pingObj.SetActive(true);
                LargeWorld.main.streamer.cellManager.RegisterCellEntity(pingObj.GetComponent<LargeWorldEntity>());
            }
#elif SN1_exp
            AddressablesUtility.LoadAsync<GameObject>(classId).Completed += (_) =>
            {
                GameObject prefab = _.Result;

                GameObject pingObj = Instantiate(prefab);
                pingObj.transform.position = pos;
                pingObj.SetActive(true);
                LargeWorld.main.streamer.cellManager.RegisterCellEntity(pingObj.GetComponent<LargeWorldEntity>());
            };
#endif
        }
    }
}
