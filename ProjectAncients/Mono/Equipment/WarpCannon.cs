using UnityEngine;

namespace ProjectAncients.Mono.Equipment
{
    public class WarpCannon : PlayerTool
    {
        public Animator animator;
        public FMODAsset fireSound;
        public FMODAsset altFireSound;
        public FMOD_StudioEventEmitter chargeLoop;
        float timeCanUseAgain = 0f;
        public float maxDistance = 40f;
        public float minDistanceInBase = 1f;
        public float maxDistanceInBase = 20f;
        public float surveyRadius = 0.2f;
        public float maxChargeSeconds = 1.5f;
        bool handDown = false;
        float timeStartedCharging = 0f;
        public float warpSpeed = 4;
        public GameObject warpInPrefab;
        public GameObject warpOutPrefab;
        public FireMode fireMode = FireMode.Warp;

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
        /// Fires the weapon while in Manipulation mode.
        /// </summary>
        /// <returns></returns>
        bool FireManipulateMode()
        {
            if (Player.main.IsInSub())
            {
                ErrorMessage.AddMessage("Cannot fire Warping Device while in Manipulate Mode while inside bases.");
                return false;
            }
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
                return LanguageCache.GetButtonFormat(Mod.warpCannonSwitchFireModeCurrentlyManipulateKey, GameInput.Button.AltTool);
            }
            return base.GetCustomUseText();
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
        /// Controls what happens when you release right click.
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
                    Utils.PlayFMODAsset(fireSound, warpPos, 20f);
                    animator.SetTrigger("use");
                    handDown = false;
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

        public override string animToolName => "stasisrifle";

        public enum FireMode
        {
            Warp,
            Manipulate
        }
    }
}
