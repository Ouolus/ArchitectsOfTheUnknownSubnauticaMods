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

        public string UseTextLanguageKey { get; set; }
        
        // the blade objects to disable when the knife is depleted
        public GameObject[] bladeObjects;
        
        // the blade renderers to be colored when a different ion cube type is selected
        public Renderer[] bladeRenderers;
        
        private IIonKnifeAction currentAction;
		
        private ElectricubeAction electricubeAction = new();
		
        private OmegaCubeAction omegaCubeAction = new();
		
        private PrecursorIonCrystalAction precursorIonCrystalAction = new();
		
        private RedIonCubeAction redIonCubeAction = new();
        
        private FMOD_CustomLoopingEmitter switchModeEmitter;

        private FMOD_StudioEventEmitter chargingSound;

        private Light pointLight;
        
        private readonly FMODAsset underWaterMissSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/swing");

        private readonly FMODAsset surfaceMissSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/swing_surface");

        private readonly FMODAsset hitSound = SNAudioEvents.GetFmodAsset("event:/tools/knife/heat_hit");

        private readonly FMODAsset bladeSpawnSound = SNAudioEvents.GetFmodAsset("event:/env/prec_light_on_2");

        private float timeStopSwitchMode;
        
        private static readonly int _detailsColor = Shader.PropertyToID("_DetailsColor");

        private static readonly int _squaresColor = Shader.PropertyToID("_SquaresColor");

        public override string animToolName => TechType.Knife.AsString(true);

        public override string GetCustomUseText()
        {
            if (string.IsNullOrEmpty(UseTextLanguageKey))
            {
                return Language.main.GetFormat(Mod.ionKnifeUseTextNoCubeLoadedFormat, uGUI.FormatButton(GameInput.Button.Reload));
            }
            return Language.main.GetFormat(Mod.ionKnifeUseTextFormat, Language.main.Get(UseTextLanguageKey), uGUI.FormatButton(GameInput.Button.Reload));
        }

        public override void Awake()
        {
            base.Awake();
            pointLight = gameObject.EnsureComponent<Light>();
            pointLight.type = LightType.Point;
            pointLight.enabled = false;
        }

        public override void OnHolster()
        {
            if (chargingSound == null)
                chargingSound = gameObject.GetComponent<FMOD_StudioEventEmitter>();

            if (chargingSound != null)
            {
                if (chargingSound.GetIsStartingOrPlaying())
                    chargingSound.Stop(false);
            }
        }

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
            bool calledSwingMethod = false;
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
                        OnSwing(lm, obj);
                        calledSwingMethod = true;
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
            if (!calledSwingMethod)
            {
                OnSwing(null, obj);
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
            if (isDrawn)
            {
                OnUpdate();
            }
        }

        public override bool OnRightHandDown()
        {
            if (currentAction is IIonKnifeRightHand rightHand)
                return !energyMixin.IsDepleted() && rightHand.OnRightHandDown(this);
            
            return !energyMixin.IsDepleted();
        }

        public override bool OnRightHandHeld()
        {
            if (currentAction is IIonKnifeRightHand rightHand)
            {
                return !energyMixin.IsDepleted() && rightHand.OnRightHandHeld(this);
            }

            return true;
        }

        public override bool OnRightHandUp()
        {
            if (currentAction is IIonKnifeRightHand rightHand)
            {
                return rightHand.OnRightHandUp(this);
            }

            return false;
        }

        public override bool GetUsedToolThisFrame()
        {
            if (currentAction is IIonKnifeUsedTool usedTool)
            {
                return usedTool.GetUsedToolThisFrame(this);
            }

            return false;
        }

        public void SetMaterialColors(Color color, Color specColor, Color detailsColor, Color squareColor)
        {
            foreach (var renderer in bladeRenderers)
            {
                renderer.material.SetColor(ShaderPropertyID._Color, color);
                renderer.material.SetColor(ShaderPropertyID._SpecColor, specColor);
                renderer.material.SetColor(_detailsColor, detailsColor);
                renderer.material.SetColor(_squaresColor, squareColor);
            }
        }

        public static bool IsCreature(LiveMixin lm)
        {
            if (!lm)
                return false;
            
            return lm.GetComponent<Creature>() != null;
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

        public void SetLightAppearance(Color color, float range, float intensity = 1f)
        {
            pointLight.intensity = intensity;
            pointLight.color = color;
            pointLight.range = range;
            pointLight.enabled = true;
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
            if (bladeObjects[0].activeSelf == powered) 
                return;
            
            SetBladeState(powered);
            
            if (powered)
            {
                Utils.PlayFMODAsset(bladeSpawnSound, transform);
                pointLight.enabled = true;
            }
            else
            {
                PlaySwitchSound(null);
                pointLight.enabled = false;
                UseTextLanguageKey = null;
            }
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
                currentAction = precursorIonCrystalAction;
            else if (tt == AUHandler.ElectricubeTechType)
                currentAction = electricubeAction;
            else if (tt == AUHandler.RedIonCubeTechType)
                currentAction = redIonCubeAction;
            else if (tt == AUHandler.OmegaCubeTechType)
                currentAction = omegaCubeAction;

            OnInitialize();
        }

        void SetBladeState(bool state)
        {
            for (int i = 0; i < bladeObjects.Length; i++)
            {
                bladeObjects[i].SetActive(state);
            }
        }

        void OnInitialize()
        {
            if (currentAction != null)
                currentAction.Initialize(this);
        }

        void OnUpdate()
        {
            if (currentAction != null)
                currentAction.OnUpdate(this);
        }

        void OnSwing(LiveMixin lm, GameObject hitGo)
        {
            if (currentAction != null)
                currentAction.OnSwing(this, lm, hitGo);
        }
    }
}
