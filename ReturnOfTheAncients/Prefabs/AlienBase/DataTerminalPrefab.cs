using RotA.Mono.AlienTech;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase
{
    public class DataTerminalPrefab : Spawnable
    {
        private string encyKey;
        private string terminalClassId;
        private string[] pingClassId;
        private string audioClipPrefix;
        private float delay = 5f;
        private TechType[] techToUnlock;
        private TechType techToAnalyze;
        private string subtitlesKey;
        public const string greenTerminalCID = "625d01c2-40b7-4c87-a1cc-493ad6101c34";
        public const string orangeTerminalCID = "dd3bf908-badb-4c8c-a195-eb50be09df63";
        public const string blueTerminalCID = "b629c806-d3cd-4ee4-ae99-7b1359b60049";
        private bool hideSymbol;
        private bool overrideColor;
        private Color fxColor;
        private bool disableInteraction;

        public DataTerminalPrefab(string classId, string encyKey, string[] pingClassId = default, string audioClipPrefix = "DataTerminal1", string terminalClassId = blueTerminalCID, TechType techToAnalyze = TechType.None, TechType[] techToUnlock = null, float delay = 5f, string subtitles = null, bool hideSymbol = false, bool overrideColor = default, Color fxColor = default, bool disableInteraction = false)
            : base(classId, "Data terminal", ".")
        {
            this.encyKey = encyKey;
            this.terminalClassId = terminalClassId;
            this.pingClassId = pingClassId;
            this.audioClipPrefix = audioClipPrefix;
            this.techToUnlock = techToUnlock;
            this.techToAnalyze = techToAnalyze;
            this.delay = delay;
            this.hideSymbol = hideSymbol;
            this.overrideColor = overrideColor;
            this.fxColor = fxColor;
            this.disableInteraction = disableInteraction;
            if (!string.IsNullOrEmpty(subtitles))
            {
                subtitlesKey = classId + "Subtitles";
                LanguageHandler.SetLanguageLine(subtitlesKey, subtitles);
            }
            else
            {
                subtitlesKey = string.Empty;
            }
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };

#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab(terminalClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            StoryHandTarget storyHandTarget = obj.GetComponent<StoryHandTarget>();
            if (!string.IsNullOrEmpty(encyKey))
            {
                storyHandTarget.goal = new Story.StoryGoal(encyKey, Story.GoalType.Encyclopedia, delay);
            }
            else
            {
                storyHandTarget.goal = null;
            }
            if (disableInteraction)
            {
                Object.DestroyImmediate(storyHandTarget);
            }
            obj.SetActive(false);
            if (pingClassId != null && pingClassId.Length > 0)
            {
                foreach (string str in pingClassId)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        DataTerminalUnlockPing unlockPing = obj.AddComponent<DataTerminalUnlockPing>();
                        unlockPing.classId = str;
                        unlockPing.pingTypeName = str;
                    }
                }
            }
            if (!string.IsNullOrEmpty(audioClipPrefix))
            {
                var playAudio = obj.AddComponent<StoryHandTargetPlayAudioClip>();
                playAudio.clipPrefix = audioClipPrefix;
                playAudio.subtitlesKey = subtitlesKey;
            }
            if (techToUnlock != null)
            {
                obj.EnsureComponent<DataTerminalUnlockTech>().techsToUnlock = techToUnlock;
            }
            if (techToAnalyze != TechType.None)
            {
                obj.AddComponent<DataTerminalAnalyzeTech>().techToUnlock = techToAnalyze;
            }
            EditFX(obj);
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(terminalClassId);
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            
            GameObject obj = GameObject.Instantiate(prefab);
            StoryHandTarget storyHandTarget = obj.GetComponent<StoryHandTarget>();
            if (!string.IsNullOrEmpty(encyKey))
            {
                storyHandTarget.goal = new Story.StoryGoal(encyKey, Story.GoalType.Encyclopedia, delay);
            }
            else
            {
            storyHandTarget.goal = null;
            }
                        if (disableInteraction)
            {
                Object.DestroyImmediate(storyHandTarget);
            }
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
                var playAudio = obj.AddComponent<StoryHandTargetPlayAudioClip>();
                playAudio.clipPrefix = audioClipPrefix;
                playAudio.subtitlesKey = subtitlesKey;
            }
            if (techToUnlock != null)
            {
                obj.AddComponent<DataTerminalUnlockTech>().techsToUnlock = techToUnlock;
            }
            if(techToAnalyze != TechType.None)
            {
                obj.AddComponent<DataTerminalAnalyzeTech>().techToUnlock = techToAnalyze;
            }
            EditFX(obj);
            gameObject.Set(obj);
        }
#endif

        private void EditFX(GameObject prefab)
        {
            GameObject fx = prefab.transform.GetChild(2).gameObject;
            if (hideSymbol)
            {
                fx.transform.GetChild(3).gameObject.SetActive(false);
                fx.transform.GetChild(5).gameObject.SetActive(false);
            }
            if (overrideColor)
            {
                foreach (Renderer renderer in fx.GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", fxColor);
                }
            }
        }
    }
}
