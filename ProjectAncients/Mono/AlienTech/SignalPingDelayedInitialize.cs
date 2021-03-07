using ECCLibrary.Internal;
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

        public static List<SignalPingDelayedInitialize> spawnedPings = new List<SignalPingDelayedInitialize>();

        void Start()
        {
            Refresh();
            spawnedPings.Add(this);
        }

        void Refresh()
        {
            var signal = GetComponent<SignalPing>();
                signal.pos = position;
                transform.position = position;
                signal.descriptionKey = label;
                signal.UpdateLabel();
        }

        void OnDestroy()
        {
            spawnedPings.Remove(this);
        }
    }
}
