using System;
using ArchitectsLibrary.Handlers;
using RotA.Interfaces;
using RotA.Mono.Equipment.IonKnifeActions;
using UnityEngine;

namespace RotA.Mono.Equipment
{
    [RequireComponent(typeof(EnergyMixin))]
    public class IonKnife : PlayerTool
    {
        public DamageType DamageType { get; set; }
        
        public float Damage { get; set; }
        
        public float AttackDistance { get; set; }
        
        public VFXEventTypes VfxEventType { get; set; }
        
        public FMODAsset AttackSound { get; set; }

        public FMODAsset UnderWaterMissSound { get; set; }
        
        public FMODAsset SurfaceMissSound { get; set; }

        // the blade object to disable when the knife is depleted
        public GameObject bladeObject;
        
        IIonKnifeAction _currentAction;
        
        public override string animToolName => TechType.Knife.AsString(true);

        public override void OnToolUseAnim(GUIHand guiHand)
        {
            var position = default(Vector3);
            GameObject obj = null;
            UWE.Utils.TraceFPSTargetPosition(Player.main.gameObject, AttackDistance, ref obj, ref position);
            if (obj == null)
            {
                var volumeUser = Player.main.gameObject.GetComponent<InteractionVolumeUser>();
                if (volumeUser != null && volumeUser.GetMostRecent() != null)
                {
                    obj = volumeUser.GetMostRecent().gameObject;
                }
            }
            if (obj)
            {
                var lm = obj.GetComponentInParent<LiveMixin>();
                if (Knife.IsValidTarget(lm))
                {
                    if (lm)
                    {
                        bool wasAlive = lm.IsAlive();
                        lm.TakeDamage(Damage, position, DamageType);
                        GiveResourceOnDamage(obj, lm.IsAlive(), wasAlive);
                        _currentAction.OnHit(this, lm);
                    }
                    Utils.PlayFMODAsset(AttackSound, transform);
                    var vfxSurface = obj.GetComponent<VFXSurface>();
                    var euler = MainCameraControl.main.transform.eulerAngles + new Vector3(300f, 90f, 0f);
                    VFXSurfaceTypeManager.main.Play(vfxSurface, VfxEventType, position, Quaternion.Euler(euler), Player.main.transform);
                }
                else
                {
                    obj = null;
                }
            }
            if (obj == null && guiHand.GetActiveTarget() == null)
            {
                if (Player.main.IsUnderwater())
                {
                    Utils.PlayFMODAsset(UnderWaterMissSound, transform);
                    return;
                }
                Utils.PlayFMODAsset(SurfaceMissSound, transform);
            }
        }
        
        void GiveResourceOnDamage(GameObject target, bool isAlive, bool wasAlive)
        {
            var tt = CraftData.GetTechType(target);
            HarvestType harvestTypeFromTech = CraftData.GetHarvestTypeFromTech(tt);
            if (tt == TechType.Creepvine)
            {
                GoalManager.main.OnCustomGoalEvent("Cut_Creepvine");
            }
            if (harvestTypeFromTech == HarvestType.DamageAlive && wasAlive || harvestTypeFromTech == HarvestType.DamageDead && !isAlive)
            {
                int cutBonus = 1;
                if (harvestTypeFromTech == HarvestType.DamageAlive && !isAlive)
                {
                    cutBonus += CraftData.GetHarvestFinalCutBonus(tt);
                }
                TechType harvestOutputData = CraftData.GetHarvestOutputData(tt);
                if (harvestOutputData != TechType.None)
                {
                    CraftData.AddToInventory(harvestOutputData, cutBonus, false, false);
                }
            }
        }

        public override bool OnRightHandDown()
        {
            return !energyMixin.IsDepleted();
        }

        #region Event Initializations 
        void OnEnable()
        {
            energyMixin.onPoweredChanged += OnPoweredChanged;
            energyMixin.batterySlot.onAddItem += OnBatteryAdded;
            energyMixin.batterySlot.onRemoveItem += OnBatteryRemoved;
        }

        void OnDisable()
        {
            energyMixin.onPoweredChanged -= OnPoweredChanged;
            energyMixin.batterySlot.onAddItem -= OnBatteryAdded;
            energyMixin.batterySlot.onRemoveItem -= OnBatteryRemoved;
        }

        void OnPoweredChanged(bool powered)
        {
            if (bladeObject.activeSelf != powered)
            {
                bladeObject.SetActive(powered);
            }
        }

        void OnBatteryRemoved(InventoryItem _)
        {
            _currentAction = null;
        }
        #endregion
        
        // also an event initializer but it's useful to keep it unregioned :)
        void OnBatteryAdded(InventoryItem item)
        {
            var tt = item.item.GetTechType();

            // add more if needed
            if (tt == TechType.PrecursorIonCrystal)
                _currentAction = gameObject.EnsureComponent<PrecursorIonCrystalAction>();
            else if (tt == AUHandler.ElectricubeTechType)
                _currentAction = gameObject.EnsureComponent<ElectricubeAction>();
            else if (tt == AUHandler.RedIonCubeTechType)
                _currentAction = gameObject.EnsureComponent<RedIonCubeAction>();
            else if (tt == AUHandler.OmegaCubeTechType)
                _currentAction = gameObject.EnsureComponent<OmegaCubeAction>();
            
            _currentAction?.Initialize(this);
        }
    }
}
