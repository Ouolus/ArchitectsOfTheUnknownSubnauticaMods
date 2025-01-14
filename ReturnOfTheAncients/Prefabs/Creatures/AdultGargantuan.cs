﻿using ArchitectsLibrary.API;
using ECCLibrary;
using RotA.Mono;
using RotA.Mono.Creatures.GargEssentials;
using UnityEngine;

namespace RotA.Prefabs.Creatures
{
    public class AdultGargantuan : GargantuanBase
    {
        public AdultGargantuan(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override float BiteDamage => 5000f;

        public override string AttachBoneName => "AttachBone";

        public override float VehicleDamagePerSecond => 70;

        public override bool OneShotsPlayer => true;

        public override float TentacleSnapSpeed => 8f;

        public override bool CanBeScaredByElectricity => true;

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(2f, 100, 3f);

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 10f, Mod.modEncyPath_gargantuan, Mod.gargAssetBundle.LoadAsset<Sprite>("Adult_Popup"), Mod.gargAssetBundle.LoadAsset<Texture2D>("Adult_Ency"));

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 45f, 25f, 30f, 17f, 30f);

        public override float MaxVelocityForSpeedParameter => 40f;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(120f, 30f, 120f), 26f, 10f, 0.1f);

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(1f, false, 30f);

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(20000, 40000, 100000);

        public override float TurnSpeed => 0.1f;

        public override (float, float) RoarSoundMinMax => (75f, 1000f);

        public override bool RoarDoesDamage => true;

        public override bool AdvancedCollisions => false;

        public override bool HasEyeTracking => false;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            Renderer mainRenderer = prefab.SearchChild("Gargantuan.004").GetComponent<SkinnedMeshRenderer>();
            Renderer eyeRenderer = prefab.SearchChild("Gargantuan.002").GetComponent<SkinnedMeshRenderer>();
            Renderer insideRenderer = prefab.SearchChild("Gargantuan.003").GetComponent<SkinnedMeshRenderer>();
            UpdateGargTransparentMaterial(mainRenderer.materials[0]);
            UpdateGargTransparentMaterial(mainRenderer.materials[1]);
            UpdateGargTransparentMaterial(mainRenderer.materials[2]);
            UpdateGargSolidMaterial(mainRenderer.materials[3]);
            UpdateGargSkeletonMaterial(insideRenderer.materials[0]);
            UpdateGargGutsMaterial(insideRenderer.materials[1]);
            UpdateGargEyeMaterial(eyeRenderer.materials[0]);
            var gargPresence = prefab.AddComponent<GargantuanSwimAmbience>();
            gargPresence.swimSoundPrefix = "GargPresence";
            gargPresence.delay = 54f;
            components.locomotion.maxAcceleration = 45f;
            components.swimRandom.swimForward = 1f;
            prefab.GetComponent<StayAtLeashPosition>().swimVelocity = 20f;

            components.locomotion.forwardRotationSpeed = 0.05f;
            components.locomotion.upRotationSpeed = 1f;

            prefab.AddComponent<GargantuanEncounterPDA>();

            var avoidObstacles = prefab.GetComponent<AvoidObstacles>();
            avoidObstacles.avoidanceDistance = 100f;
            avoidObstacles.avoidanceIterations = 20;
            avoidObstacles.scanDistance = 20;
            avoidObstacles.scanInterval = 0.2f;
            avoidObstacles.scanDistance = 100f;
            avoidObstacles.scanRadius = 100f;

            foreach(Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer.gameObject.name == "GargEyeGloss")
                {
                    renderer.material.SetFloat("_Fresnel", 0.69f);
                    renderer.material.SetFloat("_Shininess", 6.14f);
                    renderer.material.SetFloat("_SpecInt", 30f);
                }
            }

            components.worldForces.waterDepth = -30f;
            components.worldForces.aboveWaterGravity = 25f;
        }

        public static void UpdateGargTransparentMaterial(Material material)
        {
            material.SetInt("_ZWrite", 1);
            material.SetFloat("_Fresnel", 1);
        }

        public static void UpdateGargSolidMaterial(Material material)
        {
            material.SetFloat("_Fresnel", 0.6f);
            material.SetFloat("_Shininess", 8f);
            material.SetFloat("_SpecInt", 30);
            material.SetFloat("_EmissionLM", 0.1f);
            material.SetFloat("_EmissionLMNight", 0.1f);
        }

        public static void UpdateGargEyeMaterial(Material material)
        {
            material.SetFloat("_SpecInt", 15f);
            material.SetFloat("_GlowStrength", 0.70f);
            material.SetFloat("_GlowStrengthNight", 0.70f);
        }

        public static void UpdateGargSkeletonMaterial(Material material)
        {
            material.SetFloat("_Fresnel", 1);
            material.SetFloat("_SpecInt", 50);
            material.SetFloat("_GlowStrength", 2.5f);
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

        public override float EyeFov => -1f;

        public override bool DoesScreenShake => true;

        public override float CloseRoarThreshold => 350f;

        public override GargGrabFishMode GrabFishMode => GargGrabFishMode.LeviathansOnlyAndSwallow;
    }
}
