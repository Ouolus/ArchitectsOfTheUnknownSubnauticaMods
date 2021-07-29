using RotA.Mono.AlienTech;
using UnityEngine;

namespace RotA.Prefabs.AlienBase
{
    public class SecretBaseTerminal : DataTerminalPrefab
    {
        //this massive constructor lol
        public SecretBaseTerminal(string classId, string encyKey, string[] pingClassId = null, string audioClipPrefix = "DataTerminal1", string terminalClassId = "b629c806-d3cd-4ee4-ae99-7b1359b60049", TechType techToAnalyze = TechType.None, TechType[] techToUnlock = null, float delay = 5, string subtitles = null, bool hideSymbol = false, bool overrideColor = false, Color fxColor = default, bool disableInteraction = false, string achievement = null) : base(classId, encyKey, pingClassId, audioClipPrefix, terminalClassId, techToAnalyze, techToUnlock, delay, subtitles, hideSymbol, overrideColor, fxColor, disableInteraction, achievement)
        {

        }

        protected override void CustomizePrefab(GameObject prefab)
        {
            prefab.AddComponent<DataTerminalSecretCutscene>();
        }
    }
}
