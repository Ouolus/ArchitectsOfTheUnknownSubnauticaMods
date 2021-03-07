using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectAncients.Mono
{
    public class SignalPingDelayedInitialize : MonoBehaviour
    {
        public Vector3 position;
        public string pingTypeName;
        public string label;

        public static List<string> spawnedPingTypes = new List<string>();

        void Start()
        {
            Refresh();
        }

        void Refresh()
        {
            var signal = GetComponent<SignalPing>();
            signal.pos = position;
            transform.position = position;
            signal.descriptionKey = label;
            signal.UpdateLabel();
            signal.pingInstance.SetColor(3);
            spawnedPingTypes.Add(pingTypeName);
        }

        void OnDisable()
        {
            spawnedPingTypes.Remove(pingTypeName);
        }
    }
}
