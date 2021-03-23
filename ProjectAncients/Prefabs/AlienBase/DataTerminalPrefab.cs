using SMLHelper.V2.Assets;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;
using UWE;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class DataTerminalPrefab : Spawnable
    {
        private string encyKey;
        private string terminalClassId;
        private string[] pingClassId;
        private string audioClipPrefix;
        private Vector3 pingPosition;
        private float delay = 7f;
        private TechType techToUnlock;
        public const string greenTerminalCID = "625d01c2-40b7-4c87-a1cc-493ad6101c34";
        public const string orangeTerminalCID = "dd3bf908-badb-4c8c-a195-eb50be09df63";
        public const string blueTerminalCID = "b629c806-d3cd-4ee4-ae99-7b1359b60049";

        public DataTerminalPrefab(string classId, string encyKey, string[] pingClassId = default, string audioClipPrefix = "DataTerminal1", string terminalClassId = blueTerminalCID, TechType techToUnlock = TechType.None)
            : base(classId, "Data terminal", ".")
        {
            this.encyKey = encyKey;
            this.terminalClassId = terminalClassId;
            this.pingClassId = pingClassId;
            this.audioClipPrefix = audioClipPrefix;
            this.techToUnlock = techToUnlock;
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };

        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab(terminalClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            StoryHandTarget storyHandTarget = obj.GetComponent<StoryHandTarget>();
            storyHandTarget.goal = new Story.StoryGoal(encyKey, Story.GoalType.Encyclopedia, delay);
            obj.SetActive(false);
            if (pingClassId != null && pingClassId.Length > 0)
            {
                foreach (string str in pingClassId)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        DataTerminalUnlockPing unlockPing = obj.AddComponent<DataTerminalUnlockPing>();
                        unlockPing.classId = str;
                        unlockPing.pos = pingPosition;
                        unlockPing.pingTypeName = str;
                    }
                }
            }
            if (!string.IsNullOrEmpty(audioClipPrefix))
            {
                obj.AddComponent<StoryHandTargetPlayAudioClip>().clipPrefix = audioClipPrefix;
            }
            if(techToUnlock != TechType.None)
            {
                obj.AddComponent<DataTerminalUnlockTech>().techToUnlock = techToUnlock;
            }
            return obj;
        }
    }
}
