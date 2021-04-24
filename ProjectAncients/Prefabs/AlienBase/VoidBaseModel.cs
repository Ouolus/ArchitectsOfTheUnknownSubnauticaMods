using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ProjectAncients.Mono.AlienTech;
using Story;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class VoidBaseModel : GenericWorldPrefab
    {
        public VoidBaseModel(string classId, string friendlyName, string description, GameObject model, UBERMaterialProperties materialProperties, LargeWorldEntity.CellLevel cellLevel) : base(classId, friendlyName, description, model, materialProperties, cellLevel)
        {
        }

        private StoryGoal approachBaseGoal = new StoryGoal("ApproachVoidBase", Story.GoalType.Story, 0f);

        public override void CustomizePrefab()
        {
            prefab.EnsureComponent<VoidBaseReveal>();
            var voTrigger1 = prefab.AddComponent<AlienBasePlayerTrigger>();
            voTrigger1.onTrigger = new AlienBasePlayerTrigger.OnTriggered(OnTrigger1);
            voTrigger1.triggerObject = prefab.SearchChild("VOTrigger1");
        }

        public void OnTrigger1(GameObject obj)
        {
            if(!StoryGoalManager.main.OnGoalComplete(approachBaseGoal.key))
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("VoidBaseEncounter"), "VoidBaseEncounter", "Detecting leviathan-class lifeforms beyond this doorway. Approach with caution.");
            }
        }
    }
}
