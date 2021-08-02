using RotA.Mono.AlienTech;
using UnityEngine;

namespace RotA.Prefabs.AlienBase.DataTerminal
{
    public class SecretBaseTerminal : DataTerminalPrefab
    {
        public SecretBaseTerminal(string classId, DataTerminal dataTerminal)
            :base(classId, dataTerminal)
        {}

        protected override void CustomizePrefab(GameObject prefab)
        {
            prefab.AddComponent<DataTerminalSecretCutscene>();
        }
    }
}
