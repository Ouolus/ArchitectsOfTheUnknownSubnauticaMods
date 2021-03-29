using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ProjectAncients.Mono;

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

        public override float TentacleSnapSpeed => 8f;

        public override bool CanBeScaredByElectricity => true;

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(2f, 200, 3f);

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 18f, "Lifeforms/Fauna/Leviathans", Mod.assetBundle.LoadAsset<Sprite>("Adult_Popup"), null);

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 31f, 25f, 30f, 17f, 30f);

        public override string GetEncyDesc => "Adult gargantuan text";
        public override string GetEncyTitle => "Gargantuan Leviathan";

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            Renderer renderer = prefab.SearchChild("Gargantuan.001").GetComponent<SkinnedMeshRenderer>();
            UpdateGargTransparentMaterial(renderer.materials[0]);
            UpdateGargTransparentMaterial(renderer.materials[1]);
            UpdateGargTransparentMaterial(renderer.materials[2]);
            UpdateGargSkeletonMaterial(renderer.materials[3]);
            UpdateGargGutsMaterial(renderer.materials[4]);
            var gargPresence = prefab.AddComponent<GargantuanSwimAmbience>();
            gargPresence.swimSoundPrefix = "GargPresence";
            gargPresence.delay = 54f;
        }

        public static void UpdateGargTransparentMaterial(Material material)
        {
            material.SetInt("_ZWrite", 1);
            material.SetFloat("_Fresnel", 1);
        }

        public static void UpdateGargSkeletonMaterial(Material material)
        {
            material.SetFloat("_Fresnel", 1);
            material.SetFloat("_SpecInt", 50);
            material.SetFloat("_GlowStrength", 6f);
            material.SetFloat("_GlowStrengthNight", 6f);
        }

        public static void UpdateGargGutsMaterial(Material material)
        {
            material.EnableKeyword("MARMO_ALPHA_CLIP");
            material.SetFloat("_Fresnel", 1f);
            material.SetFloat("_SpecInt", 50);
            material.SetFloat("_GlowStrength", 10f);
            material.SetFloat("_GlowStrengthNight", 10f);

        }

        public override bool CanPerformCyclopsCinematic => true;
    }
}
