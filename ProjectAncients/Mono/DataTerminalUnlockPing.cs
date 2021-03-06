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

        public void OnStoryHandTarget()
        {
            if(PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject ping = Instantiate(prefab);
                ping.transform.position = pos;
                ping.SetActive(true);
            }
        }
    }
}
