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
        public DamageType[] DamageType { get; set; }

        public float[] Damage { get; set; }

        public float AttackDistance { get; set; }

        public VFXEventTypes VfxEventType { get; set; }

        public int ResourceBonus { get; set; }
        
        public FMODAsset StrongHitFishSound { get; } = SNAudioEvents.GetFmodAsset(SNAudioEvents.Paths.TigerPlantHitPlayer);
        
        public FMODAsset WarpFishSound { get; } = SNAudioEvents.GetFmodAsset(SNAudioEvents.Paths.WarperPortalOpen);
        
        // the blade object to disable when the knife is depleted
        public GameObject bladeObject;
        
        private IIonKnifeAction currentAction;

        private readonly FMODAsset underWaterMissSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/swing");

        private readonly FMODAsset surfaceMissSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/swing_surface");

        private readonly FMODAsset hitSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/heat_hit");

        private readonly FMODAsset bladeSpawnSound = SNAudioEvents.GetFmodAsset("event:/env/prec_light_on_2");

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
                        for (int i = 0; i < Damage.Length; i++)
                        {
                            lm.TakeDamage(Damage[i], position, DamageType[i]);
                        }
                        GiveResourceOnDamage(obj, lm.IsAlive(), wasAlive);
                        currentAction.OnHit(this, lm);
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

        public static bool IsCreature(LiveMixin lm)
        {
            if (!lm)
                return false;
            
            return lm.GetComponent<Creature>() != null;
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
            OnPoweredChanged(currentAction is not null);
        }

        void OnDisable()
        {
            energyMixin.onPoweredChanged -= OnPoweredChanged;
            energyMixin.batterySlot.onAddItem -= OnBatteryAdded;
            energyMixin.batterySlot.onRemoveItem -= OnBatteryRemoved;
            OnPoweredChanged(false);
        }

        void OnPoweredChanged(bool powered)
        {
            if (bladeObject.activeSelf == powered) 
                return;
            
            bladeObject.SetActive(powered);
            if (powered)
            {
                Utils.PlayFMODAsset(bladeSpawnSound, transform);
            }
            else
            {
                PlaySwitchSound(null);
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
            currentAction = null;
        }

        void OnBatteryAdded(InventoryItem item)
        {
            var tt = item.item.GetTechType();
            SetIonCubeType(tt);
        }
        #endregion

        void SetIonCubeType(TechType tt)
        {
            // add more if needed
            if (tt == TechType.PrecursorIonCrystal)
                currentAction = gameObject.EnsureComponent<PrecursorIonCrystalAction>();
            else if (tt == AUHandler.ElectricubeTechType)
                currentAction = gameObject.EnsureComponent<ElectricubeAction>();
            else if (tt == AUHandler.RedIonCubeTechType)
                currentAction = gameObject.EnsureComponent<RedIonCubeAction>();
            else if (tt == AUHandler.OmegaCubeTechType)
                currentAction = gameObject.EnsureComponent<OmegaCubeAction>();

            currentAction?.Initialize(this);
        }
    }
}
