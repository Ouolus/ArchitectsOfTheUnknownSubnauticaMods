using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ProjectAncients.Mono.Equipment
{
    public class WarpCannon : PlayerTool
    {
        public Animator animator;
        public FMODAsset portalOpenSound;
        public FMODAsset portalCloseSound;
        public FMOD_StudioEventEmitter chargeLoop;
        public WarperData warperCreatureData;
        float timeCanUseAgain = 0f;
        public float maxDistance = 40f;
        public float minDistanceInBase = 1f;
        public float maxDistanceInBase = 20f;
        public float surveyRadius = 0.2f;
        public float maxChargeSeconds = 1.5f;
        public float nodeMaxDistance = 50f;
        bool handDown = false;
        float timeStartedCharging = 0f;

        private GameObject myPrimaryNode;
        private GameObject mySecondaryNode;

        /// <summary>
        /// The speed for warping. It's a smooth animation rather than instant. You warp 2x faster in open water.
        /// </summary>
        public float warpSpeed = 4;

        public GameObject warpInPrefab;
        public GameObject warpOutPrefab;
        public GameObject warpOutPrefabDestroyAutomatically;

        public GameObject primaryNodeVfxPrefab;
        public GameObject secondaryNodeVfxPrefab;

        public FireMode fireMode = FireMode.Warp;

        List<IPropulsionCannonAmmo> iammo = new List<IPropulsionCannonAmmo>(); //IDK why this exists but the propulsion cannon does it

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
            }
            return false;
        }

        private bool PositionAboveWater(float y)
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
        /// Spawns a creature around the player.
        /// </summary>
        void Misfire(Vector3 warpPosition, bool spawnLandFauna)
        {
            string biomeName = "";
            if (LargeWorld.main)
            {
                biomeName = LargeWorld.main.GetBiome(warpPosition);
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
                return;
            }
            Vector3 creatureSpawnPosition = warpPosition + (Random.onUnitSphere * (spawnLandFauna ? 1f : 10f));
            Destroy(Utils.SpawnPrefabAt(warpInPrefab, null, creatureSpawnPosition), 2f);
            Utils.PlayFMODAsset(portalCloseSound, creatureSpawnPosition, 20f);
            int num = Random.Range(randomCreature.minNum, randomCreature.maxNum + 1);
            for (int i = 0; i < num; i++)
            {
#if SN1_exp
                UWE.CoroutineHost.StartCoroutine(WarpInCreatureAsync(randomCreature.techType, warpPosition));
#else
                WarpInCreature(randomCreature.techType, warpPosition);
#endif
            }
        }

        private WarperData.WarpInCreature GetRandomLandCreatures()
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

        /// <summary>
        /// Stolen from WarpBall.cs
        /// </summary>
        /// <param name="techType"></param>
#if SN1_exp
        private IEnumerator WarpInCreatureAsync(TechType techType, Vector3 position)
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
            warpedInCreature.SetLifeTime(10f + Random.Range(-2f, 2f));
            warpedInCreature.warpOutEffectPrefab = warpOutPrefabDestroyAutomatically;
            warpedInCreature.warpOutSound = portalCloseSound;
            if (LargeWorld.main != null && LargeWorld.main.streamer != null && LargeWorld.main.streamer.cellManager != null)
            {
                LargeWorld.main.streamer.cellManager.UnregisterEntity(spawnedCreatureObj);
            }
        }
