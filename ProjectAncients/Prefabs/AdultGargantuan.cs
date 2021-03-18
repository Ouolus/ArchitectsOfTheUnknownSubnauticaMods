using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
    public class AdultGargantuan : GargantuanBase
    {
        public AdultGargantuan(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override float BiteDamage => 2500f;

        public override string AttachBoneName => "AttachBone";

        public override float VehicleDamagePerSecond => 200f;

        public override bool OneShotsPlayer => true;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            Renderer renderer = prefab.SearchChild("Gargantuan.001").GetComponent<SkinnedMeshRenderer>();
            renderer.materials[1].SetFloat("_EmissionLM", 1.5f);
            renderer.materials[1].SetFloat("_EmissionLMNight", 1.5f);
        }

        public override bool CanPerformCyclopsCinematic => true;
    }
}
