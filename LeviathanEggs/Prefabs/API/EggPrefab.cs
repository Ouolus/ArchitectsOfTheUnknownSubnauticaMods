using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;
using UWE;
using Logger = QModManager.Utility.Logger;

namespace LeviathanEggs.Prefabs.API
{
    public class EggPrefab : Spawnable
    {
        private TechType overridenTechType;
        private GameObject _model;
        public EggPrefab(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {
            OnStartedPatching += () =>
            {
                overridenTechType = TechTypeHandler.AddTechType(MakeATechTypeToOverride.classId,
                    MakeATechTypeToOverride.friendlyName,
                    MakeATechTypeToOverride.description);
            };
            OnFinishedPatching += () =>
            {
                if (AcidImmune)
                    ECCLibrary.ECCHelpers.MakeAcidImmune(this.TechType);
                
                SpriteHandler.RegisterSprite(this.TechType, ItemSprite);
                SpriteHandler.RegisterSprite(overridenTechType, ItemSprite);
                CraftDataHandler.SetItemSize(overridenTechType, this.SizeInInventory);
            };
        }
        public virtual GameObject Model { get; }
        public virtual TechType HatchingCreature { get; }
        public virtual float HatchingTime { get; }
        public virtual Sprite ItemSprite { get; }
        public virtual float Mass => 100f;
        public virtual float MaxHealth => 60f;
        public virtual bool AcidImmune => true;
        public virtual OverrideTechType MakeATechTypeToOverride { get; }

        public sealed override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = this.ClassID, cellLevel = LargeWorldEntity.CellLevel.Medium, localScale = Vector3.one, prefabZUp = false,
            slotType = EntitySlot.Type.Medium, techType = this.TechType
        };

        public override GameObject GetGameObject()
        {
            GameObject prefab = Model;
            var obj = GameObject.Instantiate(prefab);

            obj.EnsureComponent<TechTag>().type = this.TechType;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = this.ClassID;

            var skyApplier = obj.EnsureComponent<SkyApplier>();
            skyApplier.anchorSky = Skies.Auto;
            skyApplier.emissiveFromPower = false;
            skyApplier.dynamic = false;
            skyApplier.renderers = obj.GetAllComponentsInChildren<Renderer>();
            skyApplier.enabled = true;

            obj.EnsureComponent<Pickupable>();
                
            var rb = obj.EnsureComponent<Rigidbody>();
            rb.mass = Mass;
            rb.isKinematic = true;

            var wf = obj.EnsureComponent<WorldForces>();
            wf.useRigidbody = rb;
                
            var liveMixin = obj.EnsureComponent<LiveMixin>();
            liveMixin.health = MaxHealth;
                
            var creatureEgg = obj.EnsureComponent<CreatureEgg>();
            creatureEgg.animator = obj.EnsureComponent<Animator>();
            creatureEgg.hatchingCreature = HatchingCreature;
            creatureEgg.daysBeforeHatching = HatchingTime;
            if (overridenTechType != TechType.None)
                creatureEgg.overrideEggType = overridenTechType;

            return obj;
        }

        public struct OverrideTechType
        {
            public string classId;
            public string friendlyName;
            public string description;
            public OverrideTechType(string classId, string friendlyName, string description)
            {
                this.classId = classId;
                this.friendlyName = friendlyName;
                this.description = description;
            }
        }
    }
}