#else
        private void WarpInCreature(TechType techType, Vector3 position)
        {
            if (techType == TechType.None)
            {
                return;
            }
            GameObject spawnedCreatureObj = CraftData.InstantiateFromPrefab(techType, false);
            spawnedCreatureObj.transform.position = position + (Random.insideUnitSphere * 0.5f);
            WarpedInCreature warpedInCreature = spawnedCreatureObj.AddComponent<WarpedInCreature>();
            warpedInCreature.SetLifeTime(10f + Random.Range(-2f, 2f));
            warpedInCreature.warpOutEffectPrefab = warpOutPrefabDestroyAutomatically;
            warpedInCreature.warpOutSound = portalCloseSound;
            if (LargeWorld.main != null && LargeWorld.main.streamer != null && LargeWorld.main.streamer.cellManager != null)
            {
                LargeWorld.main.streamer.cellManager.UnregisterEntity(spawnedCreatureObj);
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
                if(creatureSkyApplier is not null)
                {
                    mset.Sky baseSky = Player.main.GetCurrentSub().GetComponentInChildren<mset.Sky>();
                    if(baseSky is not null)
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
            timeStartedCharging = Time.time;
            handDown = true;
            chargeLoop.StartEvent();
            return true;
        }

        /// <summary>
        /// Warp all small enough entities around the secondary node to the primary node
        /// </summary>
        void DoWarp()
        {
            Vector3 primaryNodePosition = myPrimaryNode.transform.position;
            Vector3 secondaryNodePosition = mySecondaryNode.transform.position;
            GameObject warpVfx = GameObject.Instantiate(primaryNodeVfxPrefab, primaryNodePosition, Quaternion.identity);
            warpVfx.SetActive(true);
            Destroy(warpVfx, 3f);
            var hitColliders = UWE.Utils.OverlapSphereIntoSharedBuffer(secondaryNodePosition, 3f, -1, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < hitColliders; i++)
            {
                var collider = UWE.Utils.sharedColliderBuffer[i];
                var obj = UWE.Utils.GetEntityRoot(collider.gameObject);
                obj ??= collider.gameObject;

                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null || rb.mass > 1000f)
                {
                    continue;
                }
                bool canTeleport = true;
                var creature = obj.GetComponent<Creature>();
                if (creature is null)
                {
                    obj.GetComponents(iammo);
                    for (int j = 0; j < iammo.Count; j++)
                    {
                        if (!iammo[j].GetAllowedToGrab())
                        {
                            canTeleport = false;
                            break;
                        }
                    }
                    iammo.Clear();
                }
                if (canTeleport)
                {
                    obj.transform.position = primaryNodePosition + (Random.insideUnitSphere * 1f);
                    rb.isKinematic = false;
                }
            }
        }

        /// <summary>
        /// Fires the weapon while in Manipulation mode.
        /// </summary>
        /// <returns></returns>
        bool FireManipulateMode()
        {
            bool fail = false;
            if (Player.main.IsInSub())
            {
                fail = true;
            }
            if (fail == true)
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

            if (mySecondaryNode != null) //check if both nodes already exist. if so, do nothing.
            {
                return false;
            }
            if (myPrimaryNode != null) //check if primary node exists but secondary doesn't. if so create a secondary node
            {
                mySecondaryNode = CreateNode(secondaryNodeVfxPrefab);
                Destroy(mySecondaryNode, 2f);
                Destroy(myPrimaryNode, 2f);
                DoWarp();
                Utils.PlayFMODAsset(portalCloseSound, mySecondaryNode.transform.position, 60f); //portal close sound cus this closes the portal link
                timeCanUseAgain = Time.time + 2f; //you just teleported something. you need some decently long delay.
                return true;
            }
            myPrimaryNode = CreateNode(primaryNodeVfxPrefab); //otherwise, there should be space for a primary node
            Utils.PlayFMODAsset(portalOpenSound, myPrimaryNode.transform.position, 60f); //portal open sound cus you're creating a new portal link
            Destroy(myPrimaryNode, 60f);
            timeCanUseAgain = Time.time + 0.5f; //only a small cooldown is needed
            return true;
        }

        /// <summary>
        /// Updates animations based on charge, every frame.
        /// </summary>
        void Update()
        {
            animator.SetFloat("charge", GetChargePercent());
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
        private GameObject CreateNode(GameObject prefab)
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
                return LanguageCache.GetButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyWarpKey, GameInput.Button.AltTool);
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
            return base.GetCustomUseText();
        }

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
            if (fireMode == FireMode.Warp)
            {
                fireMode = FireMode.Manipulate;
                return true;
            }
            if (fireMode == FireMode.Manipulate)
            {
                fireMode = FireMode.Warp;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Controls what happens when you release right click. Personal teleportation/warp mode only.
        /// </summary>
        /// <returns></returns>
        public override bool OnRightHandUp()
        {
            if (fireMode != FireMode.Warp)
            {
                return false;
            }
            float chargeScale = GetChargePercent();
            if (Time.time > timeCanUseAgain && handDown)
            {
                if (WarpForward(chargeScale, out Vector3 warpPos))
                {
                    if (chargeLoop.GetIsStartingOrPlaying())
                    {
                        chargeLoop.Stop(false);
                    }
                    float delay = 0.5f;
                    if (chargeScale > 0.5f)
                    {
                        delay = 1f;
                    }
                    timeCanUseAgain = Time.time + delay;
                    Utils.PlayFMODAsset(portalOpenSound, warpPos, 20f);
                    animator.SetTrigger("use");
                    handDown = false;
                    if (!Player.main.IsInSub()) //if you are not in a base or vehicle, spawn fish
                    {
                        if (Random.value < (0.4f * chargeScale))
                        {
                            Misfire(warpPos, PositionAboveWater(warpPos.y));
                        }
                    }
                    else if (!InsideMovableSub()) //if you are inside a base, spawn land fauna
                    {
                        if (Random.value < (0.4f * chargeScale))
                        {
                            Misfire(warpPos, true);
                        }
                    }
                    return true;
                }
                else
                {
                    StopCharging();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if you are in a cyclops or something similar.
        /// </summary>
        /// <returns></returns>
        private bool InsideMovableSub()
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
        /// Forcefully cancel the charge of the weapon and add a slight cooldown.
        /// </summary>
        private void StopCharging()
        {
            if (chargeLoop.GetIsStartingOrPlaying())
            {
                chargeLoop.Stop(false);
            }
            timeCanUseAgain = Time.time + 0.5f;
            handDown = false;
        }

        /// <summary>
        /// For Warp mode only. How charged the tool is on a scale from 0.2 - 1. A charge below 0.2 counts as 0.2 because warping 0ish meters is pointless, 
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
        /// Attempt to warp the player forward, with distance being based on on <paramref name="chargeScale"/>. Only used in in personal teleportation/warp mode.
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
                    Vector3 warpDir = new Vector3(mainCam.forward.x, 0f, mainCam.forward.z);
                    if (Physics.Raycast(mainCam.position, warpDir, out RaycastHit hit, testDistance + 1f, -1, QueryTriggerInteraction.Ignore))
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
                else
                {
                    return false;
                }
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
            Instantiate(warpInPrefab, Player.main.transform.position, MainCamera.camera.transform.rotation);
            Instantiate(warpOutPrefab, position, MainCamera.camera.transform.rotation);
            //Player.main.transform.position = position;
            PlayerSmoothWarpSingleton.StartSmoothWarp(Player.main.transform.position, position, warpSpeed * 2f);
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
            var hitColliders = Physics.OverlapSphere(landingPosition + new Vector3(0f, 0f, 0f), surveyRadius, -1, QueryTriggerInteraction.Ignore);
            if (hitColliders == null)
            {
                return true;
            }
            if (hitColliders.Length == 0)
            {
                return true;
            }
            return false;
        }

        public override string animToolName => "propulsioncannon";

        public enum FireMode
        {
            Warp,
            Manipulate
        }
    }
}
