﻿using ArchitectsLibrary.API;
using ECCLibrary;
using RotA.Mono;
using UnityEngine;

namespace RotA.Prefabs.Creatures
{
    public class GargantuanJuvenile : GargantuanBase
    {
        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(80f, 30f, 80f), 10f, 2.5f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 100f);

        public override bool OneShotsPlayer => false;

        public override bool CanBeScaredByElectricity => true;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 9f, Mod.modEncyPath_gargantuan, Mod.gargAssetBundle.LoadAsset<Sprite>("Juvenile_Popup"), Mod.gargAssetBundle.LoadAsset<Texture2D>("Juvenile_Ency"));

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 20f, 15f, 20f, 17f, 30f);

        public override float EyeFov => 0.35f;

        public override float MaxVelocityForSpeedParameter => 15f;

        public override EcoTargetType EcoTargetType => EcoTargetType.CuteFish;

        public override string GetEncyTitle => LanguageSystem.Get("Ency_GargantuanJuvenile");
        
        public override string GetEncyDesc => LanguageSystem.Get("EncyDesc_GargantuanJuvenile");

        public GargantuanJuvenile(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override float VehicleDamagePerSecond => 30f;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            prefab.AddComponent<GargantuanEncounterPDA>();
        }

        public override GargGrabFishMode GrabFishMode => GargGrabFishMode.LeviathansOnlyNoSwallow;

        public override string AttachBoneName => "AttachBone";
    }
}
