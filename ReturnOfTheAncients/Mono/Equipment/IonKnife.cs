using System;
using ArchitectsLibrary.API;
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

        // the blade object to disable when the knife is depleted
        public GameObject bladeObject;

        public int ResourceBonus { get; set; }
        
        IIonKnifeAction _currentAction;

        private FMODAsset underWaterMissSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/swing");

        private FMODAsset surfaceMissSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/swing_surface");

        private FMODAsset hitSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/heat_hit");

        private FMODAsset bladeSpawnSound = SNAudioEvents.GetFmodAsset("event:/env/prec_light_on_2");

        private FMOD_CustomLoopingEmitter switchModeEmitter;

        private float timeStopSwitchMode;

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
                    Utils.PlayFMODAsset(hitSound, transform);
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
                    Utils.PlayFMODAsset(underWaterMissSound, transform);
                    return;
                }
                Utils.PlayFMODAsset(surfaceMissSound, transform);
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
                int cutBonus = 1 + ResourceBonus;
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

        void Update()
        {
            if (Time.time > timeStopSwitchMode && switchModeEmitter != null && switchModeEmitter.playing)
            {
                switchModeEmitter.Stop();
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
            var currentBattery = energyMixin.batterySlot.storedItem;
            if (currentBattery != null && currentBattery.item != null)
            {
                SetIonCubeType(currentBattery.item.GetTechType());
            }
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
                if (powered == true)
                {
                    Utils.PlayFMODAsset(bladeSpawnSound, transform);
                }
                else
                {
                    PlaySwitchSound(null);
                }
            }
        }

        public void PlaySwitchSound(string soundPath, float length = 2f)
        {
            if (string.IsNullOrEmpty(soundPath))
            {
                switchModeEmitter.Stop();
                return;
            }
            switchModeEmitter = gameObject.EnsureComponent<FMOD_CustomLoopingEmitter>();
            if (switchModeEmitter.playing) switchModeEmitter.Stop();
            switchModeEmitter.SetAsset(SNAudioEvents.GetFmodAsset(soundPath));
            switchModeEmitter.Play();
            timeStopSwitchMode = Time.time + length;
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
            SetIonCubeType(tt);
        }

        private void SetIonCubeType(TechType tt)
        {
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

            OnPoweredChanged(_currentAction != null);
        }
    }
}
