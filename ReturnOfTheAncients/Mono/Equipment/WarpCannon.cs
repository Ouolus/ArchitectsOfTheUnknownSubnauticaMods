using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace RotA.Mono.Equipment
{
    [RequireComponent(typeof(EnergyMixin))]
    public class WarpCannon : PlayerTool
    {
        public WarpCannonAnimations animations;
        public FMODAsset portalOpenSound;
        public FMODAsset portalCloseSound;
        public FMOD_StudioEventEmitter chargeLoop;
        public WarperData warperCreatureData;
        float timeCanUseAgain;
        public float maxDistance = 50f;
        public float minDistanceInBase = 1f;
        public float maxDistanceInBase = 30f;
        public float surveyRadius = 0.2f;
        public float maxChargeSeconds = 2.7f;
        public float nodeMaxDistance = 50f;
        public float spawnCreatureMaxDistance = 25f;
        public float massThreshold = 1250;
        bool handDown;
        float timeStartedCharging;
        public bool removeWarpCannonLimits;

        GameObject myPrimaryNode;
        GameObject mySecondaryNode;

        float timeWarpHomeKeyLastPressed = -1f;

        public PrecursorIllumControl illumControl;

        /// <summary>
        /// The speed for warping. It's a smooth animation rather than instant.
        /// </summary>
        public float warpSpeed = 8f;

        public float warpModeEnergyCost = 20;
        public float manipulateModeEnergyCost = 15;
        public float creatureSpawnModeEnergyCost = 40;

        public GameObject warpInPrefab;
        public GameObject warpOutPrefab;
        public GameObject warpOutPrefabDestroyAutomatically;

        public GameObject primaryNodeVfxPrefab;
        public GameObject secondaryNodeVfxPrefab;

        public FireMode fireMode = FireMode.Warp;

        List<IPropulsionCannonAmmo> iammoCache = new List<IPropulsionCannonAmmo>(); //IDK why this exists but the propulsion cannon does it

        /// <summary>
        /// Controls what happens when you right click.
        /// </summary>
        /// <returns></returns>
        public override bool OnRightHandDown()
        {
            if (handDown)
            {
                return true;
            }
            if (Time.time > timeCanUseAgain)
            {
                if (fireMode == FireMode.Manipulate)
                {
                    return FireManipulateMode();
                }
                else if (fireMode == FireMode.Warp)
                {
                    return FireWarpMode();
                }
                else if (fireMode == FireMode.CreatureSpawn)
                {
                    return FireCreatureSpawnMode();
                }
            }
            return false;
        }

        /// <summary>
        /// Simply checks if <paramref name="y"/> is above the water level.
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        static bool PositionAboveWater(float y)
        {
            float oceanLevel;
#if SN1
            oceanLevel = Ocean.main.GetOceanLevel();
#else
                oceanLevel = Ocean.GetOceanLevel();
#endif
            return y > oceanLevel;
        }

        /// <summary>
        /// Spawns creatures around <paramref name="warpPosition"/>.
        /// </summary>
        void SpawnCreaturesAtPosition(Vector3 warpPosition, bool spawnLandFauna, float spawnRadius = 10f, float timeBeforeWarpOut = 10f, bool friendly = false)
        {
            string biomeName = "";
            if (LargeWorld.main)
            {
                biomeName = LargeWorld.main.GetBiome(warpPosition);
            }
            if (biomeName.ToLower().Contains("lostriver"))
            {
                biomeName = "LostRiver";
            }
            WarperData.WarpInCreature randomCreature;
            if (spawnLandFauna)
            {
                randomCreature = GetRandomLandCreatures();
            }
            else
            {
                randomCreature = warperCreatureData.GetRandomCreature(biomeName);
            }
            if (randomCreature == null)
            {
                if(Random.value <= 0.5f)
                {
                    randomCreature = new WarperData.WarpInCreature() { techType = TechType.BoneShark, minNum = 1, maxNum = 2 };
                }
                else
                {
                    randomCreature = new WarperData.WarpInCreature() { techType = TechType.Jellyray, minNum = 1, maxNum = 2 };
                }
            }
            Vector3 creatureSpawnPosition = warpPosition + (Random.onUnitSphere * (spawnLandFauna ? 1f : spawnRadius));
            Destroy(Utils.SpawnPrefabAt(warpInPrefab, null, creatureSpawnPosition), 2f);
            Utils.PlayFMODAsset(portalCloseSound, creatureSpawnPosition);
            int num = Random.Range(randomCreature.minNum, randomCreature.maxNum + 1);
            for (int i = 0; i < num; i++)
            {
#if SN1_exp
                StartCoroutine(WarpInCreatureAsync(randomCreature.techType, warpPosition, timeBeforeWarpOut, friendly));
#else
                WarpInCreature(randomCreature.techType, warpPosition, timeBeforeWarpOut, friendly);
#endif
            }
        }

        /// <summary>
        /// Returns a random creature that walks on land or flies.
        /// </summary>
        /// <returns></returns>
        WarperData.WarpInCreature GetRandomLandCreatures()
        {
            float random = Random.value;
            if (random < 0.33f)
            {
                return new WarperData.WarpInCreature() { techType = TechType.Skyray, minNum = 2, maxNum = 3 };
            }
            if (random < 0.67f)
            {
                return new WarperData.WarpInCreature() { techType = TechType.CaveCrawler, minNum = 1, maxNum = 2 };
            }
            return new WarperData.WarpInCreature() { techType = TechType.PrecursorDroid, minNum = 1, maxNum = 1 };
        }

        // Yoinked from WarpBall.cs
#if SN1_exp
        private IEnumerator WarpInCreatureAsync(TechType techType, Vector3 position, float timeBeforeWarpOut = 10f, bool friendly = false)
        {
            if (techType == TechType.None)
            {
                yield break;
            }
            TaskResult<GameObject> task = new TaskResult<GameObject>();
            yield return CraftData.InstantiateFromPrefabAsync(techType, task);
            GameObject spawnedCreatureObj = task.Get();
            spawnedCreatureObj.transform.position = position + (Random.insideUnitSphere * 0.5f);
            WarpedInCreature warpedInCreature = spawnedCreatureObj.AddComponent<WarpedInCreature>();
            float creatureLifetime = (techType == TechType.Mesmer ? 30f : timeBeforeWarpOut) + Random.Range(-2f, 2f); //I like mesmers. They're too rare so they get to stay for longer.
            warpedInCreature.SetLifeTime(creatureLifetime);
            warpedInCreature.warpOutEffectPrefab = warpOutPrefabDestroyAutomatically;
            warpedInCreature.warpOutSound = portalCloseSound;
            if (LargeWorld.main != null && LargeWorld.main.streamer != null && LargeWorld.main.streamer.cellManager != null)
            {
                LargeWorld.main.streamer.cellManager.UnregisterEntity(spawnedCreatureObj);
            }
            if (friendly)
            {
                Creature creature = spawnedCreatureObj.GetComponent<Creature>();
                if (creature)
                {
                    creature.friend = Player.main.gameObject;
                }
            }
            bool inBase = Player.main.IsInSub();
            if (inBase)
            {
                //skyray fixes
                var flyAboveMinHeight = spawnedCreatureObj.GetComponent<FlyAboveMinHeight>();
                if (flyAboveMinHeight is not null)
                {
                    flyAboveMinHeight.enabled = false;
                }
                var drowning = spawnedCreatureObj.GetComponent<Drowning>();
                if (drowning is not null)
                {
                    Destroy(drowning);
                }
                //base sky applier fixes
                SkyApplier creatureSkyApplier = spawnedCreatureObj.GetComponent<SkyApplier>();
                if (creatureSkyApplier is not null)
                {
                    mset.Sky baseSky = Player.main.GetCurrentSub().GetComponentInChildren<mset.Sky>();
                    if (baseSky is not null)
                    {
                        creatureSkyApplier.SetCustomSky(baseSky);
                    }
                }
            }
        }
#else
        void WarpInCreature(TechType techType, Vector3 position, float timeBeforeWarpOut = 10f, bool friendly = false)
        {
            if (techType == TechType.None)
            {
                return;
            }
            GameObject spawnedCreatureObj = CraftData.InstantiateFromPrefab(techType);
            spawnedCreatureObj.transform.position = position + (Random.insideUnitSphere * 0.5f);
            WarpedInCreature warpedInCreature = spawnedCreatureObj.AddComponent<WarpedInCreature>();
            float creatureLifetime = (techType == TechType.Mesmer ? 30f : timeBeforeWarpOut) + Random.Range(-2f, 2f); //I like mesmers. They're too rare so they get to stay for longer.
            warpedInCreature.SetLifeTime(creatureLifetime);
            warpedInCreature.warpOutEffectPrefab = warpOutPrefabDestroyAutomatically;
            warpedInCreature.warpOutSound = portalCloseSound;
            if (LargeWorld.main != null && LargeWorld.main.streamer != null && LargeWorld.main.streamer.cellManager != null)
            {
                LargeWorld.main.streamer.cellManager.UnregisterEntity(spawnedCreatureObj);
            }
            if (friendly)
            {
                Creature creature = spawnedCreatureObj.GetComponent<Creature>();
                if (creature)
                {
                    creature.friend = Player.main.gameObject;
                }
            }
            bool inBase = Player.main.IsInSub();
            if (inBase)
            {
                //skyray fixes
                var flyAboveMinHeight = spawnedCreatureObj.GetComponent<FlyAboveMinHeight>();
                if (flyAboveMinHeight is not null)
                {
                    flyAboveMinHeight.enabled = false;
                }
                var drowning = spawnedCreatureObj.GetComponent<Drowning>();
                if (drowning is not null)
                {
                    Destroy(drowning);
                }
                //base sky applier fixes
                SkyApplier creatureSkyApplier = spawnedCreatureObj.GetComponent<SkyApplier>();
                if (creatureSkyApplier is not null)
                {
                    mset.Sky baseSky = Player.main.GetCurrentSub().GetComponentInChildren<mset.Sky>();
                    if (baseSky is not null)
                    {
                        creatureSkyApplier.SetCustomSky(baseSky);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Fires the weapon while in Personal teleportation mode.
        /// </summary>
        /// <returns></returns>
        bool FireWarpMode()
        {
            if (Player.main.precursorOutOfWater)
            {
                return false;
            }
            if (energyMixin.charge <= 5f)
            {
                ErrorMessage.AddMessage(Language.main.Get(Mod.warpCannonNotEnoughPowerError));
                return false;
            }
            timeStartedCharging = Time.time;
            handDown = true;
            chargeLoop.StartEvent();
            illumControl.SetTargetColor(PrecursorIllumControl.PrecursorColor.Purple, maxChargeSeconds);
            return true;
        }

        /// <summary>
        /// Warp all small enough entities around the secondary node to the primary node
        /// </summary>
        void WarpObjectsFromNodeToNode()
        {
            Vector3 primaryNodePosition = myPrimaryNode.transform.position;
            Vector3 secondaryNodePosition = mySecondaryNode.transform.position;
            GameObject warpVfx = Instantiate(primaryNodeVfxPrefab, primaryNodePosition, Quaternion.identity);
            warpVfx.SetActive(true);
            Destroy(warpVfx, 3f);
            var hitColliders = UWE.Utils.OverlapSphereIntoSharedBuffer(secondaryNodePosition, 3f, -1, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < hitColliders; i++)
            {
                var collider = UWE.Utils.sharedColliderBuffer[i];
                var obj = UWE.Utils.GetEntityRoot(collider.gameObject);
                obj ??= collider.gameObject;

                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null || rb.mass > massThreshold)
                {
                    if (!removeWarpCannonLimits)
                    {
                        continue;
                    }
                }
                bool canTeleport = true;
                if (!removeWarpCannonLimits)
                {
                    var creature = obj.GetComponent<Creature>();
                    if (creature is null)
                    {
                        obj.GetComponents(iammoCache);
                        for (int j = 0; j < iammoCache.Count; j++)
                        {
                            if (!iammoCache[j].GetAllowedToGrab())
                            {
                                canTeleport = false;
                                break;
                            }
                        }
                        iammoCache.Clear();
                    }
                }
                if (canTeleport)
                {
                    obj.transform.position = primaryNodePosition + (Random.insideUnitSphere * 1f);
                    if (rb) rb.isKinematic = false;
                }
            }
        }

        /// <summary>
        /// Fires the weapon while in Manipulation mode.
        /// </summary>
        /// <returns></returns>
        bool FireManipulateMode()
        {
            bool fail = Player.main.IsInSub();
            if (fail)
            {
                CharacterController controller = Player.main.GetComponent<CharacterController>();
                if (controller is not null)
                {
                    if (!controller.enabled) //if you're stuck inside a base and can't walk, this piece of code allows you to exit
                    {
                        fail = false;
                    }
                }
            }
            if (fail)
            {
                ErrorMessage.AddMessage("Cannot fire Warping Device in Manipulate Mode currently.");
                return false;
            }

            if (mySecondaryNode != null) //Check if both nodes already exist. if so, do nothing.
            {
                return false;
            }
            if (energyMixin.charge < manipulateModeEnergyCost && GameModeUtils.RequiresPower())
            {
                ErrorMessage.AddMessage(Language.main.Get(Mod.warpCannonNotEnoughPowerError));
                return false;
            }
            if (myPrimaryNode != null) //check if primary node exists but secondary doesn't. if so create a secondary node
            {
                illumControl.Pulse(PrecursorIllumControl.PrecursorColor.Purple, PrecursorIllumControl.PrecursorColor.Green, 0.3f, 0.2f, 0.5f);
                mySecondaryNode = CreateNode(secondaryNodeVfxPrefab);
                Destroy(mySecondaryNode, 2f);
                Destroy(myPrimaryNode, 2f);
                WarpObjectsFromNodeToNode(); //warp creatures from the newly placed node to the first node
                Utils.PlayFMODAsset(portalCloseSound, mySecondaryNode.transform.position, 60f); //portal close sound cus this closes the portal link
                timeCanUseAgain = Time.time + 2f; //you just teleported something. you need some decently long delay.
                energyMixin.ConsumeEnergy(manipulateModeEnergyCost);
                animations.PlayFireAnimation();
                return true;
            }
            else //Neither node exists
            {
                myPrimaryNode = CreateNode(primaryNodeVfxPrefab); //otherwise, there should be space for a primary node
                Utils.PlayFMODAsset(portalOpenSound, myPrimaryNode.transform.position, 60f); //portal open sound cus you're creating a new portal link
                Destroy(myPrimaryNode, 60f);
                illumControl.Pulse(PrecursorIllumControl.PrecursorColor.Purple, PrecursorIllumControl.PrecursorColor.Green, 0.4f, 0.1f, 0.25f);
                timeCanUseAgain = Time.time + 0.5f; //only a small cooldown is needed
                energyMixin.ConsumeEnergy(manipulateModeEnergyCost);
                animations.PlayFireAnimation();
                return true;
            }
        }

        /// <summary>
        /// Fires the weapon while in Creature spawn mode.
        /// </summary>
        /// <returns></returns>
        bool FireCreatureSpawnMode()
        {
            if (InsideMovableSub())
            {
                ErrorMessage.AddMessage("Cannot fire Warping Device in Creature Spawn Mode currently.");
                return false;
            }
            if (GameModeUtils.RequiresPower() && energyMixin.charge < creatureSpawnModeEnergyCost)
            {
                ErrorMessage.AddMessage(Language.main.Get(Mod.warpCannonNotEnoughPowerError));
                return false;
            }
            energyMixin.ConsumeEnergy(creatureSpawnModeEnergyCost);
            animations.SetSpinSpeedWithoutAcceleration(0.5f, false);
            animations.PlayFireAnimation();
            Vector3 spawnPosition;
            Transform mainCam = MainCamera.camera.transform;
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, spawnCreatureMaxDistance, GetOutsideLayerMask(), QueryTriggerInteraction.Ignore))
            {
                spawnPosition = hit.point + (hit.normal * 2f);
            }
            else
            {
                spawnPosition = mainCam.position + (mainCam.forward * spawnCreatureMaxDistance);
            }
            bool inBase = Player.main.IsInSub() || Player.main.precursorOutOfWater;
            SpawnCreaturesAtPosition(spawnPosition, inBase || spawnPosition.y > 0f, 2f, 45f, true);
            timeCanUseAgain = Time.time + 0.2f;
            illumControl.Pulse(PrecursorIllumControl.PrecursorColor.Purple, PrecursorIllumControl.PrecursorColor.Green, 0.2f, 0.1f, 0.1f);
            return true;
        }

        /// <summary>
        /// Updates the appearance of the weapon every frame.
        /// </summary>
        void Update()
        {
            if(fireMode == FireMode.Manipulate)
            {
                if (mySecondaryNode != null) //if both nodes exist, spin super fast
                {
                    animations.SpinSpeed = 0.5f;
                }
                else if (myPrimaryNode != null) //if only one node exists, spin pretty fast
                {
                    animations.SpinSpeed = 0.25f;
                }
                else
                {
                    animations.SpinSpeed = 0.05f;
                }
            }
            else
            {
                animations.SpinSpeed = GetChargePercent();
            }
            float chargePercent = GetBatteryPercent();
            animations.BatteryPercent = chargePercent;
            if (chargePercent <= 0.02f)
            {
                if (illumControl.TargetColor != Color.black)
                {
                    illumControl.SetTargetColor(PrecursorIllumControl.PrecursorColor.Black, 1f);
                }
            }
            else
            {
                if (illumControl.TargetColor == Color.black)
                {
                    illumControl.SetTargetColor(PrecursorIllumControl.PrecursorColor.Green, 1f);
                }
            }
            if (fireMode == FireMode.Warp)
            {
                if (Time.time >= timeCanUseAgain && Input.GetKeyDown(Mod.config.WarpToBaseKey))
                {
                    if (Time.time < (timeWarpHomeKeyLastPressed + 1f)) //checks if you have pressed the warp home key in the last second
                    {
                        timeWarpHomeKeyLastPressed = -1f; //Reset the time of you pressing the warp home key
                        timeCanUseAgain = Time.time + 3f;
                        WarpToLastVisitedBase();
                    }
                    else
                    {
                        timeWarpHomeKeyLastPressed = Time.time;
                        ErrorMessage.AddMessage($"Double tap {ArchitectsLibrary.Utility.LanguageUtils.FormatKeyCode(Mod.config.WarpToBaseKey)} to warp to your last visited base.");
                    }
                }
            }
        }

        public void WarpToLastVisitedBase()
        {
            if (GetBatteryPercent() >= 0.50f)
            {
                energyMixin.ConsumeEnergy(energyMixin.charge / energyMixin.capacity);
                animations.SetOverrideSpinSpeed(10f, 3f);
                var lastValidSub = Player.main.lastValidSub;
                if (lastValidSub is not null && Player.main.CheckSubValid(lastValidSub))
                {
                    var respawnPoint = lastValidSub.gameObject.GetComponentInChildren<RespawnPoint>();
                    if (respawnPoint is not null)
                    {
                        Player.main.SetPosition(respawnPoint.GetSpawnPosition());
                        Player.main.SetCurrentSub(lastValidSub);
                        return;
                    }
                }
                EscapePod.main.RespawnPlayer();
                Player.main.SetCurrentSub(null);
            }
            else
            {
                ErrorMessage.AddMessage("You must have above 50% energy to perform this action.");
            }
        }

        float GetBatteryPercent()
        {
            if (energyMixin.capacity > 0.01f) //we dont want a divide by 0 error
            {
                return energyMixin.charge / energyMixin.capacity;
            }
            return 0f;
        }

        /// <summary>
        /// Controls behavior done when the weapon is put away.
        /// </summary>
        public override void OnHolster()
        {
            StopCharging();
            DestroyNodes();
        }

        /// <summary>
        /// Spawns a node, either on the first terrain the raycast hits, or <see cref="nodeMaxDistance"/> meters in front of the player if no terrain is traced.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        GameObject CreateNode(GameObject prefab)
        {
            Transform mainCam = MainCamera.camera.transform;
            GameObject returnObj;
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, nodeMaxDistance, GetOutsideLayerMask(), QueryTriggerInteraction.Ignore))
            {
                returnObj = Instantiate(prefab, hit.point + (hit.normal * 1f), Quaternion.identity);
            }
            else
            {
                returnObj = Instantiate(prefab, mainCam.position + (mainCam.forward * nodeMaxDistance), Quaternion.identity);
            }
            returnObj.SetActive(true);
            return returnObj;
        }

        /// <summary>
        /// Controls the displaying of controls at the bottom of the screen.
        /// </summary>
        /// <returns></returns>
        public override string GetCustomUseText()
        {
            if (fireMode == FireMode.Warp)
            {
                return ArchitectsLibrary.Utility.LanguageUtils.GetMultipleButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyWarpKey, GameInput.Button.AltTool, ArchitectsLibrary.Utility.LanguageUtils.FormatKeyCode(Mod.config.WarpToBaseKey));
            }
            if (fireMode == FireMode.Manipulate)
            {
                if (myPrimaryNode == null)
                {
                    return ArchitectsLibrary.Utility.LanguageUtils.GetMultipleButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyManipulateFirePrimaryKey, GameInput.Button.AltTool, GameInput.Button.RightHand);
                }
                else if (mySecondaryNode == null)
                {
                    return ArchitectsLibrary.Utility.LanguageUtils.GetMultipleButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyManipulateFireSecondaryKey, GameInput.Button.AltTool, GameInput.Button.RightHand);
                }
            }
            if (fireMode == FireMode.CreatureSpawn)
            {
                return ArchitectsLibrary.Utility.LanguageUtils.GetMultipleButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyCreatureKey, GameInput.Button.AltTool, GameInput.Button.RightHand);
            }
            return base.GetCustomUseText();
        }

        /// <summary>
        /// Destroy both portals (after a few seconds to let the animation finish) without warping.
        /// </summary>
        void DestroyNodes()
        {
            if (myPrimaryNode != null)
            {
                Destroy(myPrimaryNode, 2f);
                myPrimaryNode = null;
            }
            if (mySecondaryNode != null)
            {
                Destroy(mySecondaryNode, 2f);
                mySecondaryNode = null;
            }
        }

        /// <summary>
        /// Controls the switching between fire modes.
        /// </summary>
        /// <returns></returns>
        public override bool OnAltDown()
        {
            if (Time.time < timeCanUseAgain)
            {
                return false;
            }
            if (GetChargePercent() > 0f)
            {
                return false;
            }
            illumControl.Pulse(PrecursorIllumControl.PrecursorColor.Pink, PrecursorIllumControl.PrecursorColor.Green, 0.2f, 0.1f, 0.3f);
            animations.SetSpinSpeedWithoutAcceleration(0.5f, false);
            if (fireMode == FireMode.Warp)
            {
                fireMode = FireMode.Manipulate;
                return true;
            }
            if (fireMode == FireMode.Manipulate)
            {
                fireMode = FireMode.CreatureSpawn;
                return true;
            }
            if (fireMode == FireMode.CreatureSpawn)
            {
                fireMode = FireMode.Warp;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Controls what happens when you release right click. Only used in in <see cref="FireMode.Warp"/> mode.
        /// </summary>
        /// <returns></returns>
        public override bool OnRightHandUp()
        {
            if (fireMode != FireMode.Warp)
            {
                return false; //quit if you're not in personal teleportation mode
            }
            float chargeScale = GetChargePercent();
            if (Time.time > timeCanUseAgain && handDown)
            {
                float energyToConsume = warpModeEnergyCost * chargeScale;
                if (!GameModeUtils.RequiresPower() || energyMixin.charge >= energyToConsume)
                {
                    if (WarpForward(chargeScale, out Vector3 warpPos))
                    {
                        energyMixin.ConsumeEnergy(energyToConsume);
                        if (chargeLoop.GetIsStartingOrPlaying())
                        {
                            chargeLoop.Stop(false);
                        }
                        float delay = 0.5f;
                        if (chargeScale > 0.5f)
                        {
                            delay = 1f;
                            animations.PlayFastFireAnimation(); //feels more powerful
                        }
                        else
                        {
                            animations.PlayFireAnimation();
                        }
                        timeCanUseAgain = Time.time + delay;
                        Utils.PlayFMODAsset(portalOpenSound, warpPos);
                        illumControl.SetTargetColor(PrecursorIllumControl.PrecursorColor.Green, delay);
                        handDown = false;
                        if (!Player.main.IsInSub()) //if you are not in a base or vehicle
                        {
                            if (Random.value < (0.4f * chargeScale))
                            {
                                SpawnCreaturesAtPosition(warpPos, PositionAboveWater(warpPos.y));
                            }
                        }
                        else if (!InsideMovableSub() || Player.main.precursorOutOfWater) //if you are inside a base (NOT cyclops), spawn land fauna
                        {
                            if (Random.value < (0.4f * chargeScale) && energyMixin.ConsumeEnergy(warpModeEnergyCost * chargeScale))
                            {
                                SpawnCreaturesAtPosition(warpPos, true);
                            }
                        }
                        return true;
                    }
                }
                else
                {
                    ErrorMessage.AddMessage(Language.main.Get(Mod.warpCannonNotEnoughPowerError));
                }
                StopCharging();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if you are in a cyclops or something similar. Warping inside of a Sub with a rigidbody causes issues.
        /// </summary>
        /// <returns></returns>
        bool InsideMovableSub()
        {
            SubRoot currentSubRoot = Player.main.currentSub;
            if (currentSubRoot)
            {
                if (currentSubRoot.rb is not null)
                {
                    if (!currentSubRoot.rb.isKinematic)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Forcefully cancel the charge of the weapon and add a slight cooldown. Only used in in <see cref="FireMode.Warp"/> mode.
        /// </summary>
        void StopCharging()
        {
            if (chargeLoop.GetIsStartingOrPlaying())
            {
                chargeLoop.Stop(false);
            }
            timeCanUseAgain = Time.time + 0.5f;
            illumControl.SetTargetColor(PrecursorIllumControl.PrecursorColor.Green, 0.5f);
            handDown = false;
        }

        /// <summary>
        /// For Warp mode only. How charged the tool is on a scale from 0.2 - 1. A charge below 0.2 counts as 0.2 because warping 0ish meters is pointless. Only used in in <see cref="FireMode.Warp"/> mode, otherwise returns 0.
        /// </summary>
        /// <returns></returns>
        float GetChargePercent()
        {
            if (!handDown)
            {
                return 0f;
            }
            if (fireMode == FireMode.Manipulate)
            {
                return 0f;
            }
            float timeCharged = Time.time - timeStartedCharging;
            float chargeScale = Mathf.Clamp(timeCharged / maxChargeSeconds, 0.2f, 1f);
            return chargeScale;
        }

        /// <summary>
        /// The layer mask for raycasts when outside of a base. Inside of a base you might want to include all layers.
        /// </summary>
        /// <returns></returns>
        int GetOutsideLayerMask()
        {
            return LayerMask.GetMask("Default", "Useable", "NotUseable", "TerrainCollider");
        }

        /// <summary>
        /// Attempt to warp the player forward, with distance being based on on <paramref name="chargeScale"/>. Only used in in <see cref="FireMode.Warp"/> mode.
        /// </summary>
        /// <param name="chargeScale"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        bool WarpForward(float chargeScale, out Vector3 targetPosition)
        {
            targetPosition = default;
            Transform mainCam = MainCamera.camera.transform;
            SubRoot currentSubRoot = Player.main.currentSub;
            if (currentSubRoot)
            {
                if (currentSubRoot.rb is not null)
                {
                    if (!currentSubRoot.rb.isKinematic)
                    {
                        return false;
                    }
                }
                bool shouldTeleport = false;
                float teleportDistance = float.MinValue;
                Vector3 currentWarpPos = Player.main.transform.position;
                for (int i = (int)minDistanceInBase; i <= (int)maxDistanceInBase; i++)
                {
                    float testDistance = i * chargeScale;
                    var forward = mainCam.forward;
                    Vector3 warpDir = new Vector3(forward.x, 0f, forward.z);
                    if (Physics.Raycast(mainCam.position, warpDir, out _, testDistance + 1f, -1, QueryTriggerInteraction.Ignore))
                    {
                        continue;
                    }
                    if (SurveyBaseWarpPosition(testDistance, out Vector3 warpPos))
                    {
                        if (testDistance > teleportDistance)
                        {
                            teleportDistance = testDistance;
                            currentWarpPos = warpPos;
                            shouldTeleport = true;
                        }
                    }
                }
                if (shouldTeleport)
                {
                    targetPosition = currentWarpPos;
                    MovePlayerWhileInBase(currentWarpPos);
                    return true;
                }
                return false;
            }
            else
            {
                if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, maxDistance * chargeScale, GetOutsideLayerMask(), QueryTriggerInteraction.Ignore))
                {
                    targetPosition = hit.point + (hit.normal);
                }
                else
                {
                    targetPosition = mainCam.position + (mainCam.forward * maxDistance * chargeScale);
                }
                MovePlayerWhileInWater(targetPosition);
                return true;
            }
        }

        /// <summary>
        /// Teleportation method for the player that should be done ONLY in bases (not cyclops either).
        /// </summary>
        /// <param name="position"></param>
        void MovePlayerWhileInBase(Vector3 position)
        {
            PlayerSmoothWarpSingleton.StartSmoothWarp(Player.main.transform.position, position, warpSpeed);
        }

        /// <summary>
        /// Teleportation method for the player that should be done while outside of bases and submarines.
        /// </summary>
        /// <param name="position"></param>
        void MovePlayerWhileInWater(Vector3 position)
        {
            var playerPos = Player.main.transform.position;
            var camRotation = MainCamera.camera.transform.rotation;
            Instantiate(warpInPrefab, playerPos, camRotation);
            Instantiate(warpOutPrefab, position, camRotation);
            //Player.main.transform.position = position;
            PlayerSmoothWarpSingleton.StartSmoothWarp(playerPos, position, warpSpeed);
        }

        /// <summary>
        /// Check if a location <paramref name="distance"/> meters in front of you has space for you to teleport to. If so, return true.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="landingPosition"></param>
        /// <returns></returns>
        bool SurveyBaseWarpPosition(float distance, out Vector3 landingPosition)
        {
            Transform playerTransform = Player.main.transform;
            var mainCameraForward = MainCameraControl.main.transform.forward;
            landingPosition = playerTransform.position + (new Vector3(mainCameraForward.x, 0f, mainCameraForward.z) * distance);
            var hitColliders = UWE.Utils.OverlapSphereIntoSharedBuffer(landingPosition + new Vector3(0f, 0f, 0f), surveyRadius, -1, QueryTriggerInteraction.Ignore);
            return hitColliders == 0;
        }

        public override string animToolName => "propulsioncannon";

        public enum FireMode
        {
            Warp,
            Manipulate,
            CreatureSpawn
        }
    }
}
