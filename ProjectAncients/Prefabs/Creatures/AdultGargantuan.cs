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

        public override float BiteDamage => 5000f;

        public override string AttachBoneName => "AttachBone";

        public override float VehicleDamagePerSecond => 60f;

        public override bool OneShotsPlayer => true;

        public override float TentacleSnapSpeed => 8f;

        public override bool CanBeScaredByElectricity => true;

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(2f, 200, 3f);

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 10f, "Lifeforms/Fauna/Leviathans", Mod.assetBundle.LoadAsset<Sprite>("Adult_Popup"), Mod.assetBundle.LoadAsset<Texture2D>("Adult_Ency"));

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 45f, 25f, 30f, 17f, 30f);

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(120f, 30f, 120f), 8f, 10f, 0.1f);

        public override string GetEncyTitle => "Gargantuan Leviathan";
        public override string GetEncyDesc => "Adult gargantuan text";

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            Renderer renderer = prefab.SearchChild("Gargantuan.001").GetComponent<SkinnedMeshRenderer>();
            UpdateGargTransparentMaterial(renderer.materials[0]);
            UpdateGargTransparentMaterial(renderer.materials[1]);
            UpdateGargTransparentMaterial(renderer.materials[2]);
            UpdateGargSolidMaterial(renderer.materials[3]);
            UpdateGargSkeletonMaterial(renderer.materials[4]);
            UpdateGargGutsMaterial(renderer.materials[5]);
            var gargPresence = prefab.AddComponent<GargantuanSwimAmbience>();
            gargPresence.swimSoundPrefix = "GargPresence";
            gargPresence.delay = 54f;
            components.locomotion.maxAcceleration = 45f;
            components.swimRandom.swimForward = 1f;
            prefab.GetComponent<StayAtLeashPosition>().swimVelocity = 20f;

            prefab.AddComponent<GargantuanEncounterPDA>();
        }

        public static void UpdateGargTransparentMaterial(Material material)
        {
            material.SetInt("_ZWrite", 1);
            material.SetFloat("_Fresnel", 1);
        }

        public static void UpdateGargSolidMaterial(Material material)
        {
            material.SetFloat("_Fresnel", 1);
            material.SetFloat("_SpecInt", 25);
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

        public override void ApplyAggression()
        {
            MakeAggressiveTo(120f, 6, EcoTargetType.Shark, 0.2f, 0.5f);
            MakeAggressiveTo(60f, 2, EcoTargetType.Whale, 0.23f, 2.3f);
            MakeAggressiveTo(200f, 7, EcoTargetType.Leviathan, 0.3f, 3f);
            MakeAggressiveTo(200f, 7, Mod.superDecoyTargetType, 0f, 5f);
        }

        public override bool CanPerformCyclopsCinematic => true;

        public override float EyeFov => 1f;

        public override bool DoesScreenShake => true;
    }
}